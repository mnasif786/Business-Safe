using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.AccidentRecords
{
    [TestFixture]
    public class NextStepsAvailableTests
    {

        [Test]
        public void Given_accident_is_fatal_then_next_steps_available_equals_true()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Fatal;

            Assert.IsTrue(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_employee_is_in_a_non_fatal_accident_taken_to_hospital_question_answered_and_able_continue_working_then_next_steps_available_equals_true()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = PersonInvolvedEnum.Employee;
            accidentRecord.InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes;
            accidentRecord.InjuredPersonWasTakenToHospital = true;
 
            Assert.IsTrue(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_employee_is_in_a_non_fatal_accident_taken_to_hospital_question_answered_not_able_to_continue_working_and_length_of_time_off_answered_then_next_steps_available_equals_true()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = PersonInvolvedEnum.Employee;
            accidentRecord.InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.No;
            accidentRecord.InjuredPersonWasTakenToHospital = true;
            accidentRecord.LengthOfTimeUnableToCarryOutWork = LengthOfTimeUnableToCarryOutWorkEnum.ThreeOrLessDays;

            Assert.IsTrue(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_employee_is_in_a_non_fatal_accident_taken_to_hospital_question_NOT_answered_and_able_continue_working_then_next_steps_available_equals_false()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = PersonInvolvedEnum.Employee;
            accidentRecord.InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes;
            accidentRecord.InjuredPersonWasTakenToHospital = null;

            Assert.IsFalse(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_employees_is_in_a_non_fatal_accident_taken_to_hospital_question_answered_not_able_to_continue_working_and_length_of_time_off_NOT_answered_then_next_steps_available_equals_false()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = PersonInvolvedEnum.Employee;
            accidentRecord.InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.No;
            accidentRecord.InjuredPersonWasTakenToHospital = true;
            accidentRecord.LengthOfTimeUnableToCarryOutWork = null;

            Assert.IsFalse(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_person_involved_NOT_answered_then_next_steps_available_equals_false()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = null;
            accidentRecord.InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes;
            accidentRecord.InjuredPersonWasTakenToHospital = true;

            Assert.IsFalse(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_InjuredPersonAbleToCarryOutWork_NOT_answered_then_next_steps_available_equals_false()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = PersonInvolvedEnum.Employee;
            accidentRecord.InjuredPersonAbleToCarryOutWork = null;

            Assert.IsFalse(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_a_vistor_was_in_an_accident_then_next_steps_available_equals_true()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = PersonInvolvedEnum.Visitor;
            accidentRecord.InjuredPersonWasTakenToHospital = false;

            Assert.True(accidentRecord.NextStepsAvailable);
        }

        [Test]
        public void Given_employee_was_in_a_non_fatal_accident_and_its_unknown_whether_they_have_been_able_to_return_to_work_then_next_steps_available_equals_true()
        {
            var accidentRecord = new AccidentRecord();
            accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            accidentRecord.PersonInvolved = PersonInvolvedEnum.Employee;
            accidentRecord.InjuredPersonWasTakenToHospital = false;
            accidentRecord.InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Unknown;

            Assert.True(accidentRecord.NextStepsAvailable);
        }


    }
}
