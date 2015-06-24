using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessmentStatus
{
    [TestFixture]
    public class MarkRiskAssessmentAsDeletedOrUnDeletedTests
    {
        private Mock<IRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IRiskAssessmentService>();
        }

        [Test]
        public void Given_deleting_risk_assessment_When_MarkRiskAssessmentAsDeletedOrUnDeleted_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            const int companyId = 2;
            const int riskAssessmentId = 1;
            var userId = target.CurrentUser.UserId;

            _riskAssessmentService
                .Setup(
                    x =>
                    x.MarkRiskAssessmentAsDeleted(
                        It.Is<MarkRiskAssessmentAsDeletedRequest>(
                            y =>
                            y.CompanyId == companyId && y.RiskAssessmentId == riskAssessmentId && y.UserId == userId)));

            //When
            target.MarkRiskAssessmentAsDeletedOrUnDeleted(companyId, riskAssessmentId, true);

            //Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void Given_reinstating_risk_assessment_When_MarkRiskAssessmentAsDeletedOrUnDeleted_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            const int companyId = 2;
            const int riskAssessmentId = 1;
            var userId = target.CurrentUser.UserId;

            _riskAssessmentService
                .Setup(x => x.ReinstateRiskAssessmentAsNotDeleted(It.Is<ReinstateRiskAssessmentAsDeletedRequest>(y => y.CompanyId == companyId && y.RiskAssessmentId == riskAssessmentId && y.UserId == userId)));

            //When
            target.MarkRiskAssessmentAsDeletedOrUnDeleted(companyId, riskAssessmentId, false);

            //Then
            _riskAssessmentService.VerifyAll();
        }


        private RiskAssessmentStatusController GetTarget()
        {
            var result = new RiskAssessmentStatusController(_riskAssessmentService.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}