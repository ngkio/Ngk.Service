using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Helper.Cryptography;
using Thor.Framework.Common.Helper.Extensions;
using Thor.Framework.Common.TOTP;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.Business.Implement
{
    public class ManagerLogic : BaseBusinessLogic<Manager, IManagerRepository>, IManagerLogic
    {
        private const string DefaultPassword = "ngk@123456";

        #region ctor
        public ManagerLogic(IManagerRepository repository) : base(repository)
        {

        }
        #endregion

        public override void Create(Manager entity, out ExcutedResult result)
        {
            entity.Id = Guid.NewGuid();
            entity.Salt = CodeHelper.GenerateStrNum();

            var securtyKey = entity.Id + entity.Salt;

            var setupGenerator = new TotpSetupGenerator();
            var totpSetup = setupGenerator.Generate("Ngk", entity.Account, securtyKey);

            entity.Pwd = GetHashPwd(DefaultPassword, entity.Salt);
            entity.State = (int)EnumState.Normal;
            entity.CreateTime = DateTime.UtcNow;

            base.Create(entity, out result);
            if (result.Status == EnumStatus.Success)
            {
                result.Data = totpSetup;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="authenticateNum">验证码</param>
        /// <returns>登录结果及用户信息</returns>
        public ExcutedResult SignIn(string userName, string password, int authenticateNum)
        {
            //query manager by userName
            var manager = Repository.FirstOrDefault(p => p.Account == userName && p.State == (int)EnumState.Normal);
            if (manager == null) return ExcutedResult.FailedResult(BusinessResultCode.NoUserOrPasswordError, "用户不存在或被删除");

            ExcutedResult result;
            //verify error times and last error time
            if (manager.ErrorTimes > 5)
            {
                if (manager.LastErrorTime.HasValue)
                {
                    var endTime = manager.LastErrorTime.Value.AddHours(12);
                    if (endTime > DateTime.UtcNow)
                    {
                        return ExcutedResult.FailedResult(BusinessResultCode.AccountLockedTryLater,
                            $"{BusinessResultCode.AccountLockedTryLater}:{endTime.ToStandardFormat()}。");
                    }
                }
                else
                {
                    manager.LastErrorTime = DateTime.UtcNow;
                    var endTime = manager.LastErrorTime.Value.AddHours(12);
                    Repository.Update(manager);

                    return ExcutedResult.FailedResult(BusinessResultCode.AccountLockedTryLater,
                        $"{BusinessResultCode.AccountLockedTryLater}:{endTime.ToStandardFormat()}。");
                }
            }

            //securtyKey=manager.Id+salt
            var securtyKey = manager.Id + manager.Salt;
            var validator = new TotpValidator(new TotpGenerator());

            //hashpwd=hash(salt+hash(password + salt))
            var hashPwd = GetHashPwd(password, manager.Salt);

            //hashpwd equal manager.hashpwd
            //verify authenticateNum
            if (validator.Validate(securtyKey, authenticateNum) && hashPwd.Equals(manager.Pwd))
            {
                manager.ErrorTimes = 0;
                manager.LastErrorTime = null;
                manager.LastLoginTime = DateTime.UtcNow;

                //if success, return userinfo
                var baseInfo = new PrincipalUser
                {
                    UserName = manager.Account,
                    Id = manager.Id,
                    Mobile = manager.Mobile,
                    NickName = manager.Name,
                    Role = manager.ManagerType
                };

                result = ExcutedResult.SuccessResult(baseInfo);
            }
            else
            {
                manager.ErrorTimes++;
                manager.LastErrorTime = DateTime.UtcNow;
                result = ExcutedResult.FailedResult(BusinessResultCode.NoUserOrPasswordError, "用户不存在或密码错误");
            }

            //update error times and last error time
            Repository.Update(manager);
            return result;
        }

        /// <summary>
        /// 获取密码hash值
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <param name="salt">加密盐</param>
        /// <returns>密码hash值</returns>
        private string GetHashPwd(string password, string salt)
        {
            var content = password + salt;
            var contentBytes = Encoding.UTF8.GetBytes(content);
            var part1 = Encoding.UTF8.GetBytes(salt);

            RIPEMD160Managed ripemd160Managed = new RIPEMD160Managed();
            var part2 = ripemd160Managed.ComputeHash(contentBytes);
            var part3 = ripemd160Managed.ComputeHash(part1.Concat(part2).ToArray());

            var part4 = SHA1.Create().ComputeHash(part3);

            return Base58.Encode(part4);
        }

        /// <summary>
        /// 跟新密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(String oldPassword, String newPassword)
        {
            try
            {
                if (CurrentUser.Id != default(Guid))
                {
                    var manager = Repository.FirstOrDefault(p => p.Id == CurrentUser.Id && p.State == (int)EnumState.Normal);
                    if (manager == null) throw new BusinessException(BusinessResultCode.NoUser, "用户不存在或被删除");
                    var securtyKey = manager.Id + manager.Salt;
                    var validator = new TotpValidator(new TotpGenerator());
                    var hashPwd = GetHashPwd(oldPassword, manager.Salt);
                    if (!hashPwd.Equals(manager.Pwd)) throw new BusinessException(BusinessResultCode.NoUserOrPasswordError, "用户不存在或密码错误");
                    manager.Pwd = GetHashPwd(newPassword, manager.Salt);
                    Repository.Update(manager);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, $"修改密码错误-{ex.Message}");
                return false;
            }
}
    }
}


