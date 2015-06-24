using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class TaskOverDueEscalationTests
    {
        private Mock<IGetOverDueTasksQuery> _query;
        private Mock<IOverdueTaskNotificationEmailSentCommand> _command;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _sessionManager;
        [SetUp]
        public void SetUp()
        {
            _query = new Mock<IGetOverDueTasksQuery>();
            _command = new Mock<IOverdueTaskNotificationEmailSentCommand>();
            _sessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_one_overdue_task_with_riskassessor_and_task_assigned_both_got_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateRiskAssessor(true);
            var taskAssignedTo = CreateTaskAssignedTo(true);
            var overDueTask = CreateOverDueTask(taskAssignedTo.Object.Employee, riskAssessor.Object);
            
            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueTask.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, 10, It.Is<DateTime>(y => y.Date == DateTime.Today)));
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueUserEmail>(y => y.RecipientEmail == taskAssignedTo.Object.Employee.GetEmail() &&
                                                                         y.TaskReference == overDueTask.Object.Reference &&
                                                                         y.Title == overDueTask.Object.Title &&
                                                                         y.Description == overDueTask.Object.Description &&
                                                                         y.RiskAssessor == riskAssessor.Object.Employee.FullName &&
                                                                         y.TaskCompletionDueDate == overDueTask.Object.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB"))
                                                                    )));
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueRiskAssessorEmail>(y => y.RecipientEmail == taskAssignedTo.Object.Employee.GetEmail() &&
                                                             y.TaskReference == overDueTask.Object.Reference &&
                                                             y.Title == overDueTask.Object.Title &&
                                                             y.Description == overDueTask.Object.Description &&
                                                             y.TaskAssignedTo == taskAssignedTo.Object.Employee.FullName
                                                        )));

        }

        [Test]
        public void Given_one_overdue_task_with_no_riskassessor_but_task_assigned_which_has_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            RiskAssessor riskAssessor = null;
            var taskAssignedTo = CreateTaskAssignedTo(true);
            var overDueTask = CreateOverDueTask(taskAssignedTo.Object.Employee, riskAssessor);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueTask.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, 10, It.Is<DateTime>(y => y.Date == DateTime.Today)));
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueUserEmail>(y => y.RecipientEmail == taskAssignedTo.Object.Employee.GetEmail() &&
                                                                         y.TaskReference == overDueTask.Object.Reference &&
                                                                         y.Title == overDueTask.Object.Title &&
                                                                         y.Description == overDueTask.Object.Description &&
                                                                         y.RiskAssessor == "Currently No Risk Assessor"
                                                                    )));
            _bus.Verify(x => x.Send(It.IsAny<SendTaskOverdueRiskAssessorEmail>()), Times.Never());

        }

        [Test]
        public void Given_one_overdue_task_with_no_riskassessor_and_task_assigned_has_no_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            RiskAssessor riskAssessor = null;
            var taskAssignedTo = CreateTaskAssignedTo(false);
            var overDueTask = CreateOverDueTask(taskAssignedTo.Object.Employee, riskAssessor);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueTask.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, It.IsAny<long>(),  It.IsAny<DateTime>()), Times.Never());
            _bus.Verify(x => x.Send(It.IsAny<SendTaskOverdueUserEmail>()), Times.Never());
            _bus.Verify(x => x.Send(It.IsAny<SendTaskOverdueRiskAssessorEmail>()), Times.Never());

        }

        [Test]
        public void Given_one_overdue_task_with_riskassessor_and_task_assigned_has_no_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateTaskAssignedTo(true);
            var taskAssignedTo = CreateTaskAssignedTo(false);
            var overDueTask = CreateOverDueTask(taskAssignedTo.Object.Employee, riskAssessor.Object);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueTask.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, It.IsAny<long>(), It.IsAny<DateTime>()), Times.Once());
            _bus.Verify(x => x.Send(It.IsAny<SendTaskOverdueUserEmail>()), Times.Never());
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueRiskAssessorEmail>(y => y.RecipientEmail == taskAssignedTo.Object.Employee.GetEmail() &&
                                                                                 y.TaskReference == overDueTask.Object.Reference &&
                                                                                 y.Title == overDueTask.Object.Title &&
                                                                                 y.Description == overDueTask.Object.Description &&
                                                                                 y.TaskAssignedTo == taskAssignedTo.Object.Employee.FullName
                                                                            )));

        }

        [Test]
        public void Given_five_overdue_task_with_riskassessor_and_task_assigned_both_got_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateRiskAssessor(true);
            var taskAssignedTo = CreateTaskAssignedTo(true);
            var overDueTask = CreateOverDueTask(taskAssignedTo.Object.Employee, riskAssessor.Object);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueTask.Object,
                                            overDueTask.Object,
                                            overDueTask.Object,
                                            overDueTask.Object,
                                            overDueTask.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, 10, It.Is<DateTime>(y => y.Date == DateTime.Today)));
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueUserEmail>(y => y.RecipientEmail == taskAssignedTo.Object.Employee.GetEmail() &&
                                                                         y.TaskReference == overDueTask.Object.Reference &&
                                                                         y.Title == overDueTask.Object.Title &&
                                                                         y.Description == overDueTask.Object.Description &&
                                                                         y.RiskAssessor == riskAssessor.Object.Employee.FullName
                                                                    )), Times.Exactly(5));
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueRiskAssessorEmail>(y => y.RecipientEmail == taskAssignedTo.Object.Employee.GetEmail() &&
                                                             y.TaskReference == overDueTask.Object.Reference &&
                                                             y.Title == overDueTask.Object.Title &&
                                                             y.Description == overDueTask.Object.Description &&
                                                             y.TaskAssignedTo == taskAssignedTo.Object.Employee.FullName
                                                        )), Times.Exactly(5));

        }


        [Test]
        public void Given_one_overdue_responsibility_task_with_owner_and_task_assigned_both_got_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var owner = CreateEmployee(true);
            var taskAssignedTo = CreateEmployee(true);
            var overDueTask = CreateOverResponsibilityDueTask(taskAssignedTo.Object, owner.Object);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueTask.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, 10, It.Is<DateTime>(y => y.Date == DateTime.Today)));
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueUserEmail>(y => y.RecipientEmail == taskAssignedTo.Object.GetEmail() &&
                                                                         y.TaskReference == overDueTask.Object.Reference &&
                                                                         y.Title == overDueTask.Object.Title &&
                                                                         y.Description == overDueTask.Object.Description &&
                                                                         y.RiskAssessor == owner.Object.FullName &&
                                                                         y.TaskCompletionDueDate == overDueTask.Object.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB"))
                                                                    )));
            _bus.Verify(x => x.Send(It.Is<SendTaskOverdueRiskAssessorEmail>(y => y.RecipientEmail == taskAssignedTo.Object.GetEmail() &&
                                                             y.TaskReference == overDueTask.Object.Reference &&
                                                             y.Title == overDueTask.Object.Title &&
                                                             y.Description == overDueTask.Object.Description &&
                                                             y.TaskAssignedTo == taskAssignedTo.Object.FullName
                                                        )));

        }

        
        private static Mock<Employee> CreateEmployee(bool hasEmail)
        {
            var employee = new Mock<Employee>();
            employee
                .Setup(x => x.GetEmail())
                .Returns("testing@hotmail.com");
            employee
                .Setup(x => x.HasEmail)
                .Returns(hasEmail);
            employee
                .Setup(x => x.FullName)
                .Returns("Barry Griffthes");

            return employee;
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

        private static Mock<RiskAssessor> CreateTaskAssignedTo(bool hasEmail)
        {
            var taskAssignedTo = new Mock<Employee>();
            taskAssignedTo
                .Setup(x => x.GetEmail())
                .Returns("testing@hotmail.com");
            taskAssignedTo
                .Setup(x => x.HasEmail)
                .Returns(hasEmail);

            var riskAssessor = new Mock<RiskAssessor>();
            riskAssessor.Setup(x => x.Employee)
                .Returns(taskAssignedTo.Object);
            return riskAssessor;
        }

        private static Mock<Task> CreateOverDueTask(Employee taskAssignedTo, RiskAssessor riskAssessor)
        {
            var overDueTask = new Mock<Task>();
            overDueTask
                .Setup(x => x.Id)
                .Returns(10);
            overDueTask
                .Setup(x => x.Reference)
                .Returns("Test Reference");
            overDueTask
               .Setup(x => x.SendTaskOverdueNotification)
               .Returns(true);
            overDueTask
                .Setup(x => x.Title)
                .Returns("Test Title");
            overDueTask
                .Setup(x => x.Description)
                .Returns("Test Description");
            overDueTask
                .Setup(x => x.RiskAssessment)
                .Returns(new GeneralRiskAssessment()
                             {
                                 RiskAssessor = riskAssessor
                             });
            overDueTask
                .Setup(x => x.TaskAssignedTo)
                .Returns(taskAssignedTo);
            overDueTask
                .Setup(x => x.TaskCompletionDueDate)
                .Returns(DateTime.Now);
            return overDueTask;
        }


        private static Mock<ResponsibilityTask> CreateOverResponsibilityDueTask(Employee taskAssignedTo, Employee responsibilityOwner)
        {
            var overDueTask = new Mock<ResponsibilityTask>();
            overDueTask
                .Setup(x => x.Id)
                .Returns(10);
            overDueTask
                .Setup(x => x.Reference)
                .Returns("Test Reference");
            overDueTask
               .Setup(x => x.SendTaskOverdueNotification)
               .Returns(true);
            overDueTask
                .Setup(x => x.Title)
                .Returns("Test Title");
            overDueTask
                .Setup(x => x.Description)
                .Returns("Test Description");
            overDueTask
                .Setup(x => x.Responsibility)
                .Returns(new Responsibility()
                {
                    Owner = responsibilityOwner
                });
            overDueTask
                .Setup(x => x.TaskAssignedTo)
                .Returns(taskAssignedTo);
            overDueTask
                .Setup(x => x.TaskCompletionDueDate)
                .Returns(DateTime.Now);
            return overDueTask;
        }

        private TaskOverDueEscalation CreateTaskOverDueEscalation()
        {
            var task = new TaskOverDueEscalation(_query.Object, _command.Object, _bus.Object, _sessionManager.Object);
            return task;
        }
    }
}