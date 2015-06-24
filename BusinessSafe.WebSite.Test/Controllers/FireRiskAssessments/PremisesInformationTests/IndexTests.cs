using NUnit.Framework;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using Moq;
using System.Security.Principal;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using System.Web.Mvc;
namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.PremisesInformationTests
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private const long _companyId = 213L;
        private const long _riskAssessmentId = 24L;
        private PremisesInformationViewModel _viewModel;
        private Mock<IPremisesInformationViewModelFactory> _premisesInformationViewModelFactory;
        private Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _viewModel = new PremisesInformationViewModel();

            _premisesInformationViewModelFactory = new Mock<IPremisesInformationViewModelFactory>();

            _premisesInformationViewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_premisesInformationViewModelFactory.Object);

            _premisesInformationViewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_premisesInformationViewModelFactory.Object);

            _premisesInformationViewModelFactory
                .Setup(x => x.WithUser(It.IsAny<IPrincipal>()))
                .Returns(_premisesInformationViewModelFactory.Object);
            
            _premisesInformationViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_viewModel);

            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
        }

        [Test]
        public void When_Index_called_Then_correct_methods_called()
        {
            // Arrange
            var controller = GetTarget();

            // Act
            controller.Index(_riskAssessmentId, _companyId);

            // Assert
            _premisesInformationViewModelFactory.Verify(x => x.WithRiskAssessmentId(_riskAssessmentId));
            _premisesInformationViewModelFactory.Verify(x => x.WithCompanyId(_companyId));
            _premisesInformationViewModelFactory.Verify(x => x.WithUser(It.IsAny<IPrincipal>()));
            _premisesInformationViewModelFactory.Verify(x => x.GetViewModel());
        }

        [Test]
        public void When_Index_called_Then_view_model_is_of_correct_type()
        {
            // Arrange
            var controller = GetTarget();

            // Act
            var result = controller.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.InstanceOf<PremisesInformationViewModel>());
        }

        [Test]
        public void When_Index_called_Then_correct_view_is_returned()
        {
            // Arrange
            var controller = GetTarget();

            // Act
            var result = controller.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Assert
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        private PremisesInformationController GetTarget()
        {
            var result = new PremisesInformationController(_premisesInformationViewModelFactory.Object, _fireRiskAssessmentService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
