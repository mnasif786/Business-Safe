using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;

using NUnit.Framework;
using Moq;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.AssessmentController
{
    [TestFixture]
    [Category("Unit")]
    public class SaveTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentService> _hazardousSubstanceRiskAssessmentService;
        private Mock<IAssessmentViewModelFactory> _assessmentViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _hazardousSubstanceRiskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _assessmentViewModelFactory = new Mock<IAssessmentViewModelFactory>();
        }

        [Test]
        public void Given_valid_data_is_entered_When_save_is_clicked_Then_correct_methods_are_called()
        {
            //Given
            long companyId = 1234L;
            long hazardousSubstanceRiskAssessmentId = 5678L;

            _hazardousSubstanceRiskAssessmentService
                .Setup(x => x.GetRiskAssessment(hazardousSubstanceRiskAssessmentId, companyId))
                .Returns(new HazardousSubstanceRiskAssessmentDto());

            var assessmentViewModel = new AssessmentViewModel();

            _assessmentViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(assessmentViewModel);

            var target = CreateController();

            //When
            target.Save(new AssessmentViewModel());

            // Then
            _hazardousSubstanceRiskAssessmentService.Verify(x => x.UpdateHazardousSubstanceRiskAssessmentAssessmentDetails(It.IsAny<UpdateHazardousSubstanceRiskAssessmentAssessmentDetailsRequest>()));
        }

        private WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.AssessmentController CreateController()
        {
            var result = new WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.AssessmentController(_assessmentViewModelFactory.Object, _hazardousSubstanceRiskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
