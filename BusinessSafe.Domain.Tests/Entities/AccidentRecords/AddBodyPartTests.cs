using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AccidentRecords
{
    [TestFixture]
    public class AddBodyPartTests
    {

        [Test]
        public void Given_body_part_list_is_empty_when_AddBodyPart_then_body_part_appears_in_list()
        {
            //given
            var bodyPart = new BodyPart() { Id = 3425, Description = "Toe nail" };

            var target = new AccidentRecord();

            target.AddBodyPartThatWasInjured(bodyPart, null);

            Assert.IsTrue(target.AccidentRecordBodyParts.Any(x => x.BodyPart.Id == bodyPart.Id));

            Assert.AreEqual(target, target.AccidentRecordBodyParts.First(x => x.BodyPart.Id == bodyPart.Id));
        }

        [Test]
        public void Given_body_part_is_in_list_when_AddBodyPart_then_body_part_is_not_duplicated()
        {
            //given
            var bodyPart = new BodyPart() { Id = 3425, Description = "Finger nail" };

            var target = new AccidentRecord();

            target.AddBodyPartThatWasInjured(bodyPart, null);
            target.AddBodyPartThatWasInjured(bodyPart, null);

            Assert.AreEqual(1, target.AccidentRecordBodyParts.Count);
        }

    }
}
