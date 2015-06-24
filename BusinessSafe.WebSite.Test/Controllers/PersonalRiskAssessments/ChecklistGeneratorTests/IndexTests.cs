using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

using Moq;

using NServiceBus;

using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap.Query;
using BusinessSafe.Application.Contracts.Sites;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistGeneratorTests
{
    [TestFixture]
    public class IndexTests
    {
        private IEmployeeChecklistGeneratorViewModelFactory _checklistGeneratorViewModelFactory;
        private ISelectedEmployeeViewModelFactory _selectedEmployeeViewModelFactory;
        private Mock<IEmployeeService> _employeeService;
        private Mock<IEmployeeChecklistEmailService> _employeeChecklistEmailService;
        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;
        private Mock<IChecklistService> _checklistService;
        private Mock<ISiteService> _siteService;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        private long _companyId;
        private long _riskAssessmentId;
        private string _message;
        private PersonalRiskAssessmentDto _basePersonalRiskAssessment;
        private IList<EmployeeDto> _employees;
        private IList<ChecklistDto> _checklists;
        private ChecklistGeneratorController _target;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _employeeChecklistEmailService = new Mock<IEmployeeChecklistEmailService>();
            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            _checklistService = new Mock<IChecklistService>();
            _siteService = new Mock<ISiteService>();
            _businessSafeSessionManager=new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
            _selectedEmployeeViewModelFactory = new SelectedEmployeeViewModelFactory(_personalRiskAssessmentService.Object);
            _checklistGeneratorViewModelFactory = new EmployeeChecklistGeneratorViewModelFactory(_employeeService.Object, _checklistService.Object, _personalRiskAssessmentService.Object, _siteService.Object, _selectedEmployeeViewModelFactory);
            _companyId = 23522L;
            _riskAssessmentId = 342L;
            _message = "Test Message";

            _employees = new List<EmployeeDto>
                             {
                                 new EmployeeDto
                                     {
                                         Id = _currentUserId,
                                         FullName = "Mark Mauve",
                                         MainContactDetails = new EmployeeContactDetailDto{ Email = "mark.mauve@email.com"}
                                     },
                                 new EmployeeDto
                                     {
                                         Id = new Guid("cf7ce0b2-8a97-4c32-8af1-ce3c96716fdd"),
                                         FullName = "Peter Pink"
                                     },
                                 new EmployeeDto
                                     {
                                         Id = new Guid("0b20512d-3b4e-4da3-ab8c-6433c3fa4118"),
                                         FullName = "Guy Grey",
                                         MainContactDetails = new EmployeeContactDetailDto{ Email = "guy.grey@email.com"}
                                     }
                             };

            _checklists = new List<ChecklistDto>
                              {
                                  new ChecklistDto
                                      {
                                          Id = 1L,
                                          Title = "Test Checklist 01"
                                      },
                                  new ChecklistDto
                                      {
                                          Id = 2L,
                                          Title = "Test Checklist 02"
                                      },
                                  new ChecklistDto
                                      {
                                          Id = 3L,
                                          Title = "Test Checklist 03"
                                      },
                              };

            _basePersonalRiskAssessment = new PersonalRiskAssessmentDto
                                              {
                                                  Id = _riskAssessmentId,
                                                  ChecklistGeneratorEmployees = new EmployeeDto[0],
                                                  Checklists = new ChecklistDto[0],
                                                  ChecklistGeneratorMessage = _message
                                              };

            _target = GetTarget();
            _currentUserId = _target.CurrentUser.UserId;

            _employeeService
                .Setup(x => x.GetAll(_companyId))
                .Returns(_employees);

            _checklistService
                .Setup(x => x.GetByRiskAssessmentType(ChecklistRiskAssessmentType.Personal))
                .Returns(_checklists);

            _siteService
                .Setup(x => x.GetAll(_companyId))
                .Returns(new List<SiteDto>());
        }

        [Test]
        public void Correct_methods_are_called()
        {
            // Given
            _personalRiskAssessmentService
              .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
              .Returns(_basePersonalRiskAssessment);

            // When
            _target.Index(_riskAssessmentId, _companyId) ;
            
            // Then
            _personalRiskAssessmentService.Verify(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId));
            _checklistService.Verify(x => x.GetByRiskAssessmentType(ChecklistRiskAssessmentType.Personal));
            _employeeService.Verify(x => x.GetAll(_companyId));
        }

        [Test]
        public void Correct_IsForMultipleEmployees_is_returned_when_neither_single_or_multiple_employees()
        {
            // Given
            _personalRiskAssessmentService
              .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
              .Returns(_basePersonalRiskAssessment);

            // When
            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;


            // Then
            Assert.That(viewModel.IsForMultipleEmployees, Is.EqualTo(null));
            Assert.That(viewModel.SingleEmployeesSectionVisible, Is.False);
            Assert.That(viewModel.MultipleEmployeesSectionVisible, Is.False);
        }

        [Test]
        public void Correct_IsForMultipleEmployees_is_returned_for_single_employee()
        {
            _basePersonalRiskAssessment = new PersonalRiskAssessmentDto
            {
                Id = _riskAssessmentId,
                ChecklistGeneratorEmployees = new[] { _employees[0] },
                Checklists = new ChecklistDto[0]
            };

            _personalRiskAssessmentService
                .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(_basePersonalRiskAssessment);

            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            Assert.That(viewModel.IsForMultipleEmployees, Is.EqualTo("single"));
            Assert.That(viewModel.SingleEmployeesSectionVisible, Is.True);         //Could go in separate test, but do we really want to???
            Assert.That(viewModel.MultipleEmployeesSectionVisible, Is.False);         //Could go in separate test, but do we really want to???
        }

        [Test]
        public void Correct_IsForMultipleEmployees_is_returned_for_multiple_employees()
        {
            _basePersonalRiskAssessment = new PersonalRiskAssessmentDto
            {
                Id = _riskAssessmentId,
                ChecklistGeneratorEmployees = new[] { _employees[0], _employees[1] },
                Checklists = new ChecklistDto[0]
            };

            _personalRiskAssessmentService
                .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(_basePersonalRiskAssessment);

            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            Assert.That(viewModel.IsForMultipleEmployees, Is.EqualTo("multiple"));
            Assert.That(viewModel.SingleEmployeesSectionVisible, Is.False);         //Could go in separate test, but do we really want to???
            Assert.That(viewModel.MultipleEmployeesSectionVisible, Is.True);         //Could go in separate test, but do we really want to???
        }

        [Test]
        public void Existing_email_is_displayed_for_single_employee_with_email()
        {
            _basePersonalRiskAssessment = new PersonalRiskAssessmentDto
            {
                Id = _riskAssessmentId,
                ChecklistGeneratorEmployees = new[] { _employees[0] },
                Checklists = new ChecklistDto[0]
            };

            _personalRiskAssessmentService
                .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(_basePersonalRiskAssessment);

            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            Assert.That(viewModel.ExistingEmployeeEmailVisible, Is.True);
            Assert.That(viewModel.NewEmployeeEmailVisible, Is.False);
            Assert.That(viewModel.ExistingEmployeeEmail, Is.EqualTo(_employees[0].MainContactDetails.Email));
        }

        [Test]
        public void Existing_new_email_textbox_is_displayed_for_single_employee_without_email()
        {
            _basePersonalRiskAssessment = new PersonalRiskAssessmentDto
            {
                Id = _riskAssessmentId,
                ChecklistGeneratorEmployees = new[] { _employees[1] },
                Checklists = new ChecklistDto[0]
            };

            _personalRiskAssessmentService
                .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(_basePersonalRiskAssessment);

            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            Assert.That(viewModel.ExistingEmployeeEmailVisible, Is.False);
            Assert.That(viewModel.NewEmployeeEmailVisible, Is.True);
            Assert.That(viewModel.ExistingEmployeeEmail, Is.Null);
        }

        [Test]
        public void Correct_employees_are_displayed_in_drop_down_list_for_single_employee()
        {
            _basePersonalRiskAssessment = new PersonalRiskAssessmentDto
            {
                Id = _riskAssessmentId,
                ChecklistGeneratorEmployees = new[] { _employees[0] },
                Checklists = new ChecklistDto[0]
            };

            _personalRiskAssessmentService
                .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(_basePersonalRiskAssessment);

            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            var employees = viewModel.Employees.ToList();
            Assert.That(employees.Count(), Is.EqualTo(4));
            Assert.That(employees[0].label, Is.EqualTo("--Select Option--"));
            Assert.That(employees[0].value, Is.Empty);
            Assert.That(employees[1].label, Is.EqualTo("Mark Mauve (mark.mauve@email.com)"));
            Assert.That(employees[1].value, Is.EqualTo(_employees[0].Id.ToString()));
            Assert.That(employees[2].label, Is.EqualTo("Peter Pink"));
            Assert.That(employees[2].value, Is.EqualTo(_employees[1].Id.ToString()));
            Assert.That(employees[3].label, Is.EqualTo("Guy Grey (guy.grey@email.com)"));
            Assert.That(employees[3].value, Is.EqualTo(_employees[2].Id.ToString()));
        }

        [Test]
        public void Correct_checklists_are_displayed()
        {
            // Given
            _basePersonalRiskAssessment = new PersonalRiskAssessmentDto
            {
                Id = _riskAssessmentId,
                ChecklistGeneratorEmployees = new[] { _employees[0] },
                Checklists = new[] { _checklists[0], _checklists[2] }
            };

          _personalRiskAssessmentService
                .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(_basePersonalRiskAssessment);

            // When
            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            // Then
            Assert.That(viewModel.Checklists.Count, Is.EqualTo(3));
            Assert.That(viewModel.Checklists[0].Id, Is.EqualTo(1));
            Assert.That(viewModel.Checklists[0].Title, Is.EqualTo("Test Checklist 01"));
            Assert.That(viewModel.Checklists[0].ControlId, Is.EqualTo("IncludeChecklist_1"));
            Assert.That(viewModel.Checklists[0].Checked, Is.True);
            Assert.That(viewModel.Checklists[1].Id, Is.EqualTo(2));
            Assert.That(viewModel.Checklists[1].Title, Is.EqualTo("Test Checklist 02"));
            Assert.That(viewModel.Checklists[1].ControlId, Is.EqualTo("IncludeChecklist_2"));
            Assert.That(viewModel.Checklists[1].Checked, Is.False);
            Assert.That(viewModel.Checklists[2].Id, Is.EqualTo(3));
            Assert.That(viewModel.Checklists[2].Title, Is.EqualTo("Test Checklist 03"));
            Assert.That(viewModel.Checklists[2].ControlId, Is.EqualTo("IncludeChecklist_3"));
            Assert.That(viewModel.Checklists[2].Checked, Is.True);
            Assert.That(viewModel.EmployeeId, Is.EqualTo(_employees[0].Id));
        }

        [Test]
        public void Correct_message_is_displayed()
        {
            // Given
            _personalRiskAssessmentService
              .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
              .Returns(_basePersonalRiskAssessment);

            // When
            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            // Then
            Assert.That(viewModel.Message, Is.EqualTo(_message));
        }

        [Test]
        public void On_first_load_correct_risk_assessor_email_is_set()
        {
            // Given
            _personalRiskAssessmentService
              .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
              .Returns(_basePersonalRiskAssessment);

            // When
            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            // Then
            Assert.That(viewModel.CompletionNotificationEmailAddress, Is.EqualTo(TestControllerHelpers.EmailAssigned));
        }

        [Test]
        public void On_retrieval_of_generator_info_previously_saved_risk_assessor_email_is_set()
        {
            // Given
            var previouslySavedRiskAssessment = _basePersonalRiskAssessment;
            previouslySavedRiskAssessment.CompletionNotificationEmailAddress = "aDifferentEmailAddress";

            _personalRiskAssessmentService
                .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(previouslySavedRiskAssessment);

            // When
            var viewResult = _target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var viewModel = viewResult.Model as EmployeeChecklistGeneratorViewModel;

            // Then
            Assert.That(viewModel.CompletionNotificationEmailAddress, Is.Not.EqualTo(TestControllerHelpers.EmailAssigned));
            Assert.That(viewModel.CompletionNotificationEmailAddress, Is.EqualTo(previouslySavedRiskAssessment.CompletionNotificationEmailAddress));
        }

        private ChecklistGeneratorController GetTarget()
        {
            var controller = new ChecklistGeneratorController(
                _checklistGeneratorViewModelFactory,
                _employeeService.Object,
                _personalRiskAssessmentService.Object,
                _selectedEmployeeViewModelFactory,
                _bus.Object,
                _businessSafeSessionManager.Object);

            return TestControllerHelpers.AddUserToController(controller);
        }

       
    }
}
