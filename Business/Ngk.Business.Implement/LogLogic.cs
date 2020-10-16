using System;
using Thor.Framework.Data.Model;
using Ngk.Business.Interface;
using Ngk.DataAccess.Interface.Mongo;

namespace Ngk.Business.Implement
{
    public class LogLogic : ILogLogic
    {
        private IMonitorLogRepository _monitorLogRepository;

        public LogLogic(IMonitorLogRepository monitorLogRepository)
        {
            this._monitorLogRepository = monitorLogRepository;
        }

        public ExcutedResult GetMonitorLogsDataInfo()
        {
            throw new NotImplementedException();
        }
    }
}
