using System.Collections.Generic;
using Thor.Framework.Common.Options;

namespace Ngk.Service.ManagerWebApi.Common
{
    public class CustomerSettings
    {
        public IList<DbContextOption> DatabaseSettings { get; set; }
    }
}
