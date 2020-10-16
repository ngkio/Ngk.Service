using System;

namespace Ngk.DataAccess.DTO
{
    /// <summary>
    /// AccountOrder查询请求参数
    /// </summary>
    public class AccountOrderQueryRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int BusinessState { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页的数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序方式，是否倒序
        /// </summary>
        public bool IsSortOrderDesc { get; set; }

        /// <summary>
        /// 用来排序的字段
        /// </summary>
        public string SortName { get; set; }


        /// <summary>
        /// 参数验证并且赋值
        /// </summary>
        public void VerifyAndAssign()
        {
            if (String.IsNullOrEmpty(SortName))
            {
                SortName = "CreateTime";
                IsSortOrderDesc = true;
            }

            if (PageSize == default(int)) PageSize = 10;

            if (PageIndex == default(int)) PageIndex = 1;

            var startTime = DateTime.MinValue;
            if (!String.IsNullOrEmpty(StartTime) && !DateTime.TryParse(StartTime, out startTime))
            {
                StartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                StartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            var endTime = DateTime.MaxValue;
            if (!String.IsNullOrEmpty(EndTime))
            {
                if (!DateTime.TryParse(EndTime, out endTime))
                {
                    EndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            else
            {
                EndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
