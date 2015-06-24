using System;
using System.Web.Mvc;

using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistManagerTests
{
    [TestFixture]
    public class ViewTests
    {
        private long _companyId;
        private long _personalRiskAssessmentId;
        private Mock<IChecklistManagerViewModelFactory> _viewModelFactory;
        private Mock<IEmployeeChecklistSummaryViewModelFactory> _employeeChecklistSummaryViewModelFactory;
        private ChecklistManagerController _target;
        private ChecklistManagerViewModel _returnedViewModel;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _companyId = 123L;
            _personalRiskAssessmentId = 888L;

            _returnedViewModel = new ChecklistManagerViewModel()
            {
                PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.Generated
            };

            _viewModelFactory = new Mock<IChecklistManagerViewModelFactory>();
            _employeeChecklistSummaryViewModelFactory = new Mock<IEmployeeChecklistSummaryViewModelFactory>();
            
            _target = GetTarget();
            _currentUserId = _target.CurrentUser.UserId;

            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_personalRiskAssessmentId))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithCurrentUserId(_currentUserId))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory.Setup(x => x.GetViewModel()).Returns(_returnedViewModel);

            _employeeChecklistSummaryViewModelFactory
                .Setup(x => x.WithEmployeeChecklistId(It.IsAny<Guid>()))
                .Returns(_employeeChecklistSummaryViewModelFactory.Object);

            _employeeChecklistSummaryViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new EmployeeChecklistSummaryViewModel());
        }

        [Test]
        public void Correct_view_is_returned()
        {
            // Given

            // When
            var result = _target.View(_personalRiskAssessmentId, _companyId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Correct_view_model_is_returned()
        {
            // Given

            // When
            var result = _target.View(_personalRiskAssessmentId, _companyId) as ViewResult;
            var model = result.Model;

            // Then
            _viewModelFactory.VerifyAll();
            Assert.That(model, Is.EqualTo(_returnedViewModel));
        }

        [Test]
        public void IsReadOnlyProperty_is_set()
        {
            // Given

            // When
            var result = _target.View(_personalRiskAssessmentId, _companyId) as ViewResult;

            // Then
            _viewModelFactory.VerifyAll();
            Assert.That(result.ViewBag.IsReadOnly, Is.True);
        }

        private ChecklistManagerController GetTarget()
        {
            var controller = new ChecklistManagerController(_viewModelFactory.Object, _employeeChecklistSummaryViewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
