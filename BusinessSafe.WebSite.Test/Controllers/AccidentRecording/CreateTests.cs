using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording
{
    [TestFixture]
    public class CreateTests
    {
        private Mock<IAccidentRecordService> _accidentRecordService;
        private CreateAccidentRecordSummaryViewModel _model;
        private const long _companyId = 1L;

        [SetUp]
        public void SetUp()
        {
            _accidentRecordService = new Mock<IAccidentRecordService>();
            _model = new CreateAccidentRecordSummaryViewModel
                         {
                             CompanyId = _companyId,
                             Title = "Title",
                             Reference = "Reference",
                             JurisdictionId = 1L
                         };
        }

        [Test]
        public void When_get_create_Then_should_return_correct_view()
        {
            //Given
            var target = GetTarget();

            //When
            var result = target.Index(1) as ViewResult;

            //Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
            Assert.That(result.Model, Is.TypeOf<CreateAccidentRecordSummaryViewModel>());
        }

        [Test]
        public void when_post_save_then_should_call_correct_methods()
        {
            //Given
            var target = GetTarget();

            //When
            target.Save(_model);

            //Then

            _accidentRecordService.Verify(r => r.CreateAccidentRecord(It.IsAny<SaveAccidentRecordSummaryRequest>()), Times.Once());
        }


        [Test]
        public void when_post_save_then_should_map_save_request()
        {
            //Given
            var target = GetTarget();

            //When
            target.Save(_model);

            //Then

            _accidentRecordService.Verify(
                r =>
                r.CreateAccidentRecord(
                    It.Is<SaveAccidentRecordSummaryRequest>(
                        request =>
                        request.CompanyId == _companyId &&
                        request.JurisdictionId == _model.JurisdictionId &&
                        request.Title == _model.Title &&
                        request.Reference == _model.Reference)));
        }



        private CreateController GetTarget()
        {
            var result = new CreateController(_accidentRecordService.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}