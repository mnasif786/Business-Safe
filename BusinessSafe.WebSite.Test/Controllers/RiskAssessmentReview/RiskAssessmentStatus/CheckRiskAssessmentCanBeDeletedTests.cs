using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessmentStatus
{
    [TestFixture]
    public class CheckRiskAssessmentCanBeDeletedTests
    {
        private Mock<IRiskAssessmentService> _riskAssessmentService;
        
        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IRiskAssessmentService>();
        }

        [Test]
        public void When_CheckRiskAssessmentCanBeDeleted_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            const int companyId = 2;
            const int riskAssessmentId = 1;

            _riskAssessmentService
                .Setup(x => x.HasUndeletedTasks(companyId, riskAssessmentId))
                .Returns(true);

            //When
            target.CheckRiskAssessmentCanBeDeleted(companyId,riskAssessmentId);

            //Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void Given_has_undeleted_tasks_When_CheckRiskAssessmentCanBeDeleted_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            const int companyId = 2;
            const int riskAssessmentId = 1;

            _riskAssessmentService
                .Setup(x => x.HasUndeletedTasks(companyId, riskAssessmentId))
                .Returns(true);

            //When
            var result = target.CheckRiskAssessmentCanBeDeleted(companyId, riskAssessmentId);

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ hasUndeletedTasks = False }"));
        }

        [Test]
        public void Given_has_not_got_undeleted_tasks_When_CheckRiskAssessmentCanBeDeleted_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            const int companyId = 2;
            const int riskAssessmentId = 1;

            _riskAssessmentService
                .Setup(x => x.HasUndeletedTasks(companyId, riskAssessmentId))
                .Returns(false);

            //When
            var result = target.CheckRiskAssessmentCanBeDeleted(companyId, riskAssessmentId);

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ hasUndeletedTasks = True }"));
        }

        private RiskAssessmentStatusController GetTarget()
        {
            var result = new RiskAssessmentStatusController(_riskAssessmentService.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}