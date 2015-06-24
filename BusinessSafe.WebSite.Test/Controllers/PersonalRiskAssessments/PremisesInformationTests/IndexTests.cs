using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using NUnit.Framework;
using Moq;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using System.Web.Mvc;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.PremisesInformationTests
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private const long _companyId = 213L;
        private const long _riskAssessmentId = 24L;
        private PremisesInformationViewModel _viewModel;
        private Mock<IPremisesInformationViewModelFactory> _premisesInformationViewModelFactory;

        private PremisesInformationController _target;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _viewModel = new PremisesInformationViewModel();

            _premisesInformationViewModelFactory = new Mock<IPremisesInformationViewModelFactory>();

            _target = GetTarget();
            _currentUserId = _target.CurrentUser.UserId;

            _premisesInformationViewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_premisesInformationViewModelFactory.Object);

            _premisesInformationViewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_premisesInformationViewModelFactory.Object);

            _premisesInformationViewModelFactory
                .Setup(x => x.WithCurrentUserId(_currentUserId))
                .Returns(_premisesInformationViewModelFactory.Object);

            _premisesInformationViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_viewModel);
        }

        [Test]
        public void When_Index_called_Then_correct_methods_called()
        {
            // Arrange
            // Act
            _target.Index(_riskAssessmentId, _companyId);

            // Assert
            _premisesInformationViewModelFactory.Verify(x => x.WithRiskAssessmentId(_riskAssessmentId));
            _premisesInformationViewModelFactory.Verify(x => x.WithCompanyId(_companyId));
            _premisesInformationViewModelFactory.Verify(x => x.GetViewModel());
        }

        [Test]
        public void When_Index_called_Then_view_model_is_of_correct_type()
        {
            // Arrange

            // Act
            var result = _target.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.InstanceOf<PremisesInformationViewModel>());
        }

        [Test]
        public void When_Index_called_Then_correct_view_is_returned()
        {
            // Arrange
            
            // Act
            var result = _target.Index(_riskAssessmentId, _companyId) as ViewResult;

            // Assert
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        private PremisesInformationController GetTarget()
        {
            var result = new PremisesInformationController(_premisesInformationViewModelFactory.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
