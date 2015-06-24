using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Helpers
{
    public class ReviewAuditDocumentHelper : IReviewAuditDocumentHelper
    {
      

        private readonly Dictionary<RiskAssessmentType, DocumentTypeEnum> documentTypes = new Dictionary<RiskAssessmentType, DocumentTypeEnum>()
                                                                                              {
                                                                                                  { RiskAssessmentType.GRA, DocumentTypeEnum.GRAReview },
                                                                                                  { RiskAssessmentType.HSRA, DocumentTypeEnum.HSRAReview },
                                                                                                  { RiskAssessmentType.PRA, DocumentTypeEnum.PRAReview },
                                                                                                  { RiskAssessmentType.FRA, DocumentTypeEnum.FRAReview }
                                                                                              };

        private readonly ISqlReportExecutionServiceFacade _sqlReportExecutionServiceFacade;
        private readonly IDocumentLibraryUploader _documentLibraryUploader;

        public ReviewAuditDocumentHelper(ISqlReportExecutionServiceFacade sqlReportExecutionServiceFacade, IDocumentLibraryUploader documentLibraryUploader)
        {
            _sqlReportExecutionServiceFacade = sqlReportExecutionServiceFacade;
            _documentLibraryUploader = documentLibraryUploader;
        }


        public virtual ReviewAuditDocumentResult CreateReviewAuditDocument(RiskAssessmentType riskAssessmentType, RiskAssessmentDto riskAssessment)
        {
            
            DocumentTypeEnum documentType = documentTypes[riskAssessmentType];
            SqlReportHelper.ReportType reportType = SqlReportHelper.GetSqlReportType(riskAssessmentType);
            
            var documentViewModel = GetSqlReport(riskAssessment.Id, reportType);
            var newFileName = GetNewFileName(riskAssessment, documentViewModel.FileName);
            var documentLibraryId = _documentLibraryUploader.Upload(newFileName, documentViewModel.FileStream);

            return new ReviewAuditDocumentResult()
                       {
                           DocumentLibraryId = documentLibraryId,
                           NewFileName = newFileName,
                           DocumentType = documentType
                       };
        }

        private DocumentViewModel GetSqlReport(long riskAssessmentId, SqlReportHelper.ReportType type)
        {
            return _sqlReportExecutionServiceFacade.GetReport(
                type,
                new object[] { riskAssessmentId },
                SqlReportHelper.ReportFormatType.PDF
                );
        }

        private static string GetNewFileName(RiskAssessmentDto riskAssessment, string existingReportFileName)
        {
            var extension = existingReportFileName.Substring(existingReportFileName.LastIndexOf('.'));
            return string.Format("{0}_{1}_{2}{3}", riskAssessment.Title, riskAssessment.Reference, DateTime.Now.ToString("dd_MM_yyyy"), extension);
        }
    }
}