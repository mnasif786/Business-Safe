using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.Company;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.DoesCompanyDefaultExistsTests
{
    [TestFixture]
    [Category("Unit")]
    public class DoesCompanyDefaultAlreadyExistGuardTests
    {

        [Test]
        public void Given_that_query_finds_no_matches_Then_should_return_correct_result()
        {
            //Given
            //When
            Func<IEnumerable<MatchingName>> queryNoResults = () => new List<MatchingName>();

            var result = new DoesCompanyDefaultAlreadyExistGuard().Execute(queryNoResults);
            
            //Then
            Assert.That(result.Exists, Is.False);
            Assert.That(result.MatchingResults.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Given_that_query_finds_matches_Then_should_return_correct_result()
        {
            //Given
            //When
            Func<IEnumerable<MatchingName>> queryNoResults = () => new List<MatchingName>(){ new MatchingName(){Name = "matching name 1"}, new MatchingName(){Name = "matching name 2"}};

            var result = new DoesCompanyDefaultAlreadyExistGuard().Execute(queryNoResults);

            //Then
            Assert.That(result.Exists, Is.True);
            Assert.That(result.MatchingResults.Count(), Is.EqualTo(2));
        }
    }
}