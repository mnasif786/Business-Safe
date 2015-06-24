using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.Entities
{
    public class EmployeeEmergencyContactDetail : Entity<int>
    {
        public virtual string Title { get; set; }
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Relationship { get; set; }
        public virtual bool SameAddressAsEmployee { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Town { get; set; }
        public virtual string County { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Telephone1 { get; set; }
        public virtual string Telephone2 { get; set; }
        public virtual string Telephone3 { get; set; }
        public virtual int PreferedTelephone { get; set; }
        public virtual Country Country { get; set; }
        public virtual Employee Employee { get; set; }

        public static EmployeeEmergencyContactDetail Create(EmergencyContactDetailParameters emergencyContactDetail, UserForAuditing user)
        {
            var emloyeeEmergencyContactDetail = new EmployeeEmergencyContactDetail()
                       {
                           Title = emergencyContactDetail.Title,
                           Forename = emergencyContactDetail.Forename,
                           Surname = emergencyContactDetail.Surname,
                           Relationship = emergencyContactDetail.Relationship,
                           SameAddressAsEmployee = emergencyContactDetail.SameAddressAsEmployee,
                           Telephone1 = emergencyContactDetail.Telephone1,
                           Telephone2 = emergencyContactDetail.Telephone2,
                           Telephone3 = emergencyContactDetail.Telephone3,
                           PreferedTelephone = emergencyContactDetail.PreferedTelephone,
                           CreatedOn = DateTime.Now,
                           CreatedBy = user
                       };

            if (!emergencyContactDetail.SameAddressAsEmployee)
            {
                emloyeeEmergencyContactDetail.Address1 = emergencyContactDetail.Address1;
                emloyeeEmergencyContactDetail.Address2 = emergencyContactDetail.Address2;
                emloyeeEmergencyContactDetail.Address3 = emergencyContactDetail.Address3;
                emloyeeEmergencyContactDetail.County = emergencyContactDetail.County;
                emloyeeEmergencyContactDetail.Town = emergencyContactDetail.Town;
                emloyeeEmergencyContactDetail.PostCode = emergencyContactDetail.PostCode;
                emloyeeEmergencyContactDetail.Country = emergencyContactDetail.Country;
            }

            return emloyeeEmergencyContactDetail;
        }

        public virtual void Update(EmergencyContactDetailParameters emergencyContactDetailParameters, UserForAuditing user)
        {
            Title = emergencyContactDetailParameters.Title;
            Forename = emergencyContactDetailParameters.Forename;
            Surname = emergencyContactDetailParameters.Surname;
            Relationship = emergencyContactDetailParameters.Relationship;
            SameAddressAsEmployee = emergencyContactDetailParameters.SameAddressAsEmployee;
            Telephone1 = emergencyContactDetailParameters.Telephone1;
            Telephone2 = emergencyContactDetailParameters.Telephone2;
            Telephone3 = emergencyContactDetailParameters.Telephone3;
            PreferedTelephone = emergencyContactDetailParameters.PreferedTelephone;
            SetLastModifiedDetails(user);

            if (!emergencyContactDetailParameters.SameAddressAsEmployee)
            {
                Address1 = emergencyContactDetailParameters.Address1;
                Address2 = emergencyContactDetailParameters.Address2;
                Address3 = emergencyContactDetailParameters.Address3;
                County = emergencyContactDetailParameters.County;
                Town = emergencyContactDetailParameters.Town;
                PostCode = emergencyContactDetailParameters.PostCode;
                Country = emergencyContactDetailParameters.Country;
            }
            else
            {
                Address1 = null;
                Address2 = null;
                Address3 = null;
                County = null;
                Town = null;
                PostCode = null;
                Country = null;
            }
        }
    }
}