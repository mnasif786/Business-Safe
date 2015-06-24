using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.InfrastructureContracts.Email;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class DelinkSiteTests
    {
        private Mock<ISiteRepository> _siteAddressRepository;
        private Mock<ISiteStructureElementRepository> _siteRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _siteAddressRepository = new Mock<ISiteRepository>();
            _siteRepository = new Mock<ISiteStructureElementRepository>();
            _log = new Mock<IPeninsulaLog>();
        }


        
        [Test]
        public void Given_that_delink_site_is_called_Then_delink_site_method_called_on_site_address_repository()
        {
            //Given
            var target = CreateSiteService();
            var delinkSiteRequest = DelinkSiteRequestBuilder.Create().Build();

            //When
            target.DelinkSite(delinkSiteRequest);

            //Then
            _siteAddressRepository.Verify(tp => tp.DelinkSite(delinkSiteRequest.SiteId, delinkSiteRequest.CompanyId), Times.Once());
        }

        private SiteService CreateSiteService()
        {
            return new SiteService(_siteAddressRepository.Object, _siteRepository.Object, null, null);
        }
    }
}