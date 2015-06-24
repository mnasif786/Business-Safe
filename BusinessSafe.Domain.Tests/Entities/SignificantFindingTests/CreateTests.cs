using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SignificantFindingTests
{
    [TestFixture]
    public class CreateTests
    {
        private SignificantFinding _significantFinding;
        private FireAnswer _fireAnswer;
        private UserForAuditing _currentUser;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _fireAnswer = FireAnswer.Create(null, null, YesNoNotApplicableEnum.No, "some additional info", _currentUser);
            _currentUser = new UserForAuditing
                        {
                            Id = Guid.NewGuid()
                        };

            _significantFinding = SignificantFinding.Create(_fireAnswer, _currentUser);
        }

        [Test]
        public void Given_answer_and_user_When_Create_called_Then_significant_finding__with_correct_properties_should_be_created()
        {
            Assert.That(_significantFinding.FireAnswer, Is.EqualTo(_fireAnswer));
            Assert.That(_significantFinding.CreatedBy, Is.EqualTo(_currentUser));
            Assert.That(_significantFinding.CreatedBy.Id, Is.EqualTo(_currentUser.Id));
            Assert.That(_significantFinding.CreatedBy.Id, Is.EqualTo(_currentUser.Id));
        }
    }
}
