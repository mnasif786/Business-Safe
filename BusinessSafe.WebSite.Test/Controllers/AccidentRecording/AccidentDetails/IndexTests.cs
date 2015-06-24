using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.AccidentDetails
{
    [TestFixture]
    public class IndexTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;
        private Mock<IAccidentTypeService> _accidentTypeService;
        private Mock<ICauseOfAccidentService> _causeOfAccidentService;
        private Mock<IAccidentRecordService> _accidentRecordSevice;
        private long _comapnyId = 1234L;
        private long _accidentRecordId = 1L;

        [SetUp]
        public void SetUp()
        {
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();
            _accidentTypeService = new Mock<IAccidentTypeService>();
            _causeOfAccidentService = new Mock<ICauseOfAccidentService>();
            _accidentRecordSevice = new Mock<IAccidentRecordService>();
        }

        [Test]
        public void Given_get_When_Index_Then_Returns_View()
        {
            // Given
            var target = GetTarget();
            // When
            var result = target.Index(_accidentRecordId, _comapnyId);

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
        }


        [Test]
        public void Given_get_When_Index_Then_Returns_AccidentDetailsViewModel()
        {
            // Given
            var target = GetTarget();
            // When
            var result = target.Index(_accidentRecordId, _comapnyId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<AccidentDetailsViewModel>());
        }


        public AccidentDetailsController GetTarget()
        {
            var factory = new AccidentDetailsViewModelFactory(_employeeService.Object, _siteService.Object,
                                                              _accidentTypeService.Object,
                                                              _causeOfAccidentService.Object,
                                                              _accidentRecordSevice.Object);
            var controller = new AccidentDetailsController(factory, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
