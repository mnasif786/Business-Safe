using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public class CompleteRiskAssessmentReviewRequestBuilder
    {
        private long _clientId;
        private Guid _userId;
        private CompleteReviewViewModel _completeReviewViewModel;
        private readonly List<CreateDocumentRequest> _createDocumentRequests;

        public CompleteRiskAssessmentReviewRequestBuilder()
        {
            _createDocumentRequests = new List<CreateDocumentRequest>();
        }

        public CompleteRiskAssessmentReviewRequestBuilder WithClientId(long clientId)
        {
            _clientId = clientId;
            return this;
        }

        public CompleteRiskAssessmentReviewRequestBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public CompleteRiskAssessmentReviewRequestBuilder WithCompleteReview_completeReviewViewModel(CompleteReviewViewModel completeReviewViewModel)
        {
            _completeReviewViewModel = completeReviewViewModel;
            return this;
        }

        public CompleteRiskAssessmentReviewRequestBuilder WithCreateDocumentRequest(CreateDocumentRequest request)
        {
            _createDocumentRequests.Add(request);
            return this;
        }

        public CompleteRiskAssessmentReviewRequest Build()
        {
            return new CompleteRiskAssessmentReviewRequest
            {
                ClientId = _clientId,
                RiskAssessmentId = _completeReviewViewModel.RiskAssessmentId,
                RiskAssessmentReviewId = _completeReviewViewModel.RiskAssessmentReviewId,
                IsComplete = _completeReviewViewModel.IsComplete,
                CompletedComments = _completeReviewViewModel.CompletedComments,
                NextReviewDate = _completeReviewViewModel.NextReviewDate,
                Archive = _completeReviewViewModel.Archive,
                ReviewingUserId = _userId,
                CreateDocumentRequests = _createDocumentRequests
            };
        }
    }
}