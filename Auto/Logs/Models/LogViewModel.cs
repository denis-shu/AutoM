using System.Collections.Generic;

namespace Logs.Models
{
    public class LogViewModel
    {
        public List<LogEntry> logViewModel { get; set; }

        public PageInfo pageinfo { get; set; }
    }
}
