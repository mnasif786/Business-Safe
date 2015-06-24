using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public class AccidentRecordOverviewViewModelFactory : IAccidentRecordOverviewViewModelFactory
    {
        private long _accidentRecordId;
        private long _companyId;

        private readonly IAccidentRecordService _accidentRecordService;
        private readonly ISiteService _siteService;

        public AccidentRecordOverviewViewModelFactory(IAccidentRecordService accidentRecordService)
        {
            _accidentRecordService = accidentRecordService;
            
        }

        public AccidentRecordOverviewViewModelFactory(IAccidentRecordService accidentRecordService, ISiteService siteService)
        {
            _accidentRecordService = accidentRecordService;
            _siteService = siteService;
        }

        public IAccidentRecordOverviewViewModelFactory WithAccidentRecordId(long accidentRecordId)
        {
            _accidentRecordId = accidentRecordId;
            return this;
        }

        public IAccidentRecordOverviewViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public OverviewViewModel GetViewModel()
        {
            var accidentRecord = _accidentRecordService.GetByIdAndCompanyIdWithAccidentRecordDocuments(_accidentRecordId, _companyId);
            var viewModel = new OverviewViewModel();

            viewModel.AccidentRecordId = _accidentRecordId;
            viewModel.CompanyId = _companyId;
            viewModel.DescriptionHowAccidentHappened = accidentRecord.DescriptionHowAccidentHappened;
            viewModel.IsReportable = accidentRecord.IsReportable;
            viewModel.DoNotSendEmailNotification = accidentRecord.DoNotSendEmailNotification;
            viewModel.EmailNotificationSent = accidentRecord.EmailNotificationSent;
            viewModel.Documents = new DocumentsViewModel()
                                                    {
                                                        CompanyId = _companyId,
                                                        RiskAssessmentId = 0,
                                                        ExistingDocumentsViewModel = new ExistingDocumentsViewModel()
                                                                                         {
                                                                                             CanDeleteDocuments = true,
                                                                                             CanEditDocumentType = false,
                                                                                             DocumentOriginTypeId = 1,
                                                                                             DocumentTypeId = (int) DocumentTypeEnum.AccidentRecord
                                                                                         }
                                                    };

            viewModel.Documents.ExistingDocumentsViewModel.PreviouslyAddedDocuments = accidentRecord.AccidentRecordDocuments
                .Select(x => new PreviouslyAddedDocumentGridRowViewModel()
                                 {
                                     Description = x.Description,
                                     DocumentLibraryId = x.DocumentLibraryId,
                                     DocumentOriginType = DocumentOriginType.TaskCompleted,
                                     DocumentTypeName = x.DocumentType.Name,
                                     Filename = x.Filename,
                                     Id = x.Id
                                 }).ToList();
            viewModel.NextStepsVisible = accidentRecord.NextStepsAvailable;

            if (accidentRecord.SiteWhereHappened != null)
            {
                var accidentRecordNotificationMemberEmails =
                    _siteService.GetAccidentRecordNotificationMembers(accidentRecord.SiteWhereHappened.Id).Where(a => a.HasEmail());

                viewModel.AccidentRecordNotificationMemberEmails = accidentRecordNotificationMemberEmails
                                                    .Select( a => new AccidentRecordNotificationMemberEmail(){Email =  a.Email(), Name = a.FullName()} ).ToList();

            }

            return viewModel;
        }

    }
}