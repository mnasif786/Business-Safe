using System;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.ParameterClasses.SafeCheck;
using NUnit.Framework;
using Action = BusinessSafe.Domain.Entities.Action;

namespace BusinessSafe.Domain.Tests.Entities.ActionTests
{
    [TestFixture]
    public class CreateActionTests
    {
        private CreateUpdateActionParameters _createUpdateActionParameters;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createUpdateActionParameters = new CreateUpdateActionParameters()
            {
                ActionRequired = "required",
                AreaOfNonCompliance = "Area of non compliance",
                AssignedTo = new Employee() { Id = Guid.NewGuid() },
                GuidanceNotes = "Guidance Notes",
                Reference = "Number 1",
                Status = ActionPlanStatus.Overdue,
            };
        }

        [Test]
        public void  Given_When_Create_called_Then_Correct_Action_Returned()
        {
            // Given
            var actionParameters = _createUpdateActionParameters;
            actionParameters.TimeScale = new Timescale() {Id = 0, Name = "None"};

            // When
            var action = Action.Create(actionParameters);

            // Then
            Assert.That(action.ActionRequired, Is.EqualTo(actionParameters.ActionRequired));
            Assert.That(action.AreaOfNonCompliance, Is.EqualTo(actionParameters.AreaOfNonCompliance));
            Assert.That(action.AssignedTo, Is.EqualTo(actionParameters.AssignedTo));
            Assert.That(action.GuidanceNotes, Is.EqualTo(actionParameters.GuidanceNotes));
            Assert.That(action.Reference, Is.EqualTo(actionParameters.Reference));
            Assert.That(action.TargetTimescale, Is.EqualTo(actionParameters.TimeScale.Name));
            Assert.That(action.Category, Is.EqualTo(actionParameters.Category));
        }

        #region "DueDate Tests (Based on Timescales)"

        [Test]
        public void Given_Timescale_is_Null_When_Create_is_called_Then_Returns_Null_Due_Date()
        {
            //Given
            var actionParameters = _createUpdateActionParameters;

            //when
            actionParameters.TimeScale = null;

            var action = Action.Create(actionParameters);
            action.SetDueDate(actionParameters.TimeScale);
            
            //Then
            Assert.IsNull(action.DueDate);
        }

        [Test]
        public void Given_Timescale_is_None_When_Create_is_called_Then_Returns_Null_Due_Date()
        {
            //Given
            var actionParameters = _createUpdateActionParameters;

            //when
            actionParameters.TimeScale = new Timescale() { Id = 0, Name = "None" };

            var action = Action.Create(actionParameters);
            action.SetDueDate(actionParameters.TimeScale);

            //Then
            Assert.IsNull(action.DueDate);
        }

        [Test]
        public void Given_Timescale_is_Urget_Action_Required_When_Create_is_called_Then_Returns_Due_Date_With_Current_Date_Value()
        {
            //Given
            var actionParameters = _createUpdateActionParameters;

            //when
            actionParameters.TimeScale = new Timescale() { Id = 4, Name = "Urgent Action Required" };

            var action = Action.Create(actionParameters);
            action.SetDueDate(actionParameters.TimeScale);

            //Then
            Assert.That(action.DueDate.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_Timescale_is_One_Month_When_Create_is_called_Then_Returns_Due_Date_With_Value_One_Month_after_the_Current_Date()
        {
            //Given
            var actionParameters = _createUpdateActionParameters;

            //when
            actionParameters.TimeScale = new Timescale() { Id = 1, Name = "One Month" };

            var action = Action.Create(actionParameters);
            action.SetDueDate(actionParameters.TimeScale);

            //Then
            Assert.That(action.DueDate.Value.Date, Is.EqualTo(DateTime.Now.AddMonths(1).Date));
        }

        [Test]
        public void Given_Timescale_is_Three_Month_When_Create_is_called_Then_Returns_Due_Date_With_Value_Three_Month_after_the_Current_Date()
        {
            //Given
            var actionParameters = _createUpdateActionParameters;

            //when
            actionParameters.TimeScale = new Timescale() { Id = 2, Name = "Three Months" };

            var action = Action.Create(actionParameters);
            action.SetDueDate(actionParameters.TimeScale);

            //Then
            Assert.That(action.DueDate.Value.Date, Is.EqualTo(DateTime.Now.AddMonths(3).Date));
        }

        [Test]
        public void Given_Timescale_is_Six_Month_When_Create_is_called_Then_Returns_Due_Date_With_Value_Six_Month_after_the_Current_Date()
        {
            //Given
            var actionParameters = _createUpdateActionParameters;

            //when
            actionParameters.TimeScale = new Timescale() { Id = 3, Name = "Six Months" };

            var action = Action.Create(actionParameters);
            action.SetDueDate(actionParameters.TimeScale);

            //Then
            Assert.That(action.DueDate.Value.Date, Is.EqualTo(DateTime.Now.AddMonths(6).Date));
        }

        [Test]
        public void Given_Timescale_is_Six_Weeks_When_Create_is_called_Then_Returns_Due_Date_With_Value_Six_Weeks_after_the_Current_Date()
        {
            //Given
            var actionParameters = _createUpdateActionParameters;

            //when
            actionParameters.TimeScale = new Timescale() { Id = 5, Name = "Six Weeks" };

            var action = Action.Create(actionParameters);
            action.SetDueDate(actionParameters.TimeScale);

            //Then
            Assert.That(action.DueDate.Value.Date, Is.EqualTo(DateTime.Now.AddDays(6*7).Date));
        }

        #endregion
    }
}
