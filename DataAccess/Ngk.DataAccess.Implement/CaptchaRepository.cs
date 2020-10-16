using System;
using System.Linq;
using Thor.Framework.Data;
using Thor.Framework.Data.DbContext.Relational;
using Thor.Framework.Repository.Relational;
using Ngk.DataAccess.Entities;
using Ngk.DataAccess.Interface;

namespace Ngk.DataAccess.Implement
{
    public class CaptchaRepository : BaseRepository<Captcha>, ICaptchaRepository
    {
        public CaptchaRepository(IDbContextCore dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// �����֤�룬����С��5����֮�ڵ���֤�룬���ظü�¼�����д���5���ӵĽ���ɾ��
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public Captcha CheckMobileCaptcha(string mobile, int minutes = 5)
        {
            var list = DbSet.Where(p => p.State == (int)EnumState.Normal && p.Mobile == mobile).OrderByDescending(p => p.CreateTime).ToList();
            if (list.Any())
            {
                var now = DateTime.UtcNow.AddMinutes(-minutes);
                var oneMinute = list.FirstOrDefault(p => p.CreateTime >= now);
                if (oneMinute != null)
                {
                    list.Remove(oneMinute);
                }
                foreach (var item in list)
                {
                    Delete(item.Id);
                }
                return oneMinute;
            }
            return null;
        }
    }
}


