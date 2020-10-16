using System;

namespace Ngk.DataAccess.DTO.Manager
{
   public class AdModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Type { get; set; }

        public string Page { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Day { get; set; }

        public decimal UnitPrice { get; set; }

        public string Desc { get; set; }

        public int Order { get; set; }

        public int Total { get; set; }

        /// <summary>
        /// 当前包含条数
        /// </summary>
        public int CurrentNumber { get; set; }
    }
}
