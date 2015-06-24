using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using ActionMailer.Net;
using ActionMailer.Net.Standalone;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.MessageHandlers.Emails.Activation;
using BusinessSafe.MessageHandlers.Emails.EmailSender;
using BusinessSafe.MessageHandlers.Emails.MessageHandlers;
using BusinessSafe.MessageHandlers.Emails.ViewModels;
using BusinessSafe.Messages.Emails.Commands;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test
{
    [TestFixture]
    public class SendAccidentRecordEmailTests
    {
        private Mock<IEmailSender> _emailSender;
        private SendAccidentRecordEmail _message;
        private Mock<IAccidentRecordService> _accidentRecordService;
        private Mock<IBusinessSafeEmailLinkBaseUrlConfiguration> _businesSafeEmailLinkBaseUrlConfiguration;
        private Mock<ISiteService> _siteService;
       
        [SetUp]
        public void SetUp()
        {
            _siteService = new Mock<ISiteService>();
            _emailSender = new Mock<IEmailSender>();
            _accidentRecordService = new Mock<IAccidentRecordService>();
            _businesSafeEmailLinkBaseUrlConfiguration = new Mock<IBusinessSafeEmailLinkBaseUrlConfiguration>();

            _businesSafeEmailLinkBaseUrlConfiguration.Setup(x => x.GetBaseUrl()).Returns("url");

            _message = new SendAccidentRecordEmail()
            {
                AccidentRecordId = 11L,
                CompanyId = 222L
            };

        
            var siteNotificationMembers = new List<AccidentRecordNotificationMember>()
            {
                new AccidentRecordNotificationEmployeeMember()
                {
                    Id = 1,
                    Employee = new Employee()
                    {
                        ContactDetails = new List<EmployeeContactDetail>()
                        {
                            new EmployeeContactDetail()
                            {
                                Email = "email1@email.com"
                            }
                        }
                    }
                }
            };


            _siteService.Setup(x => x.GetAccidentRecordNotificationMembers(It.IsAny<long>()))
            .Returns(siteNotificationMembers);

            var accidentRecord1 = new AccidentRecordDto()
            {
                Id = 11L,
                DateAndTimeOfAccident = DateTime.Now,
                Location = "Location",
                PersonInvolved = PersonInvolvedEnum.Employee,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                DescriptionHowAccidentHappened = "dd",
                InjuredPersonWasTakenToHospital = true,
                IsReportable = true,
                AccidentRecordInjuries = new List<AccidentRecordInjuryDto>() { new AccidentRecordInjuryDto() { Injury = new InjuryDto() { Description = "injury" } } },
                SiteWhereHappened = new SiteDto() { Id = 11, Name = "Site 1" }
            };

           
            _accidentRecordService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(accidentRecord1);
            

        }
        
        [Test]
        public void When_handle_Then_should_populate_view_model_from_AccidentRecord()
        {
            //Given
            var handler = CreateTarget();

            // When
            handler.Handle(_message);
            
            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()));
        }

        [Test]
        public void When_handle_but_notify_members_not_defined_Then_should_not_send_email()
        {
            //Given
            var accidentMembers =
               new List<AccidentRecordNotificationMember>()
                {
                    new AccidentRecordNotificationEmployeeMember()
                    {
                        Employee = new Employee()
                        {
                            Id = Guid.NewGuid(),
                            ContactDetails = new List<EmployeeContactDetail>() { new EmployeeContactDetail() { Email = null} }
                        }
                    }
                };

            _siteService.Setup(x => x.GetAccidentRecordNotificationMembers(It.IsAny<long>())).Returns(accidentMembers);

            var accidentRecord1 = new AccidentRecordDto()
            {
                Id = 11L,
                DateAndTimeOfAccident = DateTime.Now,
                Location = "Location",
                PersonInvolved = PersonInvolvedEnum.Employee,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                DescriptionHowAccidentHappened = "dd",
                InjuredPersonWasTakenToHospital = true,
                IsReportable = true,
                AccidentRecordInjuries = new List<AccidentRecordInjuryDto>() { new AccidentRecordInjuryDto() { Injury = new InjuryDto() { Description = "injury" } } },
                SiteWhereHappened = new SiteDto() { Id = 11, Name = "Site 1" }
            };
            
            _accidentRecordService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(accidentRecord1);

            var handler = CreateTarget();

            // When
            handler.Handle(_message);

            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()), Times.Never());
        }


        [Test]
        public void When_notification_email_list_assert_multiple_emails_Sent()
        {
            var siteNotificationMembers = new List<AccidentRecordNotificationMember>()
            {
                new AccidentRecordNotificationEmployeeMember()
                {
                    Id = 1,
                    Employee = new Employee()
                    {
                        ContactDetails = new List<EmployeeContactDetail>()
                        {
                            new EmployeeContactDetail()
                            {
                                Email = "email1@email.com"
                            }
                        }
                    }
                },

                new AccidentRecordNotificationEmployeeMember()
                {
                    Id = 2,
                    Employee = new Employee()
                    {
                        ContactDetails = new List<EmployeeContactDetail>()
                        {
                            new EmployeeContactDetail()
                            {
                                Email = "email2@email.com"
                            }
                        }
                    }
                },

                new AccidentRecordNotificationNonEmployeeMember() 
                {
                    Id = 3,
                    NonEmployeeEmail = "norman@not-an-employee.com",
                    NonEmployeeName = "Norman Notemployedhere"
                }
            };

            _siteService.Setup(x => x.GetAccidentRecordNotificationMembers(It.IsAny<long>()))
                .Returns(siteNotificationMembers);

            //Given
            var accidentRecord1 = new AccidentRecordDto()
            {
                Id = 11L,
                DateAndTimeOfAccident = DateTime.Now,
                Location = "Location",
                PersonInvolved = PersonInvolvedEnum.Employee,
                SeverityOfInjury = SeverityOfInjuryEnum.Fatal,
                DescriptionHowAccidentHappened = "dd",
                InjuredPersonWasTakenToHospital = true,
                IsReportable = true,
                AccidentRecordInjuries = new List<AccidentRecordInjuryDto>() { new AccidentRecordInjuryDto() { Injury = new InjuryDto() { Description = "injury" } } },
                SiteWhereHappened = new SiteDto() { Id = 11, Name = "Site 1" }
            };

            _accidentRecordService.Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
               .Returns(accidentRecord1);

             var handler = CreateTarget();

            // When
            handler.Handle(_message);

            // Then
            _emailSender.Verify(x => x.Send(It.IsAny<RazorEmailResult>()), Times.Exactly(3));
        }
        
        private SendAccidentRecordEmailHandler CreateTarget()
        {
            var handler = new MySendAccidentRecordEmailHandler(_emailSender.Object, _accidentRecordService.Object, _businesSafeEmailLinkBaseUrlConfiguration.Object, _siteService.Object);
            return handler;
        }

        public class MySendAccidentRecordEmailHandler : SendAccidentRecordEmailHandler
        {
            public MySendAccidentRecordEmailHandler(IEmailSender emailSender, IAccidentRecordService accidentRecordService, IBusinessSafeEmailLinkBaseUrlConfiguration urlConfiguration, ISiteService siteService)
                : base(emailSender, accidentRecordService, urlConfiguration, siteService)
            {
            }

            protected override RazorEmailResult CreateRazorEmailResult(AccidentRecordEmailViewModel viewModel)
            {
                return new RazorEmailResult(new Mock<IMailInterceptor>().Object,
                                            new Mock<IMailSender>().Object,
                                            new Mock<MailMessage>().Object,
                                            "ViewName",
                                            Encoding.ASCII,
                                            "ViewPath");
            }
        }

    }
}
