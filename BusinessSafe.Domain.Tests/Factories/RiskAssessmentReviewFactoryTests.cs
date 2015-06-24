using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.Factories;

namespace BusinessSafe.Domain.Tests.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class RiskAssessmentReviewFactoryTests
    {
        private Mock<ITaskCategoryRepository> _responsibilityTaskCategoryRepository;

        [SetUp]
        public void Setup()
        {
            _responsibilityTaskCategoryRepository = new Mock<ITaskCategoryRepository>();
        }

        [Test]
        public void Given_ra_is_general_When_RiskAssessmentReview_Create_is_called_Then_correct_methods_are_called_RiskAssessmentReview_is_correct_and_Task_is_also_created()
        {
            //Given
            const long companyId = 300L;
            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var user = new UserForAuditing
                           {
                               Id = Guid.NewGuid()
                           };
            var completionDueDate = DateTime.Now.AddDays(10);

            var employeeToAssignTo = Employee.Create(new AddUpdateEmployeeParameters
                                                    {
                                                        ClientId = companyId,
                                                        Forename = "Brian",
                                                        Surname = "Beige"
                                                    },
                                                    user);

            var riskAssessment = GeneralRiskAssessment.Create("Risk Assessment 01", "RA01", companyId, user);
            var taskCategory = TaskCategory.Create(3, "Test GRA Review");

            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetGeneralRiskAssessmentTaskCategory())
                .Returns(taskCategory);

            //When
            var riskAssessmentReview = RiskAssessmentReviewFactory.Create(
                riskAssessment,
                user,
                employeeToAssignTo,
                completionDueDate,
                _responsibilityTaskCategoryRepository.Object,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //Then
            _responsibilityTaskCategoryRepository.VerifyAll();
            Assert.That(riskAssessmentReview.RiskAssessment, Is.SameAs(riskAssessment));
            Assert.That(riskAssessmentReview.ReviewAssignedTo, Is.SameAs(employeeToAssignTo));
            Assert.That(riskAssessmentReview.CreatedBy, Is.SameAs(user));
            Assert.That(riskAssessmentReview.CompletionDueDate, Is.EqualTo((completionDueDate)));
            Assert.That(riskAssessment.Reviews.Count(), Is.EqualTo(1));
            Assert.That(riskAssessment.Reviews.Contains(riskAssessmentReview));
            var task = riskAssessmentReview.RiskAssessmentReviewTask;
            Assert.That(task, Is.Not.Null);
            Assert.That(task.Reference, Is.EqualTo(riskAssessment.Reference));
            Assert.That(task.Title, Is.EqualTo(riskAssessment.Title));
            Assert.That(task.Description, Is.EqualTo("GRA Review"));
            Assert.That(task.TaskCompletionDueDate, Is.EqualTo(completionDueDate));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Outstanding));
            Assert.That(task.TaskAssignedTo, Is.EqualTo(riskAssessmentReview.ReviewAssignedTo));
            Assert.That(task.TaskReoccurringType, Is.EqualTo(TaskReoccurringType.None));
            Assert.That(task.Category, Is.SameAs(taskCategory));
        }

        [Test]
        public void Given_ra_is_hazardous_substance_When_RiskAssessmentReview_Create_is_called_Then_correct_methods_are_called_RiskAssessmentReview_is_correct_and_Task_is_also_created()
        {
            //Given
            const long companyId = 300L;
            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var user = new UserForAuditing
                           {
                               Id = Guid.NewGuid()
                           };
            var completionDueDate = DateTime.Now.AddDays(10);

            var employeeToAssignTo = Employee.Create(new AddUpdateEmployeeParameters
                                                    {
                                                        ClientId = companyId,
                                                        Forename = "Brian",
                                                        Surname = "Beige"
                                                    },
                                                    user);

            var hazardousSubstance = new HazardousSubstance();
            var riskAssessment = HazardousSubstanceRiskAssessment.Create("Risk Assessment 02", "RA02", companyId, user, hazardousSubstance);
            var taskCategory = TaskCategory.Create(3, "Test GRA Review");

            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetHazardousSubstanceRiskAssessmentTaskCategory())
                .Returns(taskCategory);

            //When
            var riskAssessmentReview = RiskAssessmentReviewFactory.Create(
                riskAssessment,
                user,
                employeeToAssignTo,
                completionDueDate,
                _responsibilityTaskCategoryRepository.Object,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //Then
            _responsibilityTaskCategoryRepository.VerifyAll();
            Assert.That(riskAssessmentReview.RiskAssessment, Is.SameAs(riskAssessment));
            Assert.That(riskAssessmentReview.ReviewAssignedTo, Is.SameAs(employeeToAssignTo));
            Assert.That(riskAssessmentReview.CreatedBy, Is.SameAs(user));
            Assert.That(riskAssessmentReview.CompletionDueDate, Is.EqualTo((completionDueDate)));
            Assert.That(riskAssessment.Reviews.Count(), Is.EqualTo(1));
            Assert.That(riskAssessment.Reviews.Contains(riskAssessmentReview));
            var task = riskAssessmentReview.RiskAssessmentReviewTask;
            Assert.That(task, Is.Not.Null);
            Assert.That(task.Reference, Is.EqualTo(riskAssessment.Reference));
            Assert.That(task.Title, Is.EqualTo(riskAssessment.Title));
            Assert.That(task.Description, Is.EqualTo("HSRA Review"));
            Assert.That(task.TaskCompletionDueDate, Is.EqualTo(completionDueDate));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Outstanding));
            Assert.That(task.TaskAssignedTo, Is.EqualTo(riskAssessmentReview.ReviewAssignedTo));
            Assert.That(task.TaskReoccurringType, Is.EqualTo(TaskReoccurringType.None));
            Assert.That(task.Category, Is.SameAs(taskCategory));
        }

        [Test]
        public void Given_ra_is_personal_When_RiskAssessmentReview_Create_is_called_Then_correct_methods_are_called_RiskAssessmentReview_is_correct_and_Task_is_also_created()
        {
            //Given
            const long companyId = 300L;
            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var user = new UserForAuditing()
                           {
                               Id = Guid.NewGuid()
                           };
                
            var completionDueDate = DateTime.Now.AddDays(10);

            var employeeToAssignTo = Employee.Create(new AddUpdateEmployeeParameters
            {
                ClientId = companyId,
                Forename = "Brian",
                Surname = "Beige"
            },
                                                    user);

            var riskAssessment = PersonalRiskAssessment.Create("Risk Assessment 01", "PRA01", companyId, user);
            var taskCategory = TaskCategory.Create(5, "Test PRA Review");

            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetPersonalRiskAssessmentTaskCategory())
                .Returns(taskCategory);

            //When
            var riskAssessmentReview = RiskAssessmentReviewFactory.Create(
                riskAssessment,
                user,
                employeeToAssignTo,
                completionDueDate,
                _responsibilityTaskCategoryRepository.Object,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //Then
            _responsibilityTaskCategoryRepository.VerifyAll();
            Assert.That(riskAssessmentReview.RiskAssessment, Is.SameAs(riskAssessment));
            Assert.That(riskAssessmentReview.ReviewAssignedTo, Is.SameAs(employeeToAssignTo));
            Assert.That(riskAssessmentReview.CreatedBy, Is.SameAs(user));
            Assert.That(riskAssessmentReview.CompletionDueDate, Is.EqualTo((completionDueDate)));
            Assert.That(riskAssessment.Reviews.Count(), Is.EqualTo(1));
            Assert.That(riskAssessment.Reviews.Contains(riskAssessmentReview));
            var task = riskAssessmentReview.RiskAssessmentReviewTask;
            Assert.That(task, Is.Not.Null);
            Assert.That(task.Reference, Is.EqualTo(riskAssessment.Reference));
            Assert.That(task.Title, Is.EqualTo(riskAssessment.Title));
            Assert.That(task.Description, Is.EqualTo("PRA Review"));
            Assert.That(task.TaskCompletionDueDate, Is.EqualTo(completionDueDate));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Outstanding));
            Assert.That(task.TaskAssignedTo, Is.EqualTo(riskAssessmentReview.ReviewAssignedTo));
            Assert.That(task.TaskReoccurringType, Is.EqualTo(TaskReoccurringType.None));
            Assert.That(task.Category, Is.SameAs(taskCategory));
        }

        [Test]
        public void Given_ra_is_fire_When_RiskAssessmentReview_Create_is_called_Then_correct_methods_are_called_RiskAssessmentReview_is_correct_and_Task_is_also_created()
        {
            //Given
            const long companyId = 300L;
            var employeeContactDetail = new EmployeeContactDetail { Email = "gary@green.com" };
            var employee = new Employee { Forename = "Gary", Surname = "Green", ContactDetails = new List<EmployeeContactDetail> { employeeContactDetail } };
            var user = new UserForAuditing()
                           {
                               Id = Guid.NewGuid()
                           };
            var completionDueDate = DateTime.Now.AddDays(10);

            var employeeToAssignTo = Employee.Create(new AddUpdateEmployeeParameters
            {
                ClientId = companyId,
                Forename = "Brian",
                Surname = "Beige"
            },
                                                    user);

            var riskAssessment = FireRiskAssessment.Create("Risk Assessment 01", "FRA01", companyId, new Checklist(), user);
            var taskCategory = TaskCategory.Create(2, "Test FRA Review");

            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetFireRiskAssessmentTaskCategory())
                .Returns(taskCategory);

            //When
            var riskAssessmentReview = RiskAssessmentReviewFactory.Create(
                riskAssessment,
                user,
                employeeToAssignTo,
                completionDueDate,
                _responsibilityTaskCategoryRepository.Object,
                false,
                false,
                false,
                false,
                Guid.NewGuid());

            //Then
            _responsibilityTaskCategoryRepository.VerifyAll();
            Assert.That(riskAssessmentReview.RiskAssessment, Is.SameAs(riskAssessment));
            Assert.That(riskAssessmentReview.ReviewAssignedTo, Is.SameAs(employeeToAssignTo));
            Assert.That(riskAssessmentReview.CreatedBy, Is.SameAs(user));
            Assert.That(riskAssessmentReview.CompletionDueDate, Is.EqualTo((completionDueDate)));
            Assert.That(riskAssessment.Reviews.Count(), Is.EqualTo(1));
            Assert.That(riskAssessment.Reviews.Contains(riskAssessmentReview));
            var task = riskAssessmentReview.RiskAssessmentReviewTask;
            Assert.That(task, Is.Not.Null);
            Assert.That(task.Reference, Is.EqualTo(riskAssessment.Reference));
            Assert.That(task.Title, Is.EqualTo(riskAssessment.Title));
            Assert.That(task.Description, Is.EqualTo("FRA Review"));
            Assert.That(task.TaskCompletionDueDate, Is.EqualTo(completionDueDate));
            Assert.That(task.TaskStatus, Is.EqualTo(TaskStatus.Outstanding));
            Assert.That(task.TaskAssignedTo, Is.EqualTo(riskAssessmentReview.ReviewAssignedTo));
            Assert.That(task.TaskReoccurringType, Is.EqualTo(TaskReoccurringType.None));
            Assert.That(task.Category, Is.SameAs(taskCategory));
        }
    }
}
