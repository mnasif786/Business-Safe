using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public class AccidentSummmaryViewModelFactory : IAccidentSummaryViewModelFactory
    {
        
        private long _companyId;
        private long _accidentRecordId;
        private IAccidentRecordService _accidentRecordService;

        public AccidentSummmaryViewModelFactory(IAccidentRecordService accidentRecordService)
        {
            _accidentRecordService = accidentRecordService;
        }

        public IAccidentSummaryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAccidentSummaryViewModelFactory WithAccidentRecordId(long accidentRecordId)
        {
            _accidentRecordId = accidentRecordId;
            return this;
        }

        public AccidentSummaryViewModel GetViewModel()
        {
            var viewModel = new AccidentSummaryViewModel();

            var accidentRecord = _accidentRecordService.GetByIdAndCompanyIdWithSite(_accidentRecordId,_companyId);

            if (accidentRecord != null)
            {
                viewModel.CompanyId = accidentRecord.CompanyId;
                viewModel.AccidentRecordId = accidentRecord.Id;
                viewModel.Title = accidentRecord.Title;
                viewModel.Reference = accidentRecord.Reference;
                viewModel.JurisdictionId = accidentRecord.Jurisdiction !=null ? accidentRecord.Jurisdiction.Id : default(long);
                viewModel.NextStepsVisible = accidentRecord.NextStepsAvailable;
            }

            return viewModel;
        }
    }
}