using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.Application.Tests.Implementations.PersonalRiskAssessmentServiceTests
{
    [TestFixture]
    public class CreateWithHazardsTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IPersonalRiskAssessmentRepository> _personalRiskAssessmentRepo;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;

        private PersonalRiskAssessment _returnedRiskAssessment;

        private long _companyId;
        private Guid _currentUserId;

        [SetUp]
        public void Setup()
        {
            _companyId = 5678;
            _currentUserId = Guid.NewGuid();

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _userRepo = new Mock<IUserForAuditingRepository>();
            _userRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserForAuditing());

            _personalRiskAssessmentRepo = new Mock<IPersonalRiskAssessmentRepository>();
            _personalRiskAssessmentRepo
                .Setup(x => x.Save(It.IsAny<PersonalRiskAssessment>()));

            _riskAssessmentRepo = new Mock<IRiskAssessmentRepository>();
            _riskAssessmentRepo
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<PersonalRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()));

            _returnedRiskAssessment = PersonalRiskAssessment.Create("my title", "my ref", _companyId, new UserForAuditing());
            _returnedRiskAssessment.CreatedBy = new UserForAuditing { Employee = new EmployeeForAuditing() };

            _returnedRiskAssessment.EmployeeChecklists = new List<EmployeeChecklist>()
                                                             {
                                                                 new EmployeeChecklist()
                                                                     {
                                                                         Id = Guid.NewGuid(), 
                                                                         Checklist = new Checklist()
                                                                                         {
                                                                                             Sections = new List<Section>(),
                                                                                         },  
                                                                         Employee = new Employee()
                                                                     }
                                                                 , new EmployeeChecklist()
                                                                       {
                                                                           Id = Guid.NewGuid(), 
                                                                         Checklist = new Checklist()
                                                                                         {
                                                                                             Sections = new List<Section>(),
                                                                                         }, 
                                                                         Employee = new Employee()
                                                                       }
                                                             };

            _personalRiskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>(), _currentUserId))
                .Returns(_returnedRiskAssessment);

            _employeeRepository = new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();
        }

        [Test]
        [TestCase(1234)]
        public void LoadRiskAssessment_from_repository(long riskAssessmentId)
        {
            // Given
            var target = GetTarget();

            // When
            target.GetRiskAssessmentWithHazards(riskAssessmentId, _companyId, _currentUserId);

            // Then
            _personalRiskAssessmentRepo.Verify(x => x.GetByIdAndCompanyId(riskAssessmentId, _companyId, _currentUserId), Times.Once());
        }

        [Test]
        [TestCase(1234)]
        public void RiskAssessmentDto_is_returned(long riskAssessmentId)
        {
            // Given

            _personalRiskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>(), _currentUserId))
                .Returns(_returnedRiskAssessment);
            var target = GetTarget();

            // When
            var result = target.GetRiskAssessmentWithHazards(riskAssessmentId, _companyId, _currentUserId);

            // Then
            Assert.That(result.Title, Is.EqualTo(_returnedRiskAssessment.Title));
            Assert.That(result.Reference, Is.EqualTo(_returnedRiskAssessment.Reference));
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
        }

        [Test]
        [TestCase(1234)]
        public void RiskAssessmentService_logs_exception(long riskAssessmentId)
        {
            // Given
            _personalRiskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>(), _currentUserId))
                .Throws<Exception>();

            var target = GetTarget();

            // When
            // Then
            Assert.Throws<Exception>(() => target.GetRiskAssessmentWithHazards(riskAssessmentId, _companyId, _currentUserId));
            
        }

        [Test]
        public void CreateFromWithHazards_converts_the_employee_checklists()
        {
            var result = PersonalRiskAssessmentDto.CreateFromWithHazards(_returnedRiskAssessment);
            Assert.AreEqual(_returnedRiskAssessment.EmployeeChecklists.Count, result.EmployeeChecklists.Count());
        }

        [Test]
        [Ignore("This is not what should happen, if it is not mapped from entity it should be null. PTD.")]
        public void Given_employeechecklists_isnull_CreateFromWithHazards_returns_an_empty_employee_checklist_dto()
        {
            _returnedRiskAssessment.EmployeeChecklists = null;
            var result = PersonalRiskAssessmentDto.CreateFromWithHazards(_returnedRiskAssessment);

            Assert.IsNotNull(result.EmployeeChecklists);
            Assert.AreEqual(0, result.EmployeeChecklists.Count());
        }

        private PersonalRiskAssessmentService GetTarget()
        {
            return new PersonalRiskAssessmentService(
                _personalRiskAssessmentRepo.Object, 
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
