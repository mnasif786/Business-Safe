using System;
using System.Collections.Generic;
using System.Linq;
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
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Tests.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.AccidentRecord.Overview
{
    [TestFixture]
    public class AccidentRecordOverviewViewModelFactoryTests
    {     
        private Mock<IAccidentRecordService> _accidentRecordService;
        private AccidentRecordDto _accidentRecordDto;
        private Mock<ISiteService> _siteService;
        
        [SetUp]
        public void SetUp()
        {
            _accidentRecordDto = new AccidentRecordDto() {Id = 123123, AccidentRecordDocuments = new List<AccidentRecordDocumentDto>()};
            _accidentRecordService = new Mock<IAccidentRecordService>();
            _siteService = new Mock<ISiteService>();

            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithAccidentRecordDocuments(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => _accidentRecordDto);
        }

        [Test]
        public void given_valid_accident_record_id_when_getviewmodel_then_result_contains_correct_accident_record_Id()
        {
            //given
            var accidentRecordId = _accidentRecordDto.Id;

            var factory = GetTarget();

            //when
            var result = factory
                .WithAccidentRecordId(accidentRecordId)
                .GetViewModel();

            //then            
            Assert.That(result.AccidentRecordId, Is.EqualTo(accidentRecordId));
        }

        [Test]
        public void given_valid_events_description_on_record_when_getviewmodel_then_result_contains_correct_events_description()
        {
            //given
            _accidentRecordDto.DescriptionHowAccidentHappened = "Event Description";

            var factory = GetTarget();

            //when
            var result = factory
                .GetViewModel();

            //then            
            Assert.That(result.DescriptionHowAccidentHappened, Is.EqualTo(_accidentRecordDto.DescriptionHowAccidentHappened));
        }

        [Test]
        public void given_valid_company_when_getviewmodel_then_result_contains_correct_company()
        {
            //given
            var company = 1L;

            var factory = GetTarget();

            //when
            var result = factory
               .WithCompanyId(company)
                .GetViewModel();

            //then            
            Assert.That(result.CompanyId, Is.EqualTo(company));
        }

        [Test]
        public void given_valid_documents_on_record_when_getviewmodel_then_result_contains_correct_documents()
        {
            //given
            _accidentRecordDto.AccidentRecordDocuments = new List<AccidentRecordDocumentDto>()
                                   {
                                       new AccidentRecordDocumentDto()
                                           {
                                               Description = "description",
                                               Filename = "Filename.ext",
                                               Id = 1L,
                                               DocumentLibraryId = 2L,
                                               DocumentType = new DocumentTypeDto()
                                               {
                                                   Id = 1L,
                                                   Name = "Accident Record"
                                               }
                                           }
                                   };


            var factory = GetTarget();

            //when
            var result = factory
                .GetViewModel();

            //then            
            Assert.That(result.Documents.ExistingDocumentsViewModel.PreviouslyAddedDocuments.Count, Is.EqualTo(_accidentRecordDto.AccidentRecordDocuments.Count));
        }

        [Test]
        public void given_accident_happend_site_company_when_getviewmodel_then_result_contains_emails_of_the_members_of_distribution_group_for_this_site()
        {
            //given
            var company = 1L;

            var  accidentRecordNotificationMemberEmails = new List<AccidentRecordNotificationMember>();
            accidentRecordNotificationMemberEmails.Add( 
                new AccidentRecordNotificationEmployeeMember() { 
                    Id = 1, Employee = new Employee() {
                    ContactDetails = new List<EmployeeContactDetail>
                    {
                        new EmployeeContactDetail() {Email = "test@abc.com"}
                    }
                }});

            accidentRecordNotificationMemberEmails.Add(
                new AccidentRecordNotificationEmployeeMember()
                {
                    Id = 2,
                    Employee = new Employee()
                    {
                        ContactDetails = new List<EmployeeContactDetail>
                    {
                        new EmployeeContactDetail() {Email = "test@xyz.com"}
                    }
                    }
                });

            _accidentRecordDto.SiteWhereHappened = new SiteDto() { Id = 1L };
            _siteService.Setup(x => x.GetAccidentRecordNotificationMembers(It.IsAny<long>()))
                .Returns(() => accidentRecordNotificationMemberEmails);

            var factory = GetTarget();

            //when
            var result = factory
               .WithCompanyId(company)
                .GetViewModel();

            //then            
            Assert.That(result.AccidentRecordNotificationMemberEmails.Count, Is.EqualTo(2));
        }

        [Test]
        public void given_email_notification_sent_getviewmodel_then_result_contains_correct_email_sent_status()
        {
            //given
            var company = 1L;
            
            var accidentRecord = new AccidentRecordDto()
            {
                EmailNotificationSent = true,
                Id = 1
            };

            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithAccidentRecordDocuments(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(() => accidentRecord);

            var factory = GetTarget();

            //when
            var result = factory
               .WithCompanyId(company)
                .GetViewModel();

            //then            
            Assert.That(result.EmailNotificationSent, Is.EqualTo(true));
        }

        public AccidentRecordOverviewViewModelFactory GetTarget()
        {
            return new AccidentRecordOverviewViewModelFactory(_accidentRecordService.Object, _siteService.Object);
        }
    }
}
