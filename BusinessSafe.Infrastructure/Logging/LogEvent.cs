using System;

using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Infrastructure.Attributes;

namespace BusinessSafe.Infrastructure.Logging
{
    /// <summary>
    /// An event to add to the event log.
    /// </summary>
    /// <remarks>
    /// Author  :   Paul Davies
    /// Date    :   26th December 2010
    /// </remarks>
    [LogIgnore]
    [CoverageExclude]
    public class LogEvent
    {
        public DateTime Date { get; set; }
        public int Level { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public Exception Exception { get; set; }
        public object Objects { get; set; }
        public string ExecutingMachine { get; set; }
        public string CallingAssembly { get; set; }
        public string CallingClass { get; set; }
        public string CallingMethod { get; set; }
        public Guid ContextGuid { get; set; }
    }
}
