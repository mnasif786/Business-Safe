using System;

namespace BusinessSafe.Application.Implementations
{
    public class TryingToDeleteHazardousSubstanceThatUsedByRiskAssessmentsException : ArgumentException
    {
        public TryingToDeleteHazardousSubstanceThatUsedByRiskAssessmentsException(long hazardousSubstanceId)
            : base(string.Format("Trying to delete hazardous substance that is used by risk assessments. Hazardous Substance Id is {0}", hazardousSubstanceId))
        {}
    }
}