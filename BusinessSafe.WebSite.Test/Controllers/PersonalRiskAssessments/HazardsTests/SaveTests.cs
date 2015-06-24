using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.HazardsTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests 
    {
        private Mock<IMultiHazardRiskAssessmentAttachmentService> _multiHazardRiskAssessmentAttachmentService;

    
        [SetUp]
        public void SetUp()
        {
            _multiHazardRiskAssessmentAttachmentService = new Mock<IMultiHazardRiskAssessmentAttachmentService>();
        }

        [Test]
        public void Given_valid_viewmodel_When_save_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            Guid userId = target.CurrentUser.UserId;

            var viewModel = new SaveHazardsViewModel()
            {
                CompanyId = 500,
                RiskAssessmentId = 588,
                HazardIds = new []{ 1L},
                PeopleAtRiskIds = new[] {2L}
            };

            var expectedAttachHazardsToRiskAssessmentHazardDetail = new AttachHazardsToRiskAssessmentHazardDetail() { Id = viewModel.HazardIds[0], OrderNumber = 1 };

            AttachHazardsToRiskAssessmentRequest passedAttachHazardsToRiskAssessmentRequest = null;

            _multiHazardRiskAssessmentAttachmentService
                .Setup(x => x.AttachHazardsToRiskAssessment(It.IsAny<AttachHazardsToRiskAssessmentRequest>()))
                .Callback<AttachHazardsToRiskAssessmentRequest>(parameter =>
                {
                    passedAttachHazardsToRiskAssessmentRequest = parameter;

                });

            //When
            target.Save(viewModel);

            //Then
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.CompanyId, Is.EqualTo(viewModel.CompanyId));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.RiskAssessmentId, Is.EqualTo(viewModel.RiskAssessmentId));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.UserId, Is.EqualTo(userId));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.Hazards[0].Id, Is.EqualTo(expectedAttachHazardsToRiskAssessmentHazardDetail.Id));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.Hazards[0].OrderNumber, Is.EqualTo(expectedAttachHazardsToRiskAssessmentHazardDetail.OrderNumber));
        }

        [Test]
        public void Given_valid_viewmodel_When_save_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            
            var viewModel = new SaveHazardsViewModel()
            {
                CompanyId = 500,
                RiskAssessmentId = 588,
                HazardIds = new[] { 1L },
                PeopleAtRiskIds = new[] { 2L }
            };

            //When
            var result = target.Save(viewModel) as RedirectToRouteResult;

            //Then
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["companyId"], Is.EqualTo(viewModel.CompanyId));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(viewModel.RiskAssessmentId));
        }

        private HazardsController GetTarget()
        {
            var result = new HazardsController(null,null, _multiHazardRiskAssessmentAttachmentService.Object,null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}