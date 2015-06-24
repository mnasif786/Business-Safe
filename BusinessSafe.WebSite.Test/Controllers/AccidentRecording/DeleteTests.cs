using System;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording
{
    [TestFixture]
    public class DeleteTests
    {
        private Mock<IAccidentRecordService> _accidentRecordService;
        private long _accidentRecordId;

        [SetUp]
        public void Setup()
        {
            _accidentRecordService = new Mock<IAccidentRecordService>();
        }
        [Test]
        public void when_delete_then_call_correct_methods()
        {
            //given
            var controller = GetTarget();
            _accidentRecordId = 1L;

            //when
            controller.Delete(_accidentRecordId);

            //then
            _accidentRecordService.Verify(x => x.Delete(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned, TestControllerHelpers.UserIdAssigned));

        }

        [Test]
        public void Given_success_When_Delete_Then_return_success()
        {
            // Given
            var controller = GetTarget();

            // When
            dynamic result = controller.Delete(_accidentRecordId);

            // Then
            Assert.IsTrue(result.Data.success);
        }

        private AccidentRecordStatusController GetTarget()
        {
            var controller = new AccidentRecordStatusController(_accidentRecordService.Object);

            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
