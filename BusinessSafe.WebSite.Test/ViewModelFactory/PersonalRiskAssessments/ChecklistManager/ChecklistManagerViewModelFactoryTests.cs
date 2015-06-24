using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.PersonalRiskAssessments.ChecklistManager
{
    [TestFixture]
    public class ChecklistManagerViewModelFactoryTests
    {
        private const long _companyId = 1;
        private const long _riskAssessmentId = 2;
        private List<EmployeeChecklistDto> _employeeChecklists;
        private EmployeeDto _employee1;
        private EmployeeDto _employee2;
        private ChecklistDto _checklist2;
        private ChecklistDto _checklist1;
        private PersonalRiskAssessmentDto _personalRiskAssessmentDto;
        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _currentUserId = Guid.NewGuid();

            _employee1 = new EmployeeDto()
                             {
                                 FullName = "Barry Griffthes",
                                 MainContactDetails = new EmployeeContactDetailDto { Email = "Test1@hotmail.com" }
                             };
            _checklist1 = new ChecklistDto()
            {
                Title = "Title 1"
            };
            var employeeChecklist1 = new EmployeeChecklistDto
                                     {
                                         Employee = _employee1,
                                         Checklist = _checklist1,
                                         CompletedDate = DateTime.Now,
                                         Id = Guid.NewGuid(),
                                         IsFurtherActionRequired = true
                                     };

            _employee2 = new EmployeeDto()
            {
                FullName = "Dave Smith",
                MainContactDetails = new EmployeeContactDetailDto { Email = "Test2@hotmail.com" }
            };
            _checklist2 = new ChecklistDto()
            {
                Title = "Title 2"
            };
            var employeeChecklist2 = new EmployeeChecklistDto
                                     {
                                         Employee = _employee2,
                                         Checklist = _checklist2,
                                         Id = Guid.NewGuid(),
                                     };

            _employeeChecklists = new List<EmployeeChecklistDto>
                                      {
                                          employeeChecklist1, 
                                          employeeChecklist2
                                      };

            _personalRiskAssessmentDto = new PersonalRiskAssessmentDto {EmployeeChecklists = _employeeChecklists, PersonalRiskAssessementEmployeeChecklistStatus =  PersonalRiskAssessementEmployeeChecklistStatusEnum.Generated};

            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            
        }

        
        [Test]
        public void When_GetViewModel_Then_should_call_correct_methods()
        {
            // Given
            var target = GetTarget();

            _personalRiskAssessmentService
                .Setup(x => x.GetWithEmployeeChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(() => _personalRiskAssessmentDto);

            // When
            target
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            // Then
            _personalRiskAssessmentService.VerifyAll();
        }
        [Test]
        public void When_GetViewModel_Then_ChecklistNameForDisplay_should_containt_correct_result()
        {
            // Given
            var target = GetTarget();

            _personalRiskAssessmentService
                .Setup(x => x.GetWithEmployeeChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(() => _personalRiskAssessmentDto);

            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            Assert.AreEqual(
                result.EmployeeChecklists.FirstOrDefault(x => x.ChecklistName == "Title 1").ChecklistNameForDisplay,
                "T1");

            Assert.AreEqual(
                result.EmployeeChecklists.FirstOrDefault(x => x.ChecklistName == "Title 2").ChecklistNameForDisplay,
                "T2");
        }

        [Test]
        public void When_GetViewModel_Then_should_call_correct_result()
        {
            // Given
            var target = GetTarget();

            _personalRiskAssessmentService
                .Setup(x => x.GetWithEmployeeChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(() => _personalRiskAssessmentDto);


            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(_riskAssessmentId));

            Assert.That(result.EmployeeChecklists.Count(), Is.EqualTo(_employeeChecklists.Count));

            Assert.That(result.EmployeeChecklists.First().Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.EmployeeChecklists.First().EmployeeId, Is.EqualTo(_employee1.Id));
            Assert.That(result.EmployeeChecklists.First().EmployeeEmail, Is.EqualTo(_employee1.MainContactDetails.Email));
            Assert.That(result.EmployeeChecklists.First().EmployeeName, Is.EqualTo(_employee1.FullName));
            Assert.That(result.EmployeeChecklists.First().ChecklistName, Is.EqualTo(_checklist1.Title));
            Assert.That(result.EmployeeChecklists.First().IsFurtherActionRequired, Is.EqualTo("Yes"));
            Assert.That(result.EmployeeChecklists.First().IsCompleted, Is.True);

            Assert.That(result.EmployeeChecklists.Last().Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.EmployeeChecklists.Last().EmployeeEmail, Is.EqualTo(_employee2.MainContactDetails.Email));
            Assert.That(result.EmployeeChecklists.Last().EmployeeName, Is.EqualTo(_employee2.FullName));
            Assert.That(result.EmployeeChecklists.Last().ChecklistName, Is.EqualTo(_checklist2.Title));
            Assert.That(result.EmployeeChecklists.Last().IsCompleted, Is.False);
            Assert.AreEqual(result.PersonalRiskAssessementEmployeeChecklistStatus, _personalRiskAssessmentDto.PersonalRiskAssessementEmployeeChecklistStatus);
        }

        [Test]
        public void When_GetViewModel_Checklist_has_not_been_completed_Then_should_further_action_required_is_empty()
        {
            // Given
            var target = GetTarget();

            _personalRiskAssessmentService
                .Setup(x => x.GetWithEmployeeChecklists(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(() => _personalRiskAssessmentDto);


            // When
            var result = target
                .WithCompanyId(_companyId)
                .WithCurrentUserId(_currentUserId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            // Then
            Assert.That(result.EmployeeChecklists.Last().IsFurtherActionRequired, Is.EqualTo(string.Empty));
        }

        private ChecklistManagerViewModelFactory GetTarget()
        {
            return new ChecklistManagerViewModelFactory(_personalRiskAssessmentService.Object);
        }
    }
}