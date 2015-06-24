//todo: whats being tested here is no longer in use.

//using System.Collections.Generic;
//using BusinessSafe.Application.Contracts;
//using BusinessSafe.Application.DataTransferObjects;
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
//using StructureMap;

//namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
//{
//    [TestFixture]
//    [Category("Unit")]
//    public class BuildClassTests
//    {
//        private List<Site> _siteRequestStructures;
//        private CompanyDetailsDto _companyDetailsDto;
//        private List<SiteAddressDto> _siteAddressDtos;

//        private Mock<ISiteStructureElementRepository> _siteStructureElementRepository;
//        private Mock<ISiteRepository> _siteAddressRepository;
//        private Mock<IClientService> _clientService;
//        private Mock<IEmail> _email;
//        private Mock<IEmailTemplateRepository> _emailTemplateRepository;
//        //private Mock<ISiteFullStructureRepository> _siteFullStructureRepository;
//        private Mock<ITemplateEngine> _templateEngine;
//        private Mock<IPeninsulaLog> _log;

//        [SetUp]
//        public void SetUp()
//        {
//            _siteAddressRepository = new Mock<ISiteRepository>();
//            _clientService = new Mock<IClientService>();
//            _email = new Mock<IEmail>();
//            _emailTemplateRepository = new Mock<IEmailTemplateRepository>();
//            //_siteFullStructureRepository = new Mock<ISiteFullStructureRepository>();
//            _templateEngine = new Mock<ITemplateEngine>();
//            _siteStructureElementRepository = new Mock<ISiteStructureElementRepository>();

//            _log = new Mock<IPeninsulaLog>();

//            var siteAddress1 = Site.Create(1, 1, null, 1, "Head Office", "ref1");
//            var siteAddress2 = Site.Create(2, 2, siteAddress1, 1, "Manchester", "ref2");
//            var siteAddress3 = Site.Create(3, 3, siteAddress1, 1, "Hinckley", "ref3");
//            var siteAddress4 = Site.Create(4, 4, siteAddress1, 1, "Belfast", "ref4");
//            var siteAddress5 = Site.Create(5, 5, siteAddress1, 1, "Dublin", "ref5");
//            var siteAddress6 = Site.Create(6, 6, siteAddress1, 1, "Jersey", "ref6");
//            var siteAddress7 = Site.Create(7, 7, siteAddress2, 1, "Print Room", "ref7");
//            var siteAddress8 = Site.Create(8, 8, siteAddress2, 1, "Peninsula", "ref8");
//            siteAddress1.Children = new List<SiteStructureElement> { siteAddress2, siteAddress3, siteAddress4, siteAddress5, siteAddress6 };
//            siteAddress2.Children = new List<SiteStructureElement> { siteAddress7, siteAddress8 };

//            _siteRequestStructures = new List<Site>
//                                         {
//                                             siteAddress1,
//                                             siteAddress2,
//                                             siteAddress3,
//                                             siteAddress4,
//                                             siteAddress5,
//                                             siteAddress6,
//                                             siteAddress7,
//                                             siteAddress8
//                                         };

//            _siteAddressDtos = new List<SiteAddressDto>
//                                          {
//                                              SiteAddressDtoBuilder.Create().WithId(1).WithAddressLine1("Head Office").Build(),
//                                              SiteAddressDtoBuilder.Create().WithId(2).WithAddressLine1("Manchester").Build(),
//                                              SiteAddressDtoBuilder.Create().WithId(3).WithAddressLine1("Hinckley").Build(),
//                                              SiteAddressDtoBuilder.Create().WithId(4).WithAddressLine1("Belfast").Build(),
//                                              SiteAddressDtoBuilder.Create().WithId(5).WithAddressLine1("Dublin").Build(),
//                                              SiteAddressDtoBuilder.Create().WithId(6).WithAddressLine1("Jersey").Build(),
//                                              SiteAddressDtoBuilder.Create().WithId(7).WithAddressLine1("Print Room").Build(),
//                                              SiteAddressDtoBuilder.Create().WithId(8).WithAddressLine1("Peninsula").Build()
//                                          };

//            _companyDetailsDto = CompanyDetailsDtoBuilder.Create().WithId(1).Build();
//        }


//        [Test]
//        public void Given_that_main_site_does_not_exists_in_database_Then_a_new_record_is_added_for_main_site()
//        {
//            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());

//            //Given
//            const string siteNameExpected = "Main Site";
//            const int clientId = 1;

//            Site siteAddressSubmittedToRepo = null;

//            _siteAddressRepository.Setup(rm => rm.GetSiteAddressByCompanyId(It.IsAny<long>())).Returns(new List<Site>());
//            _siteAddressRepository.Setup(repo => repo.Save(It.IsAny<Site>())).Callback<Site>(sa => siteAddressSubmittedToRepo = sa);
//            _clientService.Setup(cs => cs.GetCompanyDetails(It.IsAny<long>())).Returns(_companyDetailsDto);

//            var target = CreateSiteService();

//            //When
//            target.GenerateSiteOrganisationalUnit(clientId);

//            //Then
//            //_siteAddressRepository.Verify(rm => rm.Save(It.IsAny<Site>()), Times.Once());
//            Assert.That(siteAddressSubmittedToRepo.Name, Is.EqualTo(siteNameExpected));
//        }


//        [Test]
//        public void Given_that_generate_site_organisation_is_called_Then_correct_number_of_childs_are_generated()
//        {
//            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());

//            //Given
//            const int clientId = 1;

//            _siteStructureElementRepository.Setup(rm => rm.GetByCompanyId(It.IsAny<long>())).Returns(_siteRequestStructures);
//            _clientService.Setup(cs => cs.GetCompanyDetails(It.IsAny<long>())).Returns(_companyDetailsDto);
//            _clientService.Setup(cs => cs.GetSites(It.IsAny<long>())).Returns(_siteAddressDtos);

//            var target = CreateSiteService();

//            //When
//            var result = target.GenerateSiteOrganisationalUnit(clientId);

//            //Then
//            Assert.That(result.Children.Count, Is.EqualTo(5));
//            Assert.That(result.Children[0].Children.Count, Is.EqualTo(2));
//        }

//        [Test]
//        public void Given_that_only_main_site_has_name_When_generate_site_called_Then_main_site_has_the_name_that_is_assigned_to()
//        {
//            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());

//            //Given
//            const string expectedMainName = "Expected Main Site Name";

//            _siteStructureElementRepository.Setup(sr => sr.GetByCompanyId(It.IsAny<long>())).Returns(new List<SiteStructureElement> { Site.Create(1, 1, null, 1, expectedMainName, "ref1") });
//            _clientService.Setup(cs => cs.GetCompanyDetails(It.IsAny<long>())).Returns(_companyDetailsDto);
//            _clientService.Setup(cs => cs.GetSites(It.IsAny<long>())).Returns(_siteAddressDtos);

//            var target = CreateSiteService();

//            //When
//            var result = target.GenerateSiteOrganisationalUnit(1);

//            //Then
//            Assert.That(result, Is.Not.Null);
//            Assert.That(result.Name, Is.EqualTo(expectedMainName));
//            Assert.That(result.Children.Count, Is.EqualTo(0));
//        }

//        [Test]
//        public void Given_that_there_is_is_one_linked_and_one_unlinked_sites_exists_Then_unlinked_site_is_added_to_unlinkedsites_list()
//        {
//            ObjectFactory.Inject<IPeninsulaLog>(new StubPeninsulaLog());

//            //Given
//            const string headOfficeName = "Head Office 2";
//            var siteRequestStructure = Site.Create(1, 1, null, 1, "Head Office 2", "ref1");
//            _siteStructureElementRepository.Setup(sr => sr.GetByCompanyId(It.IsAny<long>())).Returns(new List<Site> { siteRequestStructure });
//            _clientService.Setup(cs => cs.GetCompanyDetails(It.IsAny<long>())).Returns(CompanyDetailsDtoBuilder.Create().WithId(1).Build());
//            var target = CreateSiteService();
//            var siteDetailsDtoExpected1 = SiteAddressDtoBuilder.Create().WithId(1).Build();
//            var siteDetailsDtoExpected2 = SiteAddressDtoBuilder.Create().WithId(2).Build();

//            _clientService.Setup(cs => cs.GetSites(It.IsAny<long>())).Returns(new List<SiteAddressDto> { siteDetailsDtoExpected1, siteDetailsDtoExpected2 });

//            //When
//            var result = target.GenerateSiteOrganisationalUnit(1);

//            //Then
//            Assert.That(result.Name, Is.EqualTo(headOfficeName));
//            Assert.That(result.UnlinkedSites[0].GetType(), Is.EqualTo(typeof(SiteDetailsDto)));
//        }

//        private SiteService CreateSiteService()
//        {
//            var target = new SiteService(_siteAddressRepository.Object, _clientService.Object, _email.Object, _emailTemplateRepository.Object, _templateEngine.Object, _siteStructureElementRepository.Object, _log.Object, null);
//            return target;
//        }
//    }


//}
