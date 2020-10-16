using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class ConfigDataRepository : BaseRepository<ConfigData>, IConfigDataRepository
    {
        private readonly IMemoryCache _cache;

        public ConfigDataRepository(IDbContextCore dbContext, IConfiguration configuration, IMemoryCache memoryCache) : base(dbContext)
        {
            _cache = memoryCache;
        }
        protected override IQueryable<ConfigData> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam).Where(p => p.State == (int)EnumState.Normal);
            if (queryParam is ConfigDataParam)
            {
                var param = queryParam as ConfigDataParam;

                if (!string.IsNullOrEmpty(param.ConfigKey))
                    result = result.Where(p => p.ConfigKey == param.ConfigKey);
            }
            return result;
        }

        /// <summary>
        /// 根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetByKey(string key)
        {
            var memKey = GetKey(key);
            if (_cache.TryGetValue(memKey, out ConfigData result))
            {
                return result.ConfigValue;
            }
            var data = base.GetSingleOrDefault(p => p.ConfigKey.ToLower() == key.ToLower() && p.State == (int)EnumState.Normal);
            if (data != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                _cache.Set(memKey, data, cacheEntryOptions);
                return data.ConfigValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        public void RefreshCache()
        {
            var list = DbSet.ToList();
            foreach (var data in list)
            {
                var memKey = GetKey(data.ConfigKey);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                _cache.Set(memKey, data, cacheEntryOptions);
            }
        }

        /// <summary>
        /// 根据key获取实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConfigData GetEntityByKey(string key)
        {
            var memKey = GetKey(key);
            if (_cache.TryGetValue(memKey, out ConfigData result))
            {
                return result;
            }
            var data = base.GetSingleOrDefault(p => p.ConfigKey.ToLower() == key.ToLower() && p.State == (int)EnumState.Normal);
            if (data != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                _cache.Set(memKey, data, cacheEntryOptions);
            }
            return data;
        }

        /// <summary>
        /// 根据key集合获取配置数据集合
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        public List<ConfigData> GetByKeyList(List<string> keyList)
        {
            var list = DbSet.Where(p => keyList.Contains(p.ConfigKey) && p.State == (int)EnumState.Normal);
            return list.ToList();
        }

        /// <summary>
        /// 重写获取方法
        /// 数据状态和环境
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public override IQueryable<ConfigData> Get(Expression<Func<ConfigData, bool>> where = null)
        {
            return base.Get(where).Where(p => p.State == (int)EnumState.Normal);
        }

        private string GetKey(string key)
        {
            return $"config-data-{key.ToLower()}";
        }
    }
}


