using System;
using System.Globalization;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FurtherControlMeasureTask
{
    [TestFixture]
    [Category("Unit")]
    public class ReassignFurtherControlMeasureTaskViewModelFactoryTests
    {
        private const long CompanyId = 76179L;
        private const long FurtherControlMeasureTaskId = 76L;
        private FurtherControlMeasureTaskDto _furtherControlMeasureTask;
        private ViewFurtherControlMeasureTaskViewModel _viewFurtherControlMeasureTaskViewModel;
        private Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;
        private Mock<IViewFurtherControlMeasureTaskViewModelFactory> _viewFurtherControlMeasureTaskViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _furtherControlMeasureTask = new HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskDto();

            _viewFurtherControlMeasureTaskViewModel = new ViewFurtherControlMeasureTaskViewModel
                                                          {
                                                              FurtherControlMeasureTaskId = FurtherControlMeasureTaskId,
                                                              Reference = "Test Reference 03",
                                                              Title = "Test Title 03",
                                                              TaskDescription = "Test Description 03",
                                                              TaskStatusId = 99,
                                                              TaskAssignedTo = "Test Name 03",
                                                              TaskAssignedToId = Guid.NewGuid(),
                                                              TaskCompletionDueDate =
                                                                  DateTime.Now.AddDays(20).ToString(
                                                                      CultureInfo.InvariantCulture),
                                                              IsReoccurring = false,
                                                              TaskReoccurringTypeId = (long) TaskReoccurringType.None,
                                                              TaskReoccurringTypes = null,
                                                              TaskReoccurringType = TaskReoccurringType.None,
                                                              TaskReoccurringEndDate = null,
                                                              ExistingDocuments = new ExistingDocumentsViewModel(),
                                                              TaskCompletedDate = null,
                                                              TaskCompletedComments = null,
                                                          };

            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();

            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId))
                .Returns(_furtherControlMeasureTask);

            _viewFurtherControlMeasureTaskViewModelFactory = new Mock<IViewFurtherControlMeasureTaskViewModelFactory>();

            _viewFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_viewFurtherControlMeasureTaskViewModelFactory.Object);

            _viewFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_viewFurtherControlMeasureTaskViewModelFactory.Object);

            _viewFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel(_furtherControlMeasureTask))
                .Returns(_viewFurtherControlMeasureTaskViewModel);

        }

        [Test]
        public void When_GetViewModel_called_Then_correct_methods_called()
        {
            var factory = GetTarget();

            factory
                .WithCompanyId(CompanyId)
                .WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId)
                .GetViewModel();

            _furtherControlMeasureTaskService.Verify(x => x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId), Times.Once());
            _viewFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(_furtherControlMeasureTask), Times.Once());
        }

        [Test]
        public void Given_valid_parameters_When_GetViewModel_called_Then_correct_view_model_is_returned()
        {
            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(CompanyId)
                .WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId)
                .GetViewModel();

            Assert.That(viewModel.CompanyId, Is.EqualTo(CompanyId));
            Assert.That(viewModel.FurtherControlMeasureTaskId, Is.EqualTo(FurtherControlMeasureTaskId));
            Assert.That(viewModel.ReassignTaskTo, Is.Null);
            Assert.That(viewModel.ReassignTaskToId, Is.EqualTo(new Guid()));
            Assert.That(viewModel.ViewFurtherControlMeasureTaskViewModel, Is.EqualTo(_viewFurtherControlMeasureTaskViewModel));
        }

        private IReassignFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new ReassignFurtherControlMeasureTaskViewModelFactory(
                _furtherControlMeasureTaskService.Object,
                _viewFurtherControlMeasureTaskViewModelFactory.Object);
        }
    }
}
