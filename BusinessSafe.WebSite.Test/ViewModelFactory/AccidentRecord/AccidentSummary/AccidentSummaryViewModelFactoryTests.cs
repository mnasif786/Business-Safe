using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.AccidentRecord.AccidentSummary
{
    [TestFixture]
    [Category("Unit")]
    public class AccidentSummaryViewModelFactoryTests
    {
        private Mock<IAccidentRecordService> _accidentRecordService;
        
        [SetUp]
        public void Setup()
        {
            _accidentRecordService = new Mock<IAccidentRecordService>();
        }
        
        [Test]
        public void given_valid_request_when_get_view_model__then_returns_correct_viewmodel()
        {
            //given
            long companyId = 1234L;
            long accidentRecordId = 1L;
            var accidentRecord = new AccidentRecordDto
                                     {
                                         Id = accidentRecordId,
                                         CompanyId = companyId,
                                         Title = "Tite",
                                         Reference = "Referene",
                                         Jurisdiction = new JurisdictionDto { Id=1L}
                                     };

            _accidentRecordService
            .Setup(x => x.GetByIdAndCompanyIdWithSite(It.IsAny<long>(), It.IsAny<long>()))
            .Returns(accidentRecord);

            var accidentSummaryViewModelFactory = GetTarget();
            accidentSummaryViewModelFactory = accidentSummaryViewModelFactory.WithCompanyId(companyId);

            //when

            var viewmodel = accidentSummaryViewModelFactory.GetViewModel();
            //then

            Assert.That(viewmodel.CompanyId,Is.EqualTo(companyId));
            Assert.That(viewmodel.AccidentRecordId, Is.EqualTo(accidentRecordId));
            Assert.That(viewmodel.Title, Is.EqualTo(accidentRecord.Title));
            Assert.That(viewmodel.Reference, Is.EqualTo(accidentRecord.Reference));
            Assert.That(viewmodel.JurisdictionId, Is.EqualTo(accidentRecord.Jurisdiction.Id));
            

        }

        private IAccidentSummaryViewModelFactory GetTarget()
        {
            return
                new AccidentSummmaryViewModelFactory(_accidentRecordService.Object) as IAccidentSummaryViewModelFactory;
        }
    }
}
