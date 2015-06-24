using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.Summary
{
    [TestFixture]
    public class SaveTests
    {
        private SummaryController target;
        private Mock<IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory>();
            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithAllowableSiteIds(It.IsAny<IList<long>>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new EditSummaryViewModel());

            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _riskAssessmentService.Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()));

            target = GetTarget();
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

            // When
            target.Save(editSummaryViewModel);

            // Then
            Assert.That(target.ModelState.IsValid, Is.False);
            Assert.That(target.ModelState.ContainsKey("SiteId"));
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
                HazardousSubstanceId = 300,
                RiskAssessorId = 1234L,
                SiteId = 678L
            };
            var passedSaveRiskAssessmentRequest = new SaveHazardousSubstanceRiskAssessmentRequest();
            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()))
                .Callback<SaveHazardousSubstanceRiskAssessmentRequest>(y => passedSaveRiskAssessmentRequest = y);
            var target = GetTarget();

            // When
            var result = target.Save(editSummaryViewModel);
            var viewResult = result as ViewResult;

            // Then
            _riskAssessmentService.Verify(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveHazardousSubstanceRiskAssessmentRequest>()));
            Assert.That(passedSaveRiskAssessmentRequest.Id, Is.EqualTo(editSummaryViewModel.RiskAssessmentId));
            Assert.That(passedSaveRiskAssessmentRequest.CompanyId, Is.EqualTo(editSummaryViewModel.CompanyId));
            Assert.That(passedSaveRiskAssessmentRequest.Title, Is.EqualTo(editSummaryViewModel.Title));
            Assert.That(passedSaveRiskAssessmentRequest.Reference, Is.EqualTo(editSummaryViewModel.Reference));
            Assert.That(passedSaveRiskAssessmentRequest.UserId, Is.EqualTo(target.CurrentUser.UserId));
            Assert.That(passedSaveRiskAssessmentRequest.HazardousSubstanceId, Is.EqualTo(editSummaryViewModel.HazardousSubstanceId));
        }

        [Test]
        public void Given_Valid_Model_When_Save_Then_Request_AttachDropdownData_From_ViewModelFactory()
        {
            // Given
            var editSummaryViewModel = new EditSummaryViewModel()
            {
                RiskAssessmentId = 100,
                CompanyId = 200,
                Title = "New Title",
                Reference = "New Reference",
                HazardousSubstanceId = 300
            };
            var target = GetTarget();

            // When
            var result = target.Save(editSummaryViewModel);
            var viewResult = result as ViewResult;

            // Then
            _viewModelFactory.Verify(x => x.WithCompanyId(200));
            _viewModelFactory.Verify(x => x.AttachDropDownData(It.IsAny<EditSummaryViewModel>()));
        }

        private SummaryController GetTarget()
        {
            var result = new SummaryController(_riskAssessmentService.Object, _viewModelFactory.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
