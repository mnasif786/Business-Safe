using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.PersonalRiskAssessments.EmployeeChecklistSummaryTests
{
    [TestFixture]
    public class EmployeeChecklistSummaryViewModelFactoryTests
    {
        private EmployeeChecklistSummaryViewModelFactory _target;
        private Mock<IEmployeeChecklistService> _employeeChecklistService;
        private EmployeeChecklistDto _baseEmployeeChecklistDto;

        [SetUp]
        public void Setup()
        {
            _baseEmployeeChecklistDto = new EmployeeChecklistDto()
                                                 {
                                                     Checklist = new ChecklistDto { Title = "checklist 1" },
                                                     CompletionNotificationEmailAddress = "riskassessor email",
                                                     DueDateForCompletion = DateTime.Now.AddDays(10),
                                                     FriendlyReference = "chk_ref_1",
                                                     EmployeeChecklistEmails = new List<EmployeeChecklistEmailDto>
                                                                                   {
                                                                                       new EmployeeChecklistEmailDto
                                                                                           {
                                                                                               Message = "message body",
                                                                                               RecipientEmail = "recipient email",
                                                                                               
                                                                                           }
                                                                                   },
                                                     Employee = new EmployeeDto{ FullName = "recipient" },
                                                     IsFurtherActionRequired = true,
                                                     AssessedByEmployee = new EmployeeForAuditingDto() { Id = Guid.NewGuid(), FullName = "assessor" },
                                                     AssessmentDate = DateTime.Now
                                                 };

            _employeeChecklistService = new Mock<IEmployeeChecklistService>();
            _employeeChecklistService
                .Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(_baseEmployeeChecklistDto);
        }

        [Test]
        public void Retrieves_requested_EmployeeChecklist_from_service()
        {
            // Given
            var employeeChecklistId = Guid.NewGuid();

            _employeeChecklistService
                .Setup(x => x.GetWithCompletedOnEmployeesBehalfBy(employeeChecklistId))
                .Returns(_baseEmployeeChecklistDto);

            _target = GetTarget();

            // When
            _target
                .WithEmployeeChecklistId(employeeChecklistId)
                .GetViewModel();

            // Then
            _employeeChecklistService.Verify(x => x.GetWithCompletedOnEmployeesBehalfBy(employeeChecklistId));
        }

        [Test]
        public void Then_maps_assessment_of_checklist_fields()
        {
            // Given
            var employeeChecklistId = Guid.NewGuid();

            _employeeChecklistService
                .Setup(x => x.GetWithCompletedOnEmployeesBehalfBy(employeeChecklistId))
                .Returns(_baseEmployeeChecklistDto);

            _target = GetTarget();

            // When
            var result = _target
                .WithEmployeeChecklistId(employeeChecklistId)
                .GetViewModel();

            // Then
            Assert.That(result.IsFurtherActionRequired, Is.EqualTo(_baseEmployeeChecklistDto.IsFurtherActionRequired));
            Assert.That(result.AssessedBy, Is.EqualTo(_baseEmployeeChecklistDto.AssessedByEmployee.FullName));
            Assert.That(result.AssessmentDate, Is.EqualTo(_baseEmployeeChecklistDto.AssessmentDate.Value.ToShortDateString()));
        }

        private EmployeeChecklistSummaryViewModelFactory GetTarget()
        {
            return new EmployeeChecklistSummaryViewModelFactory(_employeeChecklistService.Object);
        }
    }
}
