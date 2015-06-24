using System;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    public class UpdateRiskAssessmentTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IGeneralRiskAssessmentRepository> _generalRiskAssessmentRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private GeneralRiskAssessmentService _target;
        private readonly UserForAuditing _user =new UserForAuditing();
        private readonly Site _site = new Site(){Id=445896};
        private readonly RiskAssessor _riskAssessor = new RiskAssessor(){Id=132847};
        private GeneralRiskAssessment _riskAssessment;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _generalRiskAssessmentRepository = new Mock<IGeneralRiskAssessmentRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();

            _riskAssessment = new GeneralRiskAssessment() {Id = 123,CompanyId = 12312, RiskAssessmentSite = new Site(){Id=13123}, RiskAssessor = new RiskAssessor(){Id=16209478}};

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<GeneralRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(false);

            _generalRiskAssessmentRepository
               .Setup(x => x.GetByIdAndCompanyId(_riskAssessment.Id, _riskAssessment.CompanyId))
               .Returns(() => _riskAssessment);

            _siteRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _site);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => _user);

            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _riskAssessor);

            _target = CreateRiskAssessmentService();
        }

        [Test]
        public void Given_site_and_risk_assessor_have_been_set_When_UpdateRiskAssessment_Then_site_should_be_set()
        {
            // Given
            var request = new SaveGeneralRiskAssessmentRequest()
            {
                CompanyId = _riskAssessment.CompanyId,
                Reference = "Reference",
                Title = "Title",
                UserId = Guid.NewGuid(),
                Id = _riskAssessment.Id,
                AssessmentDate = DateTime.Now,
                RiskAssessorId = _riskAssessor.Id,
                SiteId = _site.Id
            };

            // When
            _target.UpdateRiskAssessment(request);

            // Then
            Assert.IsNotNull(_riskAssessment.RiskAssessmentSite);
            Assert.AreEqual(request.SiteId, _riskAssessment.RiskAssessmentSite.Id);

            Assert.IsNotNull(_riskAssessment.RiskAssessor);
            Assert.AreEqual(request.RiskAssessorId, _riskAssessment.RiskAssessor.Id);
        }

        [Test]
        public void Given_site_not_set_When_UpdateRiskAssessment_Then_site_is_null()
        {
            // Given
            var request = new SaveGeneralRiskAssessmentRequest()
            {
                CompanyId = _riskAssessment.CompanyId,
                Reference = "Reference",
                Title = "Title",
                UserId = Guid.NewGuid(),
                Id = _riskAssessment.Id,
                AssessmentDate = DateTime.Now,
                RiskAssessorId = 252L,
                SiteId = null
            };

            // When
            _target.UpdateRiskAssessment(request);

            // Then
            Assert.IsNull(_riskAssessment.RiskAssessmentSite);
        }

        [Test]
        public void Given_riskassessor_not_set_When_UpdateRiskAssessment_Then_riskassessor_is_null()
        {
            // Given
            var request = new SaveGeneralRiskAssessmentRequest()
            {
                CompanyId = _riskAssessment.CompanyId,
                Reference = "Reference",
                Title = "Title",
                UserId = Guid.NewGuid(),
                Id = _riskAssessment.Id,
                AssessmentDate = DateTime.Now,
                RiskAssessorId = null,
                SiteId = 23523
            };

            // When
            _target.UpdateRiskAssessment(request);

            // Then
            Assert.IsNull(_riskAssessment.RiskAssessor);
        }

        [Test]
        public void Given_values_have_changed_When_UpdateRiskAssessment_Then_GRA_updated()
        {
            // Given
            var request = new SaveGeneralRiskAssessmentRequest()
            {
                CompanyId = _riskAssessment.CompanyId,
                Reference = "new Reference",
                Title = "new Title",
                UserId = Guid.NewGuid(),
                Id = _riskAssessment.Id,
                AssessmentDate = DateTime.Now.Date,
                RiskAssessorId = _riskAssessor.Id,
                SiteId = _site.Id,
                Location = "the new location",
                TaskProcessDescription =  "the new task process description"
            };

            // When
            _target.UpdateRiskAssessment(request);

            //then
            Assert.AreEqual(request.Reference, _riskAssessment.Reference);
            Assert.AreEqual(request.Title, _riskAssessment.Title);
            Assert.AreEqual(request.AssessmentDate, _riskAssessment.AssessmentDate);
            Assert.AreEqual(request.Location, _riskAssessment.Location);
            Assert.AreEqual(request.TaskProcessDescription,_riskAssessment.TaskProcessDescription);
            Assert.AreEqual(_riskAssessor.Id,_riskAssessment.RiskAssessor.Id);
            Assert.AreEqual(_site.Id, _riskAssessment.RiskAssessmentSite.Id);

        }


        [Test]
        public void Given_GRA_With_Reference_X_When_Updating_Another_GRA_To_Set_Reference_To_X_Then_Throw_Error()
        {
            // Given
            var request = new SaveGeneralRiskAssessmentRequest()
            {
                CompanyId = _riskAssessment.CompanyId,
                Reference = "Reference",
                Title = "Title",
                UserId = Guid.NewGuid(),
                Id = _riskAssessment.Id,
                AssessmentDate = DateTime.Now,
                RiskAssessorId = 252L,
                SiteId = 200
            };

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<GeneralRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(true);

            // When
            // Then
            Assert.Throws<ValidationException>(() => _target.UpdateRiskAssessment(request));
        }
        
        private GeneralRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new GeneralRiskAssessmentService(
                _generalRiskAssessmentRepository.Object, 
                _riskAssessmentRepository.Object, 
                _userRepository.Object, 
                _employeeRepository.Object, 
                _log.Object, 
                _siteRepository.Object,
                _riskAssessorRepository.Object);

            return riskAssessmentService;
        }
    }
}
