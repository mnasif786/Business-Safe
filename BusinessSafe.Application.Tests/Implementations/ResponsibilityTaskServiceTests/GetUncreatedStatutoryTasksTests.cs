using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilityTaskServiceTests
{
    public class GetUncreatedStatutoryTasksTests
    {
        private Mock<IResponsibilityTaskRepository> _responsibilityTaskRepository;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IUserRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private User _user;
        private UserForAuditing _userForAuditing;
        private IEnumerable<CreateDocumentParameters> _createDocumentParameters;
        private Mock<IBus> _bus;
        private Mock<IResponsibilityRepository> _responsibilityRepository;

        [SetUp]
        public void SetUp()
        {
            _responsibilityTaskRepository = new Mock<IResponsibilityTaskRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userRepository = new Mock<IUserRepository>();
            _log = new Mock<IPeninsulaLog>();
            _bus = new Mock<IBus>();
            _responsibilityRepository = new Mock<IResponsibilityRepository>();

            _user = new User
            {
                Id = Guid.NewGuid(),
                Employee = new Employee
                {
                    Id = Guid.NewGuid()
                }
            };

            _userForAuditing = new UserForAuditing { Id = Guid.NewGuid() };
            _createDocumentParameters = new List<CreateDocumentParameters>();
 
        }

        private ResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepository.Object, null, null, null, null, null, null, null, null, null, _log.Object);
        }

        [Test]
        public void Given_there_are_uncreated_statutory_tasks_when_then_returns_list()
        {
            //Given
            var resp = new Responsibility(){Id=12432357};
            resp.StatutoryResponsibilityTemplateCreatedFrom = new StatutoryResponsibilityTemplate();
            resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks = new List<StatutoryResponsibilityTaskTemplate>();
            resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks.Add(new StatutoryResponsibilityTaskTemplate() {Id = 123124});
            resp.Site = new Site();
            resp.ResponsibilityCategory = new ResponsibilityCategory();
            resp.ResponsibilityReason = new ResponsibilityReason();
            resp.ResponsibilityTasks = new List<ResponsibilityTask>();
            resp.CreatedOn = DateTime.Now;


            _responsibilityRepository.Setup(x => x.GetStatutoryByCompanyId(It.IsAny<long>()))
                .Returns(new List<Responsibility>() {resp});

            var target = GetTarget();
            
            //when
            var result = target.GetStatutoryResponsibiltiesWithUncreatedStatutoryTasks(123123123).ToList();

            //then
            Assert.AreEqual(resp.Id, result[0].Id);
            Assert.AreEqual(resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks.Count(), result[0].UncreatedStatutoryResponsibilityTaskTemplates.Count());

        }

        [Test]
        public void Given_there_are_no_uncreated_statutory_tasks_when_then_returns_list()
        {
            //Given
            var resp = new Responsibility() { Id = 12432357 };
            resp.StatutoryResponsibilityTemplateCreatedFrom = new StatutoryResponsibilityTemplate();
            resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks = new List<StatutoryResponsibilityTaskTemplate>();
            resp.StatutoryResponsibilityTemplateCreatedFrom.ResponsibilityTasks.Add(new StatutoryResponsibilityTaskTemplate() { Id = 1234 });
            resp.Site = new Site();
            resp.ResponsibilityCategory = new ResponsibilityCategory();
            resp.ResponsibilityReason = new ResponsibilityReason();
            resp.ResponsibilityTasks = new List<ResponsibilityTask>();
            resp.ResponsibilityTasks.Add(new ResponsibilityTask() {StatutoryResponsibilityTaskTemplateCreatedFrom = new StatutoryResponsibilityTaskTemplate() {Id =1234}} );
            resp.CreatedOn = DateTime.Now;


            _responsibilityRepository.Setup(x => x.GetStatutoryByCompanyId(It.IsAny<long>()))
                .Returns(new List<Responsibility>() { resp });

            var target = GetTarget();

            //when
            var result = target.GetStatutoryResponsibiltiesWithUncreatedStatutoryTasks(123123123).ToList();

            //then
            Assert.AreEqual(0, result.Count);

        }
    }

}
