using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Factories
{
    public interface IDocumentsViewModelFactory
    {
        IDocumentsViewModelFactory WithCompanyId(long companyId);
        IDocumentsViewModelFactory WithRiskAssessmentId(long riskAssessmentId);
        IDocumentsViewModelFactory WithDocumentDisplayMessage(IEnumerable<string> displayMessages);
        IDocumentsViewModelFactory WithDocumentDefaultType(DocumentTypeEnum documentType);
        DocumentsViewModel GetViewModel();
        
    }
}