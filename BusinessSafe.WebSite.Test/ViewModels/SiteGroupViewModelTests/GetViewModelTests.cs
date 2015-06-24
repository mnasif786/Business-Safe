using System.Collections.Generic;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Factories;

using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.SiteGroupViewModelTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetViewModelTests
    {
        Mock<ISiteGroupService> _siteGroupService;
        private Mock<ISiteService> _siteService;

        [SetUp]
        public void SetUp()
        {
            _siteGroupService = new Mock<ISiteGroupService>();
            _siteService = new Mock<ISiteService>();
        }

        [Test]
        public void Given_that_no_site_group_is_found_Then_a_site_group_view_model_is_created()
        {
            //Given
            const long siteGroupId = 0;
            const long clientId = 20;

            _siteGroupService.Setup(sgs => sgs.GetSiteGroup(siteGroupId, clientId)).Returns<SiteGroupDto>(null);
            

            var target = GetTarget();

            //When
            var result = target.GetViewModel();

            //Then
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Given_that_no_site_group_is_found_Then_existing_site_is_populated()
        {
            //Given
            const long siteGroupId = 0;
            const long clientId = 20;

            _siteGroupService.Setup(sgs => sgs.GetSiteGroup(siteGroupId, clientId)).Returns<SiteGroupDto>(null);
            _siteService.Setup(ss => ss.GetByCompanyId(clientId)).Returns(new List<SiteDto> { new SiteDto{ Id = 10, Name = "name" }});

            var target = GetTarget();

            //When
            var result = target.GetViewModel();

            //Then
            Assert.That(result.ExistingSites, Is.Not.Null);
        }

        private SiteGroupViewModelFactory GetTarget()
        {
            return new SiteGroupViewModelFactory(_siteGroupService.Object, _siteService.Object);
        }
    }
}
