using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Sites.ViewModels;
using BusinessSafe.WebSite.Factories;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory
{
    [TestFixture]
    [Category("Unit")]
    public class SiteTreeViewBuilderBuildTests
    {

        [Test]
        public void Given_invalid_site_type_When_build_then_should_throw_invalid_argument_exception()
        {
            // Arrange
            var siteOrganisationalUnitDto = new CompositeSiteViewModel();
            {
            };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new SiteTreeViewBuilder(siteOrganisationalUnitDto).Build());
        }

        [Test]
        public void Given_only_head_site_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            const string expectedHeadSiteName = "Boston";
            var siteOrganisationalUnitDto = new CompositeSiteViewModel()
                                                {
                                                    Name = expectedHeadSiteName,
                                                    SiteId = 1,
                                                    Id = 88,
                                                    SiteType = CompositeSiteType.SiteAddress
                                                };

            // Act
            var result = new SiteTreeViewBuilder(siteOrganisationalUnitDto).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='linked-site' data-type='siteaddress' data-id='88'>{0}</div></li>", expectedHeadSiteName)));
        }

        
        [Test]
        public void Given_head_site_with_one_child_site_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var childSite = new CompositeSiteViewModel()
                                             {
                                                 Name = "Liverpool",
                                                 SiteId = 2,
                                                 Id = 93,
                                                 SiteType = CompositeSiteType.SiteAddress
                                             };

            var masterSite = new CompositeSiteViewModel()
                                             {
                                                 Name = "Boston",
                                                 SiteId = 1,
                                                 Id = 87,
                                                 SiteType = CompositeSiteType.SiteAddress,
                                                 Children = new List<CompositeSiteViewModel>()
                                                                                             {
                                                                                                 childSite,
                                                                                             }
                                             };


            // Act
            var result = new SiteTreeViewBuilder(masterSite).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='linked-site' data-type='siteaddress' data-id='{0}'>{1}</div><ul><li><div class='linked-site' data-type='siteaddress' data-id='{2}'>{3}</div></li></ul></li>", masterSite.Id, masterSite.Name, childSite.Id, childSite.Name)));
        }

        [Test]
        public void Given_head_site_with_one_child_site_group_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var childSiteGroup = new CompositeSiteViewModel()
            {
                Name = "Liverpool",
                Id = 99,
                SiteId = 2,
                SiteType = CompositeSiteType.SiteGroup
            };

            var masterSite = new CompositeSiteViewModel()
            {
                Name = "Boston",
                SiteId = 1,
                Id = 98,
                SiteType = CompositeSiteType.SiteAddress,
                Children = new List<CompositeSiteViewModel>()
                {
                    childSiteGroup,
                }
            };


            // Act
            var result = new SiteTreeViewBuilder(masterSite).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='linked-site' data-type='siteaddress' data-id='{0}'>{1}</div><ul><li><div class='linked-site' data-type='sitegroup' data-id='{2}'>{3}</div></li></ul></li>", masterSite.Id, masterSite.Name, childSiteGroup.Id, childSiteGroup.Name)));
        }

        [Test]
        public void Given_head_site_with_two_child_site_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var childSite1 = new CompositeSiteViewModel()
            {
                Name = "Liverpool",
                SiteId = 3,
                Id = 97,
                SiteType = CompositeSiteType.SiteAddress
            };
            var childSite2 = new CompositeSiteViewModel()
            {
                Name = "Manchester",
                SiteId = 2,
                Id = 85,
                SiteType = CompositeSiteType.SiteAddress
            };
            var masterSite = new CompositeSiteViewModel()
            {
                Name = "Boston",
                SiteId = 1,
                Id = 84,
                SiteType = CompositeSiteType.SiteAddress,
                Children = new List<CompositeSiteViewModel>()
                                                                                             {
                                                                                                 childSite1,
                                                                                                 childSite2
                                                                                             },
            };

            // Act
            var result = new SiteTreeViewBuilder(masterSite).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='linked-site' data-type='siteaddress' data-id='{0}'>{1}</div><ul><li><div class='linked-site' data-type='siteaddress' data-id='{2}'>{3}</div></li><li><div class='linked-site' data-type='siteaddress' data-id='{4}'>{5}</div></li></ul></li>", masterSite.Id, masterSite.Name, childSite1.Id, childSite1.Name, childSite2.Id, childSite2.Name)));
        }

        [Test]
        public void Given_head_site_with_two_child_sites_one_child_site_has_child_site_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var secondLevelChildSite = new CompositeSiteViewModel()
                                           {
                                               Name = "Ottley",
                                               SiteId = 4,
                                               SiteType = CompositeSiteType.SiteAddress
                                           };

            var childSite1 = new CompositeSiteViewModel()
            {
                Name = "Liverpool",
                SiteId = 3,
                Id = 96,
                SiteType = CompositeSiteType.SiteAddress,
                Children = new List<CompositeSiteViewModel>()
                               {
                                   secondLevelChildSite
                               }
            };
            var childSite2 = new CompositeSiteViewModel()
            {
                Name = "Manchester",
                SiteId = 2,
                Id = 96,
                SiteType = CompositeSiteType.SiteAddress
            };
            var masterSite = new CompositeSiteViewModel()
            {
                Name = "Boston",
                SiteId = 1,
                Id = 94,
                SiteType = CompositeSiteType.SiteAddress,
                Children = new List<CompositeSiteViewModel>()
                                                                                             {
                                                                                                 childSite1,
                                                                                                 childSite2
                                                                                             }
            };

            // Act
            var result = new SiteTreeViewBuilder(masterSite).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='linked-site' data-type='siteaddress' data-id='{0}'>{1}</div><ul><li><div class='linked-site' data-type='siteaddress' data-id='{2}'>{3}</div><ul><li><div class='linked-site' data-type='siteaddress' data-id='{4}'>{5}</div></li></ul></li><li><div class='linked-site' data-type='siteaddress' data-id='{6}'>{7}</div></li></ul></li>", masterSite.Id, masterSite.Name, childSite1.Id, childSite1.Name, secondLevelChildSite.Id, secondLevelChildSite.Name, childSite2.Id, childSite2.Name)));
        }

        [Test]
        public void Given_head_site_with_two_child_sites_one_child_site_has_one_child_site__which_has_one_child_site_When_build_then_should_only_have_correct_result()
        {
            // Arrange
            var thirdLevelChildSite = new CompositeSiteViewModel()
            {
                Name = "A Shed by a house",
                SiteId = 5,
                Id = 93,
                SiteType = CompositeSiteType.SiteAddress,
            };

            var secondLevelChildSite = new CompositeSiteViewModel()
            {
                Name = "Ottley",
                SiteId = 4,
                Id = 92,
                SiteType = CompositeSiteType.SiteAddress,
                Children = new List<CompositeSiteViewModel>() { thirdLevelChildSite }
            };

            var childSite1 = new CompositeSiteViewModel()
            {
                Name = "Liverpool",
                SiteId = 3,
                Id = 91,
                SiteType = CompositeSiteType.SiteAddress,
                Children = new List<CompositeSiteViewModel>()
                               {
                                   secondLevelChildSite
                               }
            };
            var childSite2 = new CompositeSiteViewModel()
            {
                Name = "Manchester",
                SiteId = 2,
                Id = 90,
                SiteType = CompositeSiteType.SiteAddress,
            };
            var masterSite = new CompositeSiteViewModel()
            {
                Name = "Boston",
                SiteId = 1,
                Id = 89,
                SiteType = CompositeSiteType.SiteAddress,
                Children = new List<CompositeSiteViewModel>()
                                                                                             {
                                                                                                 childSite1,
                                                                                                 childSite2
                                                                                             }
            };


            // Act
            var result = new SiteTreeViewBuilder(masterSite).Build();

            // Assert
            Assert.That(result, Is.EqualTo(string.Format("<li><div class='linked-site' data-type='siteaddress' data-id='{0}'>{1}</div><ul><li><div class='linked-site' data-type='siteaddress' data-id='{2}'>{3}</div><ul><li><div class='linked-site' data-type='siteaddress' data-id='{4}'>{5}</div><ul><li><div class='linked-site' data-type='siteaddress' data-id='{6}'>{7}</div></li></ul></li></ul></li><li><div class='linked-site' data-type='siteaddress' data-id='{8}'>{9}</div></li></ul></li>", masterSite.Id, masterSite.Name, childSite1.Id, childSite1.Name, secondLevelChildSite.Id, secondLevelChildSite.Name, thirdLevelChildSite.Id, thirdLevelChildSite.Name, childSite2.Id, childSite2.Name)));
        }
    }


}
