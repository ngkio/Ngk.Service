using System;

namespace Ngk.DataAccess.DTO.Manager
{
    public  class ConfigDataCreateRequest
    {
        
       
        public string ConfigKey { get; set; }
      
        public string ConfigValue { get; set; }

        public string Remark { get; set; }
    
        public int Environment { get; set; }
       
        public DateTime CreateTime { get; set; }
       
        public int State { get; set; }

        public string Deleter { get; set; }

        public DateTime? DeleteTime { get; set; }

        public string DeleteIp { get; set; }

    }
}
