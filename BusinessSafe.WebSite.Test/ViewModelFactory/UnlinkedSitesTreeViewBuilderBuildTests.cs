using System.Collections.Generic;
using BusinessSafe.WebSite.Factories;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory
{
    [TestFixture]
    [Category("Unit")]
    public class UnlinkedSitesTreeViewBuilderBuildTests
    {
        [Test]
        public void Given_one_site_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var site = new UnlinkedSites()
                                             {
                                                 SiteDetails = "Boston",
                                                 SiteId = 1
                                             };
            var sites = new List<UnlinkedSites>
                            {
                                site
                            };

            // Act
            var result = new UnlinkedSitesTreeViewBuilder(sites).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='unlinked-site' data-type='siteaddress' data-id='{0}'>{1}</div></li>", site.SiteId, site.SiteDetails)));
        }

        [Test]
        public void Given_two_sites_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var site1 = new UnlinkedSites()
            {
                SiteDetails = "Boston",
                SiteId = 1
            };
            var site2 = new UnlinkedSites()
            {
                SiteDetails = "LA",
                SiteId = 2
            };
            var sites = new List<UnlinkedSites>
                            {
                                site1,
                                site2
                            };

            // Act
            var result = new UnlinkedSitesTreeViewBuilder(sites).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='unlinked-site' data-type='siteaddress' data-id='{0}'>{1}</div></li><li><div class='unlinked-site' data-type='siteaddress' data-id='{2}'>{3}</div></li>", site1.SiteId, site1.SiteDetails, site2.SiteId, site2.SiteDetails)));
        }

        [Test]
        public void Given_three_sites_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var site1 = new UnlinkedSites()
            {
                SiteDetails = "Boston",
                SiteId = 1
            };
            var site2 = new UnlinkedSites()
            {
                SiteDetails = "LA",
                SiteId = 2
            };
            var site3 = new UnlinkedSites()
            {
                SiteDetails = "Pheonix",
                SiteId = 3
            };
            var sites = new List<UnlinkedSites>
                            {
                                site1,
                                site2,
                                site3
                            };

            // Act
            var result = new UnlinkedSitesTreeViewBuilder(sites).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='unlinked-site' data-type='siteaddress' data-id='{0}'>{1}</div></li><li><div class='unlinked-site' data-type='siteaddress' data-id='{2}'>{3}</div></li><li><div class='unlinked-site' data-type='siteaddress' data-id='{4}'>{5}</div></li>", site1.SiteId, site1.SiteDetails, site2.SiteId, site2.SiteDetails, site3.SiteId, site3.SiteDetails)));
        }
    }
}