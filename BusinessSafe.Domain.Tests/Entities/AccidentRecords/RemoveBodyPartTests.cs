using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AccidentRecords
{
    [TestFixture]
    public class RemoveBodyPartTests
    {

        [Test]
        public void Given_bodypart_is_not_in_list_when_RemoveInjury_then_Injury_list_remains_the_same()
        {
            //given
            var bodypart = new BodyPart() { Id = 3425, Description = "Anterior cruciate ligament" };
            var bodypart2 = new BodyPart() { Id = 123124, Description = "Hip flexior" };

            var target = new AccidentRecord();

            target.AddBodyPartThatWasInjured(bodypart, null);
            target.AddBodyPartThatWasInjured(bodypart2, null);

            //when
            target.RemoveBodyPartThatWasInjured(new BodyPart() { Id = 124314, Description = "Test" }, null);


            Assert.AreEqual(2, target.AccidentRecordBodyParts.Count);
        }

        [Test]
        public void Given_bodypart_is_in_list_when_RemoveInjury_then_Injury_removed_from_list()
        {
            //given
            var bodypart = new BodyPart() { Id = 3425, Description = "Anterior cruciate ligament", Deleted = false};
            var bodypart2 = new BodyPart() { Id = 123124, Description = "Hip flexior", Deleted = false };

            var target = new AccidentRecord();

            target.AddBodyPartThatWasInjured(bodypart, null);
            target.AddBodyPartThatWasInjured(bodypart2, null);

            //when
            target.RemoveBodyPartThatWasInjured(bodypart2, null);

            Assert.AreEqual(1, target.AccidentRecordBodyParts.Count);
            Assert.IsTrue(target.AccidentRecordBodyParts.All(x => x.BodyPart.Id != bodypart2.Id));
        }
    }
}
