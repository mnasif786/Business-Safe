using System;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class UpdateRiskAssessmentSummaryTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IFireRiskAssessmentRepository> _fireRiskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IChecklistRepository> _checklistRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private FireRiskAssessmentService _target;
        private SaveRiskAssessmentSummaryRequest _request;
        private UserForAuditing _user;
        private Employee _employee = new Employee();
        private readonly RiskAssessor _riskAssessor = new RiskAssessor();
        private Site _site = new Site();

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _fireRiskAssessmentRepository = new Mock<IFireRiskAssessmentRepository>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _employeeRepository = new Mock<IEmployeeRepository>();
            _checklistRepository = new Mock<IChecklistRepository>();
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();

            _request = new SaveRiskAssessmentSummaryRequest()
                       {
                           CompanyId = 100,
                           Reference = "Reference",
                           Title = "Title",
                           UserId = Guid.NewGuid(),
                           Id = 200,
                           AssessmentDate = DateTime.Now,
                           RiskAssessorId = 25L,
                           SiteId = 1,
                           PersonAppointed = "My Person Appointed"
                       };

            

            _target = CreateRiskAssessmentService();
        }

        [Test]
        public void Given_site_and_risk_assessor_set_When_UpdateRiskAssessmentSummaryTests_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessment = new Mock<FireRiskAssessment>();

            _fireRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id, _request.CompanyId))
                .Returns(riskAssessment.Object);

            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.RiskAssessorId.Value, _request.CompanyId))
                .Returns(_riskAssessor);

            _siteRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.SiteId.Value, _request.CompanyId))
                .Returns(_site);

            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId))
                .Returns(_user);
            

            // When
            _target.UpdateRiskAssessmentSummary(_request);

            // Then
            _userForAuditingRepository.VerifyAll();
            _employeeRepository.VerifyAll();
            _siteRepository.VerifyAll();
            _fireRiskAssessmentRepository.Verify(x => x.SaveOrUpdate(riskAssessment.Object));
            riskAssessment.Verify(x => x.UpdateSummary(_request.Title, _request.Reference, _request.PersonAppointed, _request.AssessmentDate, _riskAssessor, _site, _user));
        }

        [Test]
        public void Given_no_site_and_risk_assessor_set_When_UpdateRiskAssessmentSummaryTests_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessment = new Mock<FireRiskAssessment>();

            _fireRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id, _request.CompanyId))
                .Returns(riskAssessment.Object);
            
            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId))
                .Returns(_user);

            _request.SiteId = null;
            _request.RiskAssessorId = null;

            // When
            _target.UpdateRiskAssessmentSummary(_request);

            // Then
            _userForAuditingRepository.VerifyAll();
            _employeeRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()), Times.Never());
            _siteRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()), Times.Never());
            _siteRepository.VerifyAll();
            _fireRiskAssessmentRepository.Verify(x => x.SaveOrUpdate(riskAssessment.Object));
            riskAssessment.Verify(x => x.UpdateSummary(_request.Title, _request.Reference, _request.PersonAppointed, _request.AssessmentDate, null, null, _user));
        }
        
        [Test]
        public void Given_trying_to_update_reference_to_one_that_already_exists_When_Update_Summary_Then_should_throw_correct_error()
        {
            // Given
            var riskAssessment = new FireRiskAssessment();
            _fireRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id, _request.CompanyId))
                .Returns(riskAssessment);

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<FireRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()) )
                .Returns(true);

            _target = CreateRiskAssessmentService();
            
            // When
            // Then
            Assert.Throws<ValidationException>(() =>_target.UpdateRiskAssessmentSummary(_request));
;
        }

       
        private FireRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new FireRiskAssessmentService(
                _fireRiskAssessmentRepository.Object, 
                _userForAuditingRepository.Object, 
                _checklistRepository.Object,
                null, null, _log.Object
                , _riskAssessmentRepository.Object,null,_siteRepository.Object, 
                _riskAssessorRepository.Object);
            return riskAssessmentService;
        }
    }
}
