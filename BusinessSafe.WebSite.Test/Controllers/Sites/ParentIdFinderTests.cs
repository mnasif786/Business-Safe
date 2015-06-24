using BusinessSafe.WebSite.Controllers.AutoMappers;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Sites
{
    [TestFixture]
    [Category("Unit")]
    public class ParentIdFinderTests
    {
        [Test]
        public void Given_a_site_id_link_and_site_group_id_link_set_both_to_null_When_get_Then_should_return_null()
        {
            long? linkToSiteId = null;
            long? linkToSiteGroupId = null;

            var result = new ParentIdFinder(linkToSiteId, linkToSiteGroupId).GetLinkId(); 

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Given_a_site_id_link_and_site_group_id_link_set_both_to_zero_When_get_Then_should_return_null()
        {
            long? linkToSiteId = 0;
            long? linkToSiteGroupId = 0;

            var result = new ParentIdFinder(linkToSiteId, linkToSiteGroupId).GetLinkId();

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Given_a_site_id_link_got_value_site_group_id_link_zero_When_get_Then_should_return_site_id_value()
        {
            long? linkToSiteId = 1;
            long? linkToSiteGroupId = 0;

            var result = new ParentIdFinder(linkToSiteId, linkToSiteGroupId).GetLinkId();

            Assert.That(result, Is.EqualTo(linkToSiteId));
        }

        [Test]
        public void Given_a_site_id_link_zero_and_site_group_id_link_got_value_When_get_Then_should_return_site_group_id_value()
        {
            long? linkToSiteId = 0;
            long? linkToSiteGroupId = 10;

            var result = new ParentIdFinder(linkToSiteId, linkToSiteGroupId).GetLinkId();

            Assert.That(result, Is.EqualTo(linkToSiteGroupId));
        }

      }
}