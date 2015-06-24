using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;
using Moq;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.Application.Tests.Implementations.SiteGroupServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        private Mock<ISiteGroupRepository> _siteGroupRepository;
        private Mock<ISiteRepository> _siteAddressRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<ISiteStructureElementRepository> _siteRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _siteRepository =new Mock<ISiteStructureElementRepository>();
            

           _siteGroupRepository = new Mock<ISiteGroupRepository>();
           _siteAddressRepository = new Mock<ISiteRepository>();

           _siteAddressRepository.Setup(sr => sr.GetById(It.IsAny<long>())).Returns(Site.Create(null, null, 10, "name", "ref", "", new UserForAuditing()));
           _siteGroupRepository.Setup(sr => sr.GetById(It.IsAny<long>())).Returns(SiteGroup.Create(new SiteGroup(), 10, "name", new UserForAuditing()));

           _userRepository = new Mock<IUserForAuditingRepository>();

           _log = new Mock<IPeninsulaLog>();
        }


        [Test]
        public void Given_that_update_us_called_Then_group_details_is_saved()
        {
            //Given
            var target = GetTarget();

            //When
            target.CreateUpdate(SiteGroupRequestBuilder.Create().WithName("some name").Build());

            //Then
            _siteGroupRepository.Verify(srs => srs.SaveOrUpdate(It.IsAny<SiteGroup>()), Times.Once());
        }

        [Test]
        public void Given_that_name_is_not_set_When_site_address_request_is_updated_Then_error_is_returned()
        {
            //Given
            var target = GetTarget();

            var siteGroupRequest = SiteGroupRequestBuilder
                                    .Create()
                                    .WithGroupId(0)
                                    .WithName(string.Empty)
                                    .WithLinkToSiteId(10)
                                    .Build();

            //When
            TestDelegate testDel = () => target.CreateUpdate(siteGroupRequest);

            //Then
            Assert.Throws<ValidationException>(testDel, "Name is required");
        }

        [Test]
        public void Given_that_parent_is_site_address_Then_get_site_address_is_called()
        {
            //Given
            const long groupId = 10;
            const long siteAddressId = 20;
            
            var target = GetTarget();

            //When
            var siteGroupRequest = SiteGroupRequestBuilder.Create().WithGroupId(groupId).WithLinkToSiteId(siteAddressId).WithLinkToGroupId(null).WithName("some name").Build();
            target.CreateUpdate(siteGroupRequest);

            //Then
            _siteRepository.Verify(a => a.GetById(siteAddressId), Times.Once());
            _siteGroupRepository.Verify(a => a.GetById(It.IsAny<long>()), Times.Once());
        }

        [TestCase(default(long))]
        [TestCase(null)]
        public void Given_that_parent_id_is_for_site_address_Then_correct_site_address_is_used_as_parent(long? withLinkToGroupId)
        {
            //Given
            const long groupId = 10;
            const long siteAddressId = 20;
            var siteGroupRequest = SiteGroupRequestBuilder.Create().WithGroupId(groupId).WithLinkToSiteId(siteAddressId).WithLinkToGroupId(withLinkToGroupId).WithName("some name").Build();

            var siteAddressExpectedToBeUsedAsParent = Site.Create(null, null, 10, "name", "ref", "", new UserForAuditing());
            _siteRepository.Setup(a => a.GetById(siteAddressId)).Returns(siteAddressExpectedToBeUsedAsParent);
            
            SiteGroup expectedSiteToBePassedIn = null;
            _siteGroupRepository.Setup(sgr => sgr.SaveOrUpdate(It.IsAny<SiteGroup>())).Callback<SiteGroup>(a => expectedSiteToBePassedIn = a);

            
            var target = GetTarget();

            //When            
            target.CreateUpdate(siteGroupRequest);

            //Then
            Assert.That(expectedSiteToBePassedIn.Parent, Is.EqualTo(siteAddressExpectedToBeUsedAsParent));
        }

        [Test]
        public void Given_that_parent_is_site_group_Then_get_site_group_is_called()
        {
            //Given
            const long groupId = 15;            
            const long linkToSiteGroupId = 10;

            var target = GetTarget();

            //When
            var siteGroupRequest = SiteGroupRequestBuilder.Create().WithGroupId(groupId).WithLinkToSiteId(null).WithLinkToGroupId(linkToSiteGroupId).WithName("some name").Build();
            target.CreateUpdate(siteGroupRequest);

            //Then
            _siteRepository.Verify(a => a.GetById(linkToSiteGroupId), Times.Once());
        }

        [TestCase(default(long))]
        [TestCase(null)]
        public void Given_that_parent_id_is_for_site_group_Then_correct_site_group_is_used_as_parent(long? linkToSiteId)
        {
            //Given
            const long groupId = 10;            
            const long linkToSiteGroupId = 10;

            var siteGroupRequest = SiteGroupRequestBuilder.Create().WithGroupId(groupId).WithLinkToSiteId(linkToSiteId).WithLinkToGroupId(linkToSiteGroupId).WithName("some name").Build();

            var siteGroupExpectedToBeUsedAsParent = SiteGroup.Create(new SiteGroup(), 99, "name", new UserForAuditing());
            _siteRepository.Setup(a => a.GetById(linkToSiteGroupId)).Returns(siteGroupExpectedToBeUsedAsParent);

            SiteGroup expectedSiteToBePassedIn = null;
            _siteGroupRepository.Setup(sgr => sgr.SaveOrUpdate(It.IsAny<SiteGroup>())).Callback<SiteGroup>(a => expectedSiteToBePassedIn = a);

            var target = GetTarget();

            //When
            target.CreateUpdate(siteGroupRequest);

            //Then
            Assert.That(expectedSiteToBePassedIn.Parent, Is.EqualTo(siteGroupExpectedToBeUsedAsParent));
        }

        [Test]
        public void Given_that_site_group_has_an_id_Then_site_group_is_retrived()
        {
            //Given
            const long groupId = 20;
            const long linkToSiteGroupId = 10;

            var siteGroupRequest = SiteGroupRequestBuilder.Create().WithGroupId(groupId).WithLinkToSiteId(null).WithLinkToGroupId(linkToSiteGroupId).WithName("some name").Build();
                       
            var target = GetTarget();

            //When
            target.CreateUpdate(siteGroupRequest);

            //Then
            _siteGroupRepository.Verify(sg => sg.GetById(groupId), Times.Once());
        }

        [Test]
        public void Given_that_site_group_does_not_have_an_id_Then_site_group_is_not_retrived()
        {
            //Given
            const long groupId = 0;
            const long expectedGroupId = 0;
            var siteGroupRequest = SiteGroupRequestBuilder.Create().WithGroupId(groupId).WithLinkToSiteId(null).WithLinkToGroupId(null).WithName("some name").Build();

            var target = GetTarget();

            //When
            target.CreateUpdate(siteGroupRequest);

            //Then
            _siteRepository.Verify(sg => sg.GetById(expectedGroupId), Times.Never());
        }

        [Test]
        public void Given_that_parent_id_is_for_site_group_Then_correct_values_are_mapped()
        {
            //Given
            const long groupId = 1;
            const long linkToSiteGroupId = 10;
            const string expectedName = "some name";

            var siteGroupRequest = SiteGroupRequestBuilder.Create().WithGroupId(groupId).WithLinkToSiteId(null).WithLinkToGroupId(linkToSiteGroupId).WithName(expectedName).Build();

            
            SiteGroup expectedSiteToBePassedIn = null;
            _siteGroupRepository.Setup(sgr => sgr.SaveOrUpdate(It.IsAny<SiteGroup>())).Callback<SiteGroup>(a => expectedSiteToBePassedIn = a);

            var target = GetTarget();

            //When
            target.CreateUpdate(siteGroupRequest);

            //Then
            Assert.That(expectedSiteToBePassedIn.Name, Is.EqualTo(expectedName));
            Assert.That(expectedSiteToBePassedIn.Id, Is.EqualTo(0));            
        }

        private SiteGroupService GetTarget()
        {
            var target = new SiteGroupService(_siteGroupRepository.Object, _siteRepository.Object, _userRepository.Object, _log.Object);
            return target;
        }
    }
}
