using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetPeopleAtRiskDisplayWarningMessagesTests
    {
        private Mock<IGeneralRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private long riskAssessmentId = 1;
        private long companyId = 2;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IGeneralRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_get_people_at_risk_display_warning_messages_Then_calls_the_correct_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var peopleAtRisk = new List<RiskAssessmentPeopleAtRisk>();
            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment.Setup(x => x.PeopleAtRisk).Returns(peopleAtRisk);

            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(riskAssessmentId, companyId))
                .Returns(mockRiskAssessment.Object);


            //When
            riskAssessmentService.GetPeopleAtRiskDisplayWarningMessages(riskAssessmentId, companyId);

            //Then
            _riskAssessmentRepository.Verify(x => x.GetByIdAndCompanyId(riskAssessmentId, companyId));
        }

        [Test]
        public void Given_one_people_at_risk_with_display_warning_messages_When_get_people_at_risk_display_warning_messages_Then_returns_correct_result()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var peopleAtRisk = new List<RiskAssessmentPeopleAtRisk>()
                                   {
                                       new RiskAssessmentPeopleAtRisk
                                           {
                                               PeopleAtRisk = new PeopleAtRisk()
                                                                  {
                                                                      Id =
                                                                          PeopleAtRiskWarningDisplayMessages.
                                                                          NewAndExpectantMothersId
                                                                  }
                                           }
                                   };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment.Setup(x => x.PeopleAtRisk).Returns(peopleAtRisk);

            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(riskAssessmentId, companyId))
                .Returns(mockRiskAssessment.Object);


            //When
            var result =riskAssessmentService.GetPeopleAtRiskDisplayWarningMessages(riskAssessmentId, companyId);

            //Then
            Assert.That(result.Count(),Is.EqualTo(1));
        }

        [Test]
        public void Given_two_people_at_risk_with_same_display_warning_messages_When_get_people_at_risk_display_warning_messages_Then_returns_correct_result()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var peopleAtRisk = new List<RiskAssessmentPeopleAtRisk>()
                                   {
                                       new RiskAssessmentPeopleAtRisk { PeopleAtRisk = new PeopleAtRisk()
                                           {
                                               Id = PeopleAtRiskWarningDisplayMessages.NewAndExpectantMothersId
                                           }},
                                       new RiskAssessmentPeopleAtRisk { PeopleAtRisk = new PeopleAtRisk()
                                           {
                                               Id = PeopleAtRiskWarningDisplayMessages.ChildrenAndYoungPersonsId
                                           }}
                                   };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment.Setup(x => x.PeopleAtRisk).Returns(peopleAtRisk);

            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(riskAssessmentId, companyId))
                .Returns(mockRiskAssessment.Object);


            //When
            var result = riskAssessmentService.GetPeopleAtRiskDisplayWarningMessages(riskAssessmentId, companyId);

            //Then
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_one_people_at_risk_with_no_display_warning_messages_When_get_people_at_risk_display_warning_messages_Then_returns_correct_result()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();

            var peopleAtRisk = new List<RiskAssessmentPeopleAtRisk>()
                                   {
                                       new RiskAssessmentPeopleAtRisk { PeopleAtRisk = new PeopleAtRisk()
                                           {
                                               Id = 9999999
                                           }}
                                   };
            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            mockRiskAssessment.Setup(x => x.PeopleAtRisk).Returns(peopleAtRisk);

            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(riskAssessmentId, companyId))
                .Returns(mockRiskAssessment.Object);


            //When
            var result = riskAssessmentService.GetPeopleAtRiskDisplayWarningMessages(riskAssessmentId, companyId);

            //Then
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        private GeneralRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new GeneralRiskAssessmentService(
                _riskAssessmentRepository.Object, 
                null, 
                _userRepository.Object,
                null, 
                _log.Object, 
                null,
                null);

            return riskAssessmentService;
        }
    }
}