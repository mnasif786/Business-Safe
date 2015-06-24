using System;

namespace BusinessSafe.Data.CustomExceptions
{
    public class PersonAtRiskNotFoundException : ArgumentNullException
    {
        public PersonAtRiskNotFoundException(long personAtRiskId)
            : base(string.Format("Person At Risk Not Found. Person At Risk not found for person at risk id {0}", personAtRiskId))
        {
        }
    }
}