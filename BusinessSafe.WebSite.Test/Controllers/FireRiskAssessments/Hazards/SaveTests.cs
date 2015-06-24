using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.Hazards
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests 
    {
        private Mock<IFireRiskAssessmentAttachmentService> _riskAssessmentAttachmentService;
        
        [SetUp]
        public void SetUp()
        {
            _riskAssessmentAttachmentService = new Mock<IFireRiskAssessmentAttachmentService>();
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
                                    PeopleAtRiskIds = new []{2L},
                                    SourceOfFuelsIds = new[]{3L}
                                };

            //When
            target.Save(viewModel);

            //Then
            _riskAssessmentAttachmentService.Verify(x => x.AttachPeopleAtRiskToRiskAssessment(It.Is<AttachPeopleAtRiskToRiskAssessmentRequest>(y => y.CompanyId == viewModel.CompanyId &&
                                                                                                                         y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                                                         y.PeopleAtRiskIds == viewModel.PeopleAtRiskIds &&
                                                                                                                         y.UserId == userId)));

            _riskAssessmentAttachmentService.Verify(x => x.AttachFireSafetyControlMeasuresToRiskAssessment(It.Is<AttachFireSafetyControlMeasuresToRiskAssessmentRequest>(y => y.CompanyId == viewModel.CompanyId &&
                                                                                                                         y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                                                         y.FireSafetyControlMeasureIds == viewModel.FireSafetyControlMeasureIds &&
                                                                                                                         y.UserId == userId)));

            _riskAssessmentAttachmentService.Verify(x => x.AttachSourcesOfFuelToRiskAssessment(It.Is<AttachSourceOfFuelToRiskAssessmentRequest>(y => y.CompanyId == viewModel.CompanyId &&
                                                                                                             y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                                             y.SourceOfFuelIds == viewModel.SourceOfFuelsIds &&
                                                                                                             y.UserId == userId)));

            _riskAssessmentAttachmentService.Verify(x => x.AttachSourcesOfIgnitionToRiskAssessment(It.Is<AttachSourceOfIgnitionToRiskAssessmentRequest>(y => y.CompanyId == viewModel.CompanyId &&
                                                                                                             y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                                             y.SourceOfIgnitionIds == viewModel.SourceOfIgnitionIds &&
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
            var result = target.Save(viewModel) as RedirectToRouteResult;

            //Then
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["companyId"], Is.EqualTo(viewModel.CompanyId));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(viewModel.RiskAssessmentId));
        }

        private HazardsController GetTarget()
        {
            var result = new HazardsController(null,null, _riskAssessmentAttachmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}