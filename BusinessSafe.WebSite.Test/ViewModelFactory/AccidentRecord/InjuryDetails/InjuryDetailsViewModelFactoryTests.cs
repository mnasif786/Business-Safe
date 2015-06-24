using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Constants;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Tests.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.AccidentRecord.InjuryDetails
{
    [TestFixture]
    public class InjuryDetailsViewModelFactoryTests
    {
        private Mock<IAccidentRecordService> _accidentRecordService;
        private Mock<IInjuryService> _injuryService;
        private Mock<IBodyPartService> _bodyPartService;
        private AccidentRecordDto _accidentRecord;

        private long _accidentRecordId = 1234L;
        
        private long _companyId = 54321L;
       // private long _jurisdictionId = 2;

        private const string _ROIMessage = "Was the injured person taken to hospital or treated by a registered medical practitioner?";
        private const string _NonROIMessage = "Was the injured person taken to hospital?";

        [SetUp]
        public void Setup()
        {
            _injuryService = new Mock<IInjuryService>();
            _bodyPartService = new Mock<IBodyPartService>();
            _accidentRecordService = new Mock<IAccidentRecordService>();

            _accidentRecord = new AccidentRecordDto();

            _accidentRecordService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _accidentRecord);

            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithEverything(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _accidentRecord);

            _bodyPartService.Setup(x => x.GetAll())
                .Returns(() => new BodyPartDto[] {new BodyPartDto() {Id = 123}});

            _injuryService.Setup(x => x.GetAll())
                .Returns(() => new InjuryDto[] { new InjuryDto() { Id = 123 } });
        }

        [Test]
        public void test_that_correct_message_returned_from_view_factory_for_ROI()
        {
            // Given
            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithEverything(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned))
              .Returns(
                          new AccidentRecordDto()
                          {
                              Jurisdiction = new JurisdictionDto()
                              {
                                  Id = 2, // ROI jurisdiction is 2 
                                  Name = JurisdictionNames.ROI,
                                  Order = 1
                              }
                          }
              );

            var target = GetTarget();

            // When          
            var result = target.GetViewModel(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned);             

            // Then
            Assert.That(result, Is.InstanceOf<InjuryDetailsViewModel>());

            Assert.AreEqual(_ROIMessage, result.TakenToHospitalMessage);
        }

        [Test]
        public void test_that_correct_message_returned_from_view_factory_for_jurisdictions_other_than_ROI()
        {
            // Given
            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithEverything(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned))
              .Returns(
                          new AccidentRecordDto()
                          {
                              Jurisdiction = new JurisdictionDto()
                              {
                                  Id = 1, // any jurisdiction other than ROI
                                  Name = JurisdictionNames.GB,
                                  Order = 1
                              }
                          }
              );

            var target = GetTarget();

            // When          
            var result = target.GetViewModel(_accidentRecordId, TestControllerHelpers.CompanyIdAssigned);

            // Then
            Assert.That(result, Is.InstanceOf<InjuryDetailsViewModel>());

            Assert.AreEqual(_NonROIMessage, result.TakenToHospitalMessage);
        }
		
        [Test]
        public void Given_there_is_an_unknown_injury_when_GetModel_then_injury_description_is_returned()
        {
            
            //given
            var expectedInjury = "Anterior cruciate ligament";
            _accidentRecord.AccidentRecordInjuries = new List<AccidentRecordInjuryDto>();
            _accidentRecord.AccidentRecordInjuries.Add(new AccidentRecordInjuryDto() {Id = 123123, AdditionalInformation = expectedInjury, Injury = new InjuryDto() {Id = Injury.UNKOWN_INJURY}});
            _accidentRecord.AccidentRecordInjuries.Add(new AccidentRecordInjuryDto() { Id = 1232, Injury = new InjuryDto() { Id = 345 } });
            _accidentRecord.AdditionalInjuryInformation = expectedInjury;

            var target = GetTarget();
            var result = target.GetViewModel(12312, 13123);

            Assert.IsNotNull(result.CustomInjuryDescription);
            Assert.IsTrue(result.CustomInjuryDescription.Length > 0);
            Assert.That(result.CustomInjuryDescription, Is.EqualTo(expectedInjury));
        }

        [Test]
        public void Given_there_is_an_unknown_body_part_when_GetModel_then_injury_description_is_returned()
        {
            var expectedBodyPart = "Unknown body part";
            _accidentRecord.AccidentRecordBodyParts = new List<AccidentRecordBodyPartDto>();

            _accidentRecord.AccidentRecordBodyParts.Add(new AccidentRecordBodyPartDto()
                                                            {
                                                                Id = 1234L,
                                                                AdditionalInformation = expectedBodyPart,
                                                                BodyPart =
                                                                    new BodyPartDto() {Id = BodyPart.UNKOWN_BODY_PART}
                                                            });

            _accidentRecord.AccidentRecordBodyParts.Add(new AccidentRecordBodyPartDto()
            {
                Id = 3456L,
                AdditionalInformation = expectedBodyPart,
                BodyPart =
                    new BodyPartDto() { Id = 345L }
            });

            _accidentRecord.AdditionalBodyPartInformation = expectedBodyPart;

            var target = GetTarget();
            var result = target.GetViewModel(1234L, 1234L);


            Assert.IsNotNull(result.CustomBodyPartyDescription);
            Assert.IsTrue(result.CustomBodyPartyDescription.Length > 0);
            Assert.That(result.CustomBodyPartyDescription, Is.EqualTo(expectedBodyPart));

        }

        [Test]
        public void given_injured_person_is_employee_then_injured_person_able_to_carry_out_work_section_is_visible()
        {
            //given
            _accidentRecord = new AccidentRecordDto
                                  {
                                      Id = 1L,
                                      PersonInvolved = PersonInvolvedEnum.Employee
                                  };

            _accidentRecordService
                .Setup(x => x.GetByIdAndCompanyIdWithEverything(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_accidentRecord);

            //when
            var target = GetTarget();
            var result = target.GetViewModel(1L, 1L);

            //then
            Assert.That(result.ShowInjuredPersonAbleToCarryOutWorkSection,Is.True);
        }

        [Test]
        public void given_injured_person_is_visitor_then_injured_person_able_to_carry_out_work_section_is_not_visible()
        {
            //given
            _accidentRecord = new AccidentRecordDto
            {
                Id = 1L,
                PersonInvolved = PersonInvolvedEnum.Visitor
            };

            _accidentRecordService
                .Setup(x => x.GetByIdAndCompanyIdWithEverything(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_accidentRecord);

            //when
            var target = GetTarget();
            var result = target.GetViewModel(1L, 1L);

            //then
            Assert.That(result.ShowInjuredPersonAbleToCarryOutWorkSection, Is.False);
        }

        [Test]
        public void given_injured_person_is_person_at_work_then_injured_person_able_to_carry_out_work_section_is_visible()
        {
            //given
            _accidentRecord = new AccidentRecordDto
            {
                Id = 1L,
                PersonInvolved = PersonInvolvedEnum.PersonAtWork
            };

            _accidentRecordService
                .Setup(x => x.GetByIdAndCompanyIdWithEverything(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_accidentRecord);

            //when
            var target = GetTarget();
            var result = target.GetViewModel(1L, 1L);

            //then
            Assert.That(result.ShowInjuredPersonAbleToCarryOutWorkSection, Is.True);
        }

        [Test]
        public void given_injured_person_is_other_then_injured_person_able_to_carry_out_work_section_is_not_visible()
        {
            //given
            _accidentRecord = new AccidentRecordDto
            {
                Id = 1L,
                PersonInvolved = PersonInvolvedEnum.Other
            };

            _accidentRecordService
                .Setup(x => x.GetByIdAndCompanyIdWithEverything(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_accidentRecord);

            //when
            var target = GetTarget();
            var result = target.GetViewModel(1L, 1L);

            //then
            Assert.That(result.ShowInjuredPersonAbleToCarryOutWorkSection, Is.False);
        }

 public InjuryDetailsViewModelFactory GetTarget()
        {
            return new InjuryDetailsViewModelFactory( _injuryService.Object, _bodyPartService.Object, _accidentRecordService.Object);
        }

    }
}
