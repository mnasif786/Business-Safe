using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Responsibilities.Wizard
{
    [TestFixture]
    public class GenerateResponsibilityTasksViewModelFactoryTests
    {
        private Mock<IStatutoryResponsibilityTaskTemplateService> _statutoryResponsibilityTaskTemplateService;
        private Mock<IResponsibilitiesService> _responsibilitiesService;

        [SetUp]
        public void Setup()
        {
            _statutoryResponsibilityTaskTemplateService = new Mock<IStatutoryResponsibilityTaskTemplateService>();
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
        }
        
        public GenerateResponsibilityTasksViewModelFactory GetTarget()
        {
            var target = new Mock<GenerateResponsibilityTasksViewModelFactory>(_statutoryResponsibilityTaskTemplateService.Object, null, _responsibilitiesService.Object) { CallBase = true };
            target.Setup(x => x.GetEmployees());
            target.Setup(x => x.GetFrequencyOptions());

            return target.Object;
        }

        [Test]
        public void Given_uncreated_statutory_task_when_GetViewModel_then_model_contains_the_responsibilities_and_tasks()
        {
            const long siteId = long.MaxValue;
            var tasks = new List<StatutoryResponsibilityTaskTemplateDto>
                        { 
                            new StatutoryResponsibilityTaskTemplateDto() { Id = 12312312 } 
                        };
            var respDtoA = CreateResponsibilityDto(siteId, tasks);
            var respDtoB = CreateResponsibilityDto(siteId, tasks);
            var respDtoC = CreateResponsibilityDto(siteId - 1, tasks);

            _responsibilitiesService
                .Setup(x => x.GetStatutoryResponsibilities(It.IsAny<long>()))
                .Returns(new List<ResponsibilityDto>() { respDtoA, respDtoB, respDtoC });

            var target = GetTarget();
            var result = target.WithCompanyId(123123).GetViewModel();

            Assert.AreEqual(2, result.SelectedSites.Count());
            Assert.AreEqual(2, result.SelectedSites.First().Responsibilities.Count());
            Assert.AreEqual(1, result.SelectedSites.ElementAt(1).Responsibilities.Count());
        }

        [Test]
        public void Given_uncreated_statutory_task_when_GetViewModel_then_model_contains_sites_ordered_by_name()
        {
            var tasks = new List<StatutoryResponsibilityTaskTemplateDto>
                        { 
                            new StatutoryResponsibilityTaskTemplateDto() { Id = 12312312 } 
                        };
            var respDtoA = CreateResponsibilityDto(new SiteDto { Id = 123L, Name = "c" }, tasks);
            var respDtoB = CreateResponsibilityDto(new SiteDto { Id = 456L, Name = "a" }, tasks);
            var respDtoC = CreateResponsibilityDto(new SiteDto { Id = 678L, Name = "b" }, tasks);

            _responsibilitiesService
                .Setup(x => x.GetStatutoryResponsibilities(It.IsAny<long>()))
                .Returns(new List<ResponsibilityDto>() { respDtoA, respDtoB, respDtoC });

            var target = GetTarget();
            var result = target.WithCompanyId(123123).GetViewModel();

            Assert.AreEqual(3, result.SelectedSites.Count());
            Assert.AreEqual("a", result.SelectedSites.ElementAt(0).Name);
            Assert.AreEqual("b", result.SelectedSites.ElementAt(1).Name);
            Assert.AreEqual("c", result.SelectedSites.ElementAt(2).Name);
        }

        //Conflict in business requirements Nauman, Alps.
        [Test]
        [Ignore]
        public void Given_uncreated_statutory_task_when_GetViewModel_then_responsibilitys_initially_selected_frequency_is_passed_to_task()
        {
            // Given
            const long siteId = long.MaxValue;
            var tasks = new List<StatutoryResponsibilityTaskTemplateDto>
                        { 
                            new StatutoryResponsibilityTaskTemplateDto() { Id = 12312312, TaskReoccurringType = TaskReoccurringType.TwentySixMonthly}

                        };
            const TaskReoccurringType selectedTaskReoccurringType = TaskReoccurringType.Annually;
            var respDtoA = CreateResponsibilityDto(siteId, tasks, selectedTaskReoccurringType);

            _responsibilitiesService
                .Setup(x => x.GetStatutoryResponsibilities(It.IsAny<long>()))
                .Returns(new List<ResponsibilityDto>() { respDtoA });

            var target = GetTarget();

            // When
            var result = target.WithCompanyId(123123).GetViewModel();

            // Then
            Assert.That(result.SelectedSites.First().Responsibilities.First().StatutoryResponsibilityTasks.First().InitialFrequency, Is.EqualTo(TaskReoccurringType.Annually));
        }

        [Test]
        public void Given_uncreated_statutory_task_when_GetViewModel_then_the_uncreated_tasks_frequency_is_the_set_from_the_task_template_default()
        {
            //given
            const long taskTemplateId = 12124124;
            const TaskReoccurringType expectedRecurringType = TaskReoccurringType.ThreeMonthly;
            const long siteId = long.MaxValue;
            
            var tasks = new List<StatutoryResponsibilityTaskTemplateDto>
                        { 
                            new StatutoryResponsibilityTaskTemplateDto() { Id = taskTemplateId,TaskReoccurringType = expectedRecurringType} 
                        };
            var respDtoA = CreateResponsibilityDto(siteId, tasks);
            respDtoA.InitialTaskReoccurringType = TaskReoccurringType.TwentyFourMonthly;

            _responsibilitiesService
                .Setup(x => x.GetStatutoryResponsibilities(It.IsAny<long>()))
                .Returns(new List<ResponsibilityDto>() { respDtoA});

            var target = GetTarget();

            //when
            var result = target.WithCompanyId(123123).GetViewModel();

            //then
            Assert.That(result.SelectedSites.First().Responsibilities.First().StatutoryResponsibilityTasks.First(x=>x.IsCreated==false).InitialFrequency, Is.EqualTo(expectedRecurringType));
          
        }

        [Test]
        public void Given_uncreated_statutory_task_with_no_site_set_when_GetViewModel_then_site_is_empty_list()
        {
            //given
            const long taskTemplateId = 12124124;
            const TaskReoccurringType expectedRecurringType = TaskReoccurringType.ThreeMonthly;

            var tasks = new List<StatutoryResponsibilityTaskTemplateDto>
                            {
                                new StatutoryResponsibilityTaskTemplateDto() {Id = taskTemplateId, TaskReoccurringType = expectedRecurringType}
                            };
            var respDtoA = new ResponsibilityDto
                               {
                                   Site = null,
                                   StatutoryResponsibilityTaskTemplates = tasks,
                                   InitialTaskReoccurringType = TaskReoccurringType.TwentyFourMonthly
                               };


            _responsibilitiesService
                .Setup(x => x.GetStatutoryResponsibilities(It.IsAny<long>()))
                .Returns(new List<ResponsibilityDto>() {respDtoA});

            var target = GetTarget();

            //when
            var result = target.WithCompanyId(123123).GetViewModel();

            //then
            Assert.That(result.SelectedSites.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_uncreated_statutory_task_when_GetViewModel_with_sites_then_model_contains_the_tasks_only_for_site_user_has_permissions_()
        {
            const long siteId = 1111;
           
            var tasks = new List<StatutoryResponsibilityTaskTemplateDto>
                        { 
                            new StatutoryResponsibilityTaskTemplateDto() { Id = 12312312 } 
                        };

            var respDtoA = CreateResponsibilityDto(siteId, tasks);
            var respDtoB = CreateResponsibilityDto(siteId, tasks);
            var respDtoC = CreateResponsibilityDto(siteId - 1, tasks);

            _responsibilitiesService
                .Setup(x => x.GetStatutoryResponsibilities(It.IsAny<long>()))
                .Returns(new List<ResponsibilityDto>() { respDtoA, respDtoB, respDtoC });
            
            var target = GetTarget();
            var result = target.WithCompanyId(123123).WithAllowedSiteIds(new List<long>() { siteId }).GetViewModel();

            Assert.AreEqual(1, result.SelectedSites.Count());
            Assert.AreEqual(2, result.SelectedSites.First().Responsibilities.Count());
        }
        
        private ResponsibilityDto CreateResponsibilityDto(long siteId, List<StatutoryResponsibilityTaskTemplateDto> tasks)
        {
            return new ResponsibilityDto
                   {
                       Site = new SiteDto() { Id = siteId },
                       //UncreatedStatutoryResponsibilityTaskTemplates = tasks,
                       StatutoryResponsibilityTaskTemplates = tasks
                   };
        }

        private ResponsibilityDto CreateResponsibilityDto(long siteId, List<StatutoryResponsibilityTaskTemplateDto> tasks, TaskReoccurringType taskRecurringType)
        {
            var responsibility = CreateResponsibilityDto(siteId, tasks);
            responsibility.InitialTaskReoccurringType = taskRecurringType;

            return responsibility;
        }

        private ResponsibilityDto CreateResponsibilityDto(SiteDto site, List<StatutoryResponsibilityTaskTemplateDto> tasks)
        {
            return new ResponsibilityDto
            {
                Site = site,
                UncreatedStatutoryResponsibilityTaskTemplates = tasks
            };
        }
    }
}
