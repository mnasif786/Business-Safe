using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.Summary
{
    [TestFixture]
    public class SaveTests
    {
        private Mock<IEditGeneralRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IGeneralRiskAssessmentService> _riskAssessmentService;
        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IEditGeneralRiskAssessmentSummaryViewModelFactory>();
           _riskAssessmentService = new Mock<IGeneralRiskAssessmentService>();
            _riskAssessmentService.Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()));

        }

        [Test]
        public void Given_Valid_Model_When_Save_Then_Send_Update_Request_To_Service()
        {
            // Given
            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = 100,
                CompanyId = 200,
                Title = "New Title",
                Reference = "New Reference",
                RiskAssessorId = 1234L,
                SiteId = 5678L
            };
            var passedSaveRiskAssessmentRequest = new SaveRiskAssessmentSummaryRequest();
            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()))
                .Callback<SaveRiskAssessmentSummaryRequest>(y => passedSaveRiskAssessmentRequest = y);
            var target = GetTarget();

            // When
            target.Save(editSummaryViewModel);

            // Then
            _riskAssessmentService.Verify(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()));
            Assert.That(passedSaveRiskAssessmentRequest.Id, Is.EqualTo(editSummaryViewModel.RiskAssessmentId));
            Assert.That(passedSaveRiskAssessmentRequest.CompanyId, Is.EqualTo(editSummaryViewModel.CompanyId));
            Assert.That(passedSaveRiskAssessmentRequest.Title, Is.EqualTo(editSummaryViewModel.Title));
            Assert.That(passedSaveRiskAssessmentRequest.Reference, Is.EqualTo(editSummaryViewModel.Reference));
            Assert.That(passedSaveRiskAssessmentRequest.UserId, Is.EqualTo(target.CurrentUser.UserId));
        }

        [Test]
        public void Given_no_riskassessor_Model_When_Save_Then_add_error()
        {
            // Given
            const long riskAssessmentId = 1324L;
            const long companyId = 1324L;

            _viewModelFactory
               .Setup(x => x.WithRiskAssessmentId(riskAssessmentId))
               .Returns(_viewModelFactory.Object);

            _viewModelFactory
               .Setup(x => x.WithCompanyId(companyId))
               .Returns(_viewModelFactory.Object);

            var target = GetTarget();

            var allowableSites = target.CurrentUser.GetSitesFilter();
            _viewModelFactory
               .Setup(x => x.WithAllowableSiteIds(allowableSites))
               .Returns(_viewModelFactory.Object);

            _viewModelFactory
               .Setup(x => x.GetViewModel())
               .Returns(new EditSummaryViewModel());

            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = riskAssessmentId,
                CompanyId = companyId,
                Title = "New Title",
                Reference = "New Reference"
            };
            var passedSaveRiskAssessmentRequest = new SaveRiskAssessmentSummaryRequest();
            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()))
                .Callback<SaveRiskAssessmentSummaryRequest>(y => passedSaveRiskAssessmentRequest = y);

            // When
            target.Save(editSummaryViewModel);

            // Then
            Assert.That(target.ModelState.IsValid, Is.False);
            Assert.That(target.ModelState.ContainsKey("RiskAssessorId"));
        }

        [Test]
        public void Given_no_site_Model_When_Save_Then_add_error()
        {
            // Given
            const long riskAssessmentId = 1324L;
            const long companyId = 1324L;

            _viewModelFactory
               .Setup(x => x.WithRiskAssessmentId(riskAssessmentId))
               .Returns(_viewModelFactory.Object);

            _viewModelFactory
               .Setup(x => x.WithCompanyId(companyId))
               .Returns(_viewModelFactory.Object);

            var target = GetTarget();

            var allowableSites = target.CurrentUser.GetSitesFilter();
            _viewModelFactory
               .Setup(x => x.WithAllowableSiteIds(allowableSites))
               .Returns(_viewModelFactory.Object);

            _viewModelFactory
               .Setup(x => x.GetViewModel())
               .Returns(new EditSummaryViewModel());

            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = riskAssessmentId,
                CompanyId = companyId,
                Title = "New Title",
                Reference = "New Reference"
            };
            var passedSaveRiskAssessmentRequest = new SaveRiskAssessmentSummaryRequest();
            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()))
                .Callback<SaveRiskAssessmentSummaryRequest>(y => passedSaveRiskAssessmentRequest = y);

            // When
            target.Save(editSummaryViewModel);

            // Then
            Assert.That(target.ModelState.IsValid, Is.False);
            Assert.That(target.ModelState.ContainsKey("SiteId"));
        }

        [Test]
        public void Given_Valid_Model_When_Save_Then_should_return_correct_result()
        {
            // Given
            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = 100,
                CompanyId = 200,
                Title = "New Title",
                Reference = "New Reference",
                RiskAssessorId = 1234L,
                SiteId = 5678L
            };
            var target = GetTarget();

            // When
            var result = target.Save(editSummaryViewModel) as RedirectToRouteResult;
            
            // Then
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(editSummaryViewModel.RiskAssessmentId));
            Assert.That(target.TempData["Notice"], Is.Not.Null);
        }

        [Test]
        public void Given_invalid_model_When_Save_Then_should_call_correct_methods()
        {
            var riskAssessmentId = 100L;
            var companyId = 200L;

            // Given
            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = riskAssessmentId,
                CompanyId = companyId,
                Title = "",
                Reference = ""
            };

            var target = GetTarget();
            target.ModelState.AddModelError("Anything", "Anything");

            _viewModelFactory
               .Setup(x => x.WithRiskAssessmentId(riskAssessmentId))
               .Returns(_viewModelFactory.Object);

            _viewModelFactory
               .Setup(x => x.WithCompanyId(companyId))
               .Returns(_viewModelFactory.Object);

            var allowableSites = target.CurrentUser.GetSitesFilter();
            _viewModelFactory
               .Setup(x => x.WithAllowableSiteIds(allowableSites))
               .Returns(_viewModelFactory.Object);

            _viewModelFactory
               .Setup(x => x.GetViewModel())
               .Returns(new EditSummaryViewModel());

            // When
            target.Save(editSummaryViewModel);

            // Then
            _viewModelFactory.VerifyAll();
        }

        private SummaryController GetTarget()
        {
            var result = new SummaryController(_riskAssessmentService.Object, _viewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
