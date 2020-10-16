using System;
using System.Linq;
using Thor.Framework.Common.Cache;
using Thor.Framework.Common.Helper;
using Thor.Framework.Common.Pager;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.Common.Enum;
using Ngk.DataAccess.DTO.Param;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {

        public LanguageRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        #region 获取描述

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="enumLanguageDataType">Language中Type的类型</param>
        /// <param name="enumLanguageType">需要什么样的语言类型</param>
        /// <returns></returns>
        public string GetDescribe(string code, EnumLanguageDataType enumLanguageDataType, EnumLanguageType enumLanguageType)
        {
            try
            {
                string key = string.Format("NGK_Language_{0}_{1}", code, (int)enumLanguageDataType);
                if (CacheManager.TryGet<Language>(key, out var language) && language != null)
                {
                    return GetDescribeInfoByLang(language, enumLanguageType);
                }
                return GetDescribeInfoAndRefreshCache(code, enumLanguageDataType, enumLanguageType);
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(GetType(), ex, ex.Message);
                return "System Error";
            }
        }


        /// <summary>
        /// 根据code获取数据并缓存
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="enumLanguageDataType">Language中Type的类型</param>
        /// <param name="enumLanguageType">需要什么样的语言类型</param>
        /// <returns></returns>
        private string GetDescribeInfoAndRefreshCache(string code, EnumLanguageDataType enumLanguageDataType, EnumLanguageType enumLanguageType)
        {
            string key = string.Format("NGK_Language_{0}_{1}", code, (int)enumLanguageDataType);
            Language language = DbSet.FirstOrDefault(p => p.State == (int)EnumState.Normal && p.Type == (int)enumLanguageDataType && p.Code == code);
            if (language != null)
            {
                try
                {
                    CacheManager.Set(key, language, 10 * 60);
                    CacheManager.Refresh(key);
                }
                catch { }
                return GetDescribeInfoByLang(language, enumLanguageType);
            }
            return "System Error";
        }


        /// <summary>
        /// 根据语言返回相关的面描述
        /// </summary>
        /// <param name="language"></param>
        /// <param name="enumLanguageType"></param>
        /// <returns></returns>
        private string GetDescribeInfoByLang(Language language, EnumLanguageType enumLanguageType)
        {
            switch (enumLanguageType)
            {
                case EnumLanguageType.Zh: return language.Zh;
                case EnumLanguageType.Zh_Tw: return string.IsNullOrEmpty(language.Tw) ? language.Zh : language.Tw;
                case EnumLanguageType.En: return string.IsNullOrEmpty(language.En) ? language.Zh : language.En;
                case EnumLanguageType.Ar: return string.IsNullOrEmpty(language.Ar) ? language.En : language.Ar;
                case EnumLanguageType.De: return string.IsNullOrEmpty(language.De) ? language.En : language.De;
                case EnumLanguageType.Es: return string.IsNullOrEmpty(language.Es) ? language.En : language.Es;
                case EnumLanguageType.Fr: return string.IsNullOrEmpty(language.Fr) ? language.En : language.Fr;
                case EnumLanguageType.Ja: return string.IsNullOrEmpty(language.Ja) ? language.En : language.Ja;
                case EnumLanguageType.Ko: return string.IsNullOrEmpty(language.Ko) ? language.En : language.Ko;
                case EnumLanguageType.Pt: return string.IsNullOrEmpty(language.Pt) ? language.En : language.Pt;
                case EnumLanguageType.Ru: return string.IsNullOrEmpty(language.Ru) ? language.En : language.Ru;
                default: return language.Zh;
            }
        }

        #endregion



        /// <summary>
        /// 根据code和type获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Language GetLanguageByTypeAndCode(string code, int type)
        {
            return DbSet.FirstOrDefault(p => p.State == (int)EnumState.Normal && p.Type == type && p.Code == code);
        }


        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PagedResults<Language> QueryLanguage(LanguageParam model)
        {
            var queryable = GetAdvQuery(model);
            return queryable.ToPagedResults<Language, Language>(model); ;
        }

        protected override IQueryable<Language> GetAdvQuery<TQueryParam>(TQueryParam queryParam)
        {
            var result = base.GetAdvQuery(queryParam);
            result = result.Where(p => p.State == (int)EnumState.Normal);
            if (queryParam is LanguageParam)
            {
                var param = queryParam as LanguageParam;
                if (!string.IsNullOrEmpty(param.Code))
                {
                    param.Code = param.Code.Trim();
                    result = result.Where(p => p.Code == param.Code);
                }

                if (param.Type != default(int))
                {
                    result = result.Where(p => p.Type == param.Type);
                }

                if (!string.IsNullOrEmpty(param.Desc))
                {
                    param.Desc = param.Desc.Trim();
                    result = result.Where(p => p.Desc.Contains(param.Desc));
                }

                if (!string.IsNullOrEmpty(param.Content))
                {
                    param.Content = param.Content.Trim();
                    result = result.Where(p => p.Zh.Contains(param.Content) || p.En.Contains(param.Content) || p.Ko.Contains(param.Content));
                }

                if (param.StartTime != default(DateTime))
                {
                    result = result.Where(p => p.CreateTime >= param.StartTime);
                }

                if (param.EndTime != default(DateTime))
                {
                    param.EndTime = param.EndTime.AddDays(1);
                    result = result.Where(p => p.CreateTime < param.EndTime);
                }
            }
            return result;
        }
    }
}


