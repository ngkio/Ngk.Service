namespace Ngk.DataAccess.DTO.Contract
{
    public class NewAccount
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 内存购买金额
        /// </summary>
        public decimal RAMQuantity { get; set; }

        /// <summary>
        /// 网络抵押金额
        /// </summary>
        public decimal NetStakeQuantity { get; set; }

        /// <summary>
        /// CPU抵押金额
        /// </summary>
        public decimal CPUStakeQuantity { get; set; }

        /// <summary>
        /// 抵押金额是否转移给用户
        /// </summary>
        public bool IsStakeTransfer { get; set; }

        /// <summary>
        /// Owner公钥
        /// </summary>
        public string OwnerKey { get; set; }

        /// <summary>
        /// Active公钥
        /// </summary>
        public string ActiveKey { get; set; }
    }
}