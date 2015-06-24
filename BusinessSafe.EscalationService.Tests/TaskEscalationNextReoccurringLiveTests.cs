using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.EscalateTasks;
using BusinessSafe.EscalationService.Queries;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.EscalationService.Tests
{
    [TestFixture]
    public class TaskEscalationNextReoccurringLiveTests
    {
        private Mock<IGetNextReoccurringTasksLiveQuery> _query;
        private Mock<INextReoccurringTaskNotificationEmailSentCommand> _command;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _sessionManager;
        [SetUp]
        public void SetUp()
        {
            _query = new Mock<IGetNextReoccurringTasksLiveQuery>();
            _command = new Mock<INextReoccurringTaskNotificationEmailSentCommand>();
            _bus = new Mock<IBus>();
            _sessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Given_one_next_reoccurring_task_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateRiskAssessor(true);
            var taskAssignedTo = CreateTaskAssignedTo(true);
            var reoccurringTask = CreateNewReoccurringNextLiveTask(taskAssignedTo.Object, riskAssessor.Object);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            reoccurringTask.Object
                                        });

            var task = CreateEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, 10, It.Is<DateTime>(y => y.Date == DateTime.Today)));
            _bus.Verify(x => x.Send(It.Is<SendNextReoccurringLiveTaskEmail>(y => y.RecipientEmail == taskAssignedTo.Object.GetEmail() &&
                                                                         y.TaskReference == reoccurringTask.Object.Reference &&
                                                                         y.Title == reoccurringTask.Object.Title &&
                                                                         y.Description == reoccurringTask.Object.Description &&
                                                                         y.RiskAssessor == riskAssessor.Object.Employee.FullName &&
                                                                         y.TaskCompletionDueDate == reoccurringTask.Object.TaskCompletionDueDate.Value.ToShortDateString()
                                                                    )));
            
        }

        [Test]
        public void Given_one_next_reoccurring_task_but_assigned_user_not_got_email_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateRiskAssessor(true);
            var taskAssignedTo = CreateTaskAssignedTo(false);
            var reoccurringTask = CreateNewReoccurringNextLiveTask(taskAssignedTo.Object, riskAssessor.Object);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            reoccurringTask.Object
                                        });

            var task = CreateEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, 10, It.Is<DateTime>(y => y.Date == DateTime.Today)), Times.Never());
            _bus.Verify(x => x.Send(It.IsAny<SendNextReoccurringLiveTaskEmail>()), Times.Never());

        }

        private static Mock<RiskAssessor> CreateRiskAssessor(bool hasEmail)
        {
            var riskAssessorEmployee = new Mock<Employee>();
            riskAssessorEmployee
                .Setup(x => x.GetEmail())
                .Returns("testing@hotmail.com");
            riskAssessorEmployee
                .Setup(x => x.HasEmail)
                .Returns(hasEmail);
            riskAssessorEmployee
                .Setup(x => x.FullName)
                .Returns("Barry Griffthes");

            var riskAssessor = new Mock<RiskAssessor>();
            riskAssessor.Setup(x => x.Employee)
                .Returns(riskAssessorEmployee.Object);
            return riskAssessor;
        }

        private static Mock<Employee> CreateTaskAssignedTo(bool hasEmail)
        {
            var taskAssignedTo = new Mock<Employee>();
            taskAssignedTo
                .Setup(x => x.GetEmail())
                .Returns("testing@hotmail.com");
            taskAssignedTo
                .Setup(x => x.HasEmail)
                .Returns(hasEmail);
            return taskAssignedTo;
        }

        private static Mock<Task> CreateNewReoccurringNextLiveTask(Employee taskAssignedTo, RiskAssessor riskAssessor)
        {
            var reoccurringNextLiveTask = new Mock<Task>();
            reoccurringNextLiveTask
                .Setup(x => x.Id)
                .Returns(10);
            reoccurringNextLiveTask
                .Setup(x => x.Reference)
                .Returns("Test Reference");
            reoccurringNextLiveTask
                .Setup(x => x.Title)
                .Returns("Test Title");
            reoccurringNextLiveTask
                .Setup(x => x.Description)
                .Returns("Test Description");
            reoccurringNextLiveTask
                .Setup(x => x.RiskAssessment)
                .Returns(new GeneralRiskAssessment()
                {
                    RiskAssessor = riskAssessor
                });
            reoccurringNextLiveTask
                .Setup(x => x.TaskCompletionDueDate)
                .Returns(DateTime.Now.AddDays(5));
            reoccurringNextLiveTask
                .Setup(x => x.TaskAssignedTo)
                .Returns(taskAssignedTo);
            return reoccurringNextLiveTask;
        }

        private NextReoccurringTaskLiveEscalation CreateEscalation()
        {
            var task = new NextReoccurringTaskLiveEscalation(_query.Object, _command.Object, _bus.Object, _sessionManager.Object);
            return task;
        }
    }
}