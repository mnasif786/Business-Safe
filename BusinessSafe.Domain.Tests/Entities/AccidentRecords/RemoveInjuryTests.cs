using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AccidentRecords
{
    [TestFixture]
    public class RemoveInjuryTests
    {

        [Test]
        public void Given_injury_is_not_in_list_when_RemoveInjury_then_Injury_list_remains_the_same()
        {
            //given
            var injury = new Injury() { Id = 3425, Description = "Anterior cruciate ligament" };
            var injury2 = new Injury() { Id = 123124, Description = "Hip flexior" };

            var target = new AccidentRecord();

            target.AddInjury(injury, null);
            target.AddInjury(injury2, null);

            //when
            target.RemoveInjury(new Injury() {Id = 124314, Description = "Test"},null);


            Assert.AreEqual(2, target.AccidentRecordInjuries.Count);
        }

        [Test]
        public void Given_injury_is_in_list_when_RemoveInjury_then_Injury_removed_from_list()
        {
            //given
            var injury = new Injury() { Id = 3425, Description = "Anterior cruciate ligament", Deleted = false};
            var injury2 = new Injury() { Id = 123124, Description = "Hip flexior", Deleted = false };

            var target = new AccidentRecord();

            target.AddInjury(injury, null);
            target.AddInjury(injury2, null);

            //when
            target.RemoveInjury(injury2, null);

            Assert.AreEqual(1, target.AccidentRecordInjuries.Count);
            Assert.IsTrue(target.AccidentRecordInjuries.All(x => x.Injury.Id != injury2.Id));
        }
    }
}
