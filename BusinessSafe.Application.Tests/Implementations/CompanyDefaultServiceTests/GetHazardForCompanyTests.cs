using System.Collections.Generic;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDefaultServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetHazardForCompanyTests
    {
        private Mock<IHazardRepository> _hazardRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<long>()));

            _hazardRepository = new Mock<IHazardRepository>();

        }

        [Test]
        public void When_GetHazardForCompany_is_called_Then_should_call_correct_methods()
        {
            // Given
            const long hazardId = 500;
            const long companyId = 3;

            _hazardRepository
                .Setup(x => x.GetByIdAndCompanyId(hazardId, companyId))
                .Returns(new Hazard());

            var target = CreateCompanyDefaultService();

            // When
            target.GetHazardForCompany(companyId, hazardId);

            // Then
            _hazardRepository.VerifyAll();
        }

        [Test]
        public void When_GetHazardForCompany_is_called_Then_should_map_correct_result()
        {
            // Given
            var graHazard = new Mock<HazardType>();
            graHazard.SetupGet(x => x.Id).Returns((int) HazardTypeEnum.General);

            var praHazard = new Mock<HazardType>();
            praHazard.SetupGet(x => x.Id).Returns((int)HazardTypeEnum.Personal);

            IList<HazardType> hazardTypes = new List<HazardType>()
                                                {
                                                    graHazard.Object,
                                                    praHazard.Object
                                                };
            var hazard = Hazard.Create("Name", 3, null, hazardTypes, null);
            
            _hazardRepository
                .Setup(x => x.GetByIdAndCompanyId(hazard.Id, hazard.CompanyId.Value))
                .Returns(hazard);

            var target = CreateCompanyDefaultService();

            // When
            var result = target.GetHazardForCompany(hazard.CompanyId.Value, hazard.Id);

            // Then
            Assert.That(result.Id, Is.EqualTo(hazard.Id));
            Assert.That(result.Name, Is.EqualTo(hazard.Name));
            Assert.That(result.IsGra, Is.EqualTo(true));
            Assert.That(result.IsPra, Is.EqualTo(true));
        }

        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(_hazardRepository.Object, null, null, null, null, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}