using System;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SaveHazardousSubstanceRiskAssessmentRequestBuilder
    {
        private static string _title;
        private static string _reference;
        private static DateTime _assessmentDate;
        private Guid _employeeId;
        private long _riskAssessorId = 82L;
        private int _hazardousSubstanceId   ;
        

        public static SaveHazardousSubstanceRiskAssessmentRequestBuilder Create()
        {
            var saveRiskAssessmentRequestBuilder = new SaveHazardousSubstanceRiskAssessmentRequestBuilder();
            _title = "title";
            _reference = "reference";
            _assessmentDate = DateTime.Now;
        
            return saveRiskAssessmentRequestBuilder;
        }

        public SaveHazardousSubstanceRiskAssessmentRequest Build()
        {
            var saveRiskAssessmentRequest = new SaveHazardousSubstanceRiskAssessmentRequest
                                                {
                                                    Title = _title,
                                                    Reference = _reference,
                                                    AssessmentDate = _assessmentDate,
                                                    RiskAssessorId = _riskAssessorId,
                                                    WorkspaceExposureLimits = string.Empty,
                                                    HazardousSubstanceId = _hazardousSubstanceId,
                                                };

            return saveRiskAssessmentRequest;
        }

        public SaveHazardousSubstanceRiskAssessmentRequestBuilder WithTitle(string title)
        {
            _title = title;

            return this;
        }

        public SaveHazardousSubstanceRiskAssessmentRequestBuilder WithRiskAssessor(Guid employeeId)
        {
            _employeeId = employeeId;
            return this;
        }

        public SaveHazardousSubstanceRiskAssessmentRequestBuilder WithHazardousSubstanceId(int hazardousSubstanceId)
        {
            _hazardousSubstanceId = hazardousSubstanceId;
            return this;
        }

    }
}