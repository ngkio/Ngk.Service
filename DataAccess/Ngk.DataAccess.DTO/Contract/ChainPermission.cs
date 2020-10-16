using System.Collections.Generic;

namespace Ngk.DataAccess.DTO.Contract
{
    public class ChainPermission
    {
        public string PermName { get; set; }

        public string Parent { get; set; }

        public ChainAuthority RequiredAuth { get; set; }
    }
    
    public class ChainAuthority
    {
        public uint Threshold { get; set; }

        public List<ChainAuthorityKey> Keys { get; set; }

        public List<ChainAuthorityAccount> Accounts { get; set; }

        public List<ChainAuthorityWait> Waits { get; set; }
    }
    
    public class ChainAuthorityKey
    {
        public string Key { get; set; }

        public int Weight { get; set; }
    }
    
    public class ChainAuthorityAccount
    {
        public string Account { get; set; }

        public int Weight { get; set; }
    }
    
    public class ChainAuthorityWait
    {
        public string WaitSec { get; set; }

        public int Weight { get; set; }
    }
}
