namespace BusinessSafe.Domain.Entities
{
    public interface ICompanyDefault
    {
        long Id { get; }
        string Name { get; }
        long? CompanyId { get; }

        //todo: This has no meaning - get rid of this.
        long? RiskAssessmentId { get; }
    }
}