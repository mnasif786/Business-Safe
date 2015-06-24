using System;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Implementations.Responsibilities;
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
    public class SaveResponsiblityTests
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

            _responsibilityRepository
                .Setup(x => x.GetById(It.IsAny<long>()))
                .Returns(new Responsibility {Id = 1});

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
        public void Given_valid_request_When_Save_New_Responsibility_Then_should_call_correct_methods()
        {
            //given
            var target = GetTarget();
            var request = new SaveResponsibilityRequest
                              {
                                  ResponsibilityCategoryId = default(long),
                                  Title = string.Empty,
                                  Description = string.Empty,
                                  SiteId = default(long),
                                  ResponsibilityReasonId = default(long),
                                  OwnerId = new Guid(),
                                  TaskReoccurringType = TaskReoccurringType.Weekly,
                              };
            //when
            target.SaveResponsibility(request);
            //then
            _responsibilityRepository.Verify(x=>x.SaveOrUpdate(It.IsAny<Responsibility>()),Times.Once());
        }


        [Test]
        public void Given_valid_request_When_Save_Existing_Responsibility_Then_should_call_correct_methods()
        {
            //given
            var target = GetTarget();
            var request = new SaveResponsibilityRequest
            {
                ResponsibilityId = 1L,
                ResponsibilityCategoryId = default(long),
                Title = string.Empty,
                Description = string.Empty,
                SiteId = default(long),
                ResponsibilityReasonId = default(long),
                OwnerId = new Guid(),
                TaskReoccurringType = TaskReoccurringType.Weekly,
            };
            //when
            target.SaveResponsibility(request);
            //then
            _responsibilityRepository.Verify(x => x.GetById(It.IsAny<long>()), Times.Once());
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
