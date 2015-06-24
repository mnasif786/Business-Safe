using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;
using BusinessSafe.Application.RestAPI;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.SiteDetailsViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<ISiteService> siteService;
        private Mock<IClientService> _clientService;
        private Mock<ISiteGroupService> siteGroupService;
        
        [SetUp]
        public void SetUp()
        {
            siteService= new Mock<ISiteService>();    
            siteGroupService = new Mock<ISiteGroupService>();
            _clientService = new Mock<IClientService>();
        }
   

        [Test]
        public void Given_call_get_view_model_Then_calls_appropiate_service_methods_for_not_main_site()
        {
            //Given
            const long id = 99;
            const long siteId = 1;
            const long companyId = 10;

            var siteDto = new SiteDto { Id = id, SiteId = siteId };


            siteService.Setup(ss => ss.GetByIdAndCompanyId(id, companyId)).Returns(siteDto);

            _clientService.Setup(x => x.GetSite(companyId, siteId)).Returns(new SiteAddressDto(siteId, "Add 1", "Add 2",
                                                                                               "Add 3", "Add 4", "Add 5", "county", "PC",
                                                                                               "tel", null));

            var target = CreateSiteDetailsViewModelFactory();

            //When
            target
                .WithClientId(companyId)
                .WithId(id)
                .GetViewModel();

            //Then
            siteService.Verify(ss => ss.GetByIdAndCompanyId(id, companyId), Times.Once());
            _clientService.Verify(x => x.GetSite(companyId, siteId), Times.Once());
            siteService.Verify(x => x.GetByCompanyIdNotIncluding(companyId, siteId), Times.Never());
            siteGroupService.Verify(x => x.GetByCompanyId(companyId), Times.Never());
        }

        [Test]
        public void Given_call_get_view_model_Then_calls_appropiate_service_methods_for_main_site()
        {
            //Given
            const long id = 99;
            const long siteId = 1;
            const long companyId = 10;
            const long parentId = 2;


            var siteDto = new SiteDto {Id = id, SiteId = siteId, Parent = new SiteStructureElementDto {Id = parentId}};
      
            siteService.Setup(ss => ss.GetByIdAndCompanyId(id, companyId)).Returns(siteDto);

            _clientService.Setup(x => x.GetSite(companyId, siteId)).Returns(new SiteAddressDto(siteId, "Add 1", "Add 2",
                                                                                               "Add 3", "Add 4", "Add 5", "county", "PC",
                                                                                               "tel", null));
            var target = CreateSiteDetailsViewModelFactory();

            //When
            target
                .WithClientId(companyId)
                .WithId(id)
                .GetViewModel();

            //Then
            siteService.Verify(ss => ss.GetByIdAndCompanyId(id, companyId), Times.Once());
            _clientService.Verify(x => x.GetSite(companyId, siteId), Times.Once());
            siteService.Verify(x => x.GetByCompanyIdNotIncluding(companyId, siteId), Times.Once());
            siteGroupService.Verify(x => x.GetByCompanyIdExcludingSiteGroup(companyId,siteId), Times.Once());
        }

        [Test]
        public void Given_call_get_view_model_Then_calls_appropiate_service_methods_for_unlisted_site_address()
        {
            //Given
            const long id = 99;
            const long siteId = 1;
            const long companyId = 10;


            var siteDto = new SiteDto {  SiteId = siteId };

            siteService.Setup(ss => ss.GetByIdAndCompanyId(id, companyId)).Returns(siteDto);

            _clientService.Setup(x => x.GetSite(companyId, siteId)).Returns(new SiteAddressDto(siteId, "Add 1", "Add 2",
                                                                                               "Add 3", "Add 4", "Add 5", "county", "PC",
                                                                                               "tel", null));

            var target = CreateSiteDetailsViewModelFactory();

            //When
            target
                .WithClientId(companyId)
                .WithId(id)
                .GetViewModel();

            //Then
            siteService.Verify(ss => ss.GetByIdAndCompanyId(id, companyId), Times.Once());
            _clientService.Verify(x => x.GetSite(companyId, siteId), Times.Once());
            siteService.Verify(x => x.GetByCompanyIdNotIncluding(companyId, siteId), Times.Once());
            siteGroupService.Verify(x => x.GetByCompanyIdExcludingSiteGroup(companyId, siteId), Times.Once());
        }


        [Test]
        public void When_get_view_model_Then_should_order_existing_sites()
        {
            // Arrange
            var siteDto = new SiteDto { SiteId = 1 };

            siteService.Setup(ss => ss.GetByIdAndCompanyId(1, 1)).Returns(siteDto);

            _clientService.Setup(x => x.GetSite(1, 1)).Returns(new SiteAddressDto(1, "Add 1", "Add 2",
                                                                                               "Add 3", "Add 4", "Add 5", "county", "PC",
                                                                                               "tel", null));

            string expectedLastSite = "Zoo bar";
            string expectedFirstSite = "Alph bar";

            IList<SiteDto> existingSites = new List<SiteDto>();
            existingSites.Add(new SiteDto {Id = 1, Name = expectedLastSite});
            existingSites.Add(new SiteDto {Id = 1, Name = "Random"});
            existingSites.Add(new SiteDto { Id = 1, Name = expectedFirstSite });

            siteService.Setup(x => x.GetByCompanyIdNotIncluding(1,1)).Returns(existingSites);


            siteGroupService.Setup(x => x.GetByCompanyId(1)).Returns(new List<SiteGroupDto>());

            var siteGroupDto = new SiteGroupDto();
            siteGroupService.Setup(s => s.GetSiteGroup(1, 1)).Returns(siteGroupDto);

            var target = CreateSiteDetailsViewModelFactory();

            // Act
            var result = target.WithClientId(1).WithId(1).GetViewModel();

            // Assert
            Assert.That(result.ExistingSites.Skip(1).Take(1).First().label, Is.EqualTo(expectedFirstSite));
            Assert.That(result.ExistingSites.Last().label, Is.EqualTo(expectedLastSite));
        }

        [Test]
        public void When_get_view_model_Then_should_order_existing_site_group()
        {
            // Arrange
            var siteDto = new SiteDto { SiteId = 1 };

            siteService.Setup(ss => ss.GetByIdAndCompanyId(1, 1)).Returns(siteDto);

            _clientService.Setup(x => x.GetSite(1, 1)).Returns(new SiteAddressDto(1, "Add 1", "Add 2",
                                                                                               "Add 3", "Add 4", "Add 5", "county", "PC",
                                                                                               "tel", null));

            string expectedLastGroup = "Zoo bar";
            string expectedFirstGroup = "Alph bar";

            IList<SiteGroupDto> existingGroups = new List<SiteGroupDto>();
            existingGroups.Add(new SiteGroupDto{Id = 1, Name = expectedLastGroup});
            existingGroups.Add(new SiteGroupDto{Id = 1, Name = "Random"});
            existingGroups.Add(new SiteGroupDto { Id = 1, Name = expectedFirstGroup });
            siteGroupService.Setup(x => x.GetByCompanyIdExcludingSiteGroup(1, 1)).Returns(existingGroups);

            siteService.Setup(x => x.GetByCompanyId(1)).Returns(new List<SiteDto>());

            var siteGroupDto = new SiteGroupDto();
            siteGroupService.Setup(s => s.GetSiteGroup(1, 1)).Returns(siteGroupDto);

            var target = CreateSiteDetailsViewModelFactory();

            // Act
            var result = target.WithClientId(1).WithSiteId(1).GetViewModel();

            // Assert
            Assert.That(result.ExistingGroups.Skip(1).Take(1).First().label, Is.EqualTo(expectedFirstGroup));
            Assert.That(result.ExistingGroups.Last().label, Is.EqualTo(expectedLastGroup));
        }

        private SiteDetailsViewModelFactory CreateSiteDetailsViewModelFactory()
        {
            return new SiteDetailsViewModelFactory(siteService.Object, siteGroupService.Object, _clientService.Object);
        }
    }
}
