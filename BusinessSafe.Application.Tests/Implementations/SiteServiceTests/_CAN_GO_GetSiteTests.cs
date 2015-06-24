//todo: no longer used so can go.

//using System.Collections.Generic;
//using System.Linq;
//using BusinessSafe.Application.Implementations;
//using BusinessSafe.Application.Implementations.Sites;
//using BusinessSafe.Application.RestAPI;
//using BusinessSafe.Application.Tests.Builder;
//using BusinessSafe.Domain.Entities;
//using BusinessSafe.Domain.InfrastructureContracts.Email;
//using BusinessSafe.Domain.InfrastructureContracts.Logging;
//using BusinessSafe.Domain.RepositoryContracts;
//using Moq;
//using NUnit.Framework;

//namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
//{
//    [TestFixture]
//    [Category("Unit")]
//    public class GetSiteTests
//    {
//        private Mock<ISiteRepository> _siteRequestStructureRepository;
//        private Mock<IClientService> _clientService;
//        private Mock<IEmail> _email;
//        private Mock<IEmailTemplateRepository> _emailTemplateRepository;
//        //private Mock<ISiteFullStructureRepository> _siteFullStructureRepository;
//        private Mock<ISiteStructureElementRepository> _siteRepository;
//        private Mock<IPeninsulaLog> _log;

//        [SetUp]
//        public void SetUp()
//        {
//            _siteRequestStructureRepository = new Mock<ISiteRepository>();
//            _clientService = new Mock<IClientService>();
//            _email = new Mock<IEmail>();
//            _emailTemplateRepository = new Mock<IEmailTemplateRepository>();
//            //_siteFullStructureRepository = new Mock<ISiteFullStructureRepository>();
//            _siteRepository = new Mock<ISiteStructureElementRepository>();

//            var parentSiteAddress = Site.Create(2, 2, null, 1, "Parent Head Office", "ref1");
//            var siteAddress = Site.Create(1, 1, parentSiteAddress, 1, "Head Office", "ref1");
//            var siteRequestStructures = new List<Site>
//                                            {
//                                                siteAddress,
//                                                Site.Create(2, 2, siteAddress, 1, "Manchester", "ref2")
//                                            };
//            _siteRequestStructureRepository.Setup(sr => sr.GetSiteAddressByCompanyId(1)).Returns(siteRequestStructures);
//            _siteRequestStructureRepository.Setup(sr => sr.GetSiteStructureBySiteId(1)).Returns(siteAddress);


//            _log = new Mock<IPeninsulaLog>();
//        }

//        [Test]
//        public void Given_that_get_site_is_called_Then_client_service_called_once()
//        {
//            //Given
//            _clientService.Setup(cs => cs.GetSite(It.IsAny<long>(), It.IsAny<long>())).Returns(SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build());

//            var target = CreateSiteService();

//            //When
//            target.GetSite(-1, -1);

//            //Then
//            _clientService.Verify(scs => scs.GetSite(It.IsAny<long>(), It.IsAny<long>()), Times.Once());
//        }

//        [Test]
//        public void Given_that_get_site_is_called_Then_correct_site_address_dto_is_returned()
//        {
//            //Given
//            var expectedSiteAddress = SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build();
            
//            _clientService.Setup(scs => scs.GetSite(It.IsAny<long>(), It.IsAny<long>())).Returns(SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build());

//            var target = CreateSiteService();

//            //When
//            var result = target.GetSite(-1, -1);

//            //Then
//            Assert.That(result.AddressLine1, Is.EqualTo(expectedSiteAddress.AddressLine1));
//        }

//        [TestCase(1, 2)]
//        [TestCase(7, 100)]
//        public void Given_that_site_has_id_When_get_site_is_called_Then_correct_site_and_client_id_is_used(long expectedSiteId, long expectedClientId)
//        {
//            //Given
//            _clientService.Setup(cs => cs.GetSite(It.IsAny<long>(), It.IsAny<long>())).Returns(SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build());

//            var target = CreateSiteService();

//            //When
//            target.GetSite(expectedSiteId, expectedClientId);

//            //Then
//            _clientService.Verify(s => s.GetSite(expectedSiteId, expectedClientId));
//        }


//        [Test]
//        public void Given_that_two_linked_sites_exists_When_get_site_called_Then_linked_sites_is_populated()
//        {
//            //Given
//            _clientService.Setup(cs => cs.GetSite(It.IsAny<long>(), It.IsAny<long>())).Returns(SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build());

//            var target = CreateSiteService();

//            //When
//            var result = target.GetSite(1, 1);

//            //Then
//            Assert.That(result.LinkedSites.Count(), Is.EqualTo(1));
//        }

//        [Test]
//        public void Given_that_get_site_is_called_And_site_parent_is_null_Then_linked_sites_is_null()
//        {
//            //Given
//            long? siteId = 1;
            
//            var siteAddress = Site.Create(1, siteId, null, 1, "Head Office", "ref1");
            
//            _siteRequestStructureRepository.Setup(sr => sr.GetSiteStructureBySiteId(siteId.Value)).Returns(siteAddress);
//            _clientService.Setup(cs => cs.GetSite(It.IsAny<long>(), It.IsAny<long>())).Returns(SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build());

//            var target = CreateSiteService();

//            //When
//            var result = target.GetSite(1, 1);

//            //Then
//            Assert.That(result.LinkedSites.Count(), Is.EqualTo(0));
//        }


//        [Test]
//        public void Given_that_get_site_is_called_And_no_site_is_returned_Then_linked_sites_is_null2()
//        {
//            //Given
//            _siteRequestStructureRepository.Setup(sr => sr.GetSiteStructureBySiteId(It.IsAny<long>())).Returns(default(Site));
//            _clientService.Setup(cs => cs.GetSite(It.IsAny<long>(), It.IsAny<long>())).Returns(SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build());

//            var target = CreateSiteService();

//            //When
//            var result = target.GetSite(1, 1);

//            //Then
//            Assert.That(result, Is.Not.Null);
//        }

//        private SiteService CreateSiteService()
//        {
//            var target = new SiteService(_siteRequestStructureRepository.Object, _clientService.Object, _email.Object, _emailTemplateRepository.Object, null, _siteRepository.Object, _log.Object, null);
//            return target;
//        }
//    }
//}
