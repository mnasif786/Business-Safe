using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.OverviewDetails
{
    [TestFixture]
    public class SaveTests
    {
        private Mock<IAccidentRecordService> _accidentRecordService;
        private long _companyId;
        private long _accidentRecordId;

        [SetUp]
        public void Setup()
        {
            _companyId = 1234L;
            _accidentRecordId = 1L;

            _accidentRecordService = new Mock<IAccidentRecordService>();

            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithAccidentRecordDocuments(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(new AccidentRecordDto
               {
                   Id = 1L,
                   AccidentRecordDocuments = new List<AccidentRecordDocumentDto>()
               });
        }

        [Test]
        public void Given_get_When_Save_Then_Returns_View()
        {
            // Given
            var target = GetTarget();
            var viewModel = new OverviewViewModel()
            {
                AccidentRecordId = _accidentRecordId,
                DescriptionHowAccidentHappened = "desc"
            };
            var viewDocModel = new DocumentsToSaveViewModel();

            // When
            var result = target.Save(_accidentRecordId, _companyId, viewModel, viewDocModel);

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Given_Get_When_Save_Then_Returns_OverviewViewModel()
        {
            // Given
            var target = GetTarget();
            var viewModel = new OverviewViewModel();
            var viewDocModel = new DocumentsToSaveViewModel();
            
            // When
            var result = target.Save(_accidentRecordId, _companyId, viewModel, viewDocModel) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<OverviewViewModel>());
        }

        [Test]
        public void given_description_added_check_saved()
        {
            //given
            var target = GetTarget();
            var viewModel = new OverviewViewModel
            {
                AccidentRecordId = _accidentRecordId,
                DescriptionHowAccidentHappened = "desc"
            };

            var viewDocModel = new DocumentsToSaveViewModel();

            //when
            target.Save(_accidentRecordId, _companyId, viewModel, viewDocModel);

            //Then
            _accidentRecordService.Verify(
                x =>
                    x.SetAccidentRecordOverviewDetails(
                      It.Is<AccidentRecordOverviewRequest>(y => y.Description == "desc")));
        }
        
        [Test]
        public void Given_Description_Not_Added_Check_Description_Not_Saved()
        {
            //given
            var target = GetTarget();
            var viewModel = new OverviewViewModel();
            var viewDocModel = new DocumentsToSaveViewModel();

            //when
            target.Save(_accidentRecordId, _companyId, viewModel, viewDocModel);

            //Then
            _accidentRecordService.Verify(x => x.SetAccidentRecordOverviewDetails(It.IsAny<AccidentRecordOverviewRequest>()), Times.Never());
        }

        public OverviewController GetTarget()
        {
            var factory = new AccidentRecordOverviewViewModelFactory(_accidentRecordService.Object);

            var controller = new OverviewController(factory, _accidentRecordService.Object);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
