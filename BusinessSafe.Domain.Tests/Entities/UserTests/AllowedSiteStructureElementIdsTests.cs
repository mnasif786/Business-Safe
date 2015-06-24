//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using BusinessSafe.Domain.Entities;
//using NUnit.Framework;

//namespace BusinessSafe.Domain.Tests.Entities.UserTests
//{
//    [TestFixture]
//    [Category("Unit")]
//    public class AllowedSiteStructureElementIdsTests
//    {

//        [Test] 
//        public void Given_user_has_site_as_site_structure_element_When_AllowedSiteStructureElementIds_called_Then_only_returns_id_of_root()
//        {
//            var user = new UserForAuditing {Site = GetSampleSiteStructureWithSiteRoot()};
//            var allowedSiteIds = user.AllowedSiteStructureElementIds;
//            Assert.AreEqual(1, allowedSiteIds.Count);
//            Assert.IsTrue(allowedSiteIds.Contains(11));
//        }

//        [Test]
//        public void Given_user_has_site_group_as_site_structure_element_When_AllowedSiteStructureElementIds_called_Then_only_returns_id_of_root()
//        {
//            var user = new UserForAuditing { Site = GetSampleSiteStructureWithSiteGroupRoot() };
//            var allowedSiteIds = user.AllowedSiteStructureElementIds;
//            Assert.AreEqual(13, allowedSiteIds.Count);
//            Assert.IsTrue(allowedSiteIds.Contains(11));
//            Assert.IsTrue(allowedSiteIds.Contains(21));
//            Assert.IsTrue(allowedSiteIds.Contains(22));
//            Assert.IsTrue(allowedSiteIds.Contains(31));
//            Assert.IsTrue(allowedSiteIds.Contains(32));
//            Assert.IsTrue(allowedSiteIds.Contains(33));
//            Assert.IsTrue(allowedSiteIds.Contains(34));
//            Assert.IsTrue(allowedSiteIds.Contains(41));
//            Assert.IsTrue(allowedSiteIds.Contains(42));
//            Assert.IsTrue(allowedSiteIds.Contains(43));
//            Assert.IsTrue(allowedSiteIds.Contains(44));
//            Assert.IsTrue(allowedSiteIds.Contains(45));
//            Assert.IsTrue(allowedSiteIds.Contains(46));
//        }

//        public SiteStructureElement GetSampleSiteStructureWithSiteRoot()
//        {
//            var siteStructure =
//                new Site
//                {
//                    Id = 11,
//                    Name = "UK Main Office",
//                    Children = new List<SiteStructureElement>
//                                       {
//                                           new SiteGroup
//                                               {
//                                                   Id = 21,
//                                                   Name = "North West Region",
//                                                   Children = new List<SiteStructureElement>
//                                                                  {
//                                                                      new Site
//                                                                          {
//                                                                              Id = 31,
//                                                                              Name = "Manchester City Center",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 41,
//                                                                                                         Name =
//                                                                                                             "Chorlton"
//                                                                                                     },
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 42,
//                                                                                                         Name =
//                                                                                                             "Didsbury"
//                                                                                                     },
//                                                                                             }
//                                                                          },
//                                                                      new Site
//                                                                          {
//                                                                              Id = 32,
//                                                                              Name = "Liverpool City Center",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 43,
//                                                                                                         Name =
//                                                                                                             "Liverpool One"
//                                                                                                     }
//                                                                                             }
//                                                                          }
//                                                                  }
//                                               },
//                                           new SiteGroup
//                                               {
//                                                   Id = 22,
//                                                   Name = "Midlands Region",
//                                                   Children = new List<SiteStructureElement>
//                                                                  {
//                                                                      new SiteGroup
//                                                                          {
//                                                                              Id = 33,
//                                                                              Name = "East Midlands Region",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 44,
//                                                                                                         Name =
//                                                                                                             "Nottingham"
//                                                                                                     },
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 45,
//                                                                                                         Name =
//                                                                                                             "Leicester"
//                                                                                                     }
//                                                                                             }
//                                                                          },
//                                                                      new SiteGroup
//                                                                          {
//                                                                              Id = 34,
//                                                                              Name = "West Midlands Region",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 46,
//                                                                                                         Name =
//                                                                                                             "Birmingham"
//                                                                                                     }
//                                                                                             }
//                                                                          }
//                                                                  }
//                                               }
//                                       }
//                };

//            return siteStructure;
//        }

//        public SiteStructureElement GetSampleSiteStructureWithSiteGroupRoot()
//        {
//            var siteStructure =
//                new SiteGroup
//                {
//                    Id = 11,
//                    Name = "UK",
//                    Children = new List<SiteStructureElement>
//                                       {
//                                           new SiteGroup
//                                               {
//                                                   Id = 21,
//                                                   Name = "North West Region",
//                                                   Children = new List<SiteStructureElement>
//                                                                  {
//                                                                      new Site
//                                                                          {
//                                                                              Id = 31,
//                                                                              Name = "Manchester City Center",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 41,
//                                                                                                         Name =
//                                                                                                             "Chorlton"
//                                                                                                     },
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 42,
//                                                                                                         Name =
//                                                                                                             "Didsbury"
//                                                                                                     },
//                                                                                             }
//                                                                          },
//                                                                      new Site
//                                                                          {
//                                                                              Id = 32,
//                                                                              Name = "Liverpool City Center",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 43,
//                                                                                                         Name =
//                                                                                                             "Liverpool One"
//                                                                                                     }
//                                                                                             }
//                                                                          }
//                                                                  }
//                                               },
//                                           new SiteGroup
//                                               {
//                                                   Id = 22,
//                                                   Name = "Midlands Region",
//                                                   Children = new List<SiteStructureElement>
//                                                                  {
//                                                                      new SiteGroup
//                                                                          {
//                                                                              Id = 33,
//                                                                              Name = "East Midlands Region",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 44,
//                                                                                                         Name =
//                                                                                                             "Nottingham"
//                                                                                                     },
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 45,
//                                                                                                         Name =
//                                                                                                             "Leicester"
//                                                                                                     }
//                                                                                             }
//                                                                          },
//                                                                      new SiteGroup
//                                                                          {
//                                                                              Id = 34,
//                                                                              Name = "West Midlands Region",
//                                                                              Children = new List<SiteStructureElement>
//                                                                                             {
//                                                                                                 new Site
//                                                                                                     {
//                                                                                                         Id = 46,
//                                                                                                         Name =
//                                                                                                             "Birmingham"
//                                                                                                     }
//                                                                                             }
//                                                                          }
//                                                                  }
//                                               }
//                                       }
//                };

//            return siteStructure;
//        }
//    }
//}
