using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Responsibilities
{
    [TestFixture]
    [Category("Unit")]
    public class ReassignResponsibilityTaskViewModelFactoryTests
    {
        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private Mock<IViewResponsibilityTaskViewModelFactory> _viewResponsibilityTaskViewModelFactory;

        private long _companyId;
        private long _responsibilityTaskId;
        private ResponsibilityTaskDto _task;

        [SetUp]
        public void SetUp()
        {
            _companyId = 250;
            _responsibilityTaskId = 1;
            _task = new ResponsibilityTaskDto
                        {
                            Documents = new List<TaskDocumentDto>(),
                            Responsibility = new ResponsibilityDto(){Id = 123123L,Title = "new title", Description = "the description"}

                        };

            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();

            _responsibilityTaskService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_task);

            _viewResponsibilityTaskViewModelFactory = new Mock<IViewResponsibilityTaskViewModelFactory>();

            _viewResponsibilityTaskViewModelFactory
                .Setup((x => x.WithCompanyId(It.IsAny<long>())))
                .Returns(_viewResponsibilityTaskViewModelFactory.Object);

            _viewResponsibilityTaskViewModelFactory
                .Setup((x => x.WithResponsibilityTaskId(It.IsAny<long>())))
                .Returns(_viewResponsibilityTaskViewModelFactory.Object);

            _viewResponsibilityTaskViewModelFactory
                .Setup((x => x.GetViewModel()))
                .Returns(new ViewResponsibilityTaskViewModel());
        }

        [Test]
        public void Given_search_for_current_user_When_GetViewModel_is_called_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateTarget();

            //When
            target
                .WithCompanyId(_companyId)
                .WithResponsibilityTaskId(_responsibilityTaskId)
                .GetViewModel();

            //Then
            _responsibilityTaskService.Verify(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()));

            _viewResponsibilityTaskViewModelFactory.Verify(x => x.WithCompanyId(It.IsAny<long>()));
            _viewResponsibilityTaskViewModelFactory.Verify(x => x.WithResponsibilityTaskId(It.IsAny<long>()));
            _viewResponsibilityTaskViewModelFactory.Verify(x => x.GetViewModel());
        }

        private IReassignResponsibilityTaskViewModelFactory CreateTarget()
        {
            return new ReassignResponsibilityTaskViewModelFactory(_responsibilityTaskService.Object,
                                                                  _viewResponsibilityTaskViewModelFactory.Object);
        }
    }
}
