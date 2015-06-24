using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.SignificantFindingsTests
{
    [TestFixture]
    public class MarkSignificantFindingAsDeletedTests
    {
        private Mock<ISignificantFindingService> _significantFindingService;

        [SetUp]
        public void Setup()
        {
            _significantFindingService = new Mock<ISignificantFindingService>();
        }

        [Test]
        public void When_get_MarkSignificantFindingAsDeleted_Then_calls_correct_method()
        {
            // Given
            var target = GetTarget();
            var viewModel = new MarkSignificantFindingAsDeletedViewModel
                                {
                                    CompanyId = 12L,
                                    FireChecklistId = 34L,
                                    FireQuestionId = 56L
                                };

            // When
            target.MarkSignificantFindingAsDeleted(viewModel);

            // Then
            _significantFindingService.Verify(x => x.MarkSignificantFindingAsDeleted(It.Is<MarkSignificantFindingAsDeletedRequest>(
                                    y => y.CompanyId == viewModel.CompanyId &&
                                         y.FireChecklistId == viewModel.FireChecklistId &&
                                         y.FireQuestionId == viewModel.FireQuestionId &&
                                         y.UserId == target.CurrentUser.UserId
                                    )));
        }

        [Test]
        public void When_get_MarkSignificantFindingAsDeleted_Then_returns_correct_result()
        {
            // Given
            var target = GetTarget();
            var viewModel = new MarkSignificantFindingAsDeletedViewModel
            {
                FireChecklistId = 12L,
                FireQuestionId = 34L
            };

            // When
            var result = target.MarkSignificantFindingAsDeleted(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("Success = True"));
        }

        private SignificantFindingsController GetTarget()
        {
            var result = new SignificantFindingsController(null, _significantFindingService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }

    }
}
