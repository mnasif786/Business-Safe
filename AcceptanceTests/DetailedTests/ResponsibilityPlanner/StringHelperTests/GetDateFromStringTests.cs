using System;
using BusinessSafe.AcceptanceTests.StepHelpers;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using StructureMap;

namespace BusinessSafe.AcceptanceTests.DetailedTests.ResponsibilityPlanner.StringHelperTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetDateFromString
    {       
        [TestCase("today + 1", 1)]
        [TestCase("today + 200", 200)]
        public void Given_that_raw_string_is_provided_with_positive_offset_When_string_is_processed_Then_correct_dateTime_object_is_returned(string dateStringToBeParsed, int plusDaysOffset)
        {
            //Given            
            var now = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
            var expectedDate = now.AddDays(plusDaysOffset);
            InjectICurrentTime(now);

            //When
            var result = dateStringToBeParsed.GetDateFromString();

            //Then
            Assert.That(result, Is.EqualTo(expectedDate));
        }

        private static void InjectICurrentTime(DateTime now)
        {
            ObjectFactory.EjectAllInstancesOf<ICurrentTime>();
            var mockCurrentTime = new Mock<ICurrentTime>();
            mockCurrentTime.Setup(mct => mct.GetCurrentDateTime()).Returns(now);
            ObjectFactory.Inject(mockCurrentTime.Object);
        }

        [TestCase("todaay + 1")]
        [TestCase(" + 200")]
        public void Given_that_string_is_not_in_correct_fromat_When_string_is_parsed_Then_argument_exception_is_thrown(string dateStringToBeParsed)
        {
            //When
            ActualValueDelegate result = () => dateStringToBeParsed.GetDateFromString();

            //Then
            Assert.That(result, Throws.Exception.TypeOf<ArgumentException>());
        }

        [TestCase("today + asdajk")]
        [TestCase("today + 10a")]
        public void Given_that_integer_part_of_string_is_not_in_correct_fromat_When_string_is_parsed_Then_argument_exception_is_thrown(string dateStringToBeParsed)
        {
            //When
            ActualValueDelegate result = () => dateStringToBeParsed.GetDateFromString();

            //Then
            Assert.That(result, Throws.Exception.TypeOf<ArgumentException>());
        }

        [TestCase("today - 1", -1)]
        [TestCase("today - 200", -200)]
        public void Given_that_raw_string_is_provided_with_negative_offset_When_string_is_processed_Then_correct_dateTime_object_is_returned(string dateStringToBeParsed, int plusDaysOffset)
        {
            //Given
            var now = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
            var expectedDate = now.AddDays(plusDaysOffset);
            InjectICurrentTime(now);

            //When
            var result = dateStringToBeParsed.GetDateFromString();

            //Then
            Assert.That(result, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Given_that_raw_string_is_today_when_string_is_parsed_Then_today_date_is_returned()
        {
            //Given
            const string dateStringToBeParsed = "today";            
            var expectedDateTime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime.Now);
            InjectICurrentTime(expectedDateTime);

            //When
            var result = dateStringToBeParsed.GetDateFromString();

            //Then
            Assert.That(result, Is.EqualTo(expectedDateTime));
        }

        [Test]
        public void Given_that_raw_string_is_empty_When_string_is_parsed_Then_default_datetime_is_returned()
        {
            //Given
            string dateStringTobeParsed = string.Empty;
            DateTime? expectedDateTime = null;

            //When
            var result = dateStringTobeParsed.GetDateFromString();

            //Then
            Assert.That(result, Is.EqualTo(expectedDateTime));
        }
    }
}
