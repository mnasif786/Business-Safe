using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.Summary
{
    [TestFixture]
    public class SaveTests
    {
        private Mock<IEditFireRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;
        
        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IEditFireRiskAssessmentSummaryViewModelFactory>();
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();

        }
        
        [Test]
        public void Given_valid_model_When_Save_Then_should_call_correct_methods()
        {
            // Given
            var model = new EditSummaryViewModel()
                                       {
                                           RiskAssessmentId = 100,
                                           CompanyId = 200,
                                           Title = "New Title",
                                           Reference = "New Reference",
                                           DateOfAssessment = DateTime.Now,
                                           RiskAssessorId = 364L,
                                           SiteId = 378L
                                       };
            
            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.Is<SaveRiskAssessmentSummaryRequest>(y => 
                                                                y.CompanyId == model.CompanyId && 
                                                                y.Id == model.RiskAssessmentId &&
                                                                y.Title == model.Title && 
                                                                y.Reference == model.Reference &&
                                                                y.RiskAssessorId == model.RiskAssessorId &&
                                                                y.AssessmentDate == model.DateOfAssessment)));
                
            var target = GetTarget();

            // When
            target.Save(model);

            // Then
            _riskAssessmentService.Verify(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()));
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
        public void Given_valid_model_When_Save_Then_should_return_correct_result()
        {
            // Given
            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = 100,
                CompanyId = 200,
                Title = "New Title",
                Reference = "New Reference",
                SiteId = 378L,
                RiskAssessorId = 1234L
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
            var result = new SummaryController(_viewModelFactory.Object,_riskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
