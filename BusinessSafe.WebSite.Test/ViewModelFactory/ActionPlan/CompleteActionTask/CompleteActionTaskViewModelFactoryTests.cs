using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.ActionPlan.CompleteAction
{
    [TestFixture]
    [Category("Unit")]
    public class ActionTaskViewModelFactoryTests
    {
        private Mock<IActionTaskService> _actionTaskService;
        private Mock<IViewActionTaskViewModelFactory> _viewActionTaskViewModelFactory;
        private Mock<IExistingDocumentsViewModelFactory> _existingDocumentsViewModelFactory;

        private long _actionTaskId = 1L;
        private long _companyId = 2L;
            
        [SetUp]
        public void Setup()
        {
            _actionTaskService = new Mock<IActionTaskService>();
            _viewActionTaskViewModelFactory = new Mock<IViewActionTaskViewModelFactory>();
            _existingDocumentsViewModelFactory = new Mock<IExistingDocumentsViewModelFactory>();
            
            _viewActionTaskViewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewActionTaskViewModelFactory.Object);

            _viewActionTaskViewModelFactory
                .Setup(x => x.WithActionTaskId(_actionTaskId))
                .Returns(_viewActionTaskViewModelFactory.Object);

            _existingDocumentsViewModelFactory 
                .Setup( ( x => x.WithCanDeleteDocuments(false)))
                .Returns(_existingDocumentsViewModelFactory.Object);

            _existingDocumentsViewModelFactory 
                .Setup( ( x => x.WithDefaultDocumentType(DocumentTypeEnum.Responsibility)))
                .Returns(_existingDocumentsViewModelFactory.Object);

            _existingDocumentsViewModelFactory
                .Setup(x => x.GetViewModel(It.IsAny<IEnumerable<TaskDocumentDto>>()))
                .Returns( new ExistingDocumentsViewModel());
        }

        [Test]
        public void given_company_id_when_get_view_model_then_service_is_called_with_correct_parameters()
        {
            //given
            var target = GetTarget( _viewActionTaskViewModelFactory.Object );

            _actionTaskService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new ActionTaskDto() { Site = new SiteDto() { } });

            //when
            var result = target.WithCompanyId(_companyId).WithActionTaskId(_actionTaskId).GetViewModel();

            //then
            _actionTaskService.Verify(x => x.GetByIdAndCompanyId(_actionTaskId, _companyId), Times.Once());
        }

       

        [Test]
        public void given_factory_when_get_view_model_then_result_maps_ActionTaskDto_to_viewmodel()
        {
            //given
            IViewActionTaskViewModelFactory viewActionTaskViewModelFactory = new ViewActionTaskViewModelFactory(_actionTaskService.Object, _existingDocumentsViewModelFactory.Object);

            var actionTaskDto = new ActionTaskDto()
                                    {
                                        Site = new SiteDto() {},
                                        Action = new ActionDto()
                                                     {
                                                         Reference = "123ref",
                                                         AreaOfNonCompliance = " area of non compliance",
                                                         ActionRequired = "aciont required",
                                                         GuidanceNote = "guidance note",
                                                         TargetTimescale = "timescale",
                                                         AssignedTo = Guid.NewGuid(),
                                                         DueDate = DateTime.Now,
                                                         Status = DerivedTaskStatusForDisplay.None,
                                                         Category = ActionCategory.Action
                                                     },
                                                     SendTaskNotification = true,
                                                     SendTaskCompletedNotification = true,
                                                     SendTaskOverdueNotification = true
                                    };

            var target = GetTarget(viewActionTaskViewModelFactory);            
            _actionTaskService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns( actionTaskDto);
             
            //when
            var result = target.WithCompanyId(_companyId).WithActionTaskId(_actionTaskId).GetViewModel();
            //then            
            Assert.That(result.ActionTaskId, Is.EqualTo(_actionTaskId), "Action task id not mapped");
            Assert.That(result.CompanyId, Is.EqualTo(_companyId), "Company id not mapped");          
            Assert.That(result.ActionTask.ActionSummary.AreaOfNonCompliance, Is.EqualTo(actionTaskDto.Action.AreaOfNonCompliance), "Area of non-compliance not mapped");

            Assert.That(result.ActionTask.DoNotSendTaskAssignedNotification,Is.EqualTo(!actionTaskDto.SendTaskNotification));
            Assert.That(result.ActionTask.DoNotSendTaskCompletedNotification, Is.EqualTo(!actionTaskDto.SendTaskCompletedNotification));
            Assert.That(result.ActionTask.DoNotSendTaskOverdueNotification, Is.EqualTo(!actionTaskDto.SendTaskOverdueNotification));
        }






        public CompleteActionTaskViewModelFactory GetTarget(IViewActionTaskViewModelFactory viewActionTaskViewModelFactory)
        {
           
            return new CompleteActionTaskViewModelFactory( _actionTaskService.Object, viewActionTaskViewModelFactory);
        }
    }
}
