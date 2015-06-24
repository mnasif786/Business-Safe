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
    public class GetThisAndAllAncestorsTests
    {
        [Test]
        public void Given_no_ancestors_When_GetThisAndAllAncestors_called_Then_one_site_returned()
        {
            //Given
            var site = new Site() {Id = 123, Parent = null};

            //when
            var result = site.GetThisAndAllAncestors();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_one_ancestors_When_GetThisAndAllAncestors_called_Then_two_site_returned()
        {
            //Given
            var ansectorOne = new Site() {Id = 123123, Parent = null, Name = "Parent"};
            var site = new Site() {Id = 123, Parent = ansectorOne, Name = "the site"};
            var childSite = new Site() {Id = 432523, Parent = site, Name = "Child"};

            //when
            var result = site.GetThisAndAllAncestors();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Exists(x=> x.Name == ansectorOne.Name), Is.EqualTo(true));
            Assert.That(result.Exists(x => x.Name == site.Name), Is.EqualTo(true));
        }

        [Test]
        public void Given_two_ancestors_branches_When_GetThisAndAllAncestors_called_Then_two_sites_returned()
        {
            //Given
            var ansectorOne = new Site() { Id = 123123, Parent = null, Name = "Parent" };
            var uncle = new Site() { Id = 123123, Parent = ansectorOne, Name = "Uncle" };
            var site = new Site() { Id = 123, Parent = ansectorOne };
            var childSite = new Site() { Id = 432523, Parent = site, Name = "Child" };

            //when
            var result = site.GetThisAndAllAncestors();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Exists(x => x.Name == ansectorOne.Name), Is.EqualTo(true));
            Assert.That(result.Exists(x => x.Name == site.Name), Is.EqualTo(true));
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
