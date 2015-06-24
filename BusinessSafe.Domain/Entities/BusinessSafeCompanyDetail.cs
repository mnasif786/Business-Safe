using System;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities
{
    public class BusinessSafeCompanyDetail : Entity<long>
    {
        public virtual long CompanyId { get; set; }
        public virtual EmployeeForAuditing BusinessSafeContactEmployee { get; set; }

        public static BusinessSafeCompanyDetail Create(long companyId, EmployeeForAuditing employee, UserForAuditing user)
        {
            var company = new BusinessSafeCompanyDetail
                              {
                                  CompanyId = companyId,
                                  BusinessSafeContactEmployee = employee,
                                  CreatedOn = DateTime.Now,
                                  CreatedBy = user,
                                  LastModifiedOn = DateTime.Now,
                                  LastModifiedBy = user,
                                  Deleted = false
                              };
            return company;
        }
    }
}
