using System;

namespace BusinessSafe.Application.Implementations
{
    public class TryingToDeleteRiskAssessorWithOutstandingRiskAssessmentsException : ArgumentException
    {
        public TryingToDeleteRiskAssessorWithOutstandingRiskAssessmentsException(long riskAssessorId)
            : base(string.Format("Trying to delete risk assessor with outstanding tasks. Risk Assessor Id is {0}", riskAssessorId))
        { }
    }
}