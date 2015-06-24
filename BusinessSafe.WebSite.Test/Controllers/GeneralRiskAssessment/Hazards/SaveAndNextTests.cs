using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NHibernate.AdoNet;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.Hazards
{
    [TestFixture]
    [Category("Unit")]
    public class SaveAndNextTests
    {
        private Mock<IGeneralRiskAssessmentAttachmentService> _riskAssessmentAttachmentService;
        private Mock<IMultiHazardRiskAssessmentAttachmentService> _multiHazardRiskAssessmentAttachmentService;


        [SetUp]
        public void SetUp()
        {
            _riskAssessmentAttachmentService = new Mock<IGeneralRiskAssessmentAttachmentService>();
            _multiHazardRiskAssessmentAttachmentService = new Mock<IMultiHazardRiskAssessmentAttachmentService>();
        }

        [Test]
        public void Given_valid_viewmodel_When_SaveandNext_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();
            Guid userId = target.CurrentUser.UserId;

            var viewModel = new SaveHazardsViewModel()
            {
                CompanyId = 500,
                RiskAssessmentId = 588,
                HazardIds = new[] {1L},
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
            target.SaveAndNext(viewModel);

            //Then
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.CompanyId, Is.EqualTo(viewModel.CompanyId));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.RiskAssessmentId, Is.EqualTo(viewModel.RiskAssessmentId));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.UserId, Is.EqualTo(userId));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.Hazards[0].Id, Is.EqualTo(expectedAttachHazardsToRiskAssessmentHazardDetail.Id));
            Assert.That(passedAttachHazardsToRiskAssessmentRequest.Hazards[0].OrderNumber, Is.EqualTo(expectedAttachHazardsToRiskAssessmentHazardDetail.OrderNumber));

            _riskAssessmentAttachmentService
                .Verify(x => x.AttachPeopleAtRiskToRiskAssessment(It.Is<AttachPeopleAtRiskToRiskAssessmentRequest>(y =>
                    y.CompanyId == viewModel.CompanyId &&
                    y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                    y.PeopleAtRiskIds == viewModel.PeopleAtRiskIds &&
                    y.UserId == userId)));
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
            var result = target.SaveAndNext(viewModel) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = True }"));
            
        }

        private HazardsController GetTarget()
        {
            var result = new HazardsController(null, _riskAssessmentAttachmentService.Object, null, _multiHazardRiskAssessmentAttachmentService.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}