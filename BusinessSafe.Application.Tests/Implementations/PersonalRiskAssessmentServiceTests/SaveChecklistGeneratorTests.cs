using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.PersonalRiskAssessmentServiceTests
{
    [TestFixture]
    public class SaveChecklistGeneratorTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IPersonalRiskAssessmentRepository> _personalRiskAssessmentRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _userRepo = new Mock<IUserForAuditingRepository>();
            _personalRiskAssessmentRepository = new Mock<IPersonalRiskAssessmentRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();
        }

        [Test]
        public void CallingSaveChecklistGeneratorCallsCorrectMethods()
        {
            var hasMultipleChecklistRecipients = false;
            var employeeId = Guid.NewGuid();
            var employeeEmail = "testemail@test.com";
            var personalRiskAssessmentId = 1728L;
            var currentUserId = Guid.NewGuid();

            var request = new SaveChecklistGeneratorRequest
                              {
                                  PersonalRiskAssessmentId = personalRiskAssessmentId,
                                  HasMultipleChecklistRecipients = hasMultipleChecklistRecipients,
                                  ChecklistIds = new List<long> {12, 45, 72},
                                  RequestEmployees = new List<EmployeeWithNewEmailRequest>
                                                  {
                                                      new EmployeeWithNewEmailRequest {EmployeeId = employeeId, NewEmail = employeeEmail }
                                                  },
                                  Message = "Test Message",
                                  CurrentUserId = currentUserId
                              };

            var checklistsToReturn = new List<Checklist>
                                         {
                                             new Checklist {Id = 12},
                                             new Checklist {Id = 45},
                                             new Checklist {Id = 72}
                                         };

            var employee = new Employee
                               {
                                   Id = employeeId,
                                   ContactDetails = new List<EmployeeContactDetail>
                                                        {
                                                            new EmployeeContactDetail {Email = employeeEmail}
                                                        }
                               };

            var currentUser = new UserForAuditing{ Id = currentUserId};
            var personalRiskAssessment = new Mock<PersonalRiskAssessment>();

            _checklistRepository
                .Setup(x => x.GetByIds(request.ChecklistIds))
                .Returns(checklistsToReturn);

            _employeeRepository
                .Setup(x => x.GetById(employeeId))
                .Returns(employee);

            _userRepo
                .Setup(x => x.GetById(currentUserId))
                .Returns(currentUser);

            _personalRiskAssessmentRepository
                .Setup(x => x.GetById(personalRiskAssessmentId))
                .Returns(personalRiskAssessment.Object);

            GetTarget().SaveChecklistGenerator(request);
            _checklistRepository.Verify(x => x.GetByIds(request.ChecklistIds));
            _employeeRepository.Verify(x => x.GetById(employeeId));
            _userRepo.Verify(x => x.GetById(currentUserId));
            _personalRiskAssessmentRepository.Verify(x => x.GetById(personalRiskAssessmentId));

            personalRiskAssessment.Verify(x => x.SaveChecklistGenerator(
                hasMultipleChecklistRecipients,
                It.Is<IList<EmployeesWithNewEmailsParameters>>(
                    y => y.Count() == 1 
                    && y[0].Employee == employee
                    && y[0].NewEmail == employeeEmail
                ),
                checklistsToReturn,
                request.Message,
                currentUser,
                null,
                null,
                null));
        }

        private PersonalRiskAssessmentService GetTarget()
        {
            return new PersonalRiskAssessmentService(
                _personalRiskAssessmentRepository.Object, 
                _userRepo.Object, 
                _employeeRepository.Object, 
                _checklistRepository.Object, 
                _log.Object, 
                null,
                null,
                null, null);
        }
    }
}
