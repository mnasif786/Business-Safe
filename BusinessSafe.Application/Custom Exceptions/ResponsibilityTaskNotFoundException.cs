using System;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    public class ResponsibilityTaskNotFoundException : NullReferenceException
    {
        public ResponsibilityTaskNotFoundException(long responsibilityTaskId, long companyId)
            : base(string.Format("Could not find Responsibility Task with id {0}, belonging to company {1}", responsibilityTaskId, companyId))
        {}
    }
}