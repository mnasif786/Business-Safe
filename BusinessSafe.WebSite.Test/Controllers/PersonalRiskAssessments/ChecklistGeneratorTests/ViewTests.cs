using System;
using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

using Moq;

using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistGeneratorTests
{
    [TestFixture]
    public class ViewTests
    {
        private long _companyId;
        private long _personalRiskAssessmentId;
        private Mock<IEmployeeChecklistGeneratorViewModelFactory> _checklistGeneratorViewModelFactory;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private ChecklistGeneratorController _target;
        private EmployeeChecklistGeneratorViewModel _returnedViewModel;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _companyId = 123L;
            _personalRiskAssessmentId = 333333L;

            _returnedViewModel = new EmployeeChecklistGeneratorViewModel();
            _checklistGeneratorViewModelFactory = new Mock<IEmployeeChecklistGeneratorViewModelFactory>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();

            _target = GetTarget();
            _currentUserId = _target.CurrentUser.UserId;

            _checklistGeneratorViewModelFactory.Setup(x => x.WithCompanyId(_companyId)).Returns(_checklistGeneratorViewModelFactory.Object);
            _checklistGeneratorViewModelFactory.Setup(x => x.WithRiskAssessmentId(_personalRiskAssessmentId)).Returns(_checklistGeneratorViewModelFactory.Object);
            _checklistGeneratorViewModelFactory.Setup(x => x.WithRiskAssessorEmail(TestControllerHelpers.EmailAssigned)).Returns(_checklistGeneratorViewModelFactory.Object);
            _checklistGeneratorViewModelFactory.Setup(x => x.WithCurrentUserId(_currentUserId)).Returns(_checklistGeneratorViewModelFactory.Object);
            _checklistGeneratorViewModelFactory.Setup(x => x.GetViewModel()).Returns(_returnedViewModel);
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
            _checklistGeneratorViewModelFactory.VerifyAll();
            Assert.That(model, Is.EqualTo(_returnedViewModel));
        }

        [Test]
        public void IsReadOnlyProperty_is_set()
        {
            // Given

            // When
            var result = _target.View(_personalRiskAssessmentId, _companyId) as ViewResult;

            // Then
            _checklistGeneratorViewModelFactory.VerifyAll();
            Assert.That(result.ViewBag.IsReadOnly, Is.True);
        }

        private ChecklistGeneratorController GetTarget()
        {
            var controller = new ChecklistGeneratorController(
                _checklistGeneratorViewModelFactory.Object,
                null,
                null,
                null,
                null,
                _businessSafeSessionManager.Object
                );

            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
