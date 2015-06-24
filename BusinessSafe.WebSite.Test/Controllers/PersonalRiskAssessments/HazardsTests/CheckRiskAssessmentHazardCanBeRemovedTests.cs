using System.Web.Mvc;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.HazardsTests
{
    [TestFixture]
    [Category("Unit")]
    public class CheckRiskAssessmentHazardCanBeRemovedTests
    {
        private Mock<IMultiHazardRiskAssessmentService> _riskAssessmentService;
        
        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IMultiHazardRiskAssessmentService>();
        }

        [Test]
        public void Given_valid_viewmodel_When_CheckRiskAssessmentHazardCanBeRemoved_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            long companyId = 200L;
            long riskAssessmentId = 300L;
            long riskAssessmentHazardId = 400L;

            //When
            target.CheckRiskAssessmentHazardCanBeRemoved(companyId, riskAssessmentId, riskAssessmentHazardId);

            //Then
            _riskAssessmentService.Verify(x => x.CanRemoveRiskAssessmentHazard(companyId, riskAssessmentId, riskAssessmentHazardId),Times.Once());
        }

        [Test]
        public void Given_viewmodel_that_cant_be_deleted_When_CheckRiskAssessmentHazardCanBeRemoved_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            long companyId = 200L;
            long riskAssessmentId = 300L;
            long riskAssessmentHazardId = 400L;

            _riskAssessmentService
                .Setup(x => x.CanRemoveRiskAssessmentHazard(companyId, riskAssessmentId, riskAssessmentHazardId))
                .Returns(false);

            //When
            var result = target.CheckRiskAssessmentHazardCanBeRemoved(companyId, riskAssessmentId, riskAssessmentHazardId) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ CanBeRemoved = False }"));
        }

        [Test]
        public void Given_viewmodel_that_can_be_deleted_When_CheckRiskAssessmentHazardCanBeRemoved_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            long companyId = 200L;
            long riskAssessmentId = 300L;
            long riskAssessmentHazardId = 400L;

            _riskAssessmentService
                .Setup(x => x.CanRemoveRiskAssessmentHazard(companyId, riskAssessmentId, riskAssessmentHazardId))
                .Returns(true);

            //When
            var result = target.CheckRiskAssessmentHazardCanBeRemoved(companyId, riskAssessmentId, riskAssessmentHazardId) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ CanBeRemoved = True }"));
        }

        private HazardsController GetTarget()
        {
            var result = new HazardsController(null, null, null, _riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}