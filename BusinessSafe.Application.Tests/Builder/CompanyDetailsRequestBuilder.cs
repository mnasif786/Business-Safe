using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Tests.Builder
{
    public class CreateDocumentRequestBuilder
    {
        private long _companyId;
        private long _documentLibraryId;
        private DocumentOriginType _documentOriginType;
        private DocumentTypeEnum _documentType;
        private string _filename;
        private long _siteId;

        public CreateDocumentRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public CreateDocumentRequestBuilder WithDocumentLibraryId(long documentLibraryId)
        {
            _documentLibraryId = documentLibraryId;
            return this;
        }

        public CreateDocumentRequestBuilder WithDocumentOriginTypeId(DocumentOriginType documentOriginType)
        {
            _documentOriginType = documentOriginType;
            return this;
        }

        public CreateDocumentRequestBuilder WithDocumentType(DocumentTypeEnum documentType)
        {
            _documentType = documentType;
            return this;
        }

        public CreateDocumentRequestBuilder WithFilename(string filename)
        {
            _filename = filename;
            return this;
        }

        public CreateDocumentRequestBuilder WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public CreateDocumentRequest Build()
        {
            return new CreateDocumentRequest
            {
                ClientId = _companyId,
                DocumentLibraryId = _documentLibraryId,
                DocumentOriginType = _documentOriginType,
                DocumentType = _documentType,
                Filename = _filename,
                SiteId = _siteId
            };
        }
    }
    public class CompanyDetailsRequestBuilder
    {
        private static string _companyName;
        private static string _can;
        private static string _addressLine1;
        private static string _addressLine2;
        private static string _addressLine3;
        private static string _addressLine4;
        private static string _postcode;
        private static string _telephone;
        private static string _website;
        private static string _mainContact;
        private static string _actioningUserName;
        private static string _businessSafeContactName;

        public static CompanyDetailsRequestBuilder Build()
        {
            _companyName = "Company Name Test";
            _can = "CAN Test";
            _addressLine1 = "Address Line 1";
            _addressLine2 = "Address line 2";
            _addressLine3 = "Address line 3";
            _addressLine4 = "Address line 4";
            _postcode = "PostCode test";
            _telephone = "telephone";
            _website = "website";
            _mainContact = "main Contact";
            _actioningUserName = "actioning user";
            _businessSafeContactName = "business safe contact";

            return new CompanyDetailsRequestBuilder();
        }

        public CompanyDetailsRequest Create()
        {
            return new CompanyDetailsRequest(_companyName, _can, _addressLine1, _addressLine2, _addressLine3,
                                             _addressLine4, _postcode, _telephone, _website, _mainContact, _actioningUserName,_businessSafeContactName);
        }
    }
}