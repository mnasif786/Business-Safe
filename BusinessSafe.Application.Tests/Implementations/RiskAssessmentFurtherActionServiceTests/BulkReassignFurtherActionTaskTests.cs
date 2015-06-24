using System.Collections.Generic;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using NServiceBus;
using BusinessSafe.Domain.Entities;
using System;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentFurtherActionServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class BulkReassignFurtherActionTaskTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IBus> _bus;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ITasksRepository> _taskRepository;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<IPeninsulaLog>();
            _bus = new Mock<IBus>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();

            _employeeRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new Employee{Id = Guid.NewGuid()});

            _taskRepository = new Mock<ITasksRepository>();

            _taskRepository
                .Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(new MultiHazardRiskAssessmentFurtherControlMeasureTask());
        }

        [Test]
        [Ignore("Not working like this now")]
        public void Given_a_valid_request_When_Bulk_Reassign_Then_should_call_appropriate_methods_the_correct_amount_of_times()
        {
            // Given
            var target = CreateRiskAssessmentFurtherActionService();

            
            var reassignRequests = new List<ReassignTaskToEmployeeRequest>()
                                       {
                                            new ReassignTaskToEmployeeRequest(),
                                            new ReassignTaskToEmployeeRequest(),
                                            new ReassignTaskToEmployeeRequest()
                                       };
            var request = new BulkReassignTasksToEmployeeRequest()
                              {
                                 ReassignRequests = reassignRequests
                              };

            
            // When
            target.BulkReassignTasks(request);

            // Then
            Assert.That(target.ReassignTimesCalled, Is.EqualTo(reassignRequests.Count));            
        }

        private MyTaskService CreateRiskAssessmentFurtherActionService()
        {
            var target = new MyTaskService(null, _log.Object, _taskRepository.Object, _userRepository.Object, _employeeRepository.Object, null, _bus.Object);
            return target;
        }
    }

    public class MyTaskService: TaskService
    {
        public int ReassignTimesCalled;


        public MyTaskService(ITaskCategoryRepository responsibilityTaskCategoryRepository, IPeninsulaLog log, ITasksRepository taskRepository, IUserForAuditingRepository userRepository, IEmployeeRepository employeeRepository, ISiteGroupRepository siteGroupRepository, IBus bus)
            : base(responsibilityTaskCategoryRepository, log, taskRepository, userRepository, employeeRepository, siteGroupRepository)
        {
        }

        public override void  ReassignTask(ReassignTaskToEmployeeRequest request)
        {
            ReassignTimesCalled++;
        }
    }
}