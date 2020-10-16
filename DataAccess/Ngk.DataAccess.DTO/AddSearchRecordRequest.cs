using System;

namespace Ngk.DataAccess.DTO
{
    public class AddSearchRecordRequest : ChainModel
    {
        public int Type { get; set; }

        public Guid? ItemId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}
