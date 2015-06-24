using System;
using System.Collections.Generic;
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
    public class IndexTests
    {
        private Mock<IChecklistManagerViewModelFactory> _viewModelFactory;
        private const long RiskAssessmentId = 500;
        private const long CompanyId = 600;
        private Mock<IEmployeeChecklistSummaryViewModelFactory> _employeeChecklistSummaryViewModelFactory;
        private ChecklistManagerController _target;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _viewModelFactory = new Mock<IChecklistManagerViewModelFactory>();
            _employeeChecklistSummaryViewModelFactory = new Mock<IEmployeeChecklistSummaryViewModelFactory>();

            _target = GetTarget();
            _currentUserId = _target.CurrentUser.UserId;

            _viewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(RiskAssessmentId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithCurrentUserId(_currentUserId))
                .Returns(_viewModelFactory.Object);
        }

        [Test]
        public void When_get_index_Then_should_return_correct_view()
        {
            // Given
            var viewModel = new ChecklistManagerViewModel()
                            {
                                EmployeeChecklists = new List<EmployeeChecklistViewModel>()
                                                     {
                                                         new EmployeeChecklistViewModel()
                                                     },
                                PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.Generated
                            };

            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = _target.Index(RiskAssessmentId, CompanyId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void When_get_index_Then_should_return_correct_view_model()
        {
            // Given
            var viewModel = new ChecklistManagerViewModel()
                                {
                                    EmployeeChecklists = new List<EmployeeChecklistViewModel>()
                                                             {
                                                                 new EmployeeChecklistViewModel()
                                                             },
                                    PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.Generated
                                };
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = _target.Index(RiskAssessmentId, CompanyId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.TypeOf<ChecklistManagerViewModel>());
        }

        [Test] 
        public void When_get_index_Then_should_call_correct_methods()
        {
            // Given
            var viewModel = new ChecklistManagerViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            _target.Index(RiskAssessmentId, CompanyId);

            // Then
            _viewModelFactory.VerifyAll();
        }

        private ChecklistManagerController GetTarget()
        {
            var result = new ChecklistManagerController(_viewModelFactory.Object, _employeeChecklistSummaryViewModelFactory.Object);

            return TestControllerHelpers.AddUserToController(result);
        }  
    }
}
