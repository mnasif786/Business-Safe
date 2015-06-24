using System;
using System.Collections.Generic;
using System.Globalization;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class AddEditFurtherControlMeasureTaskViewModelFactoryTests
    {
        private const long FurtherControlMeasureTaskId = 94L;
        private const long CompanyId = 28639L;
        private const bool CanDeleteDocuments = true;
        private FurtherControlMeasureTaskDto _furtherControlMeasureTask;
        private ExistingDocumentsViewModel _existingDocumentsViewModel;
        private Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        private Mock<IExistingDocumentsViewModelFactory> _existingDocumentsViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _furtherControlMeasureTask = new FireRiskAssessmentFurtherControlMeasureTaskDto
                                             {
                                                 Id = FurtherControlMeasureTaskId,
                                                 Title = "Test Title 01",
                                                 Description = "Test Description 01",
                                                 Reference = "Test Reference 01",
                                                 TaskAssignedTo = new EmployeeDto
                                                                      {
                                                                          Id = Guid.NewGuid(),
                                                                          FullName = "Test Name 01"
                                                                      },
                                                 TaskCompletionDueDate =
                                                     DateTime.Now.AddDays(20).ToString(CultureInfo.InvariantCulture),
                                                 TaskStatusString = "Test Status 01",
                                                 TaskCategory = new TaskCategoryDto() { Id = 123L, Category = "Test Category 01" },
                                                 TaskStatusId = 99,
                                                 CreatedDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                                 Documents = new List<TaskDocumentDto>(),
                                                 TaskReoccurringType = TaskReoccurringType.None,
                                                 IsReoccurring = false,
                                                 RiskAssessment = new RiskAssessmentDto
                                                                      {
                                                                          Id = 1231L,
                                                                          Title = "Test RA Title 01",
                                                                          Reference = "Test RA Reference 01"
                                                                      }

                                             };

            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();

            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId))
                .Returns(_furtherControlMeasureTask);

            _existingDocumentsViewModelFactory = new Mock<IExistingDocumentsViewModelFactory>();

            _existingDocumentsViewModelFactory
                .Setup(x => x.WithCanDeleteDocuments(CanDeleteDocuments))
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
                .WithCanDeleteDocuments(CanDeleteDocuments)
                .GetViewModel();

            _furtherControlMeasureTaskService.Verify(x => x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId), Times.Once());
            _existingDocumentsViewModelFactory.Verify(x => x.WithCanDeleteDocuments(CanDeleteDocuments), Times.Once());
            _existingDocumentsViewModelFactory.Verify(x => x.GetViewModel(_furtherControlMeasureTask.Documents), Times.Once());
        }

        [Test]
        public void Given_valid_parameters_When_GetViewModel_called_Then_correct_view_model_returned()
        {
            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(CompanyId)
                .WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId)
                .WithCanDeleteDocuments(CanDeleteDocuments)
                .GetViewModel();

            Assert.That(viewModel.FurtherControlMeasureTaskId, Is.EqualTo(FurtherControlMeasureTaskId));
            Assert.That(viewModel.CompanyId, Is.EqualTo(CompanyId));
            Assert.That(viewModel.RiskAssessmentTitle, Is.EqualTo(_furtherControlMeasureTask.RiskAssessment.Title));
            Assert.That(viewModel.Reference, Is.EqualTo(_furtherControlMeasureTask.Reference));
            Assert.That(viewModel.Title, Is.EqualTo(_furtherControlMeasureTask.Title));
            Assert.That(viewModel.Description, Is.EqualTo(_furtherControlMeasureTask.Description));
            Assert.That(viewModel.TaskAssignedToId, Is.EqualTo(_furtherControlMeasureTask.TaskAssignedTo.Id));
            Assert.That(viewModel.TaskAssignedTo, Is.EqualTo(_furtherControlMeasureTask.TaskAssignedTo.FullName));
            Assert.That(viewModel.TaskCompletionDueDate, Is.EqualTo(_furtherControlMeasureTask.TaskCompletionDueDate));
            Assert.That(viewModel.TaskStatusId, Is.EqualTo(_furtherControlMeasureTask.TaskStatusId));
            Assert.That(viewModel.TaskStatus, Is.EqualTo(_furtherControlMeasureTask.TaskStatusString));
            Assert.That(viewModel.CompletedComments, Is.EqualTo(_furtherControlMeasureTask.TaskCompletedComments));
            Assert.That(viewModel.ExistingDocuments, Is.EqualTo(_existingDocumentsViewModel));
            Assert.That(viewModel.TaskReoccurringTypeId, Is.EqualTo((int)_furtherControlMeasureTask.TaskReoccurringType));
            Assert.That(viewModel.TaskReoccurringType, Is.EqualTo(_furtherControlMeasureTask.TaskReoccurringType));
            Assert.That(viewModel.IsRecurring, Is.EqualTo(_furtherControlMeasureTask.IsReoccurring));
        }

        private IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new EditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory(
                _furtherControlMeasureTaskService.Object, 
                _existingDocumentsViewModelFactory.Object
                );
        }
    }
}