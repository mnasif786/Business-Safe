using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.Validators;

namespace BusinessSafe.Domain.Entities
{
    public class EmployeeContactDetail : Entity<long>
    {
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Town { get; set; }
        public virtual string County { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Telephone1 { get; set; }
        public virtual string Telephone2 { get; set; }
        public virtual int PreferedTelephone { get; set; }
        public virtual string Email { get; set; }
        public virtual Country Country { get; set; }
        public virtual Employee Employee { get; set; }

        public static EmployeeContactDetail Create(AddUpdateEmployeeContactDetailsParameters employeeContactDetailParameters, UserForAuditing user, Employee employee)
        {
            var employeeContactDetail = new EmployeeContactDetail()
                       {
                           Address1 = employeeContactDetailParameters.Address1,
                           Address2 = employeeContactDetailParameters.Address2,
                           Address3 = employeeContactDetailParameters.Address3,
                           County = employeeContactDetailParameters.County,
                           Country = employeeContactDetailParameters.Country,
                           PostCode = employeeContactDetailParameters.PostCode,
                           Telephone1 = employeeContactDetailParameters.Telephone1,
                           Telephone2 = employeeContactDetailParameters.Telephone2,
                           Town = employeeContactDetailParameters.Town,
                           PreferedTelephone = employeeContactDetailParameters.PreferedTelephone,
                           Email = employeeContactDetailParameters.Email,
                           CreatedOn = DateTime.Now,
                           LastModifiedOn = DateTime.Now,
                           Employee = employee,
                           CreatedBy = user,
                           LastModifiedBy = user
                       };

            var validationResult = new EmployeeContactDetailValidator().Validate(employeeContactDetail);
            if (!validationResult.IsValid)
            {
                throw new InvalidAddUpdateEmployeeContactDetailsParameters(validationResult.Errors);
            }

            return employeeContactDetail;
        }

        // TODO : Need to test this
        public virtual void Update(AddUpdateEmployeeContactDetailsParameters contactDetailsParameters, UserForAuditing userForAuditing, Employee employee)
        {
            Address1 = contactDetailsParameters.Address1;
            Address2 = contactDetailsParameters.Address2;
            Address3 = contactDetailsParameters.Address3;
            County = contactDetailsParameters.County;
            Country = contactDetailsParameters.Country;
            PostCode = contactDetailsParameters.PostCode;
            Telephone1 = contactDetailsParameters.Telephone1;
            Telephone2 = contactDetailsParameters.Telephone2;
            Town = contactDetailsParameters.Town;
            PreferedTelephone = contactDetailsParameters.PreferedTelephone;
            if(employee.CanChangeEmail)
                Email = contactDetailsParameters.Email;

            SetLastModifiedDetails(userForAuditing);

            var validationResult = new EmployeeContactDetailValidator().Validate(this);
            if (!validationResult.IsValid)
            {
                throw new InvalidAddUpdateEmployeeContactDetailsParameters(validationResult.Errors);
            }
        }
    }
}