using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.SiteGroupServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetSiteGroup
    {
        private Mock<ISiteGroupRepository> _siteGroupRepository;
        private Mock<ISiteStructureElementRepository> _siteStructureElementRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;
        private long _siteId;

        [SetUp]
        public void SetUp()
        {
            _siteId = 100L;

            _siteGroupRepository = new Mock<ISiteGroupRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();

            _siteStructureElementRepository = new Mock<ISiteStructureElementRepository>();
            _siteStructureElementRepository
                .Setup(x => x.GetChildIdsThatCannotBecomeParent(_siteId))
                .Returns(new List<long>());

            _log = new Mock<IPeninsulaLog>();
        }


        
        [Test]
        public void Given_that_get_site_group_is_called_Then_should_call_site_group_repository_get_site_group()
        {
            //Given
            var target = CreateSiteGroupService();
            const long clientId = 1;
            const long siteGroupId = 2;

            var siteGroup = new Mock<SiteGroup>();
            siteGroup.Setup(foo => foo.Id).Returns(siteGroupId);
            siteGroup.Setup(foo => foo.Children).Returns(new List<SiteStructureElement>());
            _siteGroupRepository.Setup(x => x.GetByIdAndCompanyId(siteGroupId,clientId)).Returns(siteGroup.Object);
            
            //When
            target.GetSiteGroup(siteGroupId,clientId);

            //Then
            _siteGroupRepository.VerifyAll();
        }

        [Test]
        public void Given_that_get_site_group_is_called_Then_should_return_mapped_site_group_dto_with_link_to_group_set_correctly()
        {
            //Given
            var target = CreateSiteGroupService();
            const long clientId = 1;
            const long siteGroupId = 2;

            var parent = new Mock<SiteGroup>();
            parent.Setup(foo => foo.Id).Returns(100);
            parent.Setup(foo => foo.Children).Returns(new List<SiteStructureElement> { });
            parent.Setup(x => x.Self).Returns(new SiteGroup());
            var siteGroup = new Mock<SiteGroup>();
            siteGroup.Setup(foo => foo.Children).Returns(new List<SiteStructureElement>());
            siteGroup.Setup(foo => foo.Id).Returns(siteGroupId);
            siteGroup.Setup(foo => foo.ClientId).Returns(clientId);
            siteGroup.Setup(foo => foo.Name).Returns("Test Group Name");
            siteGroup.Setup(foo => foo.Parent).Returns(parent.Object);

            _siteGroupRepository.Setup(x => x.GetByIdAndCompanyId(siteGroupId, clientId)).Returns(siteGroup.Object);

            //When
            var result = target.GetSiteGroup(siteGroupId, clientId);

            //Then
            Assert.That(result, Is.TypeOf<SiteGroupDto>());
            Assert.That(result.Id, Is.EqualTo(siteGroup.Object.Id));
            Assert.That(result.ClientId, Is.EqualTo(siteGroup.Object.ClientId));
            Assert.That(result.Name, Is.EqualTo(siteGroup.Object.Name));

            var linkToGroupId = result.Parent != null && result.Parent as SiteGroupDto != null
                                    ? result.Parent.Id
                                    : 0;

            var linkToSiteId = result.Parent != null && result.Parent as SiteDto != null
                                   ? result.Parent.Id
                                   : 0;

            Assert.That(linkToSiteId, Is.EqualTo(0));
            Assert.That(linkToGroupId, Is.EqualTo(siteGroup.Object.Parent.Id));
        }

        [Test]
        public void Given_that_get_site_group_is_called_Then_should_return_mapped_site_group_dto_with_link_to_site_set_correctly()
        {
            //Given
            var target = CreateSiteGroupService();
            const long clientId = 1;
            const long siteGroupId = 2;

            var parent = new Mock<Site>();
            parent.Setup(foo => foo.Id).Returns(200);
            parent.Setup(foo => foo.SiteId).Returns(100);
            parent.Setup(foo => foo.Children).Returns(new List<SiteStructureElement> { });
            parent.Setup(x => x.Self).Returns(new Site());
            var siteGroup = new Mock<SiteGroup>();
            siteGroup.Setup(foo => foo.Id).Returns(siteGroupId);
            siteGroup.Setup(foo => foo.ClientId).Returns(clientId);
            siteGroup.Setup(foo => foo.Name).Returns("Test Group Name");
            siteGroup.Setup(foo => foo.Parent).Returns(parent.Object);
            siteGroup.Setup(foo => foo.Children).Returns(new List<SiteStructureElement> {});

            _siteGroupRepository.Setup(x => x.GetByIdAndCompanyId(siteGroupId, clientId)).Returns(siteGroup.Object);

            //When
            var result = target.GetSiteGroup(siteGroupId, clientId);

            //Then
            Assert.That(result, Is.TypeOf<SiteGroupDto>());
            Assert.That(result.Id, Is.EqualTo(siteGroup.Object.Id));
            Assert.That(result.ClientId, Is.EqualTo(siteGroup.Object.ClientId));
            Assert.That(result.Name, Is.EqualTo(siteGroup.Object.Name));

            var linkToGroupId = result.Parent != null && result.Parent as SiteGroupDto != null
                                    ? result.Parent.Id
                                    : 0;

            var linkToSiteId = result.Parent != null && result.Parent as SiteDto != null
                                   ? result.Parent.Id
                                   : 0;

            Assert.That(linkToSiteId, Is.EqualTo(siteGroup.Object.Parent.Id));
            Assert.That(linkToGroupId, Is.EqualTo(0));
        }

        private SiteGroupService CreateSiteGroupService()
        {
            var target = new SiteGroupService(_siteGroupRepository.Object, _siteStructureElementRepository.Object, _userRepository.Object, _log.Object);
            return target;
        }
    }
}