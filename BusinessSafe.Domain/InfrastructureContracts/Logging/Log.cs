using System;

using StructureMap;

namespace BusinessSafe.Domain.InfrastructureContracts.Logging
{
    /// <summary>
    /// Static methods provide a shortcut for logging.
    /// </summary>
    /// <remarks>
    /// Author  :   Paul Davies
    /// Date    :   9th March 2012
    /// </remarks>
    [LogIgnore]
    public static class Log
    {
        private static readonly IPeninsulaLog PeninsulaLog;

        static Log()
        {
            PeninsulaLog = ObjectFactory.GetInstance<IPeninsulaLog>();
        }

        public static string Add(int level, string message, Exception ex, object objects)
        {
            return PeninsulaLog.Add(level, message, ex, objects);
        }

        public static string Add(int level, string message)
        {
            return PeninsulaLog.Add(level, message);
        }

        public static string Add(int level, string message, Exception ex)
        {
            return PeninsulaLog.Add(level, message, ex);
        }

        public static string Add(int level, string message, object objects)
        {
            return PeninsulaLog.Add(level, message, objects);
        }

        public static string Add(Exception ex)
        {
            return PeninsulaLog.Add(ex);
        }

        public static string Add(string message)
        {
            return PeninsulaLog.Add(message);
        }

        public static string Add(string message, object objects)
        {
            return PeninsulaLog.Add(message, objects);
        }

        public static string Add(object objects)
        {
            return PeninsulaLog.Add(objects);
        }

        public static string Add()
        {
            return PeninsulaLog.Add();
        }
    }
}
