using System;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Tests.Controllers.AccidentRecording.AccidentDetails;
using System.Collections.Generic;

namespace BusinessSafe.Application.Contracts.AccidentRecord
{
    public interface IAccidentRecordService
    {
        AccidentRecordDto GetByIdAndCompanyId(long accidentRecordId, long companyId);
        AccidentRecordDto GetByIdAndCompanyIdWithAccidentRecordDocuments(long accidentRecordId, long companyId);
        AccidentRecordDto GetByIdAndCompanyIdWithSite(long accidentRecordId, long companyId);
        AccidentRecordDto GetByIdAndCompanyIdWithEmployeeInjured(long accidentRecordId, long companyId);
        AccidentRecordDto GetByIdAndCompanyIdWithNextStepSections(long accidentRecordId, long companyId);
        AccidentRecordDto GetByIdAndCompanyIdWithEverything(long accidentRecordId, long companyId);
        long CreateAccidentRecord(SaveAccidentRecordSummaryRequest request);
        void SaveAccidentRecordSummary(SaveAccidentRecordSummaryRequest createAccidentRecordRequest);
        void UpdateInjuredPerson(UpdateInjuredPersonRequest request);
        void UpdateAccidentRecordAccidentDetails(UpdateAccidentRecordAccidentDetailsRequest request);

        int Count(SearchAccidentRecordsRequest request);
        IEnumerable<AccidentRecordDto> Search(SearchAccidentRecordsRequest searchAccidentRecordsRequest);
        IEnumerable<AccidentRecordDto> SearchWithEverthing(SearchAccidentRecordsRequest searchAccidentRecordsRequest);
        void AddAccidentRecordDocument(AddDocumentToAccidentReportRequest request);
        void RemoveAccidentRecordDocuments(RemoveDocumentsFromAccidentRecordRequest request);
        void SetAccidentRecordOverviewDetails(AccidentRecordOverviewRequest request);
        void UpdateInjuryDetails(UpdateInjuryDetailsRequest request);
  		void Delete(long accidentRecordId, long companyId, Guid userId);
        void UpdateAccidentRecordStatus(long accidentRecordId, long companyId, Guid userId, AccidentRecordStatusEnum status);
        void SendAccidentRecordEmails(long accidentRecordId, long companyId, Guid userId);
    }
}