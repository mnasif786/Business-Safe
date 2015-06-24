using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Constants;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using NUnit.Framework;
using Moq;
using BusinessSafe.Application.Contracts.AccidentRecord;

namespace BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.InjuryDetails
{
    //TODO: remove magic numbers for jurisdictions
    [TestFixture]
    public class IndexTests
    {
        private Mock<IAccidentRecordService> _accidentRecordService;
        private Mock<IInjuryService> _injuryService;
        private Mock<IBodyPartService> _bodyPartService;

        private long _accidentRecordId = 1234L;       
        private long _jurisdictionId = 2;

        private const string _ROIMessage = "Was the injured person taken to hospital or treated by a registered medical practitioner?";
        private const string _NonROIMessage = "Was the injured person taken to hospital?";

       private Dictionary< string,  string> _GuidanceNotesID = new Dictionary<string, string>(); 
              
        [SetUp]
        public void SetUp()
        {
            _accidentRecordService = new Mock<IAccidentRecordService>();
            _injuryService = new Mock<IInjuryService>();
            _bodyPartService = new Mock<IBodyPartService>();

            _GuidanceNotesID[JurisdictionNames.GB] = "21";
            _GuidanceNotesID[JurisdictionNames.IoM] = "";            
            _GuidanceNotesID[JurisdictionNames.NI] = "22";
            _GuidanceNotesID[JurisdictionNames.ROI] = "20";                          
            _GuidanceNotesID[JurisdictionNames.Jersey] = String.Empty;
            _GuidanceNotesID[JurisdictionNames.Guernsey] = String.Empty;            
        }


        [Test]
        public void test_that_correct_message_returned_from_controller_for_ROI()
        {
            // Given
            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithEverything(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned))
              .Returns(
                          new AccidentRecordDto()
                          {
                              Jurisdiction = new JurisdictionDto()
                              {
                                  // Id = 2, ID not used - check is agains the name string?? - ROI jurisdiction is 2 
                                  Name = JurisdictionNames.ROI,
                                  Order = 1
                              }
                          }
              );

            var target = GetTarget();

            // When
            //CurrentUser.CompanyId
            var result = target.Index(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<InjuryDetailsViewModel>());

            Assert.AreEqual(_ROIMessage, ((InjuryDetailsViewModel)result.Model).TakenToHospitalMessage);
        }

        [Test]
        public void test_that_correct_message_returned_from_controller_for_jurisdictions_other_than_ROI()
        {
            // Given
            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithEverything(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned))
              .Returns(
                          new AccidentRecordDto()
                          {
                              Jurisdiction = new JurisdictionDto()
                              {
                                  // Id = 2, ID not used - check is agains the name string?? - ROI jurisdiction is 2 
                                  Name = JurisdictionNames.GB,
                                  Order = 1
                              }
                          }
              );

            var target = GetTarget();

            // When
            //CurrentUser.CompanyId
            var result = target.Index(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<InjuryDetailsViewModel>());

            Assert.AreEqual(_NonROIMessage, ((InjuryDetailsViewModel)result.Model).TakenToHospitalMessage);
        }

    
        [Test]
        [TestCase(JurisdictionNames.GB)]
        [TestCase(JurisdictionNames.NI)]
        [TestCase(JurisdictionNames.IoM)]
        [TestCase(JurisdictionNames.ROI)]
        public void test_that_correct_guidance_notes_are_shown_for_jurisdiction(string jurisdictionName)
        {
            // Given
            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithEverything(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned))
              .Returns(
                          new AccidentRecordDto()
                          {
                              SeverityOfInjury = SeverityOfInjuryEnum.Major, 

                              Jurisdiction = new JurisdictionDto()
                              {                                  
                                  Name =  jurisdictionName,
                                  Order = 1
                              }
                          }
              );

            var target = GetTarget();

            // When
            //CurrentUser.CompanyId
            var result = target.Index(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<InjuryDetailsViewModel>());
                      
            Assert.AreEqual(_GuidanceNotesID[jurisdictionName], ((InjuryDetailsViewModel)result.Model).GuidanceNotesId);      
        }


        private InjuryDetailsController GetTarget()
        {
            InjuryDetailsViewModelFactory factory = new InjuryDetailsViewModelFactory(  _injuryService.Object, 
                                                                                        _bodyPartService.Object, 
                                                                                        _accidentRecordService.Object);

            InjuryDetailsController controller = new InjuryDetailsController(factory, _accidentRecordService.Object);           
            return TestControllerHelpers.AddUserToController(controller);        
        }
    }
}
