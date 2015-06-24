namespace BusinessSafe.Application.Request
{
    public class UpdateNonEmployeeRequest
    {
        public UpdateNonEmployeeRequest()
        {
        }

        public UpdateNonEmployeeRequest(long nonEmployeeId, long companyIdToLinkNonEmployeeTo, string name, string position, string nonEmployeeCompanyName, bool runMatchCheck)
        {
            NonEmployeeId = nonEmployeeId;
            CompanyIdLink = companyIdToLinkNonEmployeeTo;
            Name = name;
            Position = position;
            NonEmployeeCompanyName = nonEmployeeCompanyName;
            RunMatchCheck = runMatchCheck;
        }

        public long CompanyIdLink { get; set; }
        public long NonEmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string NonEmployeeCompanyName { get; set; }
        public bool RunMatchCheck { get; set; }
    }
}