using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.Search
{
    [TestFixture]
    public class SearchTests
    {
        private Mock<ISiteService> _siteService;
        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<IAccidentRecordService> _accidentRecordSevice;

        [SetUp]
        public void SetUp()
        {
            _siteService = new Mock<ISiteService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _accidentRecordSevice = new Mock<IAccidentRecordService>();
        }

        [Test]
        public void Given_get_When_Index_Then_Returns_View()
        {
            // Given
            var target = GetTarget();

            // When
            AccidentRecordsIndexViewModel model = new AccidentRecordsIndexViewModel() { Title = "some test title" };

            var result = target.Index(model);

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
        }

        private SearchController GetTarget()
        {
            var factory = new SearchAccidentRecordViewModelFactory(_accidentRecordSevice.Object,
                                                                   _siteGroupService.Object, _siteService.Object);
            var controller = new SearchController(factory);

            return TestControllerHelpers.AddUserToController(controller); // ?
        }
    }
}
