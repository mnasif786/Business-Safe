using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.WebSite.Extensions;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Extensions
{
    [TestFixture]
    public class DateTimeOffsetExtensionsTests
    {
        private DateTime dt;
        private DateTimeOffset dtOffset;

        [SetUp]
        public void Setup()
        {
            dt = new DateTime(1980, 6, 23);
            dtOffset = new DateTimeOffset(dt, new TimeSpan(1, 0, 0));
        }

        [Test]
        public void Given_DateTimeOffset_and_seed_dt_Then_dates_are_the_same()
        {
            // Given

            // When

            // Then
            Assert.That(dt.Date, Is.EqualTo(dtOffset.Date));
        }

        [Test]
        public void Given_DateTimeOffset_and_seed_dt_Then_tolocalshortdatestring_are_the_same()
        {
            // Given

            // When

            // Then
            Assert.That(dt.ToShortDateString(), Is.EqualTo(dtOffset.ToLocalShortDateString()));
        }
    }
}
