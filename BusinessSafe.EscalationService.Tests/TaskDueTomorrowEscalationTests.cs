using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.EscalationService.Commands;
using BusinessSafe.EscalationService.EscalateTasks;
using BusinessSafe.EscalationService.Queries;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NHibernate;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap.Source;

namespace BusinessSafe.EscalationService.Tests
{
    [TestFixture]
    public class TaskDueTomorrowEscalationTests
    {
        private Mock<IGetTaskDueTomorrowQuery> _taskDueTomorrowQuery;
        private Mock<ITaskDueTomorrowEmailSentCommand> _taskDueTomorrowEmailSentCommand;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _sessionManager;

        private static long _mockTaskId = 10;

        [SetUp]
        public void Setup()
        {
            _taskDueTomorrowQuery = new Mock<IGetTaskDueTomorrowQuery>();
            _taskDueTomorrowEmailSentCommand = new Mock<ITaskDueTomorrowEmailSentCommand>();
            _bus = new Mock<IBus>();
            _sessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Given_one_next_reoccurring_task_When_execute_Then_should_call_correct_methods()
        {
            var riskAssessor = CreateRiskAssessor(true);
            var employee = CreateTaskAssignedTo(true);
            var dueTomorrowTasks = CreateNewTasks(employee.Object,riskAssessor.Object);

            _taskDueTomorrowQuery
              .Setup(x => x.Execute(_sessionManager.Object.Session))
              .Returns(new List<Task>()
                                        {
                                            dueTomorrowTasks.Object
                                        });

            var task = CreateEscalation();

            // When
            task.Execute();

            // Then 
            _taskDueTomorrowQuery.VerifyAll();
            _bus.Verify(x => x.Send(It.IsAny<SendTaskDueTomorrowEmail>()), Times.Once());

            _taskDueTomorrowEmailSentCommand.Verify(x => x.Execute(It.IsAny<ISession>(), It.Is<long>(id => id.Equals(_mockTaskId)), It.IsAny<DateTime>()), Times.Once());
        }



        [Test]
        public void Given_risk_assessor_without_email_address_When_execute_Then_email_should_not_be_sent()
        {
            var riskAssessor = CreateRiskAssessor(false);

            var employee = CreateTaskAssignedTo(true);
            var dueTomorrowTasks = CreateNewTasks(employee.Object, riskAssessor.Object);

            _taskDueTomorrowQuery
              .Setup(x => x.Execute(_sessionManager.Object.Session))
              .Returns(new List<Task>()
                                        {
                                            dueTomorrowTasks.Object
                                        });

            var task = CreateEscalation();

            // When
            task.Execute();

            // Then 
            _taskDueTomorrowQuery.VerifyAll();
            _bus.Verify(x => x.Send(It.IsAny<SendTaskDueTomorrowEmail>()), Times.Never());

            _taskDueTomorrowEmailSentCommand.Verify(x => x.Execute(It.IsAny<ISession>(), It.Is<long>(id => id.Equals(_mockTaskId)), It.IsAny<DateTime>()), Times.Never());
        }


        [Test]
        public void Given_action_task_assigned_to_without_email_address_When_execute_Then_email_should_not_be_sent()
        {
    

            var employee = CreateTaskAssignedTo(false);
            var dueTomorrowTasks = CreateNewActionTask(employee.Object);

            _taskDueTomorrowQuery
              .Setup(x => x.Execute(_sessionManager.Object.Session))
              .Returns(new List<Task>()
                                        {
                                            dueTomorrowTasks.Object
                                        });

            var task = CreateEscalation();

            // When
            task.Execute();

            // Then 
            _taskDueTomorrowQuery.VerifyAll();
            _bus.Verify(x => x.Send(It.IsAny<SendTaskDueTomorrowEmail>()), Times.Never());

            _taskDueTomorrowEmailSentCommand.Verify(x => x.Execute(It.IsAny<ISession>(), It.Is<long>(id => id.Equals(_mockTaskId)), It.IsAny<DateTime>()), Times.Never());
        }





        [Test]
        public void Given_risk_assessor_with_email_address_When_execute_Then_EscalationTaskDueTomorrow_should_be_updated()
        {
            var riskAssessor = CreateRiskAssessor(true);

            var employee = CreateTaskAssignedTo(true);
            var dueTomorrowTasks = CreateNewTasks(employee.Object, riskAssessor.Object);

            _taskDueTomorrowQuery
              .Setup(x => x.Execute(_sessionManager.Object.Session))
              .Returns(new List<Task>() { dueTomorrowTasks.Object });

            var task = CreateEscalation();

            // When
            task.Execute();

            // Then 
            _taskDueTomorrowQuery.VerifyAll();
            _bus.Verify(x => x.Send( It.IsAny<SendTaskDueTomorrowEmail>() ), Times.Once());
            _taskDueTomorrowEmailSentCommand.Verify(x => x.Execute(It.IsAny<ISession>(), It.Is<long>( id => id.Equals(_mockTaskId) ), It.IsAny<DateTime>()), Times.Once());
        }
        
        private static Mock<Task> CreateNewTasks(Employee taskAssignedTo, RiskAssessor riskAssessor)
        {
            var newTask = new Mock<Task>();
            newTask
                .Setup(x => x.Id)
                .Returns( _mockTaskId );
            newTask
                .Setup(x => x.Reference)
                .Returns("Test Reference");
            newTask
                .Setup(x => x.Title)
                .Returns("Test Title");
            newTask
                .Setup(x => x.Description)
                .Returns("Test Description");
            newTask
                .Setup(x => x.RiskAssessment)
                .Returns(new GeneralRiskAssessment()
                {
                    RiskAssessor = riskAssessor
                });
            newTask
                .Setup(x => x.TaskCompletionDueDate)
                .Returns(DateTime.Now.AddDays(1));
            newTask
                .Setup(x => x.TaskAssignedTo)
                .Returns(taskAssignedTo);
            return newTask;
        }

        private static Mock<ActionTask> CreateNewActionTask(Employee taskAssignedTo /*, RiskAssessor riskAssessor*/)
        {
            var newTask = new Mock<ActionTask>();
            newTask
                .Setup(x => x.Id)
                .Returns(_mockTaskId);
            newTask
                .Setup(x => x.Reference)
                .Returns("Test Reference");
            newTask
                .Setup(x => x.Title)
                .Returns("Test Title");
            newTask
                .Setup(x => x.Description)
                .Returns("Test Description");
           
            newTask
                .Setup(x => x.TaskCompletionDueDate)
                .Returns(DateTime.Now.AddDays(1));
            newTask
                .Setup(x => x.TaskAssignedTo)
                .Returns(taskAssignedTo);
            return newTask;
        }




        private static Mock<RiskAssessor> CreateRiskAssessor(bool hasEmail)
        {
            var riskAssessorEmployee = new Mock<Employee>();

            if (hasEmail)
            {
                riskAssessorEmployee
                    .Setup(x => x.GetEmail())
                    .Returns("testing@hotmail.com");
            }
            else
            {
                riskAssessorEmployee
                    .Setup(x => x.GetEmail())
                    .Returns("");
            }


            riskAssessorEmployee
                .Setup(x => x.HasEmail)
                .Returns(hasEmail);
            riskAssessorEmployee
                .Setup(x => x.FullName)
                .Returns("Barry Griffthes");

            var riskAssessor = new Mock<RiskAssessor>();

            riskAssessor.Object.DoNotSendReviewDueNotification = false;
            riskAssessorEmployee
                .Setup(x => x.RiskAssessor)
                .Returns(riskAssessor.Object);

            riskAssessor.Setup(x => x.Employee)
                .Returns(riskAssessorEmployee.Object);

            return riskAssessor;
        }     

        private static Mock<Employee> CreateTaskAssignedTo(bool hasEmail)
        {
            var taskAssignedTo = new Mock<Employee>();

            if (hasEmail)
            {
                taskAssignedTo
                    .Setup(x => x.GetEmail())
                    .Returns("testing@hotmail.com");
            }

            taskAssignedTo
                .Setup(x => x.HasEmail)
                .Returns(hasEmail);
            return taskAssignedTo;
        }
        
        private TaskDueTomorrowEscalation CreateEscalation()
        {
            var task = new TaskDueTomorrowEscalation(_bus.Object, _sessionManager.Object, _taskDueTomorrowQuery.Object, _taskDueTomorrowEmailSentCommand.Object);
            return task;
        }
    }
}
