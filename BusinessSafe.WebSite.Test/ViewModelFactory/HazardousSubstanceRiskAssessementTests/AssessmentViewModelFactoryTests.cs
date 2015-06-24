using System;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using Moq;
using BusinessSafe.WebSite.Helpers;
using System.Web;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstanceRiskAssessementTests
{
    [TestFixture]
    public class AssessmentViewModelFactoryTests
    {
        private AssessmentViewModelFactory _target;
        private Mock<IHazardousSubstanceRiskAssessmentService> _hazardousSubstanceRiskAssessmentService;
        private Mock<IControlSystemService> _controlSystemService;
        private Mock<IVirtualPathUtilityWrapper> _virtualPathUtilityWrapper;

        [SetUp]
        public void Setup()
        {
            _hazardousSubstanceRiskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _controlSystemService = new Mock<IControlSystemService>();
            _virtualPathUtilityWrapper = new Mock<IVirtualPathUtilityWrapper>();
            _target = new AssessmentViewModelFactory(_hazardousSubstanceRiskAssessmentService.Object, _controlSystemService.Object, _virtualPathUtilityWrapper.Object);
        }

        [Test]
        public void When_get_view_model_is_called_Then_correct_methods_are_called()
        {
            //Given
            long companyId = 1234L;
            long hazardousSubstanceRiskAssessmentId = 5678L;

            var hazardousSubstanceRiskAssessmentDto = new HazardousSubstanceRiskAssessmentDto
                                                          {
                                                              Id = 99L,
                                                              MatterState = MatterState.Solid,
                                                              Quantity = Quantity.Medium,
                                                              DustinessOrVolatility = DustinessOrVolatility.Medium,
                                                              Group =
                                                                  new HazardousSubstanceGroupDto { Id = 1L, Code = "A" },
                                                              HazardousSubstance = new HazardousSubstanceDto(),
                                                              CreatedOn = DateTime.Now

                                                          };

            _hazardousSubstanceRiskAssessmentService
                .Setup(x => x.GetRiskAssessment(hazardousSubstanceRiskAssessmentId, companyId))
                .Returns(hazardousSubstanceRiskAssessmentDto);

            _controlSystemService
                .Setup(x => x.Calculate(
                    hazardousSubstanceRiskAssessmentDto.Group.Code,
                    hazardousSubstanceRiskAssessmentDto.MatterState,
                    hazardousSubstanceRiskAssessmentDto.Quantity,
                    hazardousSubstanceRiskAssessmentDto.DustinessOrVolatility))
                .Returns(new ControlSystemDto());

            _virtualPathUtilityWrapper
                .Setup(x => x.ToAbsolute(It.IsAny<string>()));

            // When
            _target
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(hazardousSubstanceRiskAssessmentId)
                .GetViewModel();

            // Then
            _hazardousSubstanceRiskAssessmentService.Verify(x => x.GetRiskAssessment(hazardousSubstanceRiskAssessmentId, companyId));
            _controlSystemService.VerifyAll();
            _virtualPathUtilityWrapper.VerifyAll();
        }

        [Test]
        public void When_get_view_model_is_called_Then_correct_view_model_is_returned()
        {
            //Given
            const long companyId = 1234L;
            const long hazardousSubstanceRiskAssessmentId = 5678L;

            var hazardousSubstanceRiskAssessmentDto = new HazardousSubstanceRiskAssessmentDto
            {
                Id = 99L,
                MatterState = MatterState.Solid,
                Quantity = Quantity.Medium,
                DustinessOrVolatility = DustinessOrVolatility.Medium,
                Group = new HazardousSubstanceGroupDto { Id = 1L, Code = "A" },
                HazardousSubstance = new HazardousSubstanceDto(),
                CreatedOn = DateTime.Now
            };

            _hazardousSubstanceRiskAssessmentService
                .Setup(x => x.GetRiskAssessment(hazardousSubstanceRiskAssessmentId, companyId))
                .Returns(hazardousSubstanceRiskAssessmentDto);

            var controlSystemDto = new ControlSystemDto
                                       {
                                           Id = 98L,
                                           Description = "Test Control System",
                                           DocumentLibraryId = 1020L
                                       };

            _controlSystemService
                .Setup(x => x.Calculate(
                    hazardousSubstanceRiskAssessmentDto.Group.Code,
                    hazardousSubstanceRiskAssessmentDto.MatterState,
                    hazardousSubstanceRiskAssessmentDto.Quantity,
                    hazardousSubstanceRiskAssessmentDto.DustinessOrVolatility))
                .Returns(controlSystemDto);

            var expectedUrlIn = "~/Documents/Document/DownloadPublicDocument?enc=" +
                              HttpUtility.UrlEncode(
                                  EncryptionHelper.Encrypt("documentLibraryId=" + controlSystemDto.DocumentLibraryId));

            var expectedUrlOut = "Test URL";

            _virtualPathUtilityWrapper
                .Setup(x => x.ToAbsolute(expectedUrlIn))
                .Returns(expectedUrlOut);

            // When
            var result = _target
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(hazardousSubstanceRiskAssessmentId)
                .GetViewModel();

            // Then
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.MatterState, result.MatterState);
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.Quantity, result.Quantity);
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.DustinessOrVolatility, result.DustinessOrVolatility);
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.Group.Code, result.HazardGroup);
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.HealthSurveillanceRequired, result.HealthSurveillanceRequired);
            Assert.AreEqual(controlSystemDto.Description, result.WorkApproach);
            Assert.AreEqual(expectedUrlOut, result.Url);
        }

        [Test]
        public void When_get_view_model_is_called_for_hsra_hazardous_substance_without_risk_phrases_Then_correct_view_model_is_returned()
        {
            //Given
            const long companyId = 1234L;
            const long hazardousSubstanceRiskAssessmentId = 5678L;

            var hazardousSubstanceRiskAssessmentDto = new HazardousSubstanceRiskAssessmentDto
            {
                Id = 99L,
                MatterState = MatterState.Solid,
                Quantity = Quantity.Medium,
                DustinessOrVolatility = DustinessOrVolatility.Medium,
                Group = null,
                HazardousSubstance = new HazardousSubstanceDto(),
                CreatedOn = DateTime.Now
            };

            _hazardousSubstanceRiskAssessmentService
                .Setup(x => x.GetRiskAssessment(hazardousSubstanceRiskAssessmentId, companyId))
                .Returns(hazardousSubstanceRiskAssessmentDto);

            // When
            var result = _target
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(hazardousSubstanceRiskAssessmentId)
                .GetViewModel();

            // Then
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.MatterState, result.MatterState);
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.Quantity, result.Quantity);
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.DustinessOrVolatility, result.DustinessOrVolatility);
            Assert.AreEqual(hazardousSubstanceRiskAssessmentDto.HealthSurveillanceRequired, result.HealthSurveillanceRequired);
            Assert.AreEqual("None", result.WorkApproach);
            Assert.AreEqual("", result.Url);
        }
    }
}
