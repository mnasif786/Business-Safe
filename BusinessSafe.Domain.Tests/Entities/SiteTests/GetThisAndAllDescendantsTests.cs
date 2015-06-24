using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Tests.Entities.SiteTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetThisAndAllDescendantsTests
    {
        [Test]
        public void Given_valid_structure_of_sites_When_GetThisAndAllDescendants_called_Then_a_list_of_all_the_descended_sites_is_returned()
        {
            var sites = GetSampleSiteStructure().GetThisAndAllDescendants();

            Assert.AreEqual(7, sites.Count);
            Assert.IsTrue(sites.Any(x => x.Id == 11));
            Assert.IsFalse(sites.Any(x => x.Id == 21));
            Assert.IsTrue(sites.Any(x => x.Id == 22));
            Assert.IsFalse(sites.Any(x => x.Id == 31));
            Assert.IsFalse(sites.Any(x => x.Id == 32));
            Assert.IsTrue(sites.Any(x => x.Id == 33));
            Assert.IsTrue(sites.Any(x => x.Id == 34));
            Assert.IsFalse(sites.Any(x => x.Id == 41));
            Assert.IsFalse(sites.Any(x => x.Id == 42));
            Assert.IsFalse(sites.Any(x => x.Id == 43));
            Assert.IsTrue(sites.Any(x => x.Id == 44));
            Assert.IsTrue(sites.Any(x => x.Id == 45));
            Assert.IsTrue(sites.Any(x => x.Id == 46));
        }

        public SiteStructureElement GetSampleSiteStructure()
        {
            var siteStructure =
                new Site
                    {
                        Id = 11,
                        Name = "Head Office",
                        Children = new List<SiteStructureElement>
                                       {
                                           new SiteGroup
                                               {
                                                   Id = 21,
                                                   Name = "North West Region",
                                                   Deleted = true,
                                                   Children = new List<SiteStructureElement>
                                                                  {
                                                                      new Site
                                                                          {
                                                                              Id = 31,
                                                                              Name = "Manchester City Centre",
                                                                              Deleted = true,
                                                                              Children = new List<SiteStructureElement>
                                                                                             {
                                                                                                 new Site
                                                                                                     {
                                                                                                         Id = 41,
                                                                                                         Name = "Chorlton",
                                                                                                         Deleted = true
                                                                                                     },
                                                                                                 new Site
                                                                                                     {
                                                                                                         Id = 42,
                                                                                                         Name = "Didsbury",
                                                                                                         Deleted = true
                                                                                                     },
                                                                                             }
                                                                          },
                                                                      new Site
                                                                          {
                                                                              Id = 32,
                                                                              Name = "Liverpool City Center",
                                                                              Deleted = true,
                                                                              Children = new List<SiteStructureElement>
                                                                                             {
                                                                                                 new Site
                                                                                                     {
                                                                                                         Id = 43,
                                                                                                         Name = "Liverpool One",
                                                                                                         Deleted = true
                                                                                                     }
                                                                                             }
                                                                          }
                                                                  }
                                               },
                                           new SiteGroup
                                               {
                                                   Id = 22,
                                                   Name = "Midlands Region",
                                                   Children = new List<SiteStructureElement>
                                                                  {
                                                                      new SiteGroup
                                                                          {
                                                                              Id = 33,
                                                                              Name = "East Midlands Region",
                                                                              Children = new List<SiteStructureElement>
                                                                                             {
                                                                                                 new Site
                                                                                                     {
                                                                                                         Id = 44,
                                                                                                         Name =
                                                                                                             "Nottingham"
                                                                                                     },
                                                                                                 new Site
                                                                                                     {
                                                                                                         Id = 45,
                                                                                                         Name =
                                                                                                             "Leicester"
                                                                                                     }
                                                                                             }
                                                                          },
                                                                      new SiteGroup
                                                                          {
                                                                              Id = 34,
                                                                              Name = "West Midlands Region",
                                                                              Children = new List<SiteStructureElement>
                                                                                             {
                                                                                                 new Site
                                                                                                     {
                                                                                                         Id = 46,
                                                                                                         Name =
                                                                                                             "Birmingham"
                                                                                                     }
                                                                                             }
                                                                          }
                                                                  }
                                               }
                                       }
                    };

            return siteStructure;
        }
    }
}
