using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    public class GetByIdAndCompanyId
    {
        private const long _company = 1;
        private const long _hazardousSubstanceId = 1;
        private Mock<IHazardousSubstancesRepository> repository;
        private Mock<IPeninsulaLog> log;
        private HazardousSubstancesService _target;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IHazardousSubstancesRepository>();
            log = new Mock<IPeninsulaLog>();
            _target = new HazardousSubstancesService(repository.Object, null, null, null, null, null, null, log.Object);
        }

        [Test]
        public void GetByIdAndCompanyId_Requests_HazardousSubstance_From_Repo()
        {
            // Given
            var returnedHazardousSubstances = new HazardousSubstance();

            repository.Setup(x => x.GetByIdAndCompanyId(_hazardousSubstanceId, _company)).Returns(returnedHazardousSubstances);

            // When
            var result = _target.GetByIdAndCompanyId(_hazardousSubstanceId, _company);

            // Then
            repository.Verify(x => x.GetByIdAndCompanyId(_hazardousSubstanceId, _company), Times.Once());
        }    
    }
}