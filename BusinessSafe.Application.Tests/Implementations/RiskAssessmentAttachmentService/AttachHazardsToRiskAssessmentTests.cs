using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Implementations.MultiHazardRiskAssessment;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentAttachmentService
{
    [TestFixture]
    [Category("Unit")]
    public class AttachHazardsToRiskAssessmentTests
    {
        private Mock<IMultiHazardRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IHazardRepository> _hazardsRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IMultiHazardRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _hazardsRepository = new Mock<IHazardRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();

            _userRepository
               .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
               .Returns(_user);
        }

        [Test]
        public void Given_valid_request_When_AttachHazards_is_called_Then_should_call_appropiate_methods()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachHazardsRequest = new AttachHazardsToRiskAssessmentRequest()
            {
                RiskAssessmentId = 2,
                CompanyId = 1,
                UserId = Guid.NewGuid()
            };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                    .Setup(rr => rr.GetByIdAndCompanyId(attachHazardsRequest.RiskAssessmentId, attachHazardsRequest.CompanyId))
                    .Returns(mockRiskAssessment.Object);

            var hazards = new List<Hazard>()
                              {
                                  new Hazard(),
                                  new Hazard(),
                                  new Hazard()
                              };
            _hazardsRepository
                            .Setup(x => x.GetByIds(It.IsAny<IList<long>>()))
                            .Returns(hazards);

            //When
            riskAssessmentService.AttachHazardsToRiskAssessment(attachHazardsRequest);

            //Then
            _riskAssessmentRepository.Verify(x => x.SaveOrUpdate(mockRiskAssessment.Object));
            _userRepository.Verify(x => x.GetByIdAndCompanyId(attachHazardsRequest.UserId, attachHazardsRequest.CompanyId));
            mockRiskAssessment.Verify(x => x.AttachHazardsToRiskAssessment(hazards, _user));
        }

        [Test]
        public void Given_request_with_order_specifed_then_hazard_order_saved()
        {
            //Given
            var expectedOrderNumber = 3;
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachHazardsRequest = new AttachHazardsToRiskAssessmentRequest()
            {
                RiskAssessmentId = 2,
                CompanyId = 1,
                UserId = Guid.NewGuid(),
                Hazards = new List<AttachHazardsToRiskAssessmentHazardDetail>()
                {
                    new AttachHazardsToRiskAssessmentHazardDetail(){Id =3, OrderNumber = 1},
                    new AttachHazardsToRiskAssessmentHazardDetail(){Id =1, OrderNumber = 2},
                    new AttachHazardsToRiskAssessmentHazardDetail(){Id =2, OrderNumber = expectedOrderNumber}
                }
            };

            var mockRiskAssessment = new Mock<GeneralRiskAssessment>();
            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(attachHazardsRequest.RiskAssessmentId, attachHazardsRequest.CompanyId))
                .Returns(() => new GeneralRiskAssessment(){});

            var hazards = new List<Hazard>()
            {
                new Hazard(){Id =1},
                new Hazard(){Id =2},
                new Hazard(){Id =3}
            };
            _hazardsRepository.Setup(x => x.GetByIds(It.IsAny<List<long>>()))
                .Returns(() => hazards);

            MultiHazardRiskAssessment savedRiskAssessment = null;
            _riskAssessmentRepository.Setup(x => x.SaveOrUpdate(It.IsAny<MultiHazardRiskAssessment>()))
                .Callback<MultiHazardRiskAssessment>(parameter => savedRiskAssessment = parameter);

            //When
            riskAssessmentService.AttachHazardsToRiskAssessment(attachHazardsRequest);

            //Then
            Assert.IsNotNull(savedRiskAssessment);
            Assert.That(savedRiskAssessment.Hazards.Count(), Is.EqualTo(attachHazardsRequest.Hazards.Count));
            Assert.That(savedRiskAssessment.Hazards.First(x => x.Hazard.Id == 2).OrderNumber, Is.EqualTo(expectedOrderNumber));
            Assert.That(savedRiskAssessment.Hazards.First(x => x.Hazard.Id == 2).LastModifiedBy, Is.Not.Null);
            Assert.That(savedRiskAssessment.Hazards.First(x => x.Hazard.Id == 2).LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_a_request_to_update_order_specifed_then_hazard_order_saved()
        {
            //Given
            var expectedOrderNumber = 3;
            var riskAssessmentService = CreateRiskAssessmentService();
            var attachHazardsRequest = new AttachHazardsToRiskAssessmentRequest()
            {
                RiskAssessmentId = 2,
                CompanyId = 1,
                UserId = Guid.NewGuid(),
                Hazards = new List<AttachHazardsToRiskAssessmentHazardDetail>()
                {
                    new AttachHazardsToRiskAssessmentHazardDetail(){Id =3, OrderNumber = 1},
                    new AttachHazardsToRiskAssessmentHazardDetail(){Id =1, OrderNumber = 2},
                    new AttachHazardsToRiskAssessmentHazardDetail(){Id =2, OrderNumber = expectedOrderNumber}
                }
            };

            var generalRiskAss = new GeneralRiskAssessment();
            generalRiskAss.AttachHazardToRiskAssessment(new Hazard(){Id = 1}, null);
            generalRiskAss.AttachHazardToRiskAssessment(new Hazard(){Id = 2}, null);
            generalRiskAss.AttachHazardToRiskAssessment(new Hazard(){Id = 3}, null);

            _riskAssessmentRepository
                .Setup(rr => rr.GetByIdAndCompanyId(attachHazardsRequest.RiskAssessmentId, attachHazardsRequest.CompanyId))
                .Returns(() => generalRiskAss);

            var hazards = new List<Hazard>()
            {
                new Hazard(){Id =1},
                new Hazard(){Id =2},
                new Hazard(){Id =3}
            };
            _hazardsRepository.Setup(x => x.GetByIds(It.IsAny<List<long>>()))
                .Returns(() => hazards);

            MultiHazardRiskAssessment savedRiskAssessment = null;
            _riskAssessmentRepository.Setup(x => x.SaveOrUpdate(It.IsAny<MultiHazardRiskAssessment>()))
                .Callback<MultiHazardRiskAssessment>(parameter => savedRiskAssessment = parameter);

            //When
            riskAssessmentService.AttachHazardsToRiskAssessment(attachHazardsRequest);

            //Then
            Assert.IsNotNull(savedRiskAssessment);
            Assert.That(savedRiskAssessment.Hazards.Count(), Is.EqualTo(attachHazardsRequest.Hazards.Count));
            Assert.That(savedRiskAssessment.Hazards.First(x => x.Hazard.Id == 2).OrderNumber, Is.EqualTo(expectedOrderNumber));
            Assert.That(savedRiskAssessment.Hazards.First(x => x.Hazard.Id == 2).LastModifiedBy, Is.Not.Null);
            Assert.That(savedRiskAssessment.Hazards.First(x => x.Hazard.Id == 2).LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        private MultiHazardRiskAssessmentAttachmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new MultiHazardRiskAssessmentAttachmentService(_riskAssessmentRepository.Object, _hazardsRepository.Object, _userRepository.Object, _log.Object);
            return riskAssessmentService;
        }
    }
}