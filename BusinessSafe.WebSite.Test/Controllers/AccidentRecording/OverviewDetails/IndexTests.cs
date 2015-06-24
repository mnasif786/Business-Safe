using System;
using System.Collections.Generic;
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

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.OverviewDetails
{
    [TestFixture]
    public class IndexTests
    {
        private Mock<IAccidentRecordService> _accidentRecordSevice;
        private long _accidentRecordId;
        private long _companyId;

        [SetUp]
        public void SetUp()
        {
            _accidentRecordId = 1L;
            _companyId = 1234L;

            _accidentRecordSevice = new Mock<IAccidentRecordService>();

            AccidentRecordDocumentDto[] docs = { new AccidentRecordDocumentDto(){ DocumentId = 1L} };

            _accidentRecordSevice.Setup(x => x.GetByIdAndCompanyIdWithAccidentRecordDocuments(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new AccidentRecordDto
                {
                    Id = 1L,
                    AccidentRecordDocuments = new List<AccidentRecordDocumentDto>()
                });
        }
        
        [Test]
        public void Given_get_When_Index_Then_Returns_View()
        {
            // Given
            var target = GetTarget();
            // When
            var result = target.Index(_accidentRecordId, 12L);

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Given_get_When_Index_Then_Returns_AccidentDetailsViewModel()
        {
            // Given
            var target = GetTarget();

            // When
            var result = target.Index(_accidentRecordId, 12L) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<OverviewViewModel>());
        }


        public OverviewController GetTarget()
        {
            var factory = new AccidentRecordOverviewViewModelFactory(_accidentRecordSevice.Object);

            var controller = new OverviewController(factory, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
