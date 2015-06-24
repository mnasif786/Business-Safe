using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.Factories;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentReviewTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateFollowUpTests
    {
        private Mock<ITaskCategoryRepository> _responsibilityTaskCategoryRepository;

        [SetUp]
        public void Setup()
        {
            _responsibilityTaskCategoryRepository = new Mock<ITaskCategoryRepository>();
        }

        [Test]
        public void Given_archive_is_false_When_complete_is_called_Then_follow_up_should_be_created()
        {
            //Given
            const long companyId = 300L;
            var completionDueDate = DateTime.Now.AddDays(10);
            var nextReviewDate = DateTime.Now.AddDays(30);
            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var user = User.CreateUser(Guid.NewGuid(), companyId, null, null, employee, null);
            var userForAuditing = new UserForAuditing()
                                      {
                                          Id = user.Id
                                      };
            var employeeToAssignTo = Employee.Create(new AddUpdateEmployeeParameters
                                                    {
                                                        ClientId = companyId,
                                                        Forename = "Brian",
                                                        Surname = "Beige"
                                                    },
                                                    userForAuditing);

            user.Employee = employeeToAssignTo;
            var riskAssessment = GeneralRiskAssessment.Create("Risk Assessment 01", "RA01", companyId, userForAuditing);
            var taskCategory = TaskCategory.Create(3, "Test GRA Review");

            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetGeneralRiskAssessmentTaskCategory())
                .Returns(taskCategory);

            var riskAssessmentReview = RiskAssessmentReviewFactory.Create(
                riskAssessment,
                userForAuditing,
                employeeToAssignTo,
                completionDueDate,
                _responsibilityTaskCategoryRepository.Object,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //When
            riskAssessmentReview.Complete("Test Completion", userForAuditing, nextReviewDate, false, new List<CreateDocumentParameters>(), user);

            //Then
            Assert.That(riskAssessment.Reviews.Count(), Is.EqualTo(2));
            var newReview = riskAssessment.Reviews[1];
            Assert.That(newReview.RiskAssessment, Is.SameAs(riskAssessment));
            Assert.That(newReview.ReviewAssignedTo, Is.SameAs(employeeToAssignTo));
            Assert.That(newReview.CreatedBy, Is.SameAs(userForAuditing));
            Assert.That(newReview.CompletionDueDate, Is.EqualTo((nextReviewDate)));
            var task = newReview.RiskAssessmentReviewTask;
            Assert.That(task, Is.Not.Null);
            Assert.That(task.Reference, Is.EqualTo(riskAssessment.Reference));
            Assert.That(task.Title, Is.EqualTo(riskAssessment.Title));
            Assert.That(task.Description, Is.EqualTo("GRA Review"));
            Assert.That(task.TaskCompletionDueDate, Is.EqualTo(nextReviewDate));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Outstanding));
            Assert.That(task.TaskAssignedTo, Is.EqualTo(riskAssessmentReview.ReviewAssignedTo));
            Assert.That(task.TaskReoccurringType, Is.EqualTo(TaskReoccurringType.None));
            Assert.That(task.Category, Is.SameAs(taskCategory));
        }
    }
}
