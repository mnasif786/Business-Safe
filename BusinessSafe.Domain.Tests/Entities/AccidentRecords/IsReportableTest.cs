using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Constants;

namespace BusinessSafe.Domain.Tests.Entities.AccidentRecords
{
    [TestFixture]
    public class IsReportableTest
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Given_Jurisdiction_Is_Jersey_Then_Result_Is_NON_Reportable()
        {
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                     new Jurisdiction { Id = 5, Name = JurisdictionNames.Jersey }, 12786L,
                                                     new UserForAuditing());

            accidentRecord.SetSummaryDetails("Test", "Test", new Jurisdiction{ Name = JurisdictionNames.Jersey }, new UserForAuditing());
            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Employee, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), null, new List<Injury>(), null, null, null, null, new UserForAuditing() );
            Assert.That(accidentRecord.IsReportable, Is.False);
        }

        [Test]
        public void Given_criteria_are_not_set_When_IsReportable_is_called_Then_returns_false()
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = JurisdictionNames.GB }, 5765L,
                                                       new UserForAuditing());

            Assert.That(accidentRecord.IsReportable, Is.False);
        }

        //When translating from matrix, 'Person at work' in Injured person column on matrix means value of Employee or PersonAtWork for PersonInvoled field.
        //'Any Person' means Visitor or Other.
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.GB, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.Guernsey, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.NI, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.ROI, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.Jersey, false)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.GB, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.Guernsey, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.NI, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.ROI, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.Jersey, false)]
        public void Given_person_involved_is_person_at_work__or_employee_and_accident_is_fatal__When_IsReportable_is_called_Then_returns_correct_value(
            PersonInvolvedEnum? personInvolved,
            string jurisdictionName,
            bool expectedIsReportable)
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = jurisdictionName }, 5765L,
                                                       new UserForAuditing());

            accidentRecord.UpdateInjuredPerson(personInvolved, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), null, new List<Injury>(), null, null, null, null, new UserForAuditing());
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(expectedIsReportable));
        }

        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.GB, true)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.Guernsey, true)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.NI, true)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.ROI, true)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.Jersey, false)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.GB, true)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.Guernsey, true)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.NI, true)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.ROI, true)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.Jersey, false)]
        public void Given_person_involved_is_visitor_or_other_and_accident_is_fatal__When_IsReportable_is_called_Then_returns_correct_value(
            PersonInvolvedEnum? personInvolved,
            string jurisdictionName,
            bool expectedIsReportable)
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = jurisdictionName }, 5765L,
                                                       new UserForAuditing());

            accidentRecord.UpdateInjuredPerson(personInvolved, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), null, new List<Injury>(), null, null, null, null, new UserForAuditing());
            //accidentRecord.PersonInvolved = personInvolved;
            //accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Fatal;
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(expectedIsReportable));
        }

        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.GB, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.NI, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.ROI, false)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.Guernsey, false)]
        [TestCase(PersonInvolvedEnum.PersonAtWork, JurisdictionNames.Jersey, false)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.GB, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.NI, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.ROI, false)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.Guernsey, false)]
        [TestCase(PersonInvolvedEnum.Employee, JurisdictionNames.Jersey, false)]
        public void Given_person_involved_is_person_at_work_or_employee_and_accident_is_major_When_IsReportable_is_called_Then_returns_correct_value(
            PersonInvolvedEnum? personInvolved,
            string jurisdictionName,
            bool expectedIsReportable)
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = jurisdictionName }, 5765L,
                                                       new UserForAuditing());

            accidentRecord.UpdateInjuredPerson(personInvolved, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Major, new List<BodyPart>(), null, new List<Injury>(), null, null, null, null, new UserForAuditing());
            //accidentRecord.PersonInvolved = personInvolved;
            //accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Major;
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(expectedIsReportable));
        }

        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.GB, false)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.NI, false)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.ROI, false)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.Guernsey, false)]
        [TestCase(PersonInvolvedEnum.Visitor, JurisdictionNames.Jersey, false)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.GB, false)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.NI, false)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.ROI, false)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.IoM, true)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.Guernsey, false)]
        [TestCase(PersonInvolvedEnum.Other, JurisdictionNames.Jersey, false)]
        public void Given_person_involved_is_visitor_or_other_and_accident_is_major_When_IsReportable_is_called_Then_returns_correct_value(
            PersonInvolvedEnum? personInvolved,
            string jurisdictionName,
            bool expectedIsReportable)
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = jurisdictionName }, 5765L,
                                                       new UserForAuditing());

            accidentRecord.UpdateInjuredPerson(personInvolved, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Major, new List<BodyPart>(), null, new List<Injury>(), null, null, null, null, new UserForAuditing());
            //accidentRecord.PersonInvolved = personInvolved;
            //accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Major;
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(expectedIsReportable));
        }

        [TestCase(JurisdictionNames.GB, true)]
        [TestCase(JurisdictionNames.NI, true)]
        [TestCase(JurisdictionNames.ROI, true)]
        [TestCase(JurisdictionNames.IoM, false)]
        [TestCase(JurisdictionNames.Guernsey, true)]
        [TestCase(JurisdictionNames.Jersey, false)]
        public void Given_person_involved_is_visitor_or_other_and_accident_requires_hospital_medical_treatment_When_IsReportable_is_called_Then_returns_correct_value(
          string jurisdictionName,
          bool expectedIsReportable)
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = jurisdictionName }, 5765L,
                                                       new UserForAuditing());

            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Other, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), null, new List<Injury>(), null, true, null, null, new UserForAuditing());
            //accidentRecord.InjuredPersonWasTakenToHospital = true;
            //accidentRecord.PersonInvolved = PersonInvolvedEnum.Other;
            //accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(expectedIsReportable));
        }

        [Test]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, JurisdictionNames.GB, false)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, JurisdictionNames.NI, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, JurisdictionNames.ROI, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, JurisdictionNames.IoM, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, JurisdictionNames.Guernsey, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, JurisdictionNames.Jersey, false)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays, JurisdictionNames.GB, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays, JurisdictionNames.NI, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays, JurisdictionNames.ROI, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays, JurisdictionNames.IoM, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays, JurisdictionNames.Guernsey, true)]
        [TestCase(LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays, JurisdictionNames.Jersey, false)]
        public void Given_certain_time_unable_to_carry_out_work_and_vertain_jurisdiction_When_IsReportable_is_called_Then_returns_correct_value(
            LengthOfTimeUnableToCarryOutWorkEnum lengthOfTimeUnableToCarryOutWork,
            string jurisdictionName,
            bool expectedIsReportable)
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = jurisdictionName }, 5765L,
                                                       new UserForAuditing());

            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Employee, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), null, new List<Injury>(), null, null, null, lengthOfTimeUnableToCarryOutWork, new UserForAuditing());
            //accidentRecord.PersonInvolved = PersonInvolvedEnum.Employee;
            //accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            //accidentRecord.LengthOfTimeUnableToCarryOutWork = lengthOfTimeUnableToCarryOutWork;
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(expectedIsReportable));
        }

        public void Given_injury_minor_or_non_reportable_When_IsReportable_is_called_Then_returns_correct_value()
        {
            var accidentRecord = AccidentRecord.Create("Test", "Test", new Jurisdiction { Name = JurisdictionNames.GB }, 5765L,
                                                       new UserForAuditing());

            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Other, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), null, new List<Injury>(), null, null, null, null, new UserForAuditing());
            //accidentRecord.PersonInvolved = PersonInvolvedEnum.Other;
            //accidentRecord.SeverityOfInjury = SeverityOfInjuryEnum.Minor;
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(false));
        }
    }
}
