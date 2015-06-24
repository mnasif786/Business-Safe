using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class DeleteSiteGroupTests
    {
        private Mock<ISiteGroupRepository> _siteGroupRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();
            _siteGroupRepository = new Mock<ISiteGroupRepository>();
            _user = new UserForAuditing();

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_delete_site_group_request_When_delete_site_group_Then_should_call_save_update_on_site_group_repository()
        {
            //Given
            var target = CreateSiteGroupService();

            var deleteSiteGroupRequest = DeleteSiteGroupRequestBuilder.Create().Build();

            var siteGroup = new Mock<SiteGroup>().Object;
            _siteGroupRepository.Setup(x => x.GetByIdAndCompanyId(deleteSiteGroupRequest.GroupId,deleteSiteGroupRequest.CompanyId)).Returns(siteGroup);

            //When
            target.Delete(deleteSiteGroupRequest);

            //Then
            _siteGroupRepository.Verify(x => x.SaveOrUpdate(siteGroup),Times.Once());
            
        }

        [Test]
        public void Given_valid_delete_site_group_request_When_delete_site_group_Then_should_call_mark_for_delete_on_site_group()
        {
            //Given
            var target = CreateSiteGroupService();

            var deleteSiteGroupRequest = DeleteSiteGroupRequestBuilder.Create().Build();

            var siteGroup = new Mock<SiteGroup>();
            _siteGroupRepository.Setup(x => x.GetByIdAndCompanyId(deleteSiteGroupRequest.GroupId,deleteSiteGroupRequest.CompanyId)).Returns(siteGroup.Object);

            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(deleteSiteGroupRequest.UserId, deleteSiteGroupRequest.CompanyId))
                .Returns(_user);

            //When
            target.Delete(deleteSiteGroupRequest);

            //Then
            siteGroup.Verify(x => x.MarkForDelete(_user), Times.Once());

        }

        private SiteGroupService CreateSiteGroupService()
        {
            return new SiteGroupService(_siteGroupRepository.Object, null, _userRepository.Object, _log.Object);
        }
    }
}