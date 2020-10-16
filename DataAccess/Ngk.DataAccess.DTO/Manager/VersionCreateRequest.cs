using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public class VersionCreateRequest
    {
        public string Name { get; set; }
        public int? Number { get; set; }
        public int ClientType { get; set; }
        public bool IsMustUpdate { get; set; }
        public DateTime Date { get; set; }
        public string Desc { get; set; }
        public string Connect { get; set; }
        public string Deleter { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteIp { get; set; }
        public int State { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
