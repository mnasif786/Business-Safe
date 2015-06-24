using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Tests.Builder
{
    public class EmployeeSearchRequestBuilder
    {
        private string _employeeReference = string.Empty;
        private string _forename = string.Empty;
        private string _surname = string.Empty;
        private long _siteId;
        private long _companyId = 0;

        public static EmployeeSearchRequestBuilder Create()
        {
            return new EmployeeSearchRequestBuilder();
        }

        public SearchEmployeesRequest Build()
        {
            return new SearchEmployeesRequest()
                       {
                           EmployeeReferenceLike = _employeeReference + "%",
                           ForenameLike = _forename + "%",
                           SurnameLike = _surname + "%",
                           SiteIds = new long[] { _siteId },
                           CompanyId = _companyId
                       };
        }

        public EmployeeSearchRequestBuilder WithEmployeeReference(string employeeReference)
        {
            _employeeReference = employeeReference;
            return this;
        }

        public EmployeeSearchRequestBuilder WithForname(string forename)
        {
            _forename = forename;
            return this;
        }

        public EmployeeSearchRequestBuilder WithSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public EmployeeSearchRequestBuilder WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public EmployeeSearchRequestBuilder WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }
    }
}