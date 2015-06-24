using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using BusinessSafe.Application.Helpers;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveResponsiblityTaskTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepository;
        private Mock<IResponsibilityCategoryRepository> _responsibilityCategoryRepository;
        private Mock<IResponsibilityReasonRepository> _responsibilityReasonRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<ITaskCategoryRepository> _taskCategoryRepository;
        private Mock<IDocumentParameterHelper> _documentParameterHelper;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _responsibilityRepository = new Mock<IResponsibilityRepository>();
            _responsibilityReasonRepository = new Mock<IResponsibilityReasonRepository>();
            _responsibilityCategoryRepository = new Mock<IResponsibilityCategoryRepository>();

            var responsibility = new Responsibility
                                     {
                                         Id = default(long),
                                         CompanyId = default(long),
                                         ResponsibilityTasks =
                                             {new ResponsibilityTask {Id = 1L}, new ResponsibilityTask {Id = 2L}}
                                     };

            responsibility.ResponsibilityTasks[0].Responsibility = responsibility;

            _responsibilityRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(responsibility);

            _responsibilityRepository
                .Setup(x=>x.SaveOrUpdate(It.IsAny<Responsibility>()))
                .Callback<Responsibility>(par => par.Id = 1);

            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _taskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_Save_New_ResponsibilityTask_Then_should_call_correct_methods()
        {
            //given
            var target = GetTarget();
            var taskGuid = Guid.NewGuid();

            var request = SaveResponsibilityTaskRequest.Create(1L,
                                                               1L,
                                                               default(long),
                                                               "Title",
                                                               "Description",
                                                               (int) TaskReoccurringType.Weekly,
                                                               "01/01/2014",
                                                               DateTime.Now,
                                                               Guid.NewGuid(),
                                                               Guid.NewGuid(),
                                                               1,
                                                               "01/01/2014",
                                                               false,
                                                               false,
                                                               false,
                                                               false,
                                                               taskGuid, 1L,
                                                               new List<CreateDocumentRequest>());
                          
            //when
            target.SaveResponsibilityTask(request);

            //then
            _responsibilityRepository.Verify(x => x.GetByIdAndCompanyId(request.ResponsibilityId, request.CompanyId), Times.Once());
            _responsibilityRepository.Verify(x=>x.SaveOrUpdate(It.IsAny<Responsibility>()),Times.Once());
        }

        [Test]
        public void Given_valid_request_When_Save_Existing_ResponsibilityTask_Then_should_call_correct_methods()
        {
            //given
            var target = GetTarget();
            var taskGuid = Guid.NewGuid();

            var request = SaveResponsibilityTaskRequest.Create(1L,
                                                               1L,
                                                               1L,
                                                               "Title",
                                                               "Description",
                                                               (int)TaskReoccurringType.Weekly,
                                                               "01/01/2014",
                                                               DateTime.Now,
                                                               Guid.NewGuid(),
                                                               Guid.NewGuid(),
                                                               1,
                                                               "01/01/2014",
                                                               false,
                                                               false,
                                                               false,
                                                               false,
                                                               taskGuid, 1L,
                                                               new List<CreateDocumentRequest>());

            //when
            target.SaveResponsibilityTask(request);

            //then
            _responsibilityRepository.Verify(x => x.GetByIdAndCompanyId(request.ResponsibilityId, request.CompanyId), Times.Once());
            _responsibilityRepository.Verify(x => x.SaveOrUpdate(It.IsAny<Responsibility>()), Times.Once());
        }

        private IResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepository.Object,
                _responsibilityCategoryRepository.Object,
                _responsibilityReasonRepository.Object,
                _employeeRepository.Object,
                _siteRepository.Object,
                _userForAuditingRepository.Object,
                _taskCategoryRepository.Object,
                _documentParameterHelper.Object, 
                null, null,
                _log.Object);
        }    
    }
}
