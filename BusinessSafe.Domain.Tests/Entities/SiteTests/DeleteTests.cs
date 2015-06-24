using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SiteTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {
        [Test]
        public void Given_siteaddress_When_mark_for_delete_Then_site_properties_should_be_correct()
        {
            //Given
            var siteAddress = new MySite();
            var user = new UserForAuditing();

            //When
            siteAddress.MarkForDelete(user);

            //Then
            Assert.That(siteAddress.Deleted, Is.True);
            Assert.That(siteAddress.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(siteAddress.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_sitegroup_When_mark_for_delete_Then_site_properties_should_be_correct()
        {
            //Given
            var siteAddress = new MySite() { };
            var user = new UserForAuditing();

            //When
            siteAddress.MarkForDelete(user);

            //Then
            Assert.That(siteAddress.Deleted, Is.True);
            Assert.That(siteAddress.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(siteAddress.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_sitegroup_with_children_When_mark_for_delete_Then_should_throw_correct_exception()
        {
            //Given
            var user = new UserForAuditing();
            var siteAddress = new MySite
            {
                Children = new List<SiteStructureElement>()
                            {
                                new Site()
                            }
            };

            //When
            //Then
            Assert.Throws<MarkForDeleteSiteGroupException>(() => siteAddress.MarkForDelete(user));

        }
    }

    public class MySite : SiteStructureElement
    {
        public MySite()
        {
            base.Id = 1;
        }
        public new long Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public override SiteStructureElement Self
        {
            get { return this; }
        }

        //public override string SiteStructureElementType
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }
}
