using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.AccidentDetails
{
    [TestFixture]
    public class SaveTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;
        private Mock<IAccidentTypeService> _accidentTypeService;
        private Mock<ICauseOfAccidentService> _causeOfAccidentService;
        private Mock<IAccidentRecordService> _accidentRecordService;
        private long _companyId;

        [SetUp]
        public void SetUp()
        {
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();
            _accidentTypeService = new Mock<IAccidentTypeService>();
            _causeOfAccidentService = new Mock<ICauseOfAccidentService>();
            _accidentRecordService = new Mock<IAccidentRecordService>();
            _companyId = 1234L;


            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithSite(It.IsAny<long>(), It.IsAny<long>())).Returns(
                new AccidentRecordDto{Id = 1L});

        }

        [Test]
        public void Given_get_When_Save_Then_Returns_View()
        {
            // Given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel();

            // When
            var result = target.Save(viewModel);

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
        }


        [Test]
        public void Given_get_When_Save_Then_Returns_AccidentDetailsViewModel()
        {
            // Given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel();

            // When
            var result = target.Save(viewModel) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<AccidentDetailsViewModel>());
        }

        [Test]
        public void Given_Valid_Model_When_Save_Then_Calls_Correct_Methods()
        {
            // Given
            var target = GetTarget();
             var viewModel = new AccidentDetailsViewModel
                                {
                                    AccidentRecordId = 1L,
                                    DateOfAccident = DateTime.Now.ToShortDateString(),
                                    TimeOfAccident = "12:30",
                                    SiteId = 1L,
                                    OffSiteName = "Other Site Name",
                                    Location = "Location Name",
                                    AccidentTypeId = 1L,
                                    OtherAccidentType = "Other Accident Type",
                                    AccidentCauseId = 1L,
                                    OtherAccidentCause = "Other Accident Cause",
                                    FirstAiderEmployeeId = new Guid(),
                                    NonEmployeeFirstAiderName = "Other First Aider Name",
                                    DetailsOfFirstAid = "Details of first aid"
                                };

            // When
            var result = target.Save(viewModel) as ViewResult;

            // Then
            _accidentRecordService.Verify(
                x => x.UpdateAccidentRecordAccidentDetails(It.IsAny<UpdateAccidentRecordAccidentDetailsRequest>()));
        }

        [Test]
        public void given_site_and_offsite_when_save_then_offsite_is_correct()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                TimeOfAccident = "12:30",
                SiteId = 1L,
                OffSiteName = "Other Site Name",
                Location = "Location Name",
                AccidentTypeId = 1L,
                OtherAccidentType = "Other Accident Type",
                AccidentCauseId = 1L,
                OtherAccidentCause = "Other Accident Cause",
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);
            
            //then
            _accidentRecordService.Verify(
               x =>
               x.UpdateAccidentRecordAccidentDetails(
                   It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                       y => y.OffSiteName == string.Empty)));
        }

        [Test]
        public void given_non_other_accident_type_when_save_then_other_accident_type_is_correct()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                TimeOfAccident = "12:30",
                SiteId = 1L,
                OffSiteName = "Other Site Name",
                Location = "Location Name",
                AccidentTypeId = 1L,
                OtherAccidentType = "Other Accident Type",
                AccidentCauseId = 1L,
                OtherAccidentCause = "Other Accident Cause",
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);

            //then
            _accidentRecordService.Verify(
               x =>
               x.UpdateAccidentRecordAccidentDetails(
                   It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                       y => y.OtherAccidentType == string.Empty)));
        }


        [Test]
        public void given_non_other_accident_cause_when_save_then_other_accident_cause_is_correct()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                TimeOfAccident = "12:30",
                SiteId = 1L,
                OffSiteName = "Other Site Name",
                Location = "Location Name",
                AccidentTypeId = 1L,
                OtherAccidentType = "Other Accident Type",
                AccidentCauseId = 1L,
                OtherAccidentCause = "Other Accident Cause",
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);

            //then
            _accidentRecordService.Verify(
               x =>
               x.UpdateAccidentRecordAccidentDetails(
                   It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                       y => y.OtherAccidentCause == string.Empty)));
        }


        [Test]
        public void given_first_aider_is_employee_when_save_then_other_non_employee_firstaider_name_is_correct()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                TimeOfAccident = "12:30",
                SiteId = 1L,
                OffSiteName = "Other Site Name",
                Location = "Location Name",
                AccidentTypeId = 1L,
                OtherAccidentType = "Other Accident Type",
                AccidentCauseId = 1L,
                OtherAccidentCause = "Other Accident Cause",
                FirstAiderEmployeeId = Guid.NewGuid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);

            //then
            _accidentRecordService.Verify(
               x =>
               x.UpdateAccidentRecordAccidentDetails(
                   It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                       y => y.NonEmployeeFirstAiderName == string.Empty)));
        }

        [Test]
        public void Given_Valid_Model_When_Save_Then_Maps_Correct_Request()
        {
            // Given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
                                {
                                    AccidentRecordId = 1L,
                                    DateOfAccident = DateTime.Now.ToShortDateString(),
                                    TimeOfAccident = "12:30",
                                    SiteId = 1L,
                                    Location = "Location Name",
                                    AccidentTypeId = 1L,                                    
                                    AccidentCauseId = 1L,
                                    FirstAiderEmployeeId = new Guid(),
                                    DetailsOfFirstAid = "Details of first aid"
                                };

            // When
            var result = target.Save(viewModel) as ViewResult;

            // Then
            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                        y => y.AccidentRecordId == viewModel.AccidentRecordId)));

            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(y => y.SiteId == viewModel.SiteId)));

            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(y => y.Location == viewModel.Location)));

            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(y => y.AccidentTypeId == viewModel.AccidentTypeId)));

            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                        y => y.AccidentCauseId == viewModel.AccidentCauseId)));

            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                        y => y.FirstAiderEmployeeId == viewModel.FirstAiderEmployeeId)));

            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                        y => y.DetailsOfFirstAid == viewModel.DetailsOfFirstAid)));
        }

        [Test]
        public void Given_Offsite_and_location_not_specified_then_modelstate_returns_error()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                SiteId = -1,
                OffSiteName = string.Empty,
                Location = "Location Name",
                AccidentTypeId = 1L,
                OtherAccidentType = "Other Accident Type",
                AccidentCauseId = 1L,
                OtherAccidentCause = "Other Accident Cause",
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);
            //then
            Assert.IsFalse(target.ModelState.IsValid);

            Assert.True(target.ModelState.Keys.Contains("OffSiteName"),"Offsite name not specified");
        }

        [Test]
        public void given_accident_type_not_selected_and_other_not_specified_when_save_then_model_state_returns_error()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                SiteId = 1L,
                AccidentTypeId = AccidentDetailsViewModel.OTHER_ACCIDENT_TYPE,
                OtherAccidentType = string.Empty,
                AccidentCauseId = 1L,
                OtherAccidentCause = "Other Accident Cause",
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);
            //then

            Assert.True(target.ModelState.Keys.Contains("OtherAccidentType"), "Other Accident Type not specified");
        }


        [Test]
        public void given_accident_cause_not_selected_and_other_not_specified_when_save_then_model_state_returns_error()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                SiteId = 1L,
                AccidentTypeId = 1L,
                OtherAccidentType = string.Empty,
                AccidentCauseId = AccidentDetailsViewModel.OTHER_ACCIDENT_CAUSE,
                OtherAccidentCause = string.Empty,
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);
            //then

            Assert.True(target.ModelState.Keys.Contains("OtherAccidentCause"), "Other Accident Cause not specified");
        }

        [Test]
        public void given_accident_date_not_specified_when_save_then_model_state_returns_error()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                SiteId = 1L,
                AccidentTypeId = 1L,
                OtherAccidentType = string.Empty,
                AccidentCauseId = 1L,
                OtherAccidentCause = string.Empty,
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);
            //then

            Assert.True(target.ModelState.Keys.Contains("DateOfAccident"), "Date of accident not specified");
        }

        [Test]
        public void given_accident_time_not_specified_when_save_then_model_state_returns_error()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                SiteId = 1L,
                AccidentTypeId = 1L,
                OtherAccidentType = string.Empty,
                AccidentCauseId = 1L,
                OtherAccidentCause = string.Empty,
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);
            //then

            Assert.True(target.ModelState.Keys.Contains("TimeOfAccident"), "Time of accident no tspecified");
        }

        [Test]
        public void given__invalid_accident_time_when_save_then_model_state_returns_error()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                TimeOfAccident = "ABC",
                SiteId = 1L,
                AccidentTypeId = 1L,
                OtherAccidentType = string.Empty,
                AccidentCauseId = 1L,
                OtherAccidentCause = string.Empty,
                FirstAiderEmployeeId = new Guid(),
                NonEmployeeFirstAiderName = "Other First Aider Name",
                DetailsOfFirstAid = "Details of first aid"
            };

            //when
            target.Save(viewModel);
            //then

            Assert.True(target.ModelState.Keys.Contains("TimeOfAccident"), "Time of accident no tspecified");
        }

        [Test]
        public void given_first_aider_not_selected_and_other_not_specified_when_save_then_model_state_returns_error()
        {
            //given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
            {
                AccidentRecordId = 1L,
                DateOfAccident = DateTime.Now.ToShortDateString(),
                SiteId = 1L,
                AccidentTypeId = 1L,
                OtherAccidentType = string.Empty,
                AccidentCauseId = 1L,
                OtherAccidentCause = string.Empty,
                FirstAidAdministered = true,
                FirstAiderEmployeeId = Guid.Empty,
                NonEmployeeFirstAiderName = string.Empty,
            };

            //when
            target.Save(viewModel);
            //then

            Assert.True(target.ModelState.Keys.Contains("NonEmployeeFirstAiderName"), "First Aider not specified");
        }

        [Test]
        public void given_valid_request_when_save_then_dateandtime_of_accident_contains_correct_value()
        {
               // Given
            var target = GetTarget();
            var viewModel = new AccidentDetailsViewModel
                                {
                                    AccidentRecordId = 1L,
                                    DateOfAccident = DateTime.Now.ToShortDateString(),
                                    TimeOfAccident = DateTime.Now.ToShortTimeString(),
                                    SiteId = 1L,
                                    OffSiteName = "Other Site Name",
                                    Location = "Location Name",
                                    AccidentTypeId = 1L,
                                    OtherAccidentType = "Other Accident Type",
                                    AccidentCauseId = 1L,
                                    OtherAccidentCause = "Other Accident Cause",
                                    FirstAiderEmployeeId = new Guid(),
                                    NonEmployeeFirstAiderName = "Other First Aider Name",
                                    DetailsOfFirstAid = "Details of first aid"
                                };

            // When
            var result = target.Save(viewModel) as ViewResult;

            // Then

            var expected =
                DateTime.Parse(viewModel.DateOfAccident).Add(DateTime.Parse(viewModel.TimeOfAccident).TimeOfDay);

            _accidentRecordService.Verify(
                x =>
                x.UpdateAccidentRecordAccidentDetails(
                    It.Is<UpdateAccidentRecordAccidentDetailsRequest>(
                        request => request.DateOfAccident ==expected)));
        }

     
        public AccidentDetailsController GetTarget()
        {
            var factory = new AccidentDetailsViewModelFactory(_employeeService.Object, _siteService.Object,
                                                              _accidentTypeService.Object,
                                                              _causeOfAccidentService.Object, _accidentRecordService.Object);
            var controller = new AccidentDetailsController(factory, _accidentRecordService.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
