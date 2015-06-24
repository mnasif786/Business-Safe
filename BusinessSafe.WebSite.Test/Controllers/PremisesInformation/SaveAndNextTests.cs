using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PremisesInformation
{
    [TestFixture]
    [Category("Unit")]
    public class SaveAndNextTests : BasePremisesInformationControllerTests
    {
        [SetUp]
        public void Setup()
        {
            RiskAssessmentService.Setup(x => x.GetRiskAssessment(It.IsAny<long>(), It.IsAny<long>())).Returns(new GeneralRiskAssessmentDto());
        }

        [Test]
        public void When_post_to_save_next_Then_should_return_correct_result()
        {
            //Given
            var target = GetTarget();
            var request = PremisesInformationViewModelBuilder
                .Create()
                .WithDateOfAssessment(null)
                .WithLocationAreaDepartment(string.Empty)
                .WithTaskProcess(string.Empty)
                .Build();

             //When
            var result = target.SaveAndNext(request) as JsonResult;

            //Then
            // Assert
            dynamic data = result.Data;
            Assert.That(data.ToString(), Contains.Substring("Success = True"));

        }

        [Test]
        public void Given_that_save_and_next_is_called_when_valid_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();

            var request = PremisesInformationViewModelBuilder
                .Create()
                .Build();

            //When
            target.SaveAndNext(request);

            //Then
            MultiHazardRiskAssessmentSerivce.Verify(x => x.UpdateRiskAssessmentPremisesInformation(It.IsAny<SaveRiskAssessmentPremisesInformationRequest>()), Times.Once());
        }

        
    }
}