using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using NUnit.Framework;
using Moq;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Factories;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FurtherControlMeasureTask
{
    [TestFixture]
    [Category("Unit")]
    public class ViewFurtherControlMeasureTaskViewModelFactoryTests
    {
        private const long FurtherControlMeasureTaskId = 94L;
        private const long CompanyId = 28639L;
        private FurtherControlMeasureTaskDto _furtherControlMeasureTask;
        private ExistingDocumentsViewModel _existingDocumentsViewModel;
        private Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        private Mock<IExistingDocumentsViewModelFactory> _existingDocumentsViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _furtherControlMeasureTask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto
                                             {
                                                 Id = FurtherControlMeasureTaskId,
                                                 Title = "Test Title 02",
                                                 Description = "Test Description 02",
                                                 Reference = "Test Reference 02",
                                                 TaskAssignedTo = new EmployeeDto() 
                                                                      {
                                                                          Id = Guid.NewGuid(),
                                                                          FullName = "Test Name 02"
                                                                      },
                                                 TaskCompletionDueDate =
                                                     DateTime.Now.AddDays(20).ToString(CultureInfo.InvariantCulture),
                                                 TaskStatusString = "Test Status 02",
                                                 TaskCategory = new TaskCategoryDto() { Id = 123L, Category = "Test Category 02" },
                                                 TaskStatusId = 99,
                                                 CreatedDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                                 Documents = new List<TaskDocumentDto>(),
                                                 TaskReoccurringType = TaskReoccurringType.None,
                                                 IsReoccurring = false,
                                                 RiskAssessment = new RiskAssessmentDto
                                                                      {
                                                                          Id = 1232L,
                                                                          Title = "Test RA Title 02",
                                                                          Reference = "Test RA Reference 02"
                                                                      }
                                                                      
                                             };

            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();

            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdIncludeDeleted(FurtherControlMeasureTaskId))
                .Returns(_furtherControlMeasureTask);

            _existingDocumentsViewModelFactory = new Mock<IExistingDocumentsViewModelFactory>();

            _existingDocumentsViewModelFactory
                .Setup(x => x.WithCanDeleteDocuments(false))
                .Returns(_existingDocumentsViewModelFactory.Object);

            _existingDocumentsViewModelFactory
                .Setup(x => x.WithDefaultDocumentType(It.IsAny<DocumentTypeEnum>()))
                .Returns(_existingDocumentsViewModelFactory.Object);

            _existingDocumentsViewModel = new ExistingDocumentsViewModel();

            _existingDocumentsViewModelFactory
                .Setup(x => x.GetViewModel(_furtherControlMeasureTask.Documents))
                .Returns(_existingDocumentsViewModel);
        }

        [Test]
        public void When_GetViewModel_called_Then_correct_methods_called()
        {
            var factory = GetTarget();

            factory
                .WithCompanyId(CompanyId)
                .WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId)
                .GetViewModel();

            _furtherControlMeasureTaskService.Verify(x => x.GetByIdIncludeDeleted(FurtherControlMeasureTaskId), Times.Once());
            _existingDocumentsViewModelFactory.Verify(x => x.GetViewModel(_furtherControlMeasureTask.Documents), Times.Once());
        }

        [Test]
        public void Given_valid_parameters_When_GetViewModel_called_Then_correct_view_model_returned()
        {
            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(CompanyId)
                .WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId)
                .GetViewModel();

            Assert.That(viewModel.FurtherControlMeasureTaskId, Is.EqualTo(FurtherControlMeasureTaskId));
            Assert.That(viewModel.Reference, Is.EqualTo(_furtherControlMeasureTask.Reference));
            Assert.That(viewModel.Title, Is.EqualTo(_furtherControlMeasureTask.Title));
            Assert.That(viewModel.TaskDescription, Is.EqualTo(_furtherControlMeasureTask.Description));
            Assert.That(viewModel.TaskAssignedToId, Is.EqualTo(_furtherControlMeasureTask.TaskAssignedTo.Id));
            Assert.That(viewModel.TaskAssignedTo, Is.EqualTo(_furtherControlMeasureTask.TaskAssignedTo.FullName));
            Assert.That(viewModel.TaskCompletionDueDate, Is.EqualTo(_furtherControlMeasureTask.TaskCompletionDueDate));
            Assert.That(viewModel.TaskStatusId, Is.EqualTo(_furtherControlMeasureTask.TaskStatusId));
            Assert.That(viewModel.ExistingDocuments, Is.EqualTo(_existingDocumentsViewModel));
            Assert.That(viewModel.TaskReoccurringTypeId, Is.EqualTo((int)_furtherControlMeasureTask.TaskReoccurringType));
            Assert.That(viewModel.TaskReoccurringType, Is.EqualTo(_furtherControlMeasureTask.TaskReoccurringType));
            Assert.That(viewModel.IsReoccurring, Is.EqualTo(_furtherControlMeasureTask.IsReoccurring));
        }

        private IViewFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new ViewFurtherControlMeasureTaskViewModelFactory(
                _furtherControlMeasureTaskService.Object,
                _existingDocumentsViewModelFactory.Object);
        }
    }
}
