using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.TaskList.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;

using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels
{
    [TestFixture]
    [Category("Unit")]
    public class TaskViewModelTests
    {
        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_no_longer_required_When_IsReassignEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
            var taskViewModel = new TaskViewModel()
                                    {
                                        TaskStatus = "No Longer Required"
                                    };

            //When
            var result = taskViewModel.IsReassignEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_deleted_When_IsReassignEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
            var taskViewModel = new TaskViewModel()
            {
                IsDeleted = true
            };

            //When
            var result = taskViewModel.IsReassignEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_completed_When_IsReassignEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
            var taskViewModel = new TaskViewModel()
            {
                TaskStatus = "Completed"
            };

            //When
            var result = taskViewModel.IsReassignEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_outstanding_When_IsReassignEnabled_Is_Requested_Then_returns_true()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
            var taskViewModel = new TaskViewModel()
            {
                TaskStatus = "Outstanding"
            };

            //When
            var result = taskViewModel.IsReassignEnabled(user.Object);

            //Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_no_longer_required_When_IsCompleteEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskStatus = "No Longer Required"
            };

            //When
            var result = taskViewModel.IsCompleteEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_complete_When_IsCompleteEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskStatus = "Completed"
            };

            //When
            var result = taskViewModel.IsCompleteEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_deleted_When_IsCompleteEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                IsDeleted = true
            };

            //When
            var result = taskViewModel.IsCompleteEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_with_edit_gra_permissions_and_a_task_that_is_outstanding_When_IsCompleteEnabled_Is_Requested_Then_returns_true()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskStatus = "Outstanding"
            };

            //When
            var result = taskViewModel.IsCompleteEnabled(user.Object);

            //Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_a_user_without_permissions_When_IsDeleteEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);

            var taskViewModel = new TaskViewModel()
            {

            };

            //When
            var result = taskViewModel.IsDeleteEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_risk_assessment_review_task_When_IsDeleteEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel
            {
                TaskType = "RiskAssessmentReviewTaskDto"
            };

            //When
            var result = taskViewModel.IsDeleteEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_deleted_task_When_IsDeleteEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel
            {
                IsDeleted = true
            };

            //When
            var result = taskViewModel.IsDeleteEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_deleted_task_is_completed_When_IsDeleteEnabled_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel
            {
                TaskStatus = "Completed"
            };

            //When
            var result = taskViewModel.IsDeleteEnabled(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_risk_assessment_task_When_IsDeleteEnabled_Is_Requested_Then_returns_true()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {

            };

            //When
            var result = taskViewModel.IsDeleteEnabled(user.Object);

            //Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_a_task_dto_When_Create_From_is_requested_Then_all_fields_is_mapped()
        {
            // Arrange
            var taskDtos = new List<TaskDto>();
            var newTaskDto = new TaskDto()
            {
                Id = 123,
                Reference = "Reference",
                Title = "Title",
                Description = "Description",
                TaskAssignedTo = new EmployeeDto(),
                TaskCategory = new TaskCategoryDto() { Id = 123L, Category = "TestCategory" },
                Deleted = true,
                IsReoccurring = true,
                TaskStatusString = "Deleted",
                TaskReoccurringType = TaskReoccurringType.Weekly,
                TaskReoccurringEndDate = DateTime.Now,
                CreatedDate = "23/06/1980",
                TaskCompletionDueDate = "23/06/2012",
                Documents = new List<TaskDocumentDto>(),
                TaskCompletedBy = new UserEmployeeDto() { Forename = "Paul", Surname = "McGrath"},
                TaskCompletedDate = new DateTimeOffset(DateTime.Now),
                DerivedDisplayStatus = DerivedTaskStatusForDisplay.NoLongerRequired
            };
            taskDtos.Add(newTaskDto);
            // Act
            var result = TaskViewModel.CreateFrom(taskDtos).ToList();

            // Assert
            Assert.That(result.ElementAt(0), Is.Not.Null);
            Assert.That(result.ElementAt(0).Id, Is.EqualTo(newTaskDto.Id));
            Assert.That(result.ElementAt(0).Reference, Is.EqualTo(newTaskDto.Reference));
            Assert.That(result.ElementAt(0).Title, Is.EqualTo(newTaskDto.Title));
            Assert.That(result.ElementAt(0).Description, Is.EqualTo(newTaskDto.Description));
            Assert.That(result.ElementAt(0).TaskAssignedTo, Is.EqualTo(newTaskDto.TaskAssignedTo.FullName));
            Assert.That(result.ElementAt(0).IsDeleted, Is.EqualTo(newTaskDto.Deleted));
            Assert.That(result.ElementAt(0).IsReoccurring, Is.EqualTo(newTaskDto.IsReoccurring));
            Assert.That(result.ElementAt(0).TaskStatus, Is.EqualTo(newTaskDto.TaskStatusString));
            Assert.That(result.ElementAt(0).TaskReoccurringType, Is.EqualTo(newTaskDto.TaskReoccurringType));
            Assert.That(result.ElementAt(0).TaskReoccurringEndDate, Is.EqualTo(newTaskDto.TaskReoccurringEndDate));
            Assert.That(result.ElementAt(0).CreatedDate, Is.EqualTo(newTaskDto.CreatedDate));
            Assert.That(result.ElementAt(0).TaskCompletionDueDate, Is.EqualTo(newTaskDto.TaskCompletionDueDate));
            Assert.That(result.ElementAt(0).CompletedBy, Is.EqualTo(newTaskDto.TaskCompletedBy.FullName));
            Assert.That(result.ElementAt(0).CompletedOn, Is.EqualTo(newTaskDto.TaskCompletedDate.Value.ToLocalShortDateString()));
            Assert.That(result.ElementAt(0).DerivedDisplayStatus, Is.EqualTo(EnumHelper.GetEnumDescription(DerivedTaskStatusForDisplay.NoLongerRequired)));
            Assert.IsFalse(result.ElementAt(0).HasDocuments);
            Assert.IsFalse(result.ElementAt(0).HasCompletedDocuments);
        }

        [Test]
        [TestCase("General", "GeneralRiskAssessments")]
        [TestCase("Fire", "FireRiskAssessments")]
        [TestCase("Hazardous", "HazardousSubstanceRiskAssessments")]
        [TestCase("Personal", "PersonalRiskAssessments")]
        public void When_GetRiskAssessmentArea_Then_should_return_correct_result(string taskCategory, string expectedArea)
        {
            // Arrange
            var taskDtos = new List<TaskDto>();
            var newTaskDto = new TaskDto()
            {
                Id = 123,
                Reference = "Reference",
                Title = "Title",
                Description = "Description",
                TaskAssignedTo = new EmployeeDto(),
                TaskCategory = new TaskCategoryDto() { Id = 123L, Category = taskCategory },
                Deleted = true,
                IsReoccurring = true,
                TaskStatusString = "Deleted",
                TaskReoccurringType = TaskReoccurringType.Weekly,
                TaskReoccurringEndDate = DateTime.Now,
                CreatedDate = "23/06/1980",
                TaskCompletionDueDate = "23/06/2012",
                Documents = new List<TaskDocumentDto>()
            };
            taskDtos.Add(newTaskDto);

            var viewModel = TaskViewModel.CreateFrom(taskDtos).First();

            // Act
            var result = viewModel.GetRiskAssessmentArea();

            // Assert
            Assert.That(result, Is.EquivalentTo(expectedArea));
        }

        [Test]
        public void Given_task_not_a_review_When_IsPrintAvailabletask_Then_should_return_true()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskType = "RiskAssessmentTaskDto"
            };

            //When
            var result = taskViewModel.IsPrintAvailable();

            //Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_task_is_a_review_When_IsPrintAvailabletask_Then_should_return_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskType = "RiskAssessmentReviewTaskDto"
            };

            //When
            var result = taskViewModel.IsPrintAvailable();

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_task_not_a_review_When_IsReviewTask_Then_should_return_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskType = "RiskAssessmentTaskDto"
            };

            //When
            var result = taskViewModel.IsReviewTask();

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_task_is_a_review_When_IsReviewTask_Then_should_return_true()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskType = "RiskAssessmentReviewTaskDto"
            };

            //When
            var result = taskViewModel.IsReviewTask();

            //Then
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("General Risk Assessment", "gra")]
        [TestCase("Fire Risk Assessment", "fra")]
        [TestCase("Hazardous Substance Risk Assessment", "hsra")]
        [TestCase("Personal", "pra")]
        [TestCase("Responsibility", "responsibility")]
        [TestCase("Action", "action")]
        public void When_GetTaskType_Then_should_return_correct_result(string taskCategory, string expectedArea)
        {
            // Arrange
            var viewModel = new TaskViewModel
                                {
                                    TaskCategory = taskCategory
                                };

            // Act
            var result = viewModel.GetTaskType();

            // Assert
            Assert.That(result, Is.EquivalentTo(expectedArea));
        }
        
        [Test]
        public void Given_task_is_a_responsibility_When_IsResponsibilityTask_Then_should_return_true()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var taskViewModel = new TaskViewModel()
            {
                TaskType = "ResponsibilityTaskDto"
            };

            //When
            var result = taskViewModel.IsResponsibilityTask();

            //Then
            Assert.IsTrue(result);
        }

        //[Test]
        //[TestCase("General Risk Assessment", "gra")]
        //[TestCase("Fire Risk Assessment", "fra")]
        //[TestCase("Hazardous Substance Risk Assessment", "hsra")]
        //[TestCase("Personal", "pra")]
        //[TestCase("Responsibility", "responsibility")]
        //public void When_GetTaskType_Then_should_return_correct_result(string taskCategory, string expectedArea)
        //{
        //    // Arrange
        //    var viewModel = new TaskViewModel
        //                        {
        //                            TaskCategory = taskCategory
        //                        };

        //    // Act
        //    var result = viewModel.GetTaskType();

        //    // Assert
        //    Assert.That(result, Is.EquivalentTo(expectedArea));
        //}


        [Test]
        public void When_task_completed_Then_view_available_returns_true()
        {
            // Arrange
            var viewModel = new TaskViewModel
            {
                TaskStatus = "Completed"
            };

            // When
            var result = viewModel.IsViewAvailable();

            // Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_task_completed_Then_view_available_returns_false()
        {
            // Arrange
            var viewModel = new TaskViewModel
            {
                TaskStatus = "Outstanding"
            };

            // When
            var result = viewModel.IsViewAvailable();

            // Then
            Assert.That(result, Is.False);
        }

        [Test]
        public void When_task_deleted_and_outstanding_Then_view_available_returns_true()
        {
            // Arrange
            var viewModel = new TaskViewModel
            {
                TaskStatus = "Outstanding",
                IsDeleted = true
            };

            // When
            var result = viewModel.IsViewAvailable();

            // Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void When_task_deleted_and_completed_Then_view_available_returns_true()
        {
            // Arrange
            var viewModel = new TaskViewModel
            {
                TaskStatus = "Completed",
                IsDeleted = true
            };

            // When
            var result = viewModel.IsViewAvailable();

            // Then
            Assert.That(result, Is.True);
        }

        [Test]
        public void Given_a_user_without_edit_gra_permissions_and_a_task_category_is_general_When_HasPermissions_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);

            var taskViewModel = new TaskViewModel()
            {
                TaskCategory = "General",
                IsDeleted = true
            };

            //When
            var result = taskViewModel.HasPermission(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_without_edit_gra_permissions_and_a_task_category_is_fire_When_HasPermissions_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);

            var taskViewModel = new TaskViewModel()
            {
                TaskCategory = "Fire",
                IsDeleted = true
            };

            //When
            var result = taskViewModel.HasPermission(user.Object);

            //Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_a_user_without_edit_gra_permissions_and_a_task_category_is_personal_When_HasPermissions_Is_Requested_Then_returns_false()
        {
            //Given
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(false);

            var taskViewModel = new TaskViewModel()
            {
                TaskCategory = "Personal",
                IsDeleted = true
            };

            //When
            var result = taskViewModel.HasPermission(user.Object);

            //Then
            Assert.IsFalse(result);
        }
    }
}
