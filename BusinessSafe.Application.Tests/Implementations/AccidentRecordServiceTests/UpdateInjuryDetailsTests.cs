using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.Constants;

namespace BusinessSafe.Application.Tests.Implementations.AccidentRecordServiceTests
{
    [TestFixture]
    public class UpdateInjuryDetailsTests
    {

        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IInjuryRepository> _injuryRepo;
        private Mock<IBodyPartRepository> _bodyRepo;

        private Mock<IPeninsulaLog> _log;
        private UserForAuditing _user = new UserForAuditing() { Id = Guid.NewGuid(), CompanyId = 589 };
        private AccidentRecord _accidentRecord = new AccidentRecord() { Id = 123123 };
        private UpdateInjuryDetailsRequest _request;

        [SetUp]
        public void Setup()
        {
            _accidentRecord = new AccidentRecord() { Id = 123123, Jurisdiction = new Jurisdiction{ Name = JurisdictionNames.GB } };

            _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
            _injuryRepo = new Mock<IInjuryRepository>();
            _bodyRepo = new Mock<IBodyPartRepository>();

            _userRepository
                .Setup(curUserRepository => curUserRepository.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => _user);

            _userRepository
                .Setup(curUserRepository => curUserRepository.GetById(It.IsAny<Guid>()))
                .Returns(() => _user);

            _accidentRecordRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _accidentRecord);

            _bodyRepo.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns<long>(x => new BodyPart(){Id = x});

            _injuryRepo.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns<long>(x => new Injury() { Id = x });

            _bodyRepo.Setup(x => x.GetByIds(It.IsAny<IList<long>>()))
                .Returns<IList<long>>(x =>  x.Select(id => new BodyPart() { Id = id }));

            _injuryRepo.Setup(x => x.GetByIds(It.IsAny<IList<long>>()))
                .Returns<IList<long>>(ids => ids.Select(id => new Injury() { Id = id }));

            _request = new UpdateInjuryDetailsRequest()
            {
                AccidentRecordId = 56456,
                CurrentUserId = _user.Id
            };
        }

        [Test]
        public void Given_body_parts_are_added_then_body_part_list_is_updated()
        {
            _request.BodyPartsInjured = new List<long>(){54234};
            var target = GetTarget();

            target.UpdateInjuryDetails(_request);

            Assert.IsTrue(_accidentRecord.AccidentRecordBodyParts.Any(x=> x.BodyPart.Id == _request.BodyPartsInjured.First()),"Body part not added");
        }

        [Test]
        public void Given_body_parts_are_removed_then_body_part_list_is_updated()
        {
            _accidentRecord.AddBodyPartThatWasInjured(new BodyPart() {Id = 12}, null);
            _accidentRecord.AddBodyPartThatWasInjured(new BodyPart() {Id = 123}, null);

            _request.BodyPartsInjured = new List<long>() { 123 };
            var target = GetTarget();

            target.UpdateInjuryDetails(_request);

            Assert.AreEqual(1, _accidentRecord.AccidentRecordBodyParts.Count);
            Assert.IsFalse(_accidentRecord.AccidentRecordBodyParts.Any(x => x.BodyPart.Id == 12), "Body part not removed");
            Assert.IsTrue(_accidentRecord.AccidentRecordBodyParts.Any(x => x.BodyPart.Id == 123), "Body part should not be removed");
        }

        [Test]
        public void Given_unknown_body_part_are_added_then_body_part_list_is_updated_with_additionalinformation()
        {
            _request.BodyPartsInjured= new List<long>{BodyPart.UNKOWN_BODY_PART,234234};
            _request.CustomBodyPartDescription = "Other unknown body part";

            var target = GetTarget();
            
            target.UpdateInjuryDetails(_request);

            Assert.That(_accidentRecord.AccidentRecordBodyParts.Any(x=>x.AdditionalInformation == _request.CustomBodyPartDescription));
            
        }

        [Test]
        public void Given_injuries_are_added_then_injury_list_is_updated()
        {
            _request.Injuries = new List<long>() { 54234,324234 };
            var target = GetTarget();

            target.UpdateInjuryDetails(_request);

            Assert.AreEqual(2, _accidentRecord.AccidentRecordInjuries.Count);
            Assert.IsTrue(_accidentRecord.AccidentRecordInjuries.Any(x => x.Injury.Id == _request.Injuries.First()), "Injury not added");
        }

        [Test]
        public void Given_unknown_injury_are_added_then_injury_list_is_updated_with_additionalinformation()
        {
            _request.Injuries = new List<long>() { Injury.UNKOWN_INJURY, 324234 };
            _request.CustomInjuryDescription = "Other unknown injury";

            var target = GetTarget();

            target.UpdateInjuryDetails(_request);

            Assert.AreEqual(2, _accidentRecord.AccidentRecordInjuries.Count);
            Assert.IsTrue(_accidentRecord.AccidentRecordInjuries.Any(x => x.AdditionalInformation == _request.CustomInjuryDescription), "Addition information not added");
        }

        [Test]
        public void Given_injuries_are_removed_then_injury_list_is_updated()
        {
            _accidentRecord.AddInjury(new Injury() { Id = 12 }, null);
            _accidentRecord.AddInjury(new Injury() { Id = 123 }, null);

            _request.Injuries = new List<long>() { 123 };
            var target = GetTarget();

            target.UpdateInjuryDetails(_request);

            Assert.AreEqual(1, _accidentRecord.AccidentRecordInjuries.Count);
            Assert.IsFalse(_accidentRecord.AccidentRecordInjuries.Any(x => x.Injury.Id == 12), "Injury not removed");
            Assert.IsTrue(_accidentRecord.AccidentRecordInjuries.Any(x => x.Injury.Id == 123), "Injury should not be removed");
        }

        [Test]
        public void Given_update_injury_request_then_accident_record_is_updated()
        {
            _request.SeverityOfInjury = SeverityOfInjuryEnum.Major;
            _request.WasTheInjuredPersonBeenTakenToHospital = true;
            _request.HasTheInjuredPersonBeenAbleToCarryOutTheirNormalWorkActivity = YesNoUnknownEnum.Yes;
            _request.LengthOfTimeUnableToCarryOutWork = LengthOfTimeUnableToCarryOutWorkEnum.FourToSevenDays;
            var target = GetTarget();

            target.UpdateInjuryDetails(_request);

            Assert.AreEqual(_request.SeverityOfInjury, _accidentRecord.SeverityOfInjury);
            Assert.AreEqual(_request.WasTheInjuredPersonBeenTakenToHospital, _accidentRecord.InjuredPersonWasTakenToHospital);
            Assert.AreEqual(_request.HasTheInjuredPersonBeenAbleToCarryOutTheirNormalWorkActivity.Value, _accidentRecord.InjuredPersonAbleToCarryOutWork.Value);
            Assert.AreEqual(_request.LengthOfTimeUnableToCarryOutWork.Value, _accidentRecord.LengthOfTimeUnableToCarryOutWork.Value);
        }


        private AccidentRecordService GetTarget()
        {
            return new AccidentRecordService(_accidentRecordRepository.Object,
                                             null,
                                             null,
                                             null,
                                             _userRepository.Object,
                                             null,
                                             null,
                                             null,
                                             null,
                                             _injuryRepo.Object,
                                             _bodyRepo.Object,
                                             _log.Object,
                                             null);
        }
    }
}
