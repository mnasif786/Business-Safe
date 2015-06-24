using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilityTaskServiceTests
{
    [TestFixture]
    public class GetStatutoryTaskTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _responsibilityRepository = new Mock<IResponsibilityRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_there_are_statutory_template_tasks_when_get_statutory_tasks_then_returns_results()
        {
            //given

            var companydId = 1234L;

            StatutoryResponsibilityTaskTemplate taskTemplate = new StatutoryResponsibilityTaskTemplate{Id = 1234L};

            var resp = new Responsibility() { Id = 12432357 };
            resp.StatutoryResponsibilityTemplateCreatedFrom = new StatutoryResponsibilityTemplate();
            resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks = new List<StatutoryResponsibilityTaskTemplate>();
            resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks.Add(taskTemplate);
            resp.Site = new Site();
            resp.ResponsibilityCategory = new ResponsibilityCategory();
            resp.ResponsibilityReason = new ResponsibilityReason();

            resp.ResponsibilityTasks = new List<ResponsibilityTask>
                                           {
                                               new ResponsibilityTask
                                                   {
                                                       Id = 1L,
                                                       StatutoryResponsibilityTaskTemplateCreatedFrom = taskTemplate
                                                   }
                                           };

            resp.CreatedOn = DateTime.Now;

            _responsibilityRepository
                .Setup(x => x.GetStatutoryByCompanyId(It.IsAny<long>()))
                .Returns(new List<Responsibility>() { resp });

            var target = GetTarget();
            
            //when
            List<ResponsibilityDto> result = target.GetStatutoryResponsibilities(companydId);

            //then

            Assert.That(result.Count,Is.EqualTo(1));
            Assert.That(result.First().StatutoryResponsibilityTaskTemplates.Count(), Is.EqualTo(resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks.Count));            
        }

        private ResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepository.Object, null, null, null, null, null, null, null, null, null, _log.Object);
        }
    }
}
