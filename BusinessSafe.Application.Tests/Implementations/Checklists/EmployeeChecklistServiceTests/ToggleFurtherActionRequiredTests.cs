using System;

using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;


namespace BusinessSafe.Application.Tests.Implementations.Checklists.EmployeeChecklistServiceTests
{
    [TestFixture]
    public class ToggleFurtherActionRequiredTests
    {
        private EmployeeChecklistService _target;
        private Mock<IUserForAuditingRepository> _userRepo;
        private Mock<IEmployeeChecklistRepository> _employeeChecklistRepo;
        private Mock<IPeninsulaLog> _log;

        private Guid _employeeChecklistId;
        private EmployeeChecklist _employeeChecklist;
        private UserForAuditing _user;

        [SetUp]
        public void Setup()
        {
            _user = new UserForAuditing() { Id = Guid.NewGuid(), Employee = new EmployeeForAuditing() { Id = Guid.NewGuid() } };
            _userRepo = new Mock<IUserForAuditingRepository>();
            _userRepo.Setup(x => x.GetById(_user.Id))
                .Returns(() => _user);

            _employeeChecklistId = Guid.NewGuid();
            _employeeChecklist = new EmployeeChecklist()
                                 {
                                     Id = _employeeChecklistId
                                 };

            _employeeChecklistRepo = new Mock<IEmployeeChecklistRepository>();
            _employeeChecklistRepo
                .Setup(x => x.GetById(_employeeChecklistId))
                .Returns(() => _employeeChecklist);

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_employeeChecklistId_When_ToggleFurtherActionRequired_Then_retrieve_from_repo()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ToggleFurtherActionRequired(_employeeChecklistId, true, _user.Id);

            // Then
            _employeeChecklistRepo.Verify(x => x.GetById(_employeeChecklistId));
        }

        [Test]
        public void Given_employeeChecklistId_When_ToggleFurtherActionRequired_true_Then_IsFurtherActionRequired_is_true()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ToggleFurtherActionRequired(_employeeChecklistId, true, _user.Id);

            // Then
            Assert.IsTrue(_employeeChecklist.IsFurtherActionRequired.Value);
        }

        [Test]
        public void Given_employeeChecklistId_When_ToggleFurtherActionRequired_false_Then_IsFurtherActionRequired_is_false()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ToggleFurtherActionRequired(_employeeChecklistId, false, _user.Id);

            // Then
            Assert.IsFalse(_employeeChecklist.IsFurtherActionRequired.Value);
        }

        [Test]
        public void Given_employeeChecklistId_When_ToggleFurtherActionRequired_false_Then_AssessedByEmployee_is_set()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ToggleFurtherActionRequired(_employeeChecklistId, false, _user.Id);

            // Then
            Assert.IsNotNull(_employeeChecklist.AssessedByEmployee);
            Assert.AreEqual(_user.Employee.Id, _employeeChecklist.AssessedByEmployee.Id);


        }

        [Test]
        public void Given_employeeChecklistId_When_ToggleFurtherActionRequired_false_Then_AssessedByDate_is_set()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ToggleFurtherActionRequired(_employeeChecklistId, false, _user.Id);

            // Then
            Assert.IsNotNull(_employeeChecklist.AssessmentDate);
            Assert.AreEqual(DateTime.Now.Date, _employeeChecklist.AssessmentDate.Value.Date);


        }

        [Test]
        public void Given_employeeChecklistId_When_ToggleFurtherActionRequired_Then_save_to_repo()
        {
            // Given
            _target = GetTarget();

            // When
            _target.ToggleFurtherActionRequired(_employeeChecklistId, true, _user.Id);

            // Then
            _employeeChecklistRepo.Verify(x => x.SaveOrUpdate(_employeeChecklist));
        }

        private EmployeeChecklistService GetTarget()
        {
            return new EmployeeChecklistService(_userRepo.Object, _employeeChecklistRepo.Object, null, _log.Object, null);
        }
    }
}
