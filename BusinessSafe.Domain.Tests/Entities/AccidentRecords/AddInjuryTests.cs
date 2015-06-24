using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AccidentRecords
{
    [TestFixture]
    public class AddInjuryTests
    {

        [Test]
        public void Given_injury_list_is_empty_when_AddInjury_then_Injury_appears_in_list()
        {
            //given
            var injury = new Injury() {Id = 3425, Description = "Anterior cruciate ligament"};

            var target = new AccidentRecord();

            target.AddInjury(injury,null);

            Assert.IsTrue(target.AccidentRecordInjuries.Any(x => x.Injury.Id == injury.Id));

            Assert.AreEqual(target, target.AccidentRecordInjuries.First(x => x.Injury.Id == injury.Id));
        }

        [Test]
        public void Given_injury_is_in_list_when_AddInjury_then_Injury_is_not_duplicated()
        {
            //given
            var injury = new Injury() { Id = 3425, Description = "Anterior cruciate ligament" };

            var target = new AccidentRecord();

            target.AddInjury(injury, null);
            target.AddInjury(injury, null);

            Assert.AreEqual(1, target.AccidentRecordInjuries.Count);
        }

    }
}
