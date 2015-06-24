using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.ActionPlan.ReassignActionPlanTask
{
    [TestFixture]
    public class ReassignActionPlanTaskTests
    {
        private Mock<IActionService> _actionService;
        private Mock<IActionTaskService> _actionTaskService;

        private const bool CanDeleteDocuments = true;
        //private ExistingDocumentsViewModel _existingDocumentsViewModel;
        private Mock<IExistingDocumentsViewModelFactory> _existingDocumentsViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _actionService = new Mock<IActionService>();
            _actionTaskService = new Mock<IActionTaskService>();

            _existingDocumentsViewModelFactory = new Mock<IExistingDocumentsViewModelFactory>();

            _existingDocumentsViewModelFactory
                .Setup(x => x.WithCanDeleteDocuments(It.IsAny<bool>()))
                .Returns(_existingDocumentsViewModelFactory.Object);

            _existingDocumentsViewModelFactory
                .Setup(x => x.WithDefaultDocumentType(It.IsAny<DocumentTypeEnum>()))
                .Returns(_existingDocumentsViewModelFactory.Object);        

            _existingDocumentsViewModelFactory
              .Setup(x => x.WithCanEditDocumentType(It.IsAny<bool>()))
              .Returns(_existingDocumentsViewModelFactory.Object);

            _existingDocumentsViewModelFactory
              .Setup(x => x.WithDocumentOriginType(It.IsAny<DocumentOriginType>()))
              .Returns(_existingDocumentsViewModelFactory.Object);
        }

        [Test]
        public void Given_viewmodel_factory_when_get_viewmodel_then_result_is_correct_type()
        {
            //given
            var target = GetTarget();
            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).
                Returns(new ActionDto
                            {
                                ActionTasks = new List<ActionTaskDto>
                                                  {
                                                      new ActionTaskDto
                                                          {
                                                              Title = "Title",
                                                              Description = "Description",
                                                              TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                                                              TaskAssignedTo = new EmployeeDto
                                                                                   {
                                                                                       Id = Guid.NewGuid(),
                                                                                       FullName = "Employee Name"
                                                                                   }
                                                          }
                                                  }
                            });

            //when
            var result = target.GetViewModel();
            //then
            Assert.That(result, Is.InstanceOf<ReassignActionTaskViewModel>());
        }

        [Test]
        public void Given_companyid_when_when_get_viewmodel_then_result_contains_matching_companyId()
        {
            //given
            var companyId = 123L;
            var target = GetTarget();
            target.WithCompanyId(companyId);

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new ActionDto
                             {
                                 ActionTasks = new List<ActionTaskDto>
                                                   {
                                                       new ActionTaskDto
                                                           {
                                                               Title = "Title",
                                                               Description = "Description",
                                                               TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                                                               TaskAssignedTo = new EmployeeDto
                                                                                    {
                                                                                        Id = Guid.NewGuid(),
                                                                                        FullName = "Employee Name"
                                                                                    }
                                                           }
                                                   }
                             });


            //when
            var result = target.GetViewModel();
            //then
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
        }

        [Test]
        public void given_company_id_when_get_view_model_then_service_is_called_with_correct_parameters()
        {
            //given
            var companyId = 123L;
            var actionId = 1L;
            var target = GetTarget();

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.Is<long>(a => a == actionId), It.Is<long>(c => c == companyId))).
                Returns(new ActionDto
                            {
                                ActionTasks = new List<ActionTaskDto>
                                                  {
                                                      new ActionTaskDto
                                                          {
                                                              Title = "Title",
                                                              Description = "Description",
                                                              TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                                                              TaskAssignedTo = new EmployeeDto
                                                                                   {
                                                                                       Id = Guid.NewGuid(),
                                                                                       FullName = "Employee Name"
                                                                                   }
                                                          }
                                                  }
                            });

            //when
            var result = target
                .WithCompanyId(companyId)
                .WithActionId(actionId)
                .GetViewModel();

            //then
            _actionService.Verify(
                x => x.GetByIdAndCompanyId(It.Is<long>(a => a == actionId), It.Is<long>(c => c == companyId)));
        }

        [Test]
        public void given_action_with_no_task_when_get_viewmodel_exception_is_thrown()
        {
            //given
            var companyId = 123L;
            var actionId = 1L;
            var target = GetTarget();

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.Is<long>(a => a == actionId), It.Is<long>(c => c == companyId))).
                Returns(new ActionDto());

            //when
            var result = target
                .WithCompanyId(companyId)
                .WithActionId(actionId);
                
            //then

            Assert.Throws<TaskNotFoundException>(() => result.GetViewModel());

        }


        [Test]
        public void given_company_id_when_get_view_model_then_get_viewmodel_maps_corect_result()
        {
            //given
            var companyId = 123L;
            var actionPlanId = 1231L;
            var actionId = 1L;

            var assignedTo = new EmployeeDto
                                 {
                                     Id = Guid.NewGuid(),
                                     FullName = "employee'"
                                 };

            var task = new ActionTaskDto
                           {
                               Id = 1L,
                               Title = "Title",
                               Description = "Description",
                               TaskAssignedTo = assignedTo
                           };
            var action = new ActionDto
                               {
                                   Id = actionId,
                                   ActionTasks = new List<ActionTaskDto>
                                                     {
                                                         task
                                                     },
                                                     GuidanceNote = "1.1"

                               };
            
            var target = GetTarget();

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.Is<long>(a => a == actionId), It.Is<long>(c => c == companyId))).
                Returns(action);


            //when
            var result = target
                .WithCompanyId(companyId)
                .WithActionPlanId(actionPlanId)
                .WithActionId(actionId)
                .GetViewModel();

            //then
            Assert.That(result.ActionPlanId, Is.EqualTo(actionPlanId));
            Assert.That(result.ActionId, Is.EqualTo(action.Id));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.Title, Is.EqualTo(task.Title));
            Assert.That(result.Description, Is.EqualTo(task.Description));
            Assert.That(result.GuidanceNotes, Is.EqualTo(action.GuidanceNote));
            Assert.That(result.DueDate, Is.EqualTo(task.TaskCompletionDueDate));
            Assert.That(result.ActionTaskAssignedTo, Is.EqualTo(task.TaskAssignedTo.FullName));
            Assert.That(result.ActionTaskAssignedToId, Is.EqualTo(task.TaskAssignedTo.Id));

        }

        [Test]
        public void Given_SendTaskNofication_is_true_then_DoNotSendTaskAssignedNotification_equals_false()
        {
            //given
            var assignedTo = new EmployeeDto
            {
                Id = Guid.NewGuid(),
                FullName = "employee'"
            };

            var task = new ActionTaskDto
            {
                Id = 1L,
                Title = "Title",
                Description = "Description",
                TaskAssignedTo = assignedTo,
                SendTaskNotification = true
            };
            var action = new ActionDto
            {
                Id = 234234,
                ActionTasks = new List<ActionTaskDto>
                {
                    task
                }
            };

            var target = GetTarget();

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).
                Returns(action);


            //when
            var result = target
                .WithCompanyId(123)
                .WithActionId(1123)
                .GetViewModel();

            //then
            Assert.That(result.DoNotSendTaskAssignedNotification, Is.EqualTo(false));
        }

        [Test]
        public void Given_SendTaskNofication_is_false_then_DoNotSendTaskAssignedNotification_equals_true()
        {
            //given
            var assignedTo = new EmployeeDto
            {
                Id = Guid.NewGuid(),
                FullName = "employee'"
            };

            var task = new ActionTaskDto
            {
                Id = 1L,
                Title = "Title",
                Description = "Description",
                TaskAssignedTo = assignedTo,
                SendTaskNotification = false
            };
            var action = new ActionDto
            {
                Id = 234234,
                ActionTasks = new List<ActionTaskDto>
                {
                    task
                }
            };

            var target = GetTarget();

            _actionService.
                Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).
                Returns(action);


            //when
            var result = target
                .WithCompanyId(123)
                .WithActionId(1123)
                .GetViewModel();

            //then
            Assert.That(result.DoNotSendTaskAssignedNotification, Is.EqualTo(true));
        }

        [Test]
        public void given_action_id_and_company_id_when_get_view_model_then_existing_documents_are_returned()
        {
            //given
            var companyId = 123L;
            var actionId = 1L;

            var target = GetTarget(); 
            
            var assignedTo = new EmployeeDto
                                 {
                                     Id = Guid.NewGuid(),
                                     FullName = "employee'"
                                 };

         
            var task = new ActionTaskDto
                           {
                               Id = 1L,
                               Title = "Title",
                               Description = "Description",
                               TaskAssignedTo = assignedTo                               
                           };

            var action = new ActionDto
                               {
                                   Id = actionId,
                                   ActionTasks = new List<ActionTaskDto>
                                                     {
                                                         task
                                                     },                                                     
                               };
            _actionService
                .Setup(x => x.GetByIdAndCompanyId( actionId, companyId ))
                .Returns(action);

            ExistingDocumentsViewModel existingDocumentsViewModel = new ExistingDocumentsViewModel();            

            existingDocumentsViewModel.PreviouslyAddedDocuments = new List<PreviouslyAddedDocumentGridRowViewModel>();

            PreviouslyAddedDocumentGridRowViewModel padGridRowViewModel = new PreviouslyAddedDocumentGridRowViewModel();            
            padGridRowViewModel.Id = 1234L;
            padGridRowViewModel.Filename = "somefilename.doc";
            padGridRowViewModel.Description = "";
            padGridRowViewModel.DocumentLibraryId = 765L;
            padGridRowViewModel.DocumentOriginType = DocumentOriginType.TaskUpdated;
            padGridRowViewModel.DocumentTypeName = "somedocumenttype";
            

            existingDocumentsViewModel.PreviouslyAddedDocuments.Add(padGridRowViewModel);         

            _existingDocumentsViewModelFactory
                .Setup(x => x.GetViewModel(It.IsAny<IEnumerable<TaskDocumentDto>>()))
                .Returns(existingDocumentsViewModel);

            var result = target
              .WithCompanyId(companyId)
              .WithActionId(actionId)
              .GetViewModel();

            // then
            Assert.IsNotNull(result.ExistingDocuments);
            Assert.IsNotNull(result.ExistingDocuments.PreviouslyAddedDocuments);
            Assert.AreEqual( 1, result.ExistingDocuments.PreviouslyAddedDocuments.Count);
            Assert.AreEqual(padGridRowViewModel.Id, result.ExistingDocuments.PreviouslyAddedDocuments[0].Id);
            //Assert.AreEqual( DocumentTypeEnum.Action, result.ExistingDocuments.PreviouslyAddedDocuments[0].DocumentTypeName);
            Assert.AreEqual(DocumentOriginType.TaskUpdated, result.ExistingDocuments.PreviouslyAddedDocuments[0].DocumentOriginType);
        }

        [Test]
        public void Given_companyid_when_when_get_viewmodel_with_taskId_then_result_contains_matching_companyId()
        {
            //given
            var companyId = 123L;
            var taskId = 1L;
            var target = GetTarget();
            target.WithCompanyId(companyId);

            _actionTaskService.
                Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new ActionTaskDto
                {
                    Id = 1111,
                    Title = "Title",
                    
                    Description = "Description",
                    TaskCompletionDueDate = DateTime.Now.ToShortDateString(),
                    TaskAssignedTo = new EmployeeDto
                    {
                       Id = Guid.Parse("8c195637-79e2-47c8-b422-bf901c328a83"),
                       FullName = "Employee Name"
                    },
                    Action = new ActionDto()
                    {
                        Id = 2222,
                        ActionPlan = new Domain.Entities.ActionPlan()
                        {
                            Id = 1
                        }
                    }
                });

            ReassignActionTaskViewModel taskViewModel = new ReassignActionTaskViewModel()
            {
                ActionId = 2222,
                ActionPlanId = 1,
                Title = "Title",
                ActionTaskAssignedToId = Guid.Parse("8c195637-79e2-47c8-b422-bf901c328a83"),
                CompanyId = 1111,
                Description = "Description"
            };

            //when
            var result = target.WithTaskId(taskId)
                         .WithCompanyId(companyId)
                         .GetViewModelByTask();

            //then
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.Title, Is.EqualTo(taskViewModel.Title));
            Assert.That(result.ActionId, Is.EqualTo(taskViewModel.ActionId));
            Assert.That(result.ActionPlanId, Is.EqualTo(taskViewModel.ActionPlanId));
            Assert.That(result.ActionTaskAssignedToId, Is.EqualTo(taskViewModel.ActionTaskAssignedToId));
        }

        [Test]
        public void given_action_with_no_task_when_get_viewmodel_with_taskid_exception_is_thrown()
        {
            //given
            var companyId = 123L;
            var taskId = 1L;
            var target = GetTarget();

            _actionTaskService.
                Setup(x => x.GetByIdAndCompanyId(It.Is<long>(a => a == taskId), It.Is<long>(c => c == companyId))).
                Throws(new ActionTaskNotFoundException(taskId,companyId));

            //when
            var result = target
                .WithCompanyId(companyId)
                .WithTaskId(taskId);

            //then

            Assert.Throws<TaskNotFoundException>(() => result.GetViewModelByTask());

        }

        private IReassignActionTaskViewModelFactory GetTarget()
        {
            var factory = new ReassignActionTaskViewModelFactory(_actionService.Object, _existingDocumentsViewModelFactory.Object, _actionTaskService.Object);
            return factory;
        }
    }
}
