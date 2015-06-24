using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.TaskServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetOutstandingTaskSummaryTests
    {
        private Mock<ITasksRepository> _tasksRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<ISiteGroupRepository> _siteGroupRepository;
        private Guid _employeeId;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _tasksRepository = new Mock<ITasksRepository>();
            _siteGroupRepository = new Mock<ISiteGroupRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_getOustandingTasks_Then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding
            };

            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.IsAny<IEnumerable<long>>(),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, true))
                .Returns(new List<Task>());


            //When
            target.GetOutstandingTasksSummary(request);

            //Then
            _tasksRepository.VerifyAll();
        }

        [Test]
        public void Given_valid_request_When_getOustandingTasks_Then_should_calculate_pending_and_overdue_totals()
        {
            //Given
            var target = GetTarget();
            var today = DateTime.Today.Date;

            _employeeId = Guid.NewGuid();
            _companyId = 200;

            var request = new SearchTasksRequest()
            {
                CompanyId = _companyId,
                EmployeeIds = new List<Guid>() { _employeeId },
                TaskStatusId = (int)TaskStatus.Outstanding
            };

            _tasksRepository.Setup(x => x.Search(It.Is<long>(y => y == _companyId),
                                                 It.Is<IEnumerable<Guid>>(y => y.Contains(_employeeId)),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<DateTime?>(),
                                                 It.IsAny<long>(),
                                                 It.Is<int>(y => y == (int)TaskStatus.Outstanding),
                                                 It.Is<bool>(y => y == false),
                                                 It.Is<bool>(y => y == false),
                                                 It.IsAny<IEnumerable<long>>(),
                                                 It.IsAny<String>(), 0, 0, TaskOrderByColumn.None, true))
                .Returns(new List<Task>()
                             {
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today },
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today.AddDays(1) },
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today.AddDays(2) },
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today.AddDays(3) },
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today.AddDays(4) },
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today.AddDays(-1) },
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today.AddDays(-2) },
                                 new RiskAssessmentReviewTask() { TaskCompletionDueDate = today.AddDays(-3) }
                             });


            //When
            var result = target.GetOutstandingTasksSummary(request);

            //Then
            Assert.That(result.TotalPendingTasks, Is.EqualTo(5));
            Assert.That(result.TotalOverdueTasks, Is.EqualTo(3));
        }

        private ITaskService GetTarget()
        {
            return new TaskService(
                null, 
                _log.Object, 
                _tasksRepository.Object, 
                null, 
                null, 
                _siteGroupRepository.Object);
        }    
    }
}