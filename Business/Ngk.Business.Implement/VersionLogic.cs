using System;
using System.Linq;
using Thor.Framework.Business.Relational;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.Model;
using Ngk.Business.Interface;
using Ngk.Common;
using Ngk.DataAccess.DTO.Manager;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Interface;
using Version = Ngk.DataAccess.Entities.Version;

namespace Ngk.Business.Implement
{
    public class VersionLogic : BaseBusinessLogic<Version, IVersionRepository>, IVersionLogic
    {
        #region ctor
        public VersionLogic(IVersionRepository repository) : base(repository)
        {

        }
        #endregion

        public override void Create(Version entity, out ExcutedResult result)
        {
            entity.State = (int)EnumState.Normal;
            entity.CreateTime = DateTime.UtcNow;
            base.Create(entity, out result);
        }

        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <param name="clientType">客户端类型，1、Web，2、IOS,3、Android</param>
        /// <returns></returns>
        public Version GetCurrentVersion(int clientType)
        {
            return Repository.GetCurrentVersion(clientType);
        }

        /// <summary>
        /// 获取最大版本
        /// </summary>
        /// <param name="clientType">客户端类型，1、Web，2、IOS,3、Android</param>
        /// <returns></returns>
        public int? GetMaxVersion(int ClientType, int Number)
        {
            return Repository.Get(x => x.ClientType == ClientType && x.Number == Number && x.State == (int)EnumState.Normal).Max(x => x.Number);
        }
        /// <summary>
        /// 查询版本记录分页
        /// </summary>
        /// <returns></returns>
        public PagedResults<Version> GetQuery(VersionParam param)
        {


            return Repository.AdvQuery(param);
        }
        public void Update(VersionEditRequest entity, out ExcutedResult result)
        {
            try
            {
                //查询原始数据
                var model = Repository.Get(x => x.Id == entity.Id && x.State == (int)EnumState.Normal).FirstOrDefault();
                //如果原始数据不存在，返回提示
                if (model == null)
                {
                    result = ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "操作失败,数据不存在");
                    return;
                }
                if (!string.IsNullOrEmpty(entity.Name))
                    model.Name = entity.Name;

                if (entity.Number != null)
                    model.Number = entity.Number;


                if (entity.Date != null)
                    model.Date = entity.Date;

                model.IsMustUpdate = entity.IsMustUpdate;

                if (!string.IsNullOrEmpty(entity.Desc))
                    model.Desc = entity.Desc;

                if (!string.IsNullOrEmpty(entity.Connect))
                    model.Connect = entity.Connect;

                using (var trans = base.GetNewTransaction())
                {
                    base.Update(model, out result);
                    if (result.Status != EnumStatus.Success)
                    {
                        throw new Exception("更新配置实体失败");
                    }
                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.OperationFailed, $"{BusinessResultCode.OperationFailed}:{e.Message}");
                Log4NetHelper.WriteError(GetType(), e, $"Id:{entity.Id},new Name:{entity.Name},new Number:{entity.Number}");
                return;
            }
        }


        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <returns></returns>
        public void DeleteLogic(Guid guid, out ExcutedResult result)
        {
            try
            {
                //查询原始数据
                var model = Repository.Get(x => x.Id == guid && x.State == (int)EnumState.Normal).FirstOrDefault();
                //如果原始数据不存在，返回提示
                if (model == null)
                {
                    result = ExcutedResult.FailedResult(BusinessResultCode.ArgumentError, "操作失败,数据不存在");
                    return;
                }
                model.State = (int)EnumState.Deleted;
                model.Deleter = CurrentUser.UserName;
                model.DeleteTime = DateTime.UtcNow;
                model.DeleteIp = CurrentUser.ClientIP;
                using (var trans = base.GetNewTransaction())
                {
                    base.Update(model, out result);
                    if (result.Status != EnumStatus.Success)
                    {
                        throw new Exception("更新配置实体失败");
                    }
                    trans.Commit();
                }
            }
            catch (Exception e)
            {
                result = ExcutedResult.FailedResult(BusinessResultCode.OperationFailed, $"{BusinessResultCode.OperationFailed}:{e.Message}");
                Log4NetHelper.WriteError(GetType(), e, $"版本删除失败Id:{guid}");
                return;
            }
        }
    }
}


