using System;

namespace BusinessSafe.Application.Custom_Exceptions
{
    public class InvalidDocumentForCompanyException : ArgumentException
    {
        public InvalidDocumentForCompanyException(long documentLibraryId, long companyId) : base(string.Format("Trying to retrieve invalid document for company: DocumentLibraryId: {0} CompanyId: {1}", documentLibraryId, companyId)) { }
    }
}