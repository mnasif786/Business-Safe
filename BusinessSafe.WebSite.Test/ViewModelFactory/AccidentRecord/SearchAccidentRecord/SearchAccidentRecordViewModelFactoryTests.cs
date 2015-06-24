using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using NUnit.Framework;
using Moq;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.AccidentRecord;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.AccidentRecord.SearchAccidentRecord
{
    [TestFixture]
    [Category("Unit")]
    public class SearchAccidentRecordViewModelFactoryTests
    {      
        private Mock<ISiteService>              _siteService;
        private Mock<ISiteGroupService>         _siteGroupService;
        private Mock<IAccidentRecordService>    _accidentRecordService;

        private long _companyId = 12345L;
        
        [SetUp]
        public void SetUp()
        {            
            _siteService = new Mock<ISiteService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _accidentRecordService = new Mock<IAccidentRecordService>();

            _accidentRecordService
                .Setup(x => x.Search(It.IsAny<SearchAccidentRecordsRequest>()) )
                .Returns(new List<AccidentRecordDto>());
           
            _siteGroupService
                .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(new List<SiteGroupDto>());

            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(new List<SiteDto>());
        }


        [Test]
        public void Given_search_by_createdTo_When_GetViewModel_is_called_Then_accident_records_created_on_or_before_are_retrieved()
        {
            //Given
            var to = DateTime.Now;
            var target = GetTarget();

            //When
            target
                .WithCreatedTo(to)
                .GetViewModel();

            //Then
            _accidentRecordService.Verify( x => x.Search( It.Is<SearchAccidentRecordsRequest>( y => y.CreatedTo.Value.Date == to.Date ) ) );
        }

        [Test]
        public void Given_search_by_createdFrom_When_GetViewModel_is_called_Then_accident_records_created_after_are_retrieved()
        {
            //Given
            var from = DateTime.Now;
            var target = GetTarget();

            //When
            target
                .WithCreatedFrom(from)
                .GetViewModel();

            //Then
            _accidentRecordService.Verify(x => x.Search(It.Is<SearchAccidentRecordsRequest>(y => y.CreatedFrom.Value.Date == from.Date)));
        }
      
        [Test]
        public void Given_search_by_site_When_GetViewModel_is_called_Then_responsibilities_for_that_site_are_retrieved()
        {
            //Given
            const long siteId = 1234L;
            var target = GetTarget();

            //When
            target
                .WithSiteId(siteId)
                .GetViewModel();

            //Then
            _accidentRecordService.Verify(x => x.Search(It.Is<SearchAccidentRecordsRequest>(y => y.SiteId == siteId )));
        }

        [Test]
        public void Given_search_by_title_When_GetViewModel_is_called_Then_accident_records_with_matching_title_are_retrieved()
        {
            //Given
            const string title = "Yabba Dabba Ouch!";
            var target = GetTarget();

            //When
            target
                .WithTitle(title)
                .GetViewModel();

            //Then
            _accidentRecordService.Verify(x => x.Search(It.Is<SearchAccidentRecordsRequest>(y => y.Title == title)));
        }

        [Test]
        public void given_search_by_off_site_when_getviewmodel_is_called_then_accident_records_site_has_correct_value()
        {
            //given
            const long siteId = -1;
            var accidentRecords = new List<AccidentRecordDto>
                                      {
                                          new AccidentRecordDto {SiteWhereHappened = new SiteDto {Id = 1234L}},
                                          new AccidentRecordDto {SiteWhereHappened = null, OffSiteSpecifics = "Another Site"}
                                      };

            var target = GetTarget();

            _accidentRecordService
                .Setup(x => x.Search(It.Is<SearchAccidentRecordsRequest>(y=>y.SiteId==-1)))
                .Returns(accidentRecords.Where(x=>x.SiteWhereHappened == null));

            //when
            var result = target.WithSiteId(siteId).GetViewModel();

            //then
            Assert.That(result.AccidentRecords.First().Site, Is.EqualTo("Off-site"));
        }

        [Test]
        public void Given_search_by_injuredPersonForename_When_GetViewModel_is_called_Then_accident_records_for_that_forename_are_retrieved()
        {
            //Given
            const string forename = "Fred";
            var target = GetTarget();

            //When
            target
                .WithInjuredPersonForename(forename)
                .GetViewModel();

            //Then
            _accidentRecordService.Verify(x => x.Search(It.Is<SearchAccidentRecordsRequest>(y => y.InjuredPersonForename == forename)));
        }


        [Test]
        public void Given_search_by_injuredPersonSurname_When_GetViewModel_is_called_Then_accident_records_for_that_forename_are_retrieved()
        {
            //Given
            const string surname = "Flintstone";
            var target = GetTarget();

            //When
            target
                .WithInjuredPersonSurname(surname)
                .GetViewModel();

            //Then
            _accidentRecordService.Verify(x => x.Search(It.Is<SearchAccidentRecordsRequest>(y => y.InjuredPersonSurname == surname)));
        }


        [Test]
        public void Given_search_with_parameters_When_GetViewModel_parameters_set_in_returned_viewModel()
        {
            //Given           
            const long siteId = 324234L;           
            var from = DateTime.Now.AddDays(-124);
            var to = DateTime.Now.AddDays(-3231);
            const string title = "Some Title";
            const string forename = "Fred";
            const string surname = "Flintstone";

            var target = GetTarget();

            //When
            var result = target
                .WithCreatedFrom(from)
                .WithCreatedTo(to)
                .WithInjuredPersonForename(forename)
                .WithInjuredPersonSurname(surname)
                .WithTitle(title)
                .WithSiteId(siteId)                
                .GetViewModel();

            //Then                       
            Assert.That(DateTime.Parse(result.CreatedFrom), Is.EqualTo(from.Date));
            Assert.That(DateTime.Parse(result.CreatedTo), Is.EqualTo(to.Date));            
            Assert.That(result.InjuredPersonForename, Is.EqualTo(forename));
            Assert.That(result.InjuredPersonSurname, Is.EqualTo(surname));
            Assert.That(result.Title, Is.EqualTo(title));
            Assert.That(result.SiteId, Is.EqualTo(siteId));           
        }

        public SearchAccidentRecordViewModelFactory GetTarget()
        {
            return new SearchAccidentRecordViewModelFactory(_accidentRecordService.Object, 
                                                            _siteGroupService.Object,
                                                            _siteService.Object);
        }
    }
}
