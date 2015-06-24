using System;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentFurtherActionServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class ReassignFurtherActionTaskTests
    {
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<ITasksRepository> _tasksRepository;
        private UserForAuditing _user;

        [SetUp]
        public void SetUp()
        {
            _tasksRepository = new Mock<ITasksRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_a_valid_request_When_Reassign_Then_should_call_appropriate_methods()
        {
            // Given
            var target = CreateRiskAssessmentFurtherActionService();

            var request = new ReassignTaskToEmployeeRequest()
                              {
                                  CompanyId = 1,
                                  TaskId = 2,
                                  UserId = Guid.NewGuid(),
                                  ReassignTaskToId = Guid.NewGuid()
                              };


            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(_user);

            var reassigningTaskTo = new Employee();
            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.ReassignTaskToId, request.CompanyId))
                .Returns(reassigningTaskTo);

            var mockFurtherControlMeasureTask = new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();

            mockFurtherControlMeasureTask
                .Setup(x => x.TaskAssignedTo)
                .Returns(new Employee {Id = Guid.NewGuid()});

            _tasksRepository
                .Setup(x => x.GetById(request.TaskId))
                .Returns(mockFurtherControlMeasureTask.Object);
            // When
            target.ReassignTask(request);

            // Then
            _userRepository.VerifyAll();
            _employeeRepository.VerifyAll();
            _tasksRepository.VerifyAll();
            mockFurtherControlMeasureTask.Verify(x => x.ReassignTask(reassigningTaskTo, _user));

        }

        private TaskService CreateRiskAssessmentFurtherActionService()
        {
            var target = new TaskService(null, _log.Object, _tasksRepository.Object, _userRepository.Object, _employeeRepository.Object, null);
            return target;
        }
    }
}