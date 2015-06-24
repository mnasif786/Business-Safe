using System;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    public class ResponsibilityNotFoundException : NullReferenceException
    {
        public ResponsibilityNotFoundException(long responsibilityId, long companyId)
            : base(string.Format("Could not find Responsibility with id {0}, belonging to company {1}", responsibilityId, companyId))
        {}
    }
}