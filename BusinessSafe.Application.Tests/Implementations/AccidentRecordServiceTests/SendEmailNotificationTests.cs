using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;
using NServiceBus;

namespace BusinessSafe.Application.Tests.Implementations.AccidentRecordServiceTests
{
    [TestFixture]
    public class SendEmailNotificationTests
    {
        private Mock<IBus> _bus;
        private Mock<IAccidentRecordRepository> _accidentRecordRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private AccidentRecord _accidentRecord;
        private long _accidentRecordId = 1L;
        private long _companyId = 12L;
        private Site _site;

        [SetUp]
        public void SetUp()
        {
            _bus = new Mock<IBus>();
            _accidentRecordRepository = new Mock<IAccidentRecordRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _user = new UserForAuditing { Id = new Guid() };
            _site = new Site();
            _accidentRecord = new AccidentRecord()
            {
                Id = _accidentRecordId,
                CompanyId = _companyId,
                SiteWhereHappened = _site,
                DoNotSendEmailNotification = false,
                EmailNotificationSent = false
            };


            _userRepository
                .Setup(curUserRepository => curUserRepository.GetByIdAndCompanyId(It.IsAny<Guid>(),It.IsAny<long>()))
                .Returns(_user);


            _accidentRecordRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_accidentRecord);

            var emp = new Employee();
            emp.ContactDetails = new List<EmployeeContactDetail>();
            emp.ContactDetails.Add(new EmployeeContactDetail() { Employee = emp, Email = "ass@asa.com" });

            _site.AddAccidentRecordNotificationMember(emp, _user);

            _siteRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(_site);
            
        }

        [Test]
        public void Given_email_notification_is_required_when_SendAccidentRecordEmails_then_tell_service_bus_to_send()
        {
            //Given
            SendAccidentRecordEmail emailCommand = null;

            _bus.Setup(x => x.Send(It.IsAny<object[]>()))
                .Callback<object[]>(y => emailCommand = y.First() as SendAccidentRecordEmail);

            //when
            var target = GetTarget();
            target.SendAccidentRecordEmails(_accidentRecordId, _companyId, _user.Id);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<object[]>()));
            Assert.That(emailCommand.CompanyId, Is.EqualTo(_companyId));
            Assert.That(emailCommand.AccidentRecordId, Is.EqualTo(_accidentRecordId));
        }

        [Test]
        public void Given_email_notification_is_required_when_SendAccidentRecordEmails_and_email_sent_then_email_notification_sent_status_is_set_to_true()
        {
            //Given
           SendAccidentRecordEmail emailCommand = null;

            _bus.Setup(x => x.Send(It.IsAny<object[]>()))
                .Callback<object[]>(y => emailCommand = y.First() as SendAccidentRecordEmail);

            //when
            var target = GetTarget();
            
            var updatedActionRecord = new AccidentRecord();
            _accidentRecordRepository.Setup(x => x.Save(It.IsAny<AccidentRecord>())).
                Callback<AccidentRecord>(y => updatedActionRecord = y);
                
            target.SendAccidentRecordEmails(_accidentRecordId, _companyId, _user.Id);

            // Then
            _bus.Verify(x => x.Send(It.IsAny<object[]>()));
            Assert.That(emailCommand.CompanyId, Is.EqualTo(_companyId));
            Assert.That(emailCommand.AccidentRecordId, Is.EqualTo(_accidentRecordId));
            Assert.That(updatedActionRecord.EmailNotificationSent, Is.EqualTo(true));
        }

        private IAccidentRecordService GetTarget()
        {
            return new AccidentRecordService(_accidentRecordRepository.Object,
                null,
                null,
                null,
                _userRepository.Object,
                null,
                null,
                _siteRepository.Object,
                null,
                null,
                null,
                null,
                _bus.Object);
        }
    }
}
