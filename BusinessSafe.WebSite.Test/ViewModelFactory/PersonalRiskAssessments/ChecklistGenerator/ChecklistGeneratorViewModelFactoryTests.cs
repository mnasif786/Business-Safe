using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;

using Moq;

using NUnit.Framework;
using BusinessSafe.Application.Contracts.Sites;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.PersonalRiskAssessments.ChecklistGenerator
{
    [TestFixture]
    public class ChecklistGeneratorViewModelFactoryTests
    {
        private const long _companyId = 1;
        private const string _riskAssessorEmail = "riskAssessorEmail@pbstests.com";
        private ISelectedEmployeeViewModelFactory _selectedEmployeeViewModelFactory;
        private EmployeeChecklistGeneratorViewModelFactory _target;
        private Mock<IEmployeeService> _employeeService;
        private List<EmployeeDto> _employees;
        private Mock<IChecklistService> _checklistService;
        private IEnumerable<ChecklistDto> _checklists;
        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;
        private Mock<ISiteService> _siteService;
        private PersonalRiskAssessmentDto _personalRiskAssessmentDto;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _currentUserId = Guid.NewGuid();

            _employeeService = new Mock<IEmployeeService>();

            _employees = new List<EmployeeDto>
                             {
                                 new EmployeeDto
                                     {
                                         Id = Guid.NewGuid(),
                                         FullName = "percy purple",
                                         Surname = "Purple",
                                         MainContactDetails = new EmployeeContactDetailDto{ Email = "percy@test.com"}
                                     },
                                 new EmployeeDto
                                     {
                                         Id = Guid.NewGuid(),
                                         FullName = "rachel red",
                                         Surname = "Red"
                                     },
                                 new EmployeeDto
                                     {
                                         Id = Guid.NewGuid(),
                                         FullName = "Barry Brown",
                                         Surname = "brown"
                                     },
                                 new EmployeeDto
                                     {
                                         Id = Guid.NewGuid(),
                                         FullName = "cyril cyan",
                                         Surname = "cyan",
                                         MainContactDetails = new EmployeeContactDetailDto{ Email = "" }
                                     },
                                 new EmployeeDto
                                     {
                                         Id = Guid.NewGuid(),
                                         FullName = "Xavi Xander",
                                         Surname = "xander"
                                     },
                                 new EmployeeDto
                                     {
                                         Id = Guid.NewGuid(),
                                         FullName = "Armstrong Armstrong",
                                         Surname = "armstrong",
                                         MainContactDetails = new EmployeeContactDetailDto{ Email = "Armstrong@test.com"}
                                     }
                             };

            _employeeService.Setup(x => x.GetAll(_companyId)).
                Returns(_employees);

            _checklists = new List<ChecklistDto>()
                              {
                                  new ChecklistDto() {Id = 1, Title = "checklist 1"},
                                  new ChecklistDto() {Id = 2, Title = "checklist 2"},
                                  new ChecklistDto() {Id = 3, Title = "checklist 3"},
                                  new ChecklistDto() {Id = 4, Title = "checklist 4"}
                              };

            _checklistService = new Mock<IChecklistService>();
            _checklistService
                .Setup(x => x.GetByRiskAssessmentType(ChecklistRiskAssessmentType.Personal))
                .Returns(_checklists);

            _personalRiskAssessmentDto = new PersonalRiskAssessmentDto
                             {
                                 ChecklistGeneratorEmployees = _employees,
                                 Checklists = _checklists
                             };

            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();

            _personalRiskAssessmentService
               .Setup(x => x.GetWithChecklistGeneratorEmployeesAndChecklists(It.IsAny<long>(), It.IsAny<long>(), _currentUserId))
               .Returns(() => _personalRiskAssessmentDto);

            _siteService = new Mock<ISiteService>();

            _siteService
                .Setup(x => x.GetAll(_companyId))
                .Returns(new List<SiteDto>());

            _selectedEmployeeViewModelFactory = new SelectedEmployeeViewModelFactory(_personalRiskAssessmentService.Object);
        }

        [Test]
        public void RetrievesEmployeesForRequestedCompany()
        {
            // Given
            _target = GetTarget();

            // When
            _target
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            // Then
            _employeeService.Verify(x => x.GetAll(_companyId));
        }

        [Test]
        public void ReturnedViewModelHasCorrectEmployees()
        {
            _target = GetTarget();

            var viewModel = _target
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            var employees = viewModel.Employees.ToList();
            Assert.That(employees.Count(), Is.EqualTo(_employees.Count + 1));
            Assert.That(employees[1].label,
                        Is.EqualTo(string.Format("{0} ({1})", _employees[0].FullName, _employees[0].MainContactDetails.Email)));
            Assert.That(employees[1].value, Is.EqualTo(_employees[0].Id.ToString()));
            Assert.That(employees[2].label, Is.EqualTo(string.Format("{0}", _employees[1].FullName)));
            Assert.That(employees[2].value, Is.EqualTo(_employees[1].Id.ToString()));
        }

        [Test]
        public void ReturnedViewModelHasAvailableChecklists()
        {
            // Given
            _target = GetTarget();

            // When
            var viewModel = _target
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            // Then
            Assert.That(viewModel.Checklists.Count(), Is.EqualTo(_checklists.Count()));
            for (var i = 0; i < _checklists.Count(); i++)
            {
                Assert.That(viewModel.Checklists.ElementAt(i).Id, Is.EqualTo(_checklists.ElementAt(i).Id));
                Assert.That(viewModel.Checklists.ElementAt(i).Title, Is.EqualTo(_checklists.ElementAt(i).Title));
            }
        }

        [Test]
        public void When_new_checklist_generation_Then_accept_risk_assessor_email()
        {
            // Given
            _target = GetTarget();

            // When
            var viewModel = _target
                .WithRiskAssessorEmail(_riskAssessorEmail)
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            // Then
            Assert.That(viewModel.CompletionNotificationEmailAddress, Is.EqualTo(_riskAssessorEmail));
        }

        [Test]
        public void When_new_checklist_generation_Then_display_employees_without_email_first()
        {
            _employeeService.Setup(x => x.GetAll(_companyId)).
              Returns(_employees);
            
            // Given
            _target = GetTarget();

            // When
            var viewModel = _target
                .WithRiskAssessorEmail(_riskAssessorEmail)
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            var emails = viewModel.MultiSelectedEmployees.Select(x => x.Email).ToList();
            
            // Then
            Assert.That(emails.ElementAt(0), Is.Null);
            Assert.That(string.IsNullOrEmpty(emails.ElementAt(1)), Is.True);
            Assert.That(emails.ElementAt(2), Is.Null);
            Assert.That(emails.ElementAt(3), Is.Null);
            Assert.That(string.IsNullOrEmpty(emails.ElementAt(4)), Is.False);
            Assert.That(string.IsNullOrEmpty(emails.ElementAt(5)), Is.False);
        }

        [Test]
        public void When_new_checklist_generation_Then_display_employees_without_email_first_and_order_by_surname()
        {
            _employeeService.Setup(x => x.GetAll(_companyId)).
              Returns(_employees);

            // Given
            _target = GetTarget();

            // When
            var viewModel = _target
                .WithRiskAssessorEmail(_riskAssessorEmail)
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .GetViewModel();

            var employees = viewModel.MultiSelectedEmployees.Select(x => x.Name).ToList();

            // Then
            Assert.That(employees.ElementAt(0), Is.EqualTo("Barry Brown")); // NO EMAIL ADDRESS
            Assert.That(employees.ElementAt(1), Is.EqualTo("cyril cyan")); // EMPTY EMAIL ADDRESS
            Assert.That(employees.ElementAt(2), Is.EqualTo("rachel red"));  // NO EMAIL ADDRESS
            Assert.That(employees.ElementAt(3), Is.EqualTo("Xavi Xander")); // NO EMAIL ADDRESS
            Assert.That(employees.ElementAt(4), Is.EqualTo("Armstrong Armstrong")); // EMAIL ADDRESS
            Assert.That(employees.ElementAt(5), Is.EqualTo("percy purple")); // EMAIL ADDRESS
        }

        private EmployeeChecklistGeneratorViewModelFactory GetTarget()
        {
            return new EmployeeChecklistGeneratorViewModelFactory(
                _employeeService.Object, 
                _checklistService.Object, 
                _personalRiskAssessmentService.Object, 
                _siteService.Object,
                _selectedEmployeeViewModelFactory);
        }
    }
}