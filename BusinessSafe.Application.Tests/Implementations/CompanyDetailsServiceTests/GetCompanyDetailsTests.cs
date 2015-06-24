using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDetailsServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetCompanyDetailsTests
    {
        [Test]
        public void Given_that_GetCompanyDetails_is_called_Then_GetCompanyDetails_in_ClientService_is_called()
        {
            //Given
            var clientService = new Mock<IClientService>();
            var log = new Mock<IPeninsulaLog>();
            var companyDetailsService = CreateCompanyDetailsService(clientService, log);

            //When
            companyDetailsService.GetCompanyDetails(0);

            //Then
            clientService.Verify(cs=>cs.GetCompanyDetails(0),Times.Once());
        }

        private static CompanyDetailsService CreateCompanyDetailsService(Mock<IClientService> clientService, Mock<IPeninsulaLog> log)
        {
            var companyDetailsService = new CompanyDetailsService(clientService.Object, log.Object,null);
            return companyDetailsService;
        }

        [Test]
        public void Given_that_GetCompanyDetails_is_called_Then_GetCompanyDetails_in_ClientService_is_returning_an_object()
        {
            //Given
            var clientService = new Mock<IClientService>();
            var log = new Mock<IPeninsulaLog>();
            clientService.Setup(sc => sc.GetCompanyDetails(It.IsAny<long>())).Returns(new CompanyDetailsDto());
            var companyDetailsService = CreateCompanyDetailsService(clientService, log);

            //When
            var result = companyDetailsService.GetCompanyDetails(0);

            //Then
            Assert.That(result,Is.Not.Null);
        }
    }
}
