using System;

namespace BusinessSafe.Domain.InfrastructureContracts.Logging
{
    public class StubPeninsulaLog : IPeninsulaLog
    {
        public string Add(int level, string message, Exception ex, object objects)
        {
            return null;
        }

        public string Add(int level, string message)
        {
            return null;
        }

        public string Add(int level, string message, Exception ex)
        {
            return null;
        }

        public string Add(int level, string message, object objects)
        {
            return null;
        }

        public string Add(Exception ex)
        {
            return null;
        }

        public string Add(string message)
        {
            return null;
        }

        public string Add(string message, object objects)
        {
            return null;
        }

        public string Add(object objects)
        {
            return null;
        }

        public string Add()
        {
            return null;
        }
    }
}
