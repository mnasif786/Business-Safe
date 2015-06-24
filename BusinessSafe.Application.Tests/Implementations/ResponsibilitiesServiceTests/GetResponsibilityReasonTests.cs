using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Implementations.Responsibilities;
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
    public class GetResponsibilityReasonTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepository;
        private Mock<IResponsibilityReasonRepository> _responsibilityReasonRepository;
        private Mock<IResponsibilityCategoryRepository> _responsibilityCategoryRepository;
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
            _responsibilityReasonRepository
                .Setup(x => x.GetAll())
                .Returns(new List<ResponsibilityReason>());

            _responsibilityCategoryRepository = new Mock<IResponsibilityCategoryRepository>();

            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _taskCategoryRepository = new Mock<ITaskCategoryRepository>();
            _documentParameterHelper = new Mock<IDocumentParameterHelper>();
            _log = new Mock<IPeninsulaLog>();

        }

        [Test]
        public void Given_valid_request_When_GetResponsibilityReasons_Then_should_call_correct_methods()
        {
            //given
            var target = GetTarget();
            //when
            target.GetResponsibilityReasons();
            //then
            _responsibilityReasonRepository
                .Verify(x => x.GetAll());
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
