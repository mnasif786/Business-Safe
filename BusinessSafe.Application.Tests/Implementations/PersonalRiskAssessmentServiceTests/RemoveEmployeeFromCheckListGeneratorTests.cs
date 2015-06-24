using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.PersonalRiskAssessmentServiceTests
{
    [TestFixture]
    public class RemoveEmployeeFromCheckListGeneratorTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IPersonalRiskAssessmentRepository> _personalRiskAssessmentRepo;

        [SetUp]
        public void Setp()
        {
            _log = new Mock<IPeninsulaLog>();
            _userRepo = new Mock<IUserForAuditingRepository>();
            _personalRiskAssessmentRepo = new Mock<IPersonalRiskAssessmentRepository>();
        }

        [Test]
        public void Given_employee_then_remove_employee_from_check_list__then_generator_calls_correct_methods()
        {
            //given
            
            var personalRiskAssessmentService = GetTarget();
            long companyId = 1234L;
            long riskAssessmentId = 1L;
            Guid employeeId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            _personalRiskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(riskAssessmentId, companyId, userId))
                .Returns(new PersonalRiskAssessment
                             {ChecklistGeneratorEmployees = new List<ChecklistGeneratorEmployee>()}).Verifiable();

            _userRepo
                .Setup(x => x.GetById(userId))
                .Returns(new UserForAuditing())
                .Verifiable();
            //when
            
            personalRiskAssessmentService.RemoveEmployeeFromCheckListGenerator(riskAssessmentId, companyId, employeeId, userId);
            
            //then
            _personalRiskAssessmentRepo.Verify();
            _userRepo.Verify();
        }


        [Test]
        public void Given_employee_when_remove_employee_from_check_list_then_generator_removes_checklist_from_list()
        {
            //given
            
            var personalRiskAssessmentService = GetTarget();
            long companyId = 1234L;
            long riskAssessmentId = 1L;
            Guid employeeId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            var riskAssessment = new PersonalRiskAssessment
                                     {
                                         Id = 1L,
                                         ChecklistGeneratorEmployees =
                                             new List<ChecklistGeneratorEmployee>
                                                 {
                                                     new ChecklistGeneratorEmployee{Employee = new Employee{Id = employeeId}},
                                                     new ChecklistGeneratorEmployee{Employee = new Employee{Id = Guid.NewGuid()}},
                                                 }
                                     };

            _personalRiskAssessmentRepo.
                Setup(x => x.GetByIdAndCompanyId(riskAssessmentId, companyId, userId))
                .Returns(riskAssessment);

            _userRepo.Setup(x => x.GetById(userId));

            //when

            personalRiskAssessmentService.RemoveEmployeeFromCheckListGenerator(riskAssessmentId, companyId, employeeId, userId);

            //then
            Assert.That(riskAssessment.ChecklistGeneratorEmployees.Where(x=>!x.Deleted).Count(),Is.EqualTo(1));
        }

        [Test]
        public void Given_employee_when_remove_employee_from_check_list__then_generator_removes_personal_risk_assessment_is_saved()
        {
            //given

            var personalRiskAssessmentService = GetTarget();
            long companyId = 1234L;
            long riskAssessmentId = 1L;
            Guid employeeId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            var riskAssessment = new PersonalRiskAssessment
            {
                Id = 1L,
                ChecklistGeneratorEmployees =
                    new List<ChecklistGeneratorEmployee>
                                                 {
                                                     new ChecklistGeneratorEmployee{Employee = new Employee{Id = employeeId}},
                                                     new ChecklistGeneratorEmployee{Employee = new Employee{Id = Guid.NewGuid()}},
                                                 }
            };

            _personalRiskAssessmentRepo.
                Setup(x => x.GetByIdAndCompanyId(riskAssessmentId, companyId, userId))
                .Returns(riskAssessment);

            _userRepo.Setup(x => x.GetById(userId));

            _personalRiskAssessmentRepo.Setup(x=>x.Update(riskAssessment)).Verifiable();

            //when
            personalRiskAssessmentService.RemoveEmployeeFromCheckListGenerator(riskAssessmentId, companyId, employeeId, userId);

            //then
            _personalRiskAssessmentRepo.Verify();
        }
        private PersonalRiskAssessmentService GetTarget()
        {
            return new PersonalRiskAssessmentService(_personalRiskAssessmentRepo.Object, _userRepo.Object
                                                     , null
                                                     , null
                                                     , _log.Object
                                                     , null
                                                     , null
                                                     , null, null);
        }

    }
}
