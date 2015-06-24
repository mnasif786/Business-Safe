using System;

namespace BusinessSafe.Application.Request
{
    public class SaveNonEmployeeRequest
    {
        public SaveNonEmployeeRequest()
        {
        }

        public SaveNonEmployeeRequest(long nonEmployeeId,long companyIdToLinkNonEmployeeTo, string name, string position, string nonEmployeeCompanyName, bool runMatchCheck, Guid userId)
        {
            Id = nonEmployeeId;
            CompanyId = companyIdToLinkNonEmployeeTo;
            Name = name;
            Position = position;
            NonEmployeeCompanyName = nonEmployeeCompanyName;
            RunMatchCheck = runMatchCheck;
            UserId = userId;
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string NonEmployeeCompanyName { get; set; }
        public bool RunMatchCheck { get; set; }
        public Guid UserId { get; set; }
    }
}