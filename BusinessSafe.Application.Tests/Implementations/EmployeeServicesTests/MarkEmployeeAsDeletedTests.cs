using System;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class MarkEmployeeAsDeletedTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<ITaskService> _taskService;
        private Mock<IBus> _bus;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
            _taskService = new Mock<ITaskService>();
            _bus = new Mock<IBus>();

        }

        [Test]
        public void Given_valid_request_When_mark_employee_as_deleted_Then_should_call_correct_methods()
        {

            // Given
            var request = new MarkEmployeeAsDeletedRequest
            {
                                  CompanyId = 1,
                                  EmployeeId = Guid.NewGuid(),
                                  UserId = Guid.NewGuid()
                              };


            var target = CreateEmployeeService();
            var employee = new Mock<Employee>();
            employee.Setup(x => x.User)
                .Returns(new User() {Id = Guid.NewGuid()});

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(employee.Object);


            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(new UserForAuditing { Id = request.UserId });

            _taskService
                .Setup(x => x.HasEmployeeGotOutstandingTasks(request.EmployeeId, request.CompanyId))
                .Returns(false);

            // When
            target.MarkEmployeeAsDeleted(request);

            // Then
            _employeeRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Employee>()));
            employee.Verify(x => x.MarkForDelete(It.Is<UserForAuditing>(y => y.Id == request.UserId)));
            _taskService.VerifyAll();
        }

        [Test]
        public void Given_valid_request_When_mark_employee_as_deleted_Then_should_send_command_to_PeninsulaOnline_to_delete_user()
        {

            // Given
            var request = new MarkEmployeeAsDeletedRequest
            {
                CompanyId = 1,
                EmployeeId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };


            var target = CreateEmployeeService();
            var employee = new Employee();
            employee.User = new User() {Id = Guid.NewGuid()};

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId))
                .Returns(() => employee);


            _userRepository.Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId)).Returns(new UserForAuditing { Id = request.UserId });

            _taskService
                .Setup(x => x.HasEmployeeGotOutstandingTasks(request.EmployeeId, request.CompanyId))
                .Returns(false);

            DeleteUser deleteUserCommand = null;
            _bus.Setup(x=>x.Send(It.IsAny<Object[]>()))
                .Callback<Object[]>(x => deleteUserCommand = (DeleteUser) x[0]);
                

            // When
            target.MarkEmployeeAsDeleted(request);

            var message = new Peninsula.Online.Messages.Commands.DeleteUser() {ActioningUserId = request.UserId, UserId = employee.User.Id};

            // Then
            _bus.Verify(x => x.Send(It.IsAny<DeleteUser>()),Times.Once());
            Assert.AreEqual(request.UserId,deleteUserCommand.ActioningUserId);
            Assert.AreEqual(employee.User.Id, deleteUserCommand.UserId);

        }

        [Test]
        public void Given_invalid_request_employee_has_outstanding_tasks_When_mark_employee_as_deleted_Then_should_throw_correct_exception()
        {

            // Given
            var request = new MarkEmployeeAsDeletedRequest
            {
                CompanyId = 1,
                EmployeeId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };


            var target = CreateEmployeeService();
            
            
            _taskService
                .Setup(x => x.HasEmployeeGotOutstandingTasks(request.EmployeeId, request.CompanyId))
                .Returns(true);

            // When
            // Then
            Assert.Throws<TryingToDeleteEmployeeWithOutstandingTasksException>(() => target.MarkEmployeeAsDeleted(request));

            
            
        }

        private EmployeeService CreateEmployeeService()
        {
            return new EmployeeService(_employeeRepository.Object, null, null, _userRepository.Object, null, _taskService.Object, _log.Object, _bus.Object, null, null);
        }
    }
}