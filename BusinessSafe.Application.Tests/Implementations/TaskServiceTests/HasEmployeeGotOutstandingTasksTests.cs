using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class HasEmployeeGotOutstandingTasksTests 
    {
        private Mock<ITasksRepository> _tasksRepository;
        private Mock<IPeninsulaLog> _log;
        private Guid _employeeId;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _tasksRepository = new Mock<ITasksRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_HasEmployeeGotOutstandingTasks_Then_should_call_correct_methods()
        {
        
            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                  It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<long>(),
                                                  It.Is<int>(y => y == (decimal)TaskStatus.Outstanding),
                                                  It.Is<bool>(y => y == false),
                                                  It.Is<bool>(y => y == false),
                                                  It.IsAny<IEnumerable<long>>(),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, false))
                           .Returns(new List<Task>());


            //When
            target.HasEmployeeGotOutstandingTasks(_employeeId, _companyId);
            
            //Then
            _tasksRepository.VerifyAll();
        }


        [Test]
        public void Given_valid_request_not_got_any_tasks_When_HasEmployeeGotOutstandingTasks_Then_should_return_correct_result()
        {

            //Given
            var target = GetTarget();

            _tasksRepository.Setup(x => x.Search(It.IsAny<long>(),
                                                  It.IsAny<IEnumerable<Guid>>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<long>(),
                                                  It.IsAny<int>(),
                                                  It.IsAny<bool>(),
                                                  It.IsAny<bool>(),
                                                  It.IsAny<IEnumerable<long>>(),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, true))
                           .Returns(new List<Task>());


            //When
            var result = target.HasEmployeeGotOutstandingTasks(_employeeId, _companyId);

            //Then
            Assert.False(result);
        }

        [Test]
        [Ignore]
        public void Given_valid_request_got_tasks_When_HasEmployeeGotOutstandingTasks_Then_should_return_correct_result()
        {

            //Given
            var target = GetTarget();

            _tasksRepository.Setup(x => x.Search(It.IsAny<long>(),
                                                  It.IsAny<IEnumerable<Guid>>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<DateTime?>(),
                                                  It.IsAny<long>(),
                                                  It.IsAny<int>(),
                                                  It.IsAny<bool>(),
                                                  It.IsAny<bool>(),
                                                  It.IsAny<IEnumerable<long>>(),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, true))
                           .Returns(new List<Task>()
                                        {
                                            new MultiHazardRiskAssessmentFurtherControlMeasureTask()
                                        });


            //When
            var result = target.HasEmployeeGotOutstandingTasks(_employeeId, _companyId);

            //Then
            Assert.True(result);
        }
      
        private ITaskService GetTarget()
        {
            return new TaskService(
                null, 
                _log.Object, 
                _tasksRepository.Object, 
                null, 
                null, 
                null);
        }
    }
}
