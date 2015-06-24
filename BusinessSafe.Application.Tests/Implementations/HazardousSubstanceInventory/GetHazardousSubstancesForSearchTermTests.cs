using System.Linq;

using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstances
{
    [TestFixture]
    public class GetHazardousSubstancesForSearchTermTests
    {
        private Mock<IHazardousSubstancesRepository> hazardousSubstanceRepository;
        private Mock<IUserForAuditingRepository> userRepository;
        private Mock<ISupplierRepository> supplierRepository;
        private Mock<IRiskPhraseRepository> riskPhraseRepository;
        private Mock<ISafetyPhraseRepository> safetyPhraseRepository;
        private Mock<IPictogramRepository> pictogramRepository;
        private HazardousSubstancesService target;
        private Mock<IPeninsulaLog> log;

        [SetUp]
        public void Setup()
        {
            hazardousSubstanceRepository = new Mock<IHazardousSubstancesRepository>();
            supplierRepository = new Mock<ISupplierRepository>();
            userRepository = new Mock<IUserForAuditingRepository>();
            pictogramRepository = new Mock<IPictogramRepository>();
            riskPhraseRepository = new Mock<IRiskPhraseRepository>();
            safetyPhraseRepository = new Mock<ISafetyPhraseRepository>();
            log = new Mock<IPeninsulaLog>();

            target = new HazardousSubstancesService(
                hazardousSubstanceRepository.Object,
                userRepository.Object,
                supplierRepository.Object,
                pictogramRepository.Object,
                riskPhraseRepository.Object,
                safetyPhraseRepository.Object, null, log.Object);
        }

        [Test]
        public void When_GetHazardousSubstancesForSearchTerm_Then_should_call_correct_methods()
        {
            // Given
            string term = "anything";
            long companyId = 1;
            int pageLimit = 100;

            var hazardousSubstances = new HazardousSubstance[] { };
            hazardousSubstanceRepository
                .Setup(x => x.GetByTermSearch(term, companyId, pageLimit))
                .Returns(hazardousSubstances);

            // When
            target.GetHazardousSubstancesForSearchTerm(term, companyId, pageLimit);

            // Then
            hazardousSubstanceRepository.VerifyAll();
        }

        [Test]
        public void When_GetHazardousSubstancesForSearchTerm_Then_should_return_correct_results()
        {
            // Given
            string term = "anything";
            long companyId = 1;
            int pageLimit = 100;

            var hazardousSubstances = new[]
                                          {
                                              new HazardousSubstance()
                                                  {
                                                      Id = 200, Name = "Beauty"
                                                  }, 
                                              new HazardousSubstance()
                                                  {
                                                      Id = 300,
                                                      Name = "Beast"
                                                  }
                                          };
            hazardousSubstanceRepository
                .Setup(x => x.GetByTermSearch(term, companyId, pageLimit))
                .Returns(hazardousSubstances);

            // When
            var result = target.GetHazardousSubstancesForSearchTerm(term, companyId, pageLimit);

            // Then
            Assert.That(result.Count(), Is.EqualTo(hazardousSubstances.Length));
            Assert.That(result.First().Id, Is.EqualTo(hazardousSubstances.First().Id));
            Assert.That(result.First().Name, Is.EqualTo(hazardousSubstances.First().Name));
        }

    }
}