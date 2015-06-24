using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Responsibilities
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteResponsibilityViewModelFactoryTests
    {
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;

        private Mock<IExistingDocumentsViewModelFactory> _existingDocumentsViewModelFactory;
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;

        private long _companyId;
        private long _responsibilityId;

        [SetUp]
        public void SetUp()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();
            _existingDocumentsViewModelFactory = new Mock<IExistingDocumentsViewModelFactory>();
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();
            _companyId = 250;
            _responsibilityId = 1;

            _employeeService.Setup(x => x.GetEmployeeNames(It.IsAny<long>()))
                .Returns(() => new List<EmployeeName>());
        }

        [Test]
        public void Given_search_for_current_user_When_GetViewModel_is_called_Then_returns_model()
        {
            //Given
            var taskId = 124124124L;
            var target = CreateTarget();

            var site = new SiteDto() {Id = 1L, Name = "the main site"};
            var taskDocument = new TaskDocumentDto()
                                   {Id = 12312, DocumentLibraryId = 13123, Description = "doc description", Filename = "the filename"};
            var responsibility = new ResponsibilityDto{Id = 1L, CompanyId = _companyId, Title = "Responsibilty Title",Description = "Responsibilty Description"};
            var responsibilityTask = new ResponsibilityTaskDto
                                       {
                                           Id = taskId,
                                           Title = "task title",
                                           Description = "task description",
                                           TaskAssignedTo = new EmployeeDto {FullName = "Test employee name", Id=Guid.NewGuid()},
                                           CreatedDate = DateTime.Now.ToShortDateString(),
                                           TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                                           TaskStatusString = string.Empty,
                                           Site = site,
                                           Responsibility = responsibility,
                                           TaskReoccurringType = TaskReoccurringType.Monthly,
                                           TaskReoccurringEndDate = DateTime.Now.AddDays(234),
                                           IsReoccurring = true,
                                           SendTaskCompletedNotification = true,
                                           SendTaskNotification = true,
                                           SendTaskOverdueNotification = true,
                                           Documents = new List<TaskDocumentDto>(){taskDocument}
                                       };

            _responsibilityTaskService
                .Setup(x=> x.GetByIdAndCompanyId(taskId, It.IsAny<long>()))
                .Returns(() => responsibilityTask );

            //When
            var result = target
                .WithCompanyId(_companyId)
                .WithResponsibilityTaskId(taskId)
                .GetViewModel();

            //Then
            Assert.That(result.CompanyId, Is.EqualTo(responsibility.CompanyId));
            Assert.That(result.ResponsibilityTaskId, Is.EqualTo(responsibilityTask.Id));
            Assert.That(result.ResponsibilitySummary.Id, Is.EqualTo(responsibility.Id));
            Assert.That(result.ResponsibilitySummary.Title, Is.EqualTo(responsibility.Title));
            Assert.That(result.ResponsibilitySummary.Description, Is.EqualTo(responsibility.Description));

            Assert.That(result.ResponsibilityTask.CompanyId, Is.EqualTo(responsibility.CompanyId));
            Assert.That(result.ResponsibilityTask.ResponsibilityTaskId, Is.EqualTo(responsibilityTask.Id));
            Assert.That(result.ResponsibilityTask.Title, Is.EqualTo(responsibilityTask.Title));
            Assert.That(result.ResponsibilityTask.Description, Is.EqualTo(responsibilityTask.Description));
            Assert.That(result.ResponsibilityTask.IsRecurring, Is.EqualTo(responsibilityTask.IsReoccurring));
            Assert.That(result.ResponsibilityTask.TaskReoccurringType, Is.EqualTo(responsibilityTask.TaskReoccurringType));
            Assert.That(result.ResponsibilityTask.TaskReoccurringTypeId, Is.EqualTo((int)responsibilityTask.TaskReoccurringType));
            Assert.That(result.ResponsibilityTask.ReoccurringStartDate, Is.EqualTo(responsibilityTask.TaskCompletionDueDate));
            Assert.That(result.ResponsibilityTask.ReoccurringEndDate, Is.EqualTo(responsibilityTask.TaskReoccurringEndDate));
            Assert.That(result.ResponsibilityTask.CompletionDueDate, Is.EqualTo(responsibilityTask.TaskCompletionDueDate));
            Assert.That(result.ResponsibilityTask.ResponsibilityTaskSite, Is.EqualTo(responsibilityTask.Site.Name));
            Assert.That(result.ResponsibilityTask.ResponsibilityTaskSiteId, Is.EqualTo(responsibilityTask.Site.Id));
            Assert.That(result.ResponsibilityTask.AssignedTo, Is.EqualTo(responsibilityTask.TaskAssignedTo.FullName));
            Assert.That(result.ResponsibilityTask.AssignedToId, Is.EqualTo(responsibilityTask.TaskAssignedTo.Id));
            Assert.That(result.ResponsibilityTask.DoNotSendTaskAssignedNotification, Is.EqualTo(!responsibilityTask.SendTaskNotification));
            Assert.That(result.ResponsibilityTask.DoNotSendTaskCompletedNotification, Is.EqualTo(!responsibilityTask.SendTaskCompletedNotification));
            Assert.That(result.ResponsibilityTask.DoNotSendTaskOverdueNotification, Is.EqualTo(!responsibilityTask.SendTaskOverdueNotification));
            Assert.That(result.ResponsibilityTask.TaskStatusId, Is.EqualTo(responsibilityTask.TaskStatusId));
            Assert.That(result.ResponsibilityTask.ExistingDocuments.CanDeleteDocuments, Is.EqualTo(false));
            Assert.That(result.ResponsibilityTask.ExistingDocuments.DocumentTypeId, Is.EqualTo((int)DocumentTypeEnum.Responsibility));

            Assert.That(result.ResponsibilityTask.ExistingDocuments.PreviouslyAddedDocuments[0].Id, Is.EqualTo(taskDocument.Id));
            Assert.That(result.ResponsibilityTask.ExistingDocuments.PreviouslyAddedDocuments[0].DocumentLibraryId, Is.EqualTo(taskDocument.DocumentLibraryId));
            Assert.That(result.ResponsibilityTask.ExistingDocuments.PreviouslyAddedDocuments[0].Filename, Is.EqualTo(taskDocument.Filename));
            Assert.That(result.ResponsibilityTask.ExistingDocuments.PreviouslyAddedDocuments[0].Description, Is.EqualTo(taskDocument.Description));
            Assert.That(result.ResponsibilityTask.ResponsibilitySummary.Id, Is.EqualTo(responsibility.Id));
            Assert.That(result.ResponsibilityTask.ResponsibilitySummary.Title, Is.EqualTo(responsibility.Title));
            Assert.That(result.ResponsibilityTask.ResponsibilitySummary.Description, Is.EqualTo(responsibility.Description));
        }

      

        private static CustomPrincipal CreateCustomPrincipal(UserDto userDto)
        {
            var customPrincipal = new CustomPrincipal(userDto, new CompanyDto());
            return customPrincipal;
        }

        private CompleteResponsibilityTaskViewModelFactory CreateTarget()
        {
            return new CompleteResponsibilityTaskViewModelFactory(_responsibilityTaskService.Object,
                new ViewResponsibilityTaskViewModelFactory(_responsibilityTaskService.Object, _existingDocumentsViewModelFactory.Object));
        }
    }
}

