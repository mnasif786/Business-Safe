using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.InjuryDetails
{
    [TestFixture]
    public class SaveTests
    {
        private Mock<IInjuryDetailsViewModelFactory> _injuryDetailsViewModelFactory;
        private Mock<IAccidentRecordService> _accidentRecordService;

        [SetUp]
        public void setup()
        {
            _injuryDetailsViewModelFactory = new Mock<IInjuryDetailsViewModelFactory>();
            _accidentRecordService = new Mock<IAccidentRecordService>();

            _injuryDetailsViewModelFactory.Setup(x => x.GetViewModel(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => new InjuryDetailsViewModel());

        }

        [Test]
        public void give_valid_request_when_save_then_service_called_with_correct_parameters()
        {
            //given
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
                            {
                                AccidentRecordId = 1L,
                                SeverityOfInjury = SeverityOfInjuryEnum.Major,
                                SelectedInjuryIds = new long[]{2L},
                                SelectedBodyPartIds = new long[]{3L,4L},
                                InjuredPersonWasTakenToHospital = true,
                                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.No,
                                LengthOfTimeUnableToCarryOutWork = LengthOfTimeUnableToCarryOutWorkEnum.MoreThanSevenDays
                            };

            UpdateInjuryDetailsRequest request = null;
            _accidentRecordService.Setup(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()))
                .Callback<UpdateInjuryDetailsRequest>(x => request = x);

            //when
            controller.Save(model);

            //then
            Assert.That(model.AccidentRecordId,Is.EqualTo(request.AccidentRecordId)); 
            Assert.That(TestControllerHelpers.CompanyIdAssigned,Is.EqualTo(request.CompanyId));
            Assert.That(TestControllerHelpers.UserIdAssigned,Is.EqualTo(request.CurrentUserId));
            Assert.That(model.SelectedInjuryIds,Is.EqualTo(request.Injuries));
            Assert.That(model.SelectedBodyPartIds, Is.EqualTo(request.BodyPartsInjured));
            Assert.That(model.InjuredPersonWasTakenToHospital, Is.EqualTo(request.WasTheInjuredPersonBeenTakenToHospital));
            Assert.That(model.InjuredPersonAbleToCarryOutWork,Is.EqualTo(request.HasTheInjuredPersonBeenAbleToCarryOutTheirNormalWorkActivity));
            Assert.That(model.LengthOfTimeUnableToCarryOutWork,Is.EqualTo(request.LengthOfTimeUnableToCarryOutWork));
            Assert.That(model.CustomInjuryDescription,Is.EqualTo(request.CustomInjuryDescription));
            Assert.That(model.CustomBodyPartyDescription, Is.EqualTo(request.CustomBodyPartDescription));

        }

        [Test]
        public void Given_severity_is_fatal_and_injury_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                SelectedInjuryIds = new long[0],
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("Injury"), "Injury is mandatory when severity is fatal");
        }

        [Test]
        public void Given_severity_is_fatal_and_injury_specified_when_save_then_model_state_does_not_return_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                SelectedInjuryIds = new long[] { 1L, 2L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x=>x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()),Times.Once());
        }

        [Test]
        public void Given_severity_is_Major_and_injury_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Major,
                SelectedInjuryIds = new long[0],
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("Injury"), "Injury is mandatory when severity is major");
        }

        [Test]
        public void Given_severity_is_Major_and_injury_specified_when_save_then_model_state_does_not_return_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Major,
                SelectedInjuryIds = new long[] { 1L, 2L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_Minor_and_injury_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[0],
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("Injury"), "Injury is mandatory when severity is minor");
        }

        [Test]
        public void Given_severity_is_Minor_and_injury_specified_when_save_then_model_state_does_not_return_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 1L, 2L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_no_apparent_injury_and_injury_not_specified_when_save_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.NoApparentInjury,
                SelectedInjuryIds = null,
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_fatal_and_bodypart_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[0] { },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("BodyPartsSection"), "Body part is mandatory when severity is fatal");
        }

        [Test]
        public void Given_severity_is_fatal_and_bodypart_specified_when_save_then_model_state_does_not_return_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                SelectedInjuryIds = new long[] { 1L, 2L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_Major_and_bodypart_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Major,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[0] { },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("BodyPartsSection"), "Body part is mandatory when severity is major");
        }

        [Test]
        public void Given_severity_is_Major_and_bodypart_specified_when_save_then_model_state_does_not_return_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Major,
                SelectedInjuryIds = new long[] { 1L, 2L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_Minor_and_bodypart_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[0] { },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("BodyPartsSection"), "Body part is mandatory when severity is minor");
        }

        [Test]
        public void Given_severity_is_Minor_and_bodypart_specified_when_save_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 1L, 2L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_no_apparent_injury_and_bodypart_not_specified_when_save_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.NoApparentInjury,
                SelectedInjuryIds = new long[] {5L},
                SelectedBodyPartIds = null,
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_not_fatal_and_taken_to_hospital_question_not_answered_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = null
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("TakenToHospitalSection"), "Taken to hospital question is mandatory when severity is not fatal");
        }

        [Test]
        public void Given_severity_is_fatal_and_taken_to_hospital_question_not_answered_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = null,
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_not_fatal_and_carry_out_work_question_not_answered_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                ShowInjuredPersonAbleToCarryOutWorkSection = true,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = false,
                InjuredPersonAbleToCarryOutWork = null
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("InjuredPersonAbleToCarryOutWork"), "Injured person able to carry out work is mandatory when severity is not fatal");
        }


        [Test]
        public void Given_severity_is_not_fatal_back_to_work_section_is_not_visible_then_model_state_is_valid()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                ShowInjuredPersonAbleToCarryOutWorkSection = false,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = false,
                InjuredPersonAbleToCarryOutWork = null
            };

            controller.Save(model);
            Assert.That(controller.ModelState.IsValid,Is.True);
        }


        [Test]
        public void Given_severity_is_fatal_and_carry_out_work_question_not_answered_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = null
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_not_fatal_and_carry_out_work_question_answered_no_and_time_off_not_entered_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = false,
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.No,
                LengthOfTimeUnableToCarryOutWork = null
            };

            controller.Save(model);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("LengthOfTimeUnableToCarryOutWork"), "Time off is mandatory when injured person has not been able to carry out their normal work.");
        }

        [Test]
        public void Given_severity_is_not_fatal_and_carry_out_work_question_answered_no_and_time_off_entered_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = false,
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.No,
                LengthOfTimeUnableToCarryOutWork = LengthOfTimeUnableToCarryOutWorkEnum.ThreeOrLessDays
            };

            controller.Save(model);
            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_not_fatal_and_carry_out_work_question_answered_yes_and_time_off_not_entered_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonWasTakenToHospital = false,
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                LengthOfTimeUnableToCarryOutWork = null
            };

            controller.Save(model);
            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_is_fatal_and_carry_out_work_question_answered_no_and_time_off_not_entered_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.No,
                LengthOfTimeUnableToCarryOutWork = null
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_severity_not_entered_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = null,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.No,
                LengthOfTimeUnableToCarryOutWork = null
            };

            controller.Save(model);

            Assert.True(controller.ModelState.Keys.Contains("SeverityOfInjury"), "Severity of injury is mandatory.");
        }

        [Test]
        public void Given_other_unknown_injury_selected_but_description_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { Injury.UNKOWN_INJURY },
                SelectedBodyPartIds = new long[] { 3L, 4L },
            };

            controller.Save(model);

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("CustomInjuryDescription"), "Custom Injury Description is mandatory when unknown injury is selected");
        }

        [Test]
        public void Given_other_unknown_injury_selected_and_description_entered_when_save_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { Injury.UNKOWN_INJURY },
                CustomInjuryDescription = "Broken collar bone",
                SelectedBodyPartIds = new long[] { 3L, 4L },
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false

            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        [Test]
        public void Given_other_unknown_body_part_selected_but_description_not_specified_when_save_then_model_state_returns_error()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
                            {
                                AccidentRecordId = 1L,
                                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                                SelectedInjuryIds = new long[] {3L, 4L},
                                SelectedBodyPartIds = new long[] {BodyPart.UNKOWN_BODY_PART},
                                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                                InjuredPersonWasTakenToHospital = false,
                                CustomBodyPartyDescription = null
                            };

            controller.Save(model);

            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.True(controller.ModelState.Keys.Contains("CustomBodyPartyDescription"), "Custom body part description is mandatory when unknown body part is selected");
        }

        [Test]
        public void Given_other_unknown_body_part_selected_and_description_entered_when_save_then_model_is_saved()
        {
            var controller = GetTarget();
            var model = new InjuryDetailsViewModel
            {
                AccidentRecordId = 1L,
                SeverityOfInjury = SeverityOfInjuryEnum.Minor,
                SelectedInjuryIds = new long[] { 3L, 4L },
                SelectedBodyPartIds = new long[] {  BodyPart.UNKOWN_BODY_PART},
                InjuredPersonAbleToCarryOutWork = YesNoUnknownEnum.Yes,
                InjuredPersonWasTakenToHospital = false,
                CustomBodyPartyDescription = "Patella"
            };

            controller.Save(model);

            Assert.IsTrue(controller.ModelState.IsValid);
            _accidentRecordService.Verify(x => x.UpdateInjuryDetails(It.IsAny<UpdateInjuryDetailsRequest>()), Times.Once());
        }

        private InjuryDetailsController GetTarget()
        {
            var controller =  new InjuryDetailsController(_injuryDetailsViewModelFactory.Object, _accidentRecordService.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
