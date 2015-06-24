using System.Web.Mvc;

using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.Summary
{
    [TestFixture]
    public class SaveTests
    {
        private Mock<IEditPersonalRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IPersonalRiskAssessmentService> _riskAssessmentService;
        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IEditPersonalRiskAssessmentSummaryViewModelFactory>();
            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new EditSummaryViewModel());

            _riskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            //_riskAssessmentService.Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<SaveRiskAssessmentSummaryRequest>()));

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
                                               SiteId = 785L
                                           };
            var passedSaveRiskAssessmentRequest = new UpdatePersonalRiskAssessmentSummaryRequest();

            _riskAssessmentService
                .Setup(x => x.UpdateRiskAssessmentSummary(It.IsAny<UpdatePersonalRiskAssessmentSummaryRequest>()))
                .Callback<UpdatePersonalRiskAssessmentSummaryRequest>(y => passedSaveRiskAssessmentRequest = y);
            var target = GetTarget();

            // When
            target.Save(editSummaryViewModel);
            
            // Then
            _riskAssessmentService.Verify(x => x.UpdateRiskAssessmentSummary(It.IsAny<UpdatePersonalRiskAssessmentSummaryRequest>()));
            Assert.That(passedSaveRiskAssessmentRequest.Id, Is.EqualTo(editSummaryViewModel.RiskAssessmentId));
            Assert.That(passedSaveRiskAssessmentRequest.CompanyId, Is.EqualTo(editSummaryViewModel.CompanyId));
            Assert.That(passedSaveRiskAssessmentRequest.Title, Is.EqualTo(editSummaryViewModel.Title));
            Assert.That(passedSaveRiskAssessmentRequest.Reference, Is.EqualTo(editSummaryViewModel.Reference));
            Assert.That(passedSaveRiskAssessmentRequest.UserId, Is.EqualTo(target.CurrentUser.UserId));
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
                                               SiteId = 7876L
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

        private SummaryController GetTarget()
        {
            var result = new SummaryController(_riskAssessmentService.Object, _viewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}