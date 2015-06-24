using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Tests.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.AccidentRecord.AccidentDetails
{
    [TestFixture]
    [Category("Unit")]
    public class AccidentDetailsViewModelFactoryTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;
        private Mock<IAccidentTypeService> _accidentTypeService;
        private Mock<ICauseOfAccidentService> _causeOfAccidentService;
        private Mock<IAccidentRecordService> _accidentRecordService;
        
        [SetUp]
        public void SetUp()
        { 
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();
            _accidentTypeService = new Mock<IAccidentTypeService>();
            _causeOfAccidentService = new Mock<ICauseOfAccidentService>();
            _accidentRecordService = new Mock<IAccidentRecordService>();
        }

        [Test]
        public void given_valid_company_when_getviewmodel_then_result_contains_correct_employees()
        {
            //given
            var employeeDto = new EmployeeDto
                                  {
                                      Id = new Guid()
                                  };
            var employees = new List<EmployeeDto>
                                {
                                    employeeDto
                                };

            _employeeService.Setup(x => x.GetAll(It.IsAny<long>())).Returns(employees);

            
            var factory = GetTarget();

            //when
            var result = factory
                .WithCompanyId(1L)
                .GetViewModel();

            //then            
            Assert.That(result.Employees.Skip(1).First().value, Is.EqualTo(employeeDto.Id.ToString()));
        }


         [Test]
        public void given_valid_company_when_getviewmodel_then_result_contains_correct_sites()
         {
             //given
             var site = new SiteDto
                            {
                                Id =1L
                            };
             var sites = new List<SiteDto>
                             {
                                 site
                             };

             _siteService.Setup(x => x.Search(It.IsAny<SearchSitesRequest>())).Returns(sites);

             var factory = GetTarget();

             //when
             var result = factory
                 .WithCompanyId(1L)
                 .GetViewModel();

             //then            
             Assert.That(result.Sites.Skip(1).First().value, Is.EqualTo(site.Id.ToString()));
         }

        [Test]
         public void given_valid_company_when_getviewmodel_then_result_contains_correct_AccidentTypes()
        {
            //given
            var accidentType = new AccidentTypeDto
                                   {
                                       Id = 1L
                                   };

            var accidentTypes = new List<AccidentTypeDto>
                                    {
                                        accidentType
                                    };

            _accidentTypeService.Setup(x => x.GetAllForCompany(It.IsAny<long>())).Returns(accidentTypes);

            var factory = GetTarget();

            //when
            var result = factory
                .WithCompanyId(1L)
                .GetViewModel();

            //then            
            Assert.That(result.AccidentTypes.Skip(1).First().value, Is.EqualTo(accidentType.Id.ToString()));

        }

        [Test]
        public void given_valid_company_when_getviewmodel_then_result_contains_correct_CauseOfAccident()
        {
            //given
            var causeOfAccident = new CauseOfAccidentDto
            {
                Id = 1L
            };

            var causeOfAccidents = new List<CauseOfAccidentDto>
                                    {
                                        causeOfAccident
                                    };

            _causeOfAccidentService.Setup(x => x.GetAll()).Returns(causeOfAccidents);

            var factory = GetTarget();

            //when
            var result = factory
                .WithCompanyId(1L)
                .GetViewModel();

            Assert.That(result.AccidentCauses.Skip(1).First().value, Is.EqualTo(causeOfAccident.Id.ToString()));

        }

        [Test]
        public void given_valid_accident_record_id_when_getviewmodel_then_result_contains_correct_accident_record()
        {
            //given
            var accidentRecordId = 1L;
            var accidentRecord = new AccidentRecordDto
                                     {
                                         Id = accidentRecordId,
                                         DateAndTimeOfAccident = DateTime.Now,
                                         SiteWhereHappened = new SiteDto { Id = 1L, Name = "Site" },
                                         OffSiteSpecifics = string.Empty,
                                         Location = "Location",
                                         AccidentType = new AccidentTypeDto { Id = 1L, Description = "Accident Type" },
                                         AccidentTypeOther = "Other accident type",
                                         CauseOfAccident = new CauseOfAccidentDto() { Id = 1L, Description = "Accident Cause" },
                                         CauseOfAccidentOther = "Other accident cause",
                                         EmployeeFirstAider = new EmployeeDto { Id = new Guid(), FullName = "Joe Blogs"},
                                         NonEmployeeFirstAiderSpecifics = "Another Person",
                                         DetailsOfFirstAidTreatment = "First Aid Details" 

                                     };

            _accidentRecordService.Setup(x => x.GetByIdAndCompanyIdWithSite(It.IsAny<long>(), It.IsAny<long>())).Returns(accidentRecord);

            var factory = GetTarget();

            //when
            var result = factory
                .WithCompanyId(1L)
                .GetViewModel();

            Assert.That(result.AccidentRecordId, Is.EqualTo(accidentRecordId));
            Assert.That(result.DateOfAccident, Is.EqualTo(accidentRecord.DateAndTimeOfAccident.Value.ToShortDateString()));
            Assert.That(result.TimeOfAccident, Is.EqualTo(accidentRecord.DateAndTimeOfAccident.Value.ToShortTimeString()));
            Assert.That(result.SiteId, Is.EqualTo(accidentRecord.SiteWhereHappened.Id));
            Assert.That(result.Site, Is.EqualTo(accidentRecord.SiteWhereHappened.Name));
            Assert.That(result.OffSiteName, Is.EqualTo(accidentRecord.OffSiteSpecifics));
            Assert.That(result.Location, Is.EqualTo(accidentRecord.Location));
            Assert.That(result.AccidentTypeId, Is.EqualTo(accidentRecord.AccidentType.Id));
            Assert.That(result.AccidentType, Is.EqualTo(accidentRecord.AccidentType.Description));
            Assert.That(result.OtherAccidentType, Is.EqualTo(accidentRecord.AccidentTypeOther));
            Assert.That(result.AccidentCauseId, Is.EqualTo(accidentRecord.CauseOfAccident.Id));
            Assert.That(result.AccidentCause, Is.EqualTo(accidentRecord.CauseOfAccident.Description));
            Assert.That(result.OtherAccidentCause, Is.EqualTo(accidentRecord.CauseOfAccidentOther));
            Assert.That(result.FirstAiderEmployeeId, Is.EqualTo(accidentRecord.EmployeeFirstAider.Id));
            Assert.That(result.FirstAiderEmployee, Is.EqualTo(accidentRecord.EmployeeFirstAider.FullName));
            Assert.That(result.NonEmployeeFirstAiderName, Is.EqualTo(accidentRecord.NonEmployeeFirstAiderSpecifics));
            Assert.That(result.DetailsOfFirstAid, Is.EqualTo(accidentRecord.DetailsOfFirstAidTreatment));
        }


        [Test]
        public void given_valid_request_when_get_viewmodel_then_calls_correct_methods()
        {
            //given
            var companyId = 1234L;
            var accidentRecorId = 1L;

            
            var target = GetTarget();
            var factory = target
                .WithCompanyId(companyId)
                .WithAccidentRecordId(accidentRecorId);

            //when
            var viewModel = factory.GetViewModel();
            //then

            _accidentRecordService.Verify(
                x =>
                x.GetByIdAndCompanyIdWithSite(accidentRecorId, companyId), Times.Once());


        }

        public AccidentDetailsViewModelFactory GetTarget()
        {
            return new AccidentDetailsViewModelFactory(_employeeService.Object, _siteService.Object,
                                                                _accidentTypeService.Object,
                                                                _causeOfAccidentService.Object, _accidentRecordService.Object);
        }
    }
}
