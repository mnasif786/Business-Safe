namespace BusinessSafe.Application.Request
{
    public class GuardDefaultExistsRequest
    {
        public GuardDefaultExistsRequest(string name, long excludId, long companyId)
        {
            Name = name;
            ExcludeId = excludId;
            CompanyId = companyId;
        }

        public GuardDefaultExistsRequest(string name, long companyId)
        {
            Name = name;
            ExcludeId = 0;
            CompanyId = companyId;
        }

        public long CompanyId { get; private set; }
        public string Name { get; private set; }
        public long ExcludeId { get; private set; }
    }
}