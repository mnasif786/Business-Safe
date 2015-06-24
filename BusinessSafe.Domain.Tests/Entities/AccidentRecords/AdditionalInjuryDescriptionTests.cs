using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AccidentRecords
{
    [TestFixture]
    public class AdditionalInjuryDescriptionTests
    {
        /// <summary>
        /// Created so we can add injuries to a private field
        /// </summary>
        public class AccidentRecordForTesting : AccidentRecord
        {
            public void AddAccidentRecordInjury(AccidentRecordInjury accidentRecordInjury)
            {
                _accidentRecordInjuries.Add(accidentRecordInjury);
            }
        }

        [Test]
        public void given_accident_record_contains_unknown_injury_then_addition_injury_information_returns_correct_result()
        {
            var expectedInjury = "Anterior cruciate ligament";
            var target = new AccidentRecordForTesting();
            target.AddAccidentRecordInjury(new AccidentRecordInjury() { Id = 213123, Injury = new Injury() { Id = 3495834 } });
            target.AddAccidentRecordInjury(new AccidentRecordInjury() { Id = 213123, AdditionalInformation = expectedInjury, Injury = new Injury() { Id = Injury.UNKOWN_INJURY } });
 
            Assert.That(target.AdditionalInjuryInformation, Is.EqualTo(expectedInjury));

        }
    }
}
