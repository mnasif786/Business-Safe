using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeChecklistServiceTests
{
    [TestFixture]
    public class GetSummaryByEmployeeChecklistTests
    {
        private EmployeeChecklistService _target;

        private Mock<IEmployeeChecklistRepository> _employeeChecklistRepository;
        private Mock<IPeninsulaLog> _log;

        private readonly Guid _employeeChecklistId = Guid.NewGuid();

        private string _completionNotificationEmailAddress;
        private DateTime _dueDate;
        private string _recipientEmail;
        private string _recipientForename;
        private string _recipientSurname;
        private Checklist _checklist;
        private string _friendlyReference = string.Empty;
        private List<EmployeeChecklistEmail> _employeeChecklistEmails;

        [SetUp]
        public void Setup()
        {
            _employeeChecklistRepository = new Mock<IEmployeeChecklistRepository>();

            _completionNotificationEmailAddress = "email@example.com";
            _dueDate = DateTime.Today.AddDays(7);
            _recipientEmail = "bertie.basset@rowntree.co.uk";
            _recipientForename = "Bertie";
            _recipientSurname = " Basset";
            _checklist = new Checklist { Title = "My checklist", Sections = new List<Section>() };
            //_friendlyReference = "Basset0123";
            _employeeChecklistEmails = new List<EmployeeChecklistEmail>()
            {
                new EmployeeChecklistEmail() { Message = "ten days ago", CreatedOn = DateTime.Now.AddDays(-10)},
                new EmployeeChecklistEmail() { Message = "five days ago", CreatedOn = DateTime.Now.AddDays(-5)},
                new EmployeeChecklistEmail() { Message = "one day ago", CreatedOn = DateTime.Now.AddDays(-1)},
            };

            var employeeChecklist = GetEmployeeChecklist(
                _friendlyReference,
                _completionNotificationEmailAddress,
                _dueDate,
                _checklist,
                _recipientEmail,
                _recipientForename,
                _recipientSurname,
                _employeeChecklistEmails);

            _employeeChecklistRepository
                .Setup(x => x.GetById(_employeeChecklistId))
                .Returns(employeeChecklist);

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void GetSummaryByEmployeeChecklistId_calls_employee_checklist_repo()
        {
            // Given
            _target = GetTarget();

            // When
            _target.GetById(_employeeChecklistId);

            // Then
            _employeeChecklistRepository.Verify(x => x.GetById(_employeeChecklistId));
        }

        //TODO: PTD change this.
        private static EmployeeChecklist GetEmployeeChecklist(string friendlyReference, string completionNotificationEmailAddress, DateTime dueDate, Checklist checklist, string recipientEmail, string recipientForename, string recipientSurname, List<EmployeeChecklistEmail> employeeChecklistEmails)
        {
            var employee = new Employee
                           {
                               Forename = recipientForename,
                               Surname = recipientSurname,
                               ContactDetails = new List<EmployeeContactDetail>
                                                {
                                                    new EmployeeContactDetail
                                                    {
                                                        Email = recipientEmail
                                                    }
                                                }
                           };

            var employeeChecklist = new EmployeeChecklist()
            {
                CompletionNotificationEmailAddress = completionNotificationEmailAddress,
                DueDateForCompletion = dueDate,
                Checklist = checklist,
                Employee = employee,
                //FriendlyReference = friendlyReference,
                EmployeeChecklistEmails = employeeChecklistEmails,
                Answers = new List<PersonalAnswer>()
            };
            return employeeChecklist;
        }

        private EmployeeChecklistService GetTarget()
        {
            return new EmployeeChecklistService(
                null, // user repo
                _employeeChecklistRepository.Object, // employee checklist repo
                null, // quesiton repo
                _log.Object, null // log
            );
        }
    }
}
