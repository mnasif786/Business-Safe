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
    public class UpdateRiskAssessmentSummaryTests
    {
        private Mock<IPeninsulaLog> _log;
        private Mock<IGeneralRiskAssessmentRepository> _generalRiskAssessmentRepository;
        private Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private GeneralRiskAssessmentService _target;
        private SaveRiskAssessmentSummaryRequest _request;
        private readonly UserForAuditing _user =new UserForAuditing();
        private readonly Employee _employee = new Employee();
        private readonly Site _site = new Site();
        private readonly RiskAssessor _riskAssessor = new RiskAssessor();

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

            _request = new SaveRiskAssessmentSummaryRequest()
                       {
                           CompanyId = 100,
                           Reference = "Reference",
                           Title = "Title",
                           UserId = Guid.NewGuid(),
                           Id = 200,
                           AssessmentDate = DateTime.Now,
                           RiskAssessorId = 252L,
                           SiteId = 200
                       };

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<GeneralRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(false);

            _target = CreateRiskAssessmentService();
        }

        [Test]
        public void Given_site_and_risk_assessor_have_been_set_When_UpdateRiskAssessmentSummaryTests_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessment = new Mock<GeneralRiskAssessment>();
            
            _generalRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id, _request.CompanyId))
                .Returns(riskAssessment.Object);

            _siteRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.SiteId.Value, _request.CompanyId))
                .Returns(_site);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId))
                .Returns(_user);

            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.RiskAssessorId.Value, _request.CompanyId))
                .Returns(_riskAssessor);

            // When
            _target.UpdateRiskAssessmentSummary(_request);

            // Then
            _userRepository.VerifyAll();
            _employeeRepository.VerifyAll();
            _siteRepository.VerifyAll();
            riskAssessment.Verify(x => x.UpdateSummary(_request.Title, _request.Reference, _request.AssessmentDate, _riskAssessor, _site, _user));
            _generalRiskAssessmentRepository.Verify(x => x.SaveOrUpdate(riskAssessment.Object));
        }

        [Test]
        public void Given_no_site_and_risk_assessor_have_been_set_When_UpdateRiskAssessmentSummaryTests_Then_should_call_correct_methods()
        {
            // Given
            var riskAssessment = new Mock<GeneralRiskAssessment>();

            _generalRiskAssessmentRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.Id, _request.CompanyId))
                .Returns(riskAssessment.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(_request.UserId, _request.CompanyId))
                .Returns(_user);
            
            _request.SiteId = null;
            _request.RiskAssessorId = null;

            // When
            _target.UpdateRiskAssessmentSummary(_request);

            // Then
            _employeeRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()), Times.Never());
            _siteRepository.Verify(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()), Times.Never());
            _userRepository.VerifyAll();
            riskAssessment.Verify(x => x.UpdateSummary(_request.Title, _request.Reference, _request.AssessmentDate, null, null, _user));
            _generalRiskAssessmentRepository.Verify(x => x.SaveOrUpdate(riskAssessment.Object));
        }
       
      
        [Test]
        public void Given_GRA_With_Reference_X_When_Updating_Another_GRA_To_Set_Reference_To_X_Then_Throw_Error()
        {
            // Given
            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistWithTheSameReference<GeneralRiskAssessment>(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<long?>()))
                .Returns(true);

            // When
            // Then
            Assert.Throws<ValidationException>(() => _target.UpdateRiskAssessmentSummary(_request));
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
