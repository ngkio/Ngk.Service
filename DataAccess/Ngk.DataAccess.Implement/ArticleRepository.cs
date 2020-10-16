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
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        public Article GetByCode(string code)
        {
            var data = DbSet.FirstOrDefault(p => p.Code == code && p.State == (int)EnumState.Normal);
            return data;
        }
    }
}


