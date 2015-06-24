using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Tests.Builder;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;

namespace BusinessSafe.WebSite.Tests.Controllers.PremisesInformation
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests : BasePremisesInformationControllerTests
    {
        [SetUp]
        public void Setup()
        {
            RiskAssessmentService.Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>())).Returns(new GeneralRiskAssessmentDto { CreatedOn = DateTime.Now });
        }
        
        [Test]
        public void Given_that_create_is_called_when_valid_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();

            var request = PremisesInformationViewModelBuilder
                               .Create()
                               .Build();
        
            //When
            target.Save(request);

            //Then
            MultiHazardRiskAssessmentSerivce.Verify(x => x.UpdateRiskAssessmentPremisesInformation(It.IsAny<SaveRiskAssessmentPremisesInformationRequest>()), Times.Once());
        }

        [Test]
        public void Given_that_create_is_called_when_not_valid_Then_should_call_correct_methods()
        {
            //Given
            MultiHazardRiskAssessmentSerivce.Setup(a => a.UpdateRiskAssessmentPremisesInformation(It.IsAny<SaveRiskAssessmentPremisesInformationRequest>())).Throws(new ValidationException(new List<ValidationFailure>()));
            
            var target = GetTarget();

            var request = PremisesInformationViewModelBuilder
                               .Create()
                               .Build();

            //When
            target.Save(request);

            //Then
            RiskAssessmentService.Verify(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>()), Times.Once());
        }


        [Test]
        public void Given_valid_request_Then_risk_assessment_is_saved_and_redirected_to_correct_view()
        {
            //Given
            var target = GetTarget();

            var request = PremisesInformationViewModelBuilder
                               .Create()
                               .WithRiskAssessmentId(300)
                               .WithCompanyId(400)
                               .Build();

            

            //When
            var result = target.Save(request) as RedirectToRouteResult;

            //Then
            Assert.That(target.TempData["Notice"], Is.Not.Null);
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["riskAssessmentId"], Is.EqualTo(request.RiskAssessmentId));
            Assert.That(result.RouteValues["CompanyId"], Is.EqualTo(request.CompanyId));

        }
    }
}
