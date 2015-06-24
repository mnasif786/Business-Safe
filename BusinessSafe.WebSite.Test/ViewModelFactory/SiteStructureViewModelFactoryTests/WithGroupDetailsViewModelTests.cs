using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.SiteStructureViewModelFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class WithGroupDetailsViewModelTests
    {
        private Mock<ISiteService> _siteService;
        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<IClientService> _clientService;

        [SetUp]
        public void SetUp()
        {
            _siteService = new Mock<ISiteService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _clientService = new Mock<IClientService>();
        }

        [Test]
        public void Given_that_get_view_model_is_requested_Then_site_group_view_model_is_present()
        {
            //Given
            const long clientId = 100;
            const long siteId = 10;
            const long groupId = 5;

            _siteService
                .Setup(ss => ss.GetSiteStructureByCompanyId(clientId))
                .Returns(new SiteDto{ SiteId = siteId, ClientId = clientId, Name = "name 2" });

            _siteService.Setup(x => x.MainSiteExists(It.IsAny<long>())).Returns(true);
           
            var groupService = new Mock<ISiteGroupService>();
            groupService.Setup(ss => ss.GetByCompanyId(clientId)).Returns(new List<SiteGroupDto> { new SiteGroupDto{ Id = -1, Name = "" }});
            _clientService.Setup(x => x.GetCompanyDetails(clientId)).Returns(new CompanyDetailsDto((int)clientId, "Test Name",
                                                                                                   "TST001", "Add1",
                                                                                                   "Add2", "Add3",
                                                                                                   "Add4", 88, "M11AA",
                                                                                                   "09999999", null,
                                                                                                   null));

            var target = new SiteStructureViewModelFactory(_siteService.Object, groupService.Object, _clientService.Object);

            //When
            target.WithClientId(clientId).WithGroupDetailsViewModel(SiteGroupDetailsViewModelBuilder.Create().WithGroupId(groupId).WithName("name test").Build());

            //Then
            var viewModel = target.GetViewModel();
            Assert.That(viewModel.SiteSiteGroupsViewModel, Is.Not.Null);
            _siteService.VerifyAll();            
        }

        [Test]
        public void Given_that_get_view_model_is_requested_Then_site_group_view_model_is_present2()
        {
            //Given
            const long clientId = 100;
            const long siteId = 10;
            const long groupId = 5;
            const string expectedGroupName = "site group name";
            const long siteGroupId = 100;
            const string expectedSiteGroupId = "100";

            //_siteService.Setup(ss => ss.GenerateSiteOrganisationalUnit(clientId)).Returns(new SiteOrganisationalUnitDto(siteId, clientId, "name 2"));
            //_siteService.Setup(ss => ss.GenerateSiteOrganisationalUnit(clientId)).Returns(new SiteOrganisationalUnitDto(siteId, clientId, "name 2") { SiteType = SiteTypeDto.SiteAddress });

            _siteService
                .Setup(ss => ss.GetSiteStructureByCompanyId(clientId))
                .Returns(new SiteDto { SiteId = siteId, ClientId = clientId, Name = "name 2" });
            _siteService.Setup(x => x.MainSiteExists(It.IsAny<long>())).Returns(true);

            //_siteGroupService.Setup(ss => ss.GetLinkedGroups(clientId)).Returns(new List<LinkedGroupsDto> { new LinkedGroupsDto(siteGroupId, expectedGroupName) });
            _siteGroupService.Setup(ss => ss.GetByCompanyId(clientId)).Returns(new List<SiteGroupDto> { new SiteGroupDto{ Id = siteGroupId, Name = expectedGroupName }});

            _clientService.Setup(x => x.GetCompanyDetails(clientId)).Returns(new CompanyDetailsDto((int)clientId, "Test Name",
                                                                                                   "TST001", "Add1",
                                                                                                   "Add2", "Add3",
                                                                                                   "Add4", 88, "M11AA",
                                                                                                   "09999999", null,
                                                                                                   null));

            var target = GetTarget();

            //When
            target.WithClientId(clientId).WithGroupDetailsViewModel(SiteGroupDetailsViewModelBuilder.Create().WithGroupId(groupId).WithName("name test").Build());

            //Then
            var viewModel = target.GetViewModel();
            Assert.That(viewModel.SiteSiteGroupsViewModel.ExistingGroups.Skip(1).Take(1).First().label, Is.EqualTo(expectedGroupName));
            Assert.That(viewModel.SiteSiteGroupsViewModel.ExistingGroups.Skip(1).Take(1).First().value, Is.EqualTo(expectedSiteGroupId));
        }

        private SiteStructureViewModelFactory GetTarget()
        {
            var target = new SiteStructureViewModelFactory(_siteService.Object, _siteGroupService.Object, _clientService.Object);
            return target;
        }
    }

}


