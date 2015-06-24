namespace BusinessSafe.Application.Request
{
    public class PeopleNotInvolvedInRiskAssessmentSearchBaseRequest
    {
        public string SearchTerm { get; set; }
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public int PageLimit { get; set; }
    }

    public class NonEmployeesNotAttachedToRiskAssessmentSearchRequest :PeopleNotInvolvedInRiskAssessmentSearchBaseRequest{}
    public class EmployeesNotAttachedToRiskAssessmentSearchRequest : PeopleNotInvolvedInRiskAssessmentSearchBaseRequest { }
}