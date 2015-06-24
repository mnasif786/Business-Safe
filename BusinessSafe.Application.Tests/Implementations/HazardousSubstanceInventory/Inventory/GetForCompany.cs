using System;
using System.Collections.Generic;

using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Domain.InfrastructureContracts.Logging;

using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Tests.Implementations.HazardousSubstances.Inventory
{
    [TestFixture]
    public class GetForCompany
    {
        private Mock<IHazardousSubstancesRepository> repository;
        private Mock<IUserForAuditingRepository> userRepository;
        private Mock<ISupplierRepository> supplierRepository;
        private Mock<IRiskPhraseRepository> riskPhraseRepository;
        private Mock<ISafetyPhraseRepository> safetyPhraseRepository;
        private Mock<IPictogramRepository> pictogramRepository;
        private Mock<IPeninsulaLog> log;
        private HazardousSubstancesService _target;

        [SetUp]
        public void Setup()
        {
            repository = new Mock<IHazardousSubstancesRepository>();
            repository.Setup(x => x.GetForCompany(It.IsAny<long>())).Returns(new List<HazardousSubstance>());

            userRepository = new Mock<IUserForAuditingRepository>();
            userRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new UserForAuditing());

            pictogramRepository = new Mock<IPictogramRepository>();
            riskPhraseRepository = new Mock<IRiskPhraseRepository>();
            safetyPhraseRepository = new Mock<ISafetyPhraseRepository>();

            supplierRepository = new Mock<ISupplierRepository>();

            log = new Mock<IPeninsulaLog>();
            _target = new HazardousSubstancesService(
                repository.Object, 
                userRepository.Object,
                supplierRepository.Object,
                pictogramRepository.Object,
                riskPhraseRepository.Object, 
                safetyPhraseRepository.Object, null, log.Object);
        }

        [Test]
        public void GetForCompany_Requests_Substances_For_Company_From_Repo()
        {
            // Given
            var returnedHazardousSubstances = new List<HazardousSubstance>()
                                                  {
                                                      new HazardousSubstance()
                                                  };
            repository.Setup(x => x.GetForCompany(1234)).Returns(returnedHazardousSubstances);

            // When
            var result = _target.GetForCompany(1234);

            // Then
            repository.Verify(x => x.GetForCompany(1234), Times.Once());
        }

        [Test]
        public void GetForCompany_Maps_Entities_To_Dtos()
        {
            // Given
            var returnedHazardousSubstances = new List<HazardousSubstance>()
                                                  {
                                                      new HazardousSubstance()
                                                          {
                                                              
                                                          }
                                                  };
            repository.Setup(x => x.GetForCompany(1234)).Returns(returnedHazardousSubstances);

            // When
            var result = _target.GetForCompany(1234);

            // Then
            repository.Verify(x => x.GetForCompany(1234), Times.Once());
        }
    }
}
