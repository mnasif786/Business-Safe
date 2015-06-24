using System;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class SaveRiskAssessmentRequestBuilder
    {
        private static string _title;
        private static string _reference;
        
        public static SaveRiskAssessmentRequestBuilder Create()
        {
            var saveRiskAssessmentRequestBuilder = new SaveRiskAssessmentRequestBuilder();
            _title = "title";
            _reference = "reference";
        
            return saveRiskAssessmentRequestBuilder;
        }

        public CreateRiskAssessmentRequest Build()
        {
            var saveRiskAssessmentRequest = new CreateRiskAssessmentRequest
            {
                Title = _title,
                Reference = _reference,
            };

            return saveRiskAssessmentRequest;
        }

        public SaveRiskAssessmentRequestBuilder WithTitle(string title)
        {
            _title = title;

            return this;
        }
    }
}

