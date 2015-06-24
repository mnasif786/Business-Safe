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
    public class NextStepsSectionsTests
    {
        [Test]
        public void Given_GB_Jurisdiction_And_Fatal_Injury_then_entity_has_correct_next_steps_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.GB }, 12786L,
                                                  new UserForAuditing());

            // When
           accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), "", new List<Injury>(),
                                              "", true, YesNoUnknownEnum.Unknown, null, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_1));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_NI_Jurisdiction_And_Fatal_Injury_then_entity_has_correct_next_steps_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.NI }, 12786L,
                                                  new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), "", new List<Injury>(),
                                              "", true, YesNoUnknownEnum.Unknown, null, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_3));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_ROI_Jurisdiction_And_Fatal_Injury_then_entity_has_correct_next_steps_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.ROI }, 12786L,
                                                  new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), "", new List<Injury>(),
                                              "", true, YesNoUnknownEnum.Unknown, null, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_3));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }
        
        [Test]
        public void Given_IoM_Jurisdiction_And_Fatal_Injury_then_entity_has_correct_next_steps_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.IoM }, 12786L,
                                                  new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), "", new List<Injury>(),
                                              "", true, YesNoUnknownEnum.Unknown, null, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_6));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_Guernsey_Jurisdiction_And_Fatal_Injury_then_entity_has_correct_next_steps_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.Guernsey }, 12786L,
                                                  new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Fatal, new List<BodyPart>(), "", new List<Injury>(),
                                              "", true, YesNoUnknownEnum.Unknown, null, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_4));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_jurisdiction_is_Jersey_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction {Name = JurisdictionNames.Jersey}, 12786L,
                                                       new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(false));
        }

        [Test]
        public void Given_accident_record_is_created_as_Jersey_When_I_call_SetSummaryDetails_With_ROI_as_jurisdiction_Then_entity_has_correct_next_steps()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.Jersey }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.SetSummaryDetails("Test Title 1", "Test01",
                                             new Jurisdiction {Name = JurisdictionNames.ROI}, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count(x => !x.Deleted), Is.EqualTo(3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count(x => x.Deleted), Is.EqualTo(2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_2));
            // No injury details or personal set so not reportable
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(false));
        }

        [Test]
        public void Given_accident_record_is_created_as_GB_When_I_call_SetSummaryDetails_With_Jersey_as_jurisdiction_Then_entity_has_correct_next_steps()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.GB }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.SetSummaryDetails("Test Title 1", "Test01",
                                             new Jurisdiction { Name = JurisdictionNames.Jersey }, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(false));
        }

        [Test]
        public void Given_GB_is_jurisdiction_and_injured_person_taken_to_hospital_is_true_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.GB }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", true, YesNoUnknownEnum.Unknown, LengthOfTimeUnableToCarryOutWorkEnum.ThreeOrLessDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_1));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_NI_is_jurisdiction_and_injured_person_taken_to_hospital_is_true_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.NI }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", true, YesNoUnknownEnum.Unknown, LengthOfTimeUnableToCarryOutWorkEnum.ThreeOrLessDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_2));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_ROI_is_jurisdiction_and_injured_person_taken_to_hospital_is_true_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.ROI }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", true, YesNoUnknownEnum.Unknown, LengthOfTimeUnableToCarryOutWorkEnum.ThreeOrLessDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_3));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_Guernsey_is_jurisdiction_and_injured_person_taken_to_hospital_is_true_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.Guernsey }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", true, YesNoUnknownEnum.Unknown, LengthOfTimeUnableToCarryOutWorkEnum.ThreeOrLessDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_4));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }


        //Injury severity = major

        [Test]
        public void Given_GB_is_jurisdiction_and_person_involved_is_employee_or_person_at_work_and_time_out_of_work_is_8_or_more_days_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.GB }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Employee, null, new Employee{ Id = Guid.NewGuid()}, null, null, null, null, null, null, null, null, null, null, null, null);

           
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", false, YesNoUnknownEnum.No, LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_1));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_NI_is_jurisdiction_and_person_involved_is_employee_or_person_at_work_and_time_out_of_work_is_4_or_more_days_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.NI }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Employee, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);

            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", false, YesNoUnknownEnum.No, LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_2));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_ROI_is_jurisdiction_and_person_involved_is_employee_or_person_at_work_and_time_out_of_work_is_4_or_more_days_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.ROI }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Employee, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);

            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", false, YesNoUnknownEnum.No, LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, new UserForAuditing());
            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_3));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_IoM_is_jurisdiction_and_person_involved_is_employee_or_person_at_work_and_time_out_of_work_is_4_or_more_days_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.IoM }, 12786L,
                                                       new UserForAuditing());
            // When
            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Employee, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);

            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", false, YesNoUnknownEnum.No, LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_6));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_Guernsey_is_jurisdiction_and_person_involved_is_employee_or_person_at_work_and_time_out_of_work_is_4_or_more_days_When_I_call_Create_Then_Entity_has_correct_next_step_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                       new Jurisdiction { Name = JurisdictionNames.Guernsey }, 12786L,
                                                       new UserForAuditing());

            // When
            accidentRecord.UpdateInjuredPerson(PersonInvolvedEnum.Employee, null, new Employee { Id = Guid.NewGuid() }, null, null, null, null, null, null, null, null, null, null, null, null);

            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Minor, new List<BodyPart>(), "", new List<Injury>(),
                                               "", false, YesNoUnknownEnum.No, LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_4));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_IOM_Jurisdiction_and_Major_Injury_and_AnyPerson_then_entity_has_correct_next_steps_sections()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.IoM }, 12786L,
                                                  new UserForAuditing());
            // When
            accidentRecord.UpdateInjuryDetails(SeverityOfInjuryEnum.Major, new List<BodyPart>(), "", new List<Injury>(),
                                               "", true, YesNoUnknownEnum.Unknown, null, new UserForAuditing());

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(4));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section4_6));
            Assert.That(accidentRecord.IsReportable, Is.EqualTo(true));
        }

        [Test]
        public void Given_ROI_is_jurisdiction_and_accident_is_non_reportable_show_correct_next_steps()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.ROI }, 12786L,
                                                  new UserForAuditing());
            // When
            accidentRecord.IsReportable = false;

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_2));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_2));
        }

        [Test]
        public void Given_IOM_Jurisdiction_and_accident_is_non_reportable_show_correct_next_steps()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.IoM }, 12786L,
                                                  new UserForAuditing());

            // When
            accidentRecord.IsReportable = false;

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
        }

        [Test]
        public void Given_Guernsey_Jurisdiction_and_accident_is_non_reportable_show_correct_next_steps()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.Guernsey }, 12786L,
                                                  new UserForAuditing());

            // When
            accidentRecord.IsReportable = false;

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
        }

        [Test]
        public void Given_GB_Jurisdiction_and_accident_is_non_reportable_show_correct_next_steps()
        {
            //given
            var accidentRecord = AccidentRecord.Create("Test Title 1", "Test01",
                                                  new Jurisdiction { Name = JurisdictionNames.GB }, 12786L,
                                                  new UserForAuditing());

            // When
            accidentRecord.IsReportable = false;

            // Then
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Count, Is.EqualTo(3));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section2_1));
            Assert.That(accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).Contains(NextStepsSectionEnum.Section3_1));
        }
    }

}
