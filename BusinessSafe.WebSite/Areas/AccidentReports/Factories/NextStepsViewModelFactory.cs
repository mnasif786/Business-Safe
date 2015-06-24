using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.Application.Contracts.AccidentRecord;
using System.Configuration;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public class NextStepsViewModelFactory : INextStepsViewModelFactory
    {
        private readonly IAccidentRecordService _accidentRecordService;
        private long _accidentRecordId;
        private long _companyId;

        public NextStepsViewModelFactory(IAccidentRecordService accidentRecordService)
        {
            _accidentRecordService = accidentRecordService;
        }

        public INextStepsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public INextStepsViewModelFactory WithAccidentRecordId(long accidentRecordId)
        {
            _accidentRecordId = accidentRecordId;
            return this;
        }

        public NextStepsViewModel GetViewModel()
        {
            var accidentRecord = _accidentRecordService.GetByIdAndCompanyIdWithNextStepSections(_accidentRecordId, _companyId);
            
            var viewModel = new NextStepsViewModel();
            viewModel.AccidentRecordId = _accidentRecordId;
            viewModel.CompanyId = _companyId;
            viewModel.SectionsToShow = accidentRecord.AccidentRecordNextStepSections.Select(x => x.NextStepsSection).ToList();
            viewModel.AccidentInvestigationFormClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["AccidentInvestigationForm.ClientDocumentationId"]);
            viewModel.GbAccidentReportGuidanceNoteClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["AccidentRecordGuidanceNote.ClientDocumentationID.GB"]);
            viewModel.NiAccidentReportGuidanceNoteClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["AccidentRecordGuidanceNote.ClientDocumentationID.NI"]);
            viewModel.RoiAccidentReportGuidanceNoteClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["AccidentRecordGuidanceNote.ClientDocumentationID.ROI"]);
            viewModel.GbOnlineAccidentReportFormClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["OnlineAccidentReportForm.ClientDocumentationId.GB"]);
            viewModel.NiOnlineAccidentReportFormClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["OnlineAccidentReportForm.ClientDocumentationId.NI"]);
            viewModel.RoiOnlineAccidentReportFormClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["OnlineAccidentReportForm.ClientDocumentationId.ROI"]);
            viewModel.IomOnlineAccidentReportFormClientDocumentationId = Convert.ToInt64(ConfigurationManager.AppSettings["OnlineAccidentReportForm.ClientDocumentationId.IOM"]);
            viewModel.NextStepsVisible = accidentRecord.NextStepsAvailable;
            return viewModel;
        }
    }
}