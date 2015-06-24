using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstances.Inventory
{
    [TestFixture]
    public class Create
    {
        private Mock<IHazardousSubstancesRepository> hazardousSubstanceRepository;
        private Mock<IUserForAuditingRepository> userRepository;
        private Mock<ISupplierRepository> supplierRepository;
        private Mock<IPictogramRepository> pictogramRepository;
        private Mock<IRiskPhraseRepository> riskPhraseRepository;
        private Mock<ISafetyPhraseRepository> safetyPhraseRepository;
        private HazardousSubstancesService target;
        private Mock<IPeninsulaLog> log;

        [SetUp]
        public void Setup()
        {
            hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
            hazardousSubstanceRepository.Setup(x => x.SaveOrUpdate(It.IsAny<HazardousSubstance>()));
            userRepository = new Mock<IUserForAuditingRepository>();
            userRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new UserForAuditing());
            supplierRepository = new Mock<ISupplierRepository>();
            pictogramRepository = new Mock<IPictogramRepository>();
            riskPhraseRepository = new Mock<IRiskPhraseRepository>();
            safetyPhraseRepository = new Mock<ISafetyPhraseRepository>();
            log = new Mock<IPeninsulaLog>();
            log.Setup(x => x.Add(It.IsAny<Exception>()));
            log.Setup(x => x.Add(It.IsAny<object>()));

            target = new HazardousSubstancesService(
                hazardousSubstanceRepository.Object, 
                userRepository.Object,
                supplierRepository.Object,
                pictogramRepository.Object,
                riskPhraseRepository.Object, 
                safetyPhraseRepository.Object, null, log.Object);
        }

        [Test]
        public void On_create_logs_stuff()
        {
            // Given
            var request = new AddHazardousSubstanceRequest();

            // When
            target.Add(request);

            // Then
            log.Verify(x => x.Add(request), Times.Once());
        }

        [Test]
        public void On_create_passes_new_hazardous_substance_to_repo()
        {
            // When
            target.Add(new AddHazardousSubstanceRequest());

            // Then
            hazardousSubstanceRepository.Verify(x => x.SaveOrUpdate(It.IsAny<HazardousSubstance>()), Times.Once());
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void On_repo_throwing_exception_log_it_and_throw_it()
        {
            // Given
            var anException = new Exception();
            hazardousSubstanceRepository.Setup(x => x.SaveOrUpdate(It.IsAny<HazardousSubstance>())).Throws(anException);

            // When
            target.Add(new AddHazardousSubstanceRequest());

            // Then
            log.Verify(x => x.Add(anException), Times.Once());

        }

        [Test]
        public void On_create_gets_user_from_user_repo()
        {
            // Given
            var userId = Guid.NewGuid();
            var companyId = 1234;

            // When
            target.Add(new AddHazardousSubstanceRequest()
                          {
                              UserId = userId,
                              CompanyId = companyId
                          });

            // Then
            userRepository.Verify(x => x.GetByIdAndCompanyId(userId, companyId), Times.Once());
        }

        [Test]
        public void On_create_populates_new_hazardous_substance()
        {
            // Given
            var userId = Guid.NewGuid();
            var returnedUser = new UserForAuditing()
                               {
                                   CompanyId = 5678,
                                   Id = userId
                               };
            userRepository
                .Setup(x => x.GetByIdAndCompanyId(userId, 5678)).Returns(returnedUser);

            supplierRepository
                .Setup(x => x.GetByIdAndCompanyId(1234, 5678))
                .Returns(new Supplier() { Id = 1234, Name = "Test Supplier 1" });

            var passedRequest = new HazardousSubstance();
            var request = new AddHazardousSubstanceRequest()
                          {
                              CompanyId = 5678,
                              UserId = userId,
                              Name = "new title",
                              Reference = "new reference",
                              SdsDate = new DateTime(2012, 6, 23),
                              SupplierId = 1234
                          };

            hazardousSubstanceRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<HazardousSubstance>()))
                .Callback<HazardousSubstance>(y => passedRequest = y);

            // When
            target.Add(request);

            // Then
            Assert.That(passedRequest.Name, Is.EqualTo(request.Name));
            Assert.That(passedRequest.CompanyId, Is.EqualTo(request.CompanyId));
            Assert.That(passedRequest.CreatedBy, Is.EqualTo(returnedUser));
            Assert.That(passedRequest.CreatedBy.Id, Is.EqualTo(request.UserId));
            Assert.That(passedRequest.Reference, Is.EqualTo(request.Reference));
            Assert.That(passedRequest.SdsDate, Is.EqualTo(request.SdsDate));
            Assert.That(passedRequest.Supplier.Id, Is.EqualTo(request.SupplierId));
        }

        [Test]
        public void Given_risk_and_safety_phrases_are_included_When_add_is_clicked_then_risk_and_safety_phrases_persited_against_substance()
        {
            //Given
            userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), 5678)).Returns(new UserForAuditing());

            supplierRepository
                .Setup(x => x.GetByIdAndCompanyId(1234, 5678))
                .Returns(new Supplier() { Id = 1234, Name = "Test Supplier 1" });

            riskPhraseRepository
                .Setup(x => x.GetByIds(new long[] { 5L, 6L, 7L }))
                .Returns(new List<RiskPhrase>
                         {
                             new RiskPhrase() { Id = 5, Title = "RX05" },
                             new RiskPhrase() { Id = 6, Title = "RX06" },
                             new RiskPhrase() { Id = 7, Title = "RX07" },
                         });

            safetyPhraseRepository
                .Setup(x => x.GetByIds(new long[] { 8L, 9L, 10L }))
                .Returns(new List<SafetyPhrase>
                         {
                             new SafetyPhrase() { Id = 8, Title = "SX08" },
                             new SafetyPhrase() { Id = 9, Title = "SX09" },
                             new SafetyPhrase() { Id = 10, Title = "SX10" },
                         });

            var passedRequest = new HazardousSubstance();
            var request = new AddHazardousSubstanceRequest()
                          {
                              CompanyId = 5678,
                              Name = "new title",
                              RiskPhraseIds = new long[] { 5L, 6L, 7L },
                              SafetyPhraseIds = new long[] { 8L, 9L, 10L,},
                              AdditionalInformation = new List<SafetyPhraseAdditionalInformationRequest>()
                                                          {
                                                              new SafetyPhraseAdditionalInformationRequest()
                                                                  {
                                                                      SafetyPhaseId =  8L,
                                                                      AdditionalInformation = "Testing Additional Information"
                                                                  }
                                                          }
                          };

            hazardousSubstanceRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<HazardousSubstance>()))
                .Callback<HazardousSubstance>(y => passedRequest = y);

            // When
            target.Add(request);

            // Then
            var riskPhrases = passedRequest.HazardousSubstanceRiskPhrases.Select(x => x.RiskPhrase).ToList();
            Assert.That(riskPhrases.Count, Is.EqualTo(3));
            Assert.That(riskPhrases[0].Title, Is.EqualTo("RX05"));
            Assert.That(riskPhrases[1].Title, Is.EqualTo("RX06"));
            Assert.That(riskPhrases[2].Title, Is.EqualTo("RX07"));
            
            var safetyPhrases = passedRequest.HazardousSubstanceSafetyPhrases.ToList();
            Assert.That(safetyPhrases.Count, Is.EqualTo(3));
            Assert.That(safetyPhrases[0].SafetyPhrase.Title, Is.EqualTo("SX08"));
            Assert.That(safetyPhrases[0].AdditionalInformation, Is.EqualTo("Testing Additional Information"));
            Assert.That(safetyPhrases[1].SafetyPhrase.Title, Is.EqualTo("SX09"));
            Assert.That(safetyPhrases[2].SafetyPhrase.Title, Is.EqualTo("SX10"));
        }



        [Test]
        public void Given_pictograms_are_included_When_add_is_clicked_then_pictograms_persisted_against_substance()
        {
            //Given
            userRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), 5678)).Returns(new UserForAuditing());

            supplierRepository
                .Setup(x => x.GetByIdAndCompanyId(1234, 5678))
                .Returns(new Supplier() { Id = 1234, Name = "Test Supplier 1" });

            var requestedPictogramIds = new long[0];

            pictogramRepository
                .Setup(x => x.GetByIds(new long[] { 1L, 2L, 3L }))
                .Returns(new List<Domain.Entities.Pictogram>()
                         {
                             new Domain.Entities.Pictogram() { Id = 1, Title = "Picto 1", HazardousSubstanceStandard = HazardousSubstanceStandard.Global},
                             new Domain.Entities.Pictogram() { Id = 2, Title = "Picto 2", HazardousSubstanceStandard = HazardousSubstanceStandard.Global},
                             new Domain.Entities.Pictogram() { Id = 3, Title = "Picto 3", HazardousSubstanceStandard = HazardousSubstanceStandard.Global}
                         })
                .Callback<long[]>(y => requestedPictogramIds = y);

            var passedRequest = new HazardousSubstance();
            var request = new AddHazardousSubstanceRequest()
            {
                CompanyId = 5678,
                Name = "new title",
                PictogramIds = new long[] { 1L, 2L, 3L  }

            };

            hazardousSubstanceRepository
                .Setup(x => x.SaveOrUpdate(It.IsAny<HazardousSubstance>()))
                .Callback<HazardousSubstance>(y => passedRequest = y);

            // When
            target.Add(request);

            // Then
            pictogramRepository.Verify(x => x.GetByIds(new long[] { 1L, 2L, 3L }), Times.Once());

            var pictograms = passedRequest.HazardousSubstancePictograms.Select(hs => hs.Pictogram).ToList();
            Assert.That(pictograms.Count, Is.EqualTo(3));
            Assert.That(pictograms[0].Title, Is.EqualTo("Picto 1"));
            Assert.That(pictograms[1].Title, Is.EqualTo("Picto 2"));
            Assert.That(pictograms[2].Title, Is.EqualTo("Picto 3"));
            Assert.That(pictograms[0].Id, Is.EqualTo(1));
            Assert.That(pictograms[1].Id, Is.EqualTo(2));
            Assert.That(pictograms[2].Id, Is.EqualTo(3));
            Assert.That(pictograms[0].HazardousSubstanceStandard, Is.EqualTo(HazardousSubstanceStandard.Global));
            Assert.That(pictograms[1].HazardousSubstanceStandard, Is.EqualTo(HazardousSubstanceStandard.Global));
            Assert.That(pictograms[2].HazardousSubstanceStandard, Is.EqualTo(HazardousSubstanceStandard.Global));
        }
    }
}
