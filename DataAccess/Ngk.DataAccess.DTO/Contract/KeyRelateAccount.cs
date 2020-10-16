using System.Collections.Generic;

namespace Ngk.DataAccess.DTO.Contract
{
    public class KeyRelateAccount
    {
        public AccountInfo AccountInfo { get; set; }

        public List<string> PermissionNames { get; set; }
    }
}