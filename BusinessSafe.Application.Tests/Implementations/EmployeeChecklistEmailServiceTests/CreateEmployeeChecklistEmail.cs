using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeChecklistEmailServiceTests
{
    [TestFixture]
    public class CreateEmployeeChecklistEmail
    {
        private Guid _employeeId;
        private Employee _employee;
        private Guid _userId;
        private UserForAuditing _user;
        private List<long> _checklistIds;
        private List<Checklist> _checklist;
        private Mock<IEmployeeChecklistEmailRepository> _employeeChecklistEmailRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IEmployeeChecklistRepository> _employeeChecklistRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IPersonalRiskAssessmentRepository> _riskAssessmentRepository;
        private PersonalRiskAssessment _personalRiskAssessment;

        [SetUp]
        public void Setup()
        {

            _employeeId = Guid.NewGuid();
            _employee = GetEmployee();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _employeeRepository
                .Setup(x => x.GetByIds(new[] { _employeeId }))
                .Returns(new[] { _employee });

            _employeeChecklistEmailRepository = new Mock<IEmployeeChecklistEmailRepository>();
            _employeeChecklistRepository = new Mock<IEmployeeChecklistRepository>();

            _checklistIds = GetChecklistIds();
            _checklist = GetChecklist();
            _checklistRepository = new Mock<IChecklistRepository>();
            _checklistRepository
                .Setup(x => x.GetByIds(_checklistIds))
                .Returns(_checklist);

            _userId = Guid.NewGuid();
            _user = GetUser();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _userRepository
                .Setup(x => x.GetById(_userId))
                .Returns(_user);

            

            _log = new Mock<IPeninsulaLog>();

            _personalRiskAssessment = new PersonalRiskAssessment();
            _riskAssessmentRepository = new Mock<IPersonalRiskAssessmentRepository>();
            _riskAssessmentRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(() => _personalRiskAssessment);

        }

        [Test]
        public void EmployeeChecklistEmail_calls_correct_methods()
        {
            // Given
            var target = GetTarget();
            var request = GetRequest();

            // When
            target.Generate(request);

            // Then
            _log.Verify(x => x.Add(request), Times.Once());
            _employeeRepository.Verify(x => x.GetByIds(new[] { _employeeId }), Times.Once());
            _checklistRepository.Verify(x => x.GetByIds(_checklistIds), Times.Once());
            _userRepository.Verify(x => x.GetById(_userId), Times.Once());
            _employeeChecklistEmailRepository.Verify(x => x.Save(It.IsAny<EmployeeChecklistEmail>()), Times.Once());
            _employeeChecklistEmailRepository.Verify(x => x.Flush(), Times.Once());

        }

        [Test]
        public void Given_a_generate_EmployeeChecklistEmails_request_when_generating_then_the_status_is_set_to_generated()
        {
            // Given
            var target = GetTarget();
            var request = GetRequest();
            _personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.Generating;

            _riskAssessmentRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(() => _personalRiskAssessment);

            // When
            target.Generate(request);

            // Then
            Assert.AreEqual(PersonalRiskAssessementEmployeeChecklistStatusEnum.Generated, _personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus);

        }

        [Test]
        [Ignore("Not directly saving the PRA anymore - letting it cascade when saving EmployeeChecklistEmail.")]
        public void Given_a_generate_EmployeeChecklistEmails_request_when_generating_then_the_PRA_is_saved()
        {
            // Given
            var target = GetTarget();
            var request = GetRequest();
            _personalRiskAssessment.PersonalRiskAssessementEmployeeChecklistStatus = PersonalRiskAssessementEmployeeChecklistStatusEnum.Generating;

            _riskAssessmentRepository.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(() => _personalRiskAssessment);

            // When
            target.Generate(request);

            // Then
            _riskAssessmentRepository.Verify(x => x.Save(_personalRiskAssessment));

        }


        private EmployeeChecklistEmailService GetTarget()
        {
            return new EmployeeChecklistEmailService(
                _employeeChecklistEmailRepository.Object,
                _employeeRepository.Object,
                _checklistRepository.Object,
                _userRepository.Object,
                _log.Object,
                _employeeChecklistRepository.Object,
                _riskAssessmentRepository.Object
                );
        }

        private GenerateEmployeeChecklistEmailRequest GetRequest()
        {
            return new GenerateEmployeeChecklistEmailRequest
            {
                ChecklistIds = _checklistIds,
                GeneratingUserId = _userId,
                Message = "some message",
                RequestEmployees = new List<EmployeeWithNewEmailRequest>
                {
                    new EmployeeWithNewEmailRequest
                    {
                        EmployeeId = _employee.Id
                    }
                }

            };
        }

        private Employee GetEmployee()
        {
            return new Employee
            {
                Id = _employeeId,
                ContactDetails = new List<EmployeeContactDetail>
                {
                    GetContactDetail()
                }
            };
        }

        private EmployeeContactDetail GetContactDetail()
        {
            return new EmployeeContactDetail
            {
                Email = "someone@microsoft.com"
            };
        }

        private UserForAuditing GetUser()
        {
            return new UserForAuditing
            {
                Id = _userId
            };
        }

        private List<long> GetChecklistIds()
        {
            return new List<long>
            {
                10,
                20,
                30
            };
        }

        private List<Checklist> GetChecklist()
        {
            return new List<Checklist>
            {
                new Checklist
                {
                    Id = 10
                },
                new Checklist
                {
                    Id = 20
                },
                new Checklist
                {
                    Id = 30
                }
            };
        }
    }
}
