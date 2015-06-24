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
    public class ReviewOverDueEscalationTests
    {
        private Mock<IGetOverDueReviewsQuery> _query;
        private Mock<IOverdueReviewNotificationEmailSentCommand> _command;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _sessionManager;
        [SetUp]
        public void SetUp()
        {
            _query = new Mock<IGetOverDueReviewsQuery>();
            _command = new Mock<IOverdueReviewNotificationEmailSentCommand>();
            _sessionManager = new Mock<IBusinessSafeSessionManager>();
            _bus = new Mock<IBus>();
        }

        [Test]
        public void Given_one_overdue_task_with_riskassessor_with_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateRiskAssessor(true);
            var taskAssignedTo = CreateTaskAssignedTo(true);
            var overDueTask = CreateOverDueReview(taskAssignedTo.Object, riskAssessor.Object);
            
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
            _bus.Verify(x => x.Send(It.Is<SendReviewOverdueRiskAssessorEmail>(y => y.RecipientEmail == taskAssignedTo.Object.GetEmail() &&
                                                                         y.TaskReference == overDueTask.Object.Reference &&
                                                                         y.Title == overDueTask.Object.Title &&
                                                                         y.Description == overDueTask.Object.Description &&
                                                                         y.TaskCompletionDueDate == overDueTask.Object.TaskCompletionDueDate.Value.ToString("d", new CultureInfo("en-GB"))
                                                                    )));

        }

        [Test]
        public void Given_one_overdue_task_with_riskassessor_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateRiskAssessor(true);
            var taskAssignedTo = CreateTaskAssignedTo(false);
            var overDueReview = CreateOverDueReview(taskAssignedTo.Object, riskAssessor.Object);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueReview.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, It.IsAny<long>(), It.IsAny<DateTime>()), Times.Once());
            _bus.Verify(x => x.Send(It.IsAny<SendTaskOverdueUserEmail>()), Times.Never());
            _bus.Verify(x => x.Send(It.Is<SendReviewOverdueRiskAssessorEmail>(y => y.RecipientEmail == taskAssignedTo.Object.GetEmail() &&
                                                                                 y.TaskReference == overDueReview.Object.Reference &&
                                                                                 y.Title == overDueReview.Object.Title &&
                                                                                 y.Description == overDueReview.Object.Description &&
                                                                                 y.TaskAssignedTo == taskAssignedTo.Object.FullName
                                                                            )));

        }

        [Test]
        public void Given_five_overdue_task_with_riskassessor_with_email_address_When_execute_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessor = CreateRiskAssessor(true);
            var taskAssignedTo = CreateTaskAssignedTo(true);
            var overDueReview = CreateOverDueReview(taskAssignedTo.Object, riskAssessor.Object);

            _query
                .Setup(x => x.Execute(_sessionManager.Object.Session))
                .Returns(new List<Task>()
                                        {
                                            overDueReview.Object,
                                            overDueReview.Object,
                                            overDueReview.Object,
                                            overDueReview.Object,
                                            overDueReview.Object
                                        });

            var task = CreateTaskOverDueEscalation();

            // When
            task.Execute();

            // Then 
            _query.VerifyAll();
            _command.Verify(x => x.Execute(_sessionManager.Object.Session, 10, It.Is<DateTime>(y => y.Date == DateTime.Today)));
            _bus.Verify(x => x.Send(It.Is<SendReviewOverdueRiskAssessorEmail>(y => 
                                                                         y.RecipientEmail == taskAssignedTo.Object.GetEmail() &&
                                                                         y.TaskReference == overDueReview.Object.Reference &&
                                                                         y.Title == overDueReview.Object.Title &&
                                                                         y.Description == overDueReview.Object.Description
                                                                    )), Times.Exactly(5));
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

        private static Mock<Task> CreateOverDueReview(Employee taskAssignedTo, RiskAssessor riskAssessor)
        {
            var review = new Mock<Task>();
            review
                .Setup(x => x.Id)
                .Returns(10);
            review
                .Setup(x => x.Reference)
                .Returns("Test Reference");
            review
               .Setup(x => x.SendTaskOverdueNotification)
               .Returns(true);
            review
                .Setup(x => x.Title)
                .Returns("Test Title");
            review
                .Setup(x => x.Description)
                .Returns("Test Description");
            review
                .Setup(x => x.RiskAssessment)
                .Returns(new GeneralRiskAssessment()
                             {
                                 RiskAssessor = riskAssessor
                             });
            review
                .Setup(x => x.TaskAssignedTo)
                .Returns(taskAssignedTo);
            review
                .Setup(x => x.TaskCompletionDueDate)
                .Returns(DateTime.Now);
            return review;
        }

        private ReviewOverDueEscalation CreateTaskOverDueEscalation()
        {
            var task = new ReviewOverDueEscalation(
                _query.Object,
                _command.Object,
                _bus.Object,
                _sessionManager.Object
                );
            return task;
        }
    }
}