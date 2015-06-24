using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.SiteGroupViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        private Mock<ISiteGroupService> siteGroupService; 
        private Mock<ISiteService> siteService; 

        [SetUp]
        public void Setup()
        {
            siteGroupService = new Mock<ISiteGroupService>();
            siteService = new Mock<ISiteService>();
        }

        [Test]
        public void Given_valid_site_group_id_and_client_id_When_get_view_model_Then_return_correct_view_model()
        {
            //Given
            const long siteGroupId = 1;
            const long clientId = 10;
            

            var siteGroupDto = new SiteGroupDto();
            siteGroupService.Setup(s => s.GetSiteGroup(siteGroupId, clientId)).Returns(siteGroupDto);
            
            var target = CreateSiteGroupViewModelFactory();

            //When
            var result = target.WithClientId(clientId).WithSiteGroupId(siteGroupId).GetViewModel();

            //Then
            Assert.That(result, Is.TypeOf<SiteGroupDetailsViewModel>());
            
        }

        [Test]
        public void When_get_view_model_Then_should_order_existing_sites()
        {
            // Arrange
            const long siteGroupId = 1;
            const long clientId = 10;

            string expectedLastSite = "Zoo bar";
            string expectedFirstSite = "Alph bar";

            IList<SiteDto> existingSites = new List<SiteDto>();
            existingSites.Add(new SiteDto{ Id = 1, Name = expectedLastSite});
            existingSites.Add(new SiteDto{Id = 1, Name = "Random"});
            existingSites.Add(new SiteDto{Id = 1, Name = expectedFirstSite});

            siteService.Setup(x => x.GetByCompanyId(0)).Returns(existingSites);


            siteGroupService.Setup(x => x.GetByCompanyId(0)).Returns(new List<SiteGroupDto>());

            var siteGroupDto = new SiteGroupDto();
            siteGroupService.Setup(s => s.GetSiteGroup(siteGroupId, clientId)).Returns(siteGroupDto);

            var target = CreateSiteGroupViewModelFactory();

            // Act
            var result = target.GetViewModel();

            // Assert
            Assert.That(result.ExistingSites.Skip(1).Take(1).First().label, Is.EqualTo(expectedFirstSite));
            Assert.That(result.ExistingSites.Last().label, Is.EqualTo(expectedLastSite));
        }

        [Test]
        public void When_get_view_model_Then_should_order_existing_site_group()
        {
            // Arrange
            const long siteGroupId = 1;
            const long clientId = 10;

            string expectedLastGroup = "Zoo bar";
            string expectedFirstGroup = "Alph bar";

            IList<SiteGroupDto> existingGroups = new List<SiteGroupDto>();
            existingGroups.Add(new SiteGroupDto{ Id = 1, Name = expectedLastGroup});
            existingGroups.Add(new SiteGroupDto{ Id = 1, Name = "Random"});
            existingGroups.Add(new SiteGroupDto { Id = 1, Name = expectedFirstGroup });
            siteGroupService.Setup(x => x.GetByCompanyIdExcludingSiteGroup(0,0)).Returns(existingGroups);

            siteService.Setup(x => x.GetByCompanyId(0)).Returns(new List<SiteDto>());

            var siteGroupDto = new SiteGroupDto();
            siteGroupService.Setup(s => s.GetSiteGroup(siteGroupId, clientId)).Returns(siteGroupDto);

            var target = CreateSiteGroupViewModelFactory();

            // Act
            var result = target.GetViewModel();

            // Assert
            Assert.That(result.ExistingGroups.Skip(1).Take(1).First().label, Is.EqualTo(expectedFirstGroup));
            Assert.That(result.ExistingGroups.Last().label, Is.EqualTo(expectedLastGroup));
        }

        private SiteGroupViewModelFactory CreateSiteGroupViewModelFactory()
        {
            var target = new SiteGroupViewModelFactory(siteGroupService.Object, siteService.Object);
            return target;
        }
    }
}
