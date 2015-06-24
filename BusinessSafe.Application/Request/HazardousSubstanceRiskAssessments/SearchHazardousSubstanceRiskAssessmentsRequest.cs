
namespace BusinessSafe.Application.Request.HazardousSubstanceRiskAssessments
{
    public class SearchHazardousSubstanceRiskAssessmentsRequest : SearchRiskAssessmentsRequest
    {
        public long HazardousSubstanceId { get; set; }
    }
}