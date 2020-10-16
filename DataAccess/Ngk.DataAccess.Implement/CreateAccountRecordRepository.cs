using System;
using System.Linq;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.Common.Enum;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class CreateAccountRecordRepository : BaseRepository<CreateAccountRecord>, ICreateAccountRecordRepository
    {
        public CreateAccountRecordRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public override int Add(CreateAccountRecord entity, bool withTrigger = false)
        {
            entity.CreateTime = DateTime.UtcNow;
            return base.Add(entity, withTrigger);
        }

        /// <summary>
        /// 获取今天IP免费注册数
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public int CheckIpRegisterNum(string ip)
        {
            var date = DateTime.UtcNow.Date;
            var num = DbSet.Count(p => p.ClientIp == ip && p.Type == (int)EnumAccountType.Free && p.CreateTime >= date);
            return num;
        }

        /// <summary>
        /// 获取创建数
        /// </summary>
        /// <param name="chainCode"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public int GetFreeCreateNum(string chainCode, string uuid)
        {
            var num = DbSet.Count(p => p.State == (int)EnumState.Normal && p.ChainCode == chainCode && p.Uuid == uuid);
            return num;
        }
    }
}


