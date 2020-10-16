using System;

namespace Ngk.DataAccess.DTO
{
    public class TransferRecordModel
    {
        public string FromAccount { get; set; }

        public string ToAccount { get; set; }

        public string Amount { get; set; }

        public string Symbol { get; set; }

        public string Contract { get; set; }

        public string CreateTimeStr
        {
            get { return CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        public string Memo { get; set; }

        public string TransferId { get; set; }

        public long BlockNum { get; set; }

        public DateTime CreateTime { get; set; }

        public override bool Equals(object obj)
        {
            var o = obj as TransferRecordModel;
            if (o == null)
            {
                return false;
            }
            return FromAccount.Equals(o.FromAccount) && ToAccount.Equals(o.ToAccount) && Amount.Equals(o.Amount) &&
                TransferId.Equals(o.TransferId) && Symbol.Equals(o.Symbol) && Contract.Equals(o.Contract) &&
                CreateTime.Equals(o.CreateTime);
        }

        public override int GetHashCode()
        {
            return FromAccount.GetHashCode()+ToAccount.GetHashCode()+Amount.GetHashCode()+Symbol.GetHashCode()+TransferId.GetHashCode();
        }
    }
}
