using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Controllers;
using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.HazardousSubstanceRiskAssessmentSummaryTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkGeneralRiskAssessmentIsDraftStatusTests
    {
        private Mock<IRiskAssessmentService> _riskAssessmentService;
        private const long CompanyId = 100;
        private const long RiskAssessmentId = 200;
        
        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IRiskAssessmentService>();
            
        }

        [Test]
        public void Given_valid_request_to_mark_as_draft_When_mark_risk_assessment_is_draft_status_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            var request = new MarkRiskAssessmentIsDraftStatusViewModel()
                              {
                                  CompanyId = CompanyId,
                                  RiskAssessmentId = RiskAssessmentId,
                                  IsDraft = true
                              };

            // When
            controller.MarkRiskAssessmentIsDraftStatus(request);

            // Then
            _riskAssessmentService.Verify(x => x.MarkRiskAssessmentAsDraft(It.Is<MarkRiskAssessmentAsDraftRequest>(y => y.CompanyId == request.CompanyId && y.RiskAssessmentId == request.RiskAssessmentId && y.UserId == controller.CurrentUser.UserId)));
        }

        [Test]
        public void Given_valid_request_to_mark_not_as_draft_When_mark_risk_assessment_is_draft_status_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            var request = new MarkRiskAssessmentIsDraftStatusViewModel()
            {
                CompanyId = CompanyId,
                RiskAssessmentId = RiskAssessmentId,
                IsDraft = false
            };

            _riskAssessmentService.Setup(x => x.CanMarkRiskAssessmentAsLive(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(true);
            // When
            controller.MarkRiskAssessmentIsDraftStatus(request);
            
            // Then
            _riskAssessmentService.Verify(x => x.MarkRiskAssessmentAsLive(It.Is<MarkRiskAssessmentAsLiveRequest>(y => y.CompanyId == request.CompanyId && y.RiskAssessmentId == request.RiskAssessmentId && y.UserId == controller.CurrentUser.UserId)));
            
        }

        [Test]
        public void Given_valid_request_to_mark_not_as_draft_When_mark_risk_assessment_is_draft_status_and_cannot_be_marked_as_not_draft_Then_should_call_correct_methods()
        {
            // Given
            var controller = GetTarget();
            var request = new MarkRiskAssessmentIsDraftStatusViewModel()
            {
                CompanyId = CompanyId,
                RiskAssessmentId = RiskAssessmentId,
                IsDraft = false
            };

            _riskAssessmentService.Setup(x => x.CanMarkRiskAssessmentAsLive(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(false);
            // When
            controller.MarkRiskAssessmentIsDraftStatus(request);

            // Then
            _riskAssessmentService.Verify(x => x.MarkRiskAssessmentAsLive(It.Is<MarkRiskAssessmentAsLiveRequest>(y => y.CompanyId == request.CompanyId && y.RiskAssessmentId == request.RiskAssessmentId && y.UserId == controller.CurrentUser.UserId)),Times.Never());

        }



        private RiskAssessmentStatusController GetTarget()
        {
            var result = new RiskAssessmentStatusController(_riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }    
    }
}