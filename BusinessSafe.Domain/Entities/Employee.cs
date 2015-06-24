using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Constants;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.Validators;
using FluentValidation.Results;

namespace BusinessSafe.Domain.Entities
{
    public class Employee : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Title { get; set; }
        public virtual string PreviousSurname { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual string Sex { get; set; }
        public virtual bool? HasDisability { get; set; }
        public virtual string DisabilityDescription { get; set; }
        public virtual string NINumber { get; set; }
        public virtual string DrivingLicenseNumber { get; set; }
        public virtual DateTime? DrivingLicenseExpirationDate { get; set; }
        public virtual string WorkVisaNumber { get; set; }
        public virtual DateTime? WorkVisaExpirationDate { get; set; }
        public virtual string PPSNumber { get; set; }
        public virtual string PassportNumber { get; set; }
        public virtual bool? HasCompanyVehicle { get; set; }
        public virtual string CompanyVehicleRegistration { get; set; }
        public virtual CompanyVehicleType CompanyVehicleType { get; set; }
        public virtual Site Site { get; set; }
        public virtual long? OrganisationalUnitId { get; set; }
        public virtual string JobTitle { get; set; }
        public virtual EmploymentStatus EmploymentStatus { get; set; }
        public virtual string EmployeeReference { get; set; }
        public virtual long CompanyId { get; set; }
        public virtual IList<EmployeeContactDetail> ContactDetails { get; set; }
        public virtual IList<EmployeeEmergencyContactDetail> EmergencyContactDetails { get; set; }
        public virtual User User { get; set; }

        public virtual RiskAssessor RiskAssessor
        {
            get
            {
                return RiskAssessors.FirstOrDefault(x => !x.Deleted);
            }
        }

        public virtual NotificationType NotificationType { get; set; }
        public virtual int? NotificationFrequecy { get; set; }

        /// <summary>
        /// This property exists because a _bug in the risk assessor screen creates duplicate risk assessor entities.
        /// </summary>
        public virtual IList<RiskAssessor> RiskAssessors { get; protected set; }
        
        public virtual string FullName
        {
            get
            {
                string fullName = Forename;

                if (!String.IsNullOrEmpty(Forename) && !String.IsNullOrEmpty(Surname))
                    fullName += " ";

                fullName += Surname;
                return fullName;
            }
        }

        public virtual string PrefixForEmployeeChecklists
        {
            get
            {
                if (string.IsNullOrEmpty(Surname))
                {
                    return string.Empty;
                }

                return Surname
                    .Replace(" ", "")
                    .Replace("-", "")
                    .Replace("'", "")
                    .ToUpper();
            }
        }

        //Current logic is that employee has only one set of contact details, so use that.
        public virtual EmployeeContactDetail MainContactDetails
        {
            get
            {
                if(ContactDetails == null || !ContactDetails.Any())
                {
                    return null;
                }

                return ContactDetails.First();
            }
        }

        public Employee()
        {
            ContactDetails = new List<EmployeeContactDetail>();
            EmergencyContactDetails = new List<EmployeeEmergencyContactDetail>();
            RiskAssessors = new List<RiskAssessor>();
        }

        public static Employee Create(AddUpdateEmployeeParameters parameters, UserForAuditing actioningUser)
        {
            var employee = new Employee
                               {
                                   Id = Guid.NewGuid(),
                                   CreatedOn = DateTime.Now,
                                   CreatedBy = actioningUser,
                                   LastModifiedOn = DateTime.Now,
                                   LastModifiedBy = actioningUser
                               };
            SetEmployeeProperties(parameters, employee);

            return employee;
        }

        public virtual void Update(AddUpdateEmployeeParameters userAmendingEmployee, AddUpdateEmployeeContactDetailsParameters employeeContactDetails, UserForAuditing user)
        {
            SetEmployeeProperties(userAmendingEmployee, this);
            SetLastModifiedDetails(user);
            UpdateContactDetails(employeeContactDetails, user);
        }

        public virtual void AddContactDetails(EmployeeContactDetail contactDetail)
        {
            if (ContactDetails.Count(x => x.Id == contactDetail.Id) > 0)
            {
                throw new ContactDetailsAlreadyAttachedToEmployeeException(this, contactDetail);
            }

            contactDetail.Employee = this;
            ContactDetails.Add(contactDetail);
        }

        public virtual void UpdateContactDetails(AddUpdateEmployeeContactDetailsParameters contactDetailsParameters, UserForAuditing user)
        {
            var contactDetail = ContactDetails.FirstOrDefault(x => x.Id == contactDetailsParameters.Id);
            if (contactDetail != null)
            {
                contactDetail.Update(contactDetailsParameters, user, this);
                return;
            }

            var newContactDetail = EmployeeContactDetail.Create(contactDetailsParameters, user, this);
            AddContactDetails(newContactDetail);
        }

        public virtual void AddEmergencyContacts(IEnumerable<EmployeeEmergencyContactDetail> emergencyContactDetail)
        {
            foreach (var contactDetail in emergencyContactDetail)
            {
                contactDetail.Employee = this;
                EmergencyContactDetails.Add(contactDetail);
            }
        }

        public virtual void AddEmergencyContact(EmployeeEmergencyContactDetail emergencyContactDetail, UserForAuditing user)
        {
            emergencyContactDetail.Employee = this;
            EmergencyContactDetails.Add(emergencyContactDetail);
            SetLastModifiedDetails(user);
        }

        public virtual void UpdateEmergencyContact(EmergencyContactDetailParameters emergencyContactDetailParameters, UserForAuditing user)
        {
            var employeeEmergencyContactDetail = EmergencyContactDetails.FirstOrDefault(x => x.Id == emergencyContactDetailParameters.EmergencyContactId);
            if (employeeEmergencyContactDetail == null)
            {
                throw new AttemptingToUpdateEmergencyContactEmergencyContactsDetailsNotFoundForEmployeeException(Id);
            }
            employeeEmergencyContactDetail.Update(emergencyContactDetailParameters, user);
        }


        public virtual void ReinstateEmployeeAsNotDeleted(UserForAuditing user)
        {
            SetLastModifiedDetails(user);
            Deleted = false;
        }

        public virtual void CreateUser(RegisterEmployeeAsUserParameters parameters)
        {
            if (User != null)
            {
                throw new AttemptingToCreateEmployeeAsUserWhenUserExistsException(this);
            }

            if (CompanyId != parameters.CompanyId)
                throw new AttemptingToCreateEmployeeAsUserForDifferentCompanyException(this, parameters.CompanyId);

            SiteStructureElement userSite = parameters.Site ?? parameters.MainSite;

            if (userSite == null)
            {
                throw new AttemptingToCreateEmployeeAsUserNoUserSiteSetException(this, parameters.CompanyId);
            }

            User = User.CreateUser(parameters.NewUserId, CompanyId, parameters.Role, userSite, this, parameters.ActioningUser);
            SetLastModifiedDetails(parameters.ActioningUser);
        }

        public virtual ValidationResult ValidateRegisterAsUser(RegisterEmployeeAsUserParameters parameters)
        {
            var validator = new RegisterEmployeeAsUserValidator();
            return validator.Validate(this);
        }

        public virtual void MarkEmergencyContactForDelete(long emergencyContactId, UserForAuditing user)
        {
            var emergencyContactForDelete = EmergencyContactDetails.FirstOrDefault(x => x.Id == emergencyContactId);
            if (emergencyContactForDelete == null)
            {
                throw new AttemptingToMarkForDeleteEmergencyContactEmergencyContactNotFoundOnEmployeeException(Id, emergencyContactId);
            }

            emergencyContactForDelete.MarkForDelete(user);
            SetLastModifiedDetails(user);
        }

        private static void SetEmployeeProperties(AddUpdateEmployeeParameters parameters, Employee employee)
        {
            employee.Forename = parameters.Forename;
            employee.Surname = parameters.Surname;
            employee.Title = parameters.Title;
            employee.PreviousSurname = parameters.PreviousSurname;
            employee.MiddleName = parameters.MiddleName;
            employee.DateOfBirth = parameters.DateOfBirth;
            employee.Nationality = parameters.Nationality;
            employee.Sex = parameters.Sex;
            employee.HasDisability = parameters.HasDisability;
            employee.DisabilityDescription = parameters.DisabilityDescription;
            employee.NINumber = parameters.NINumber;
            employee.DrivingLicenseNumber = parameters.DrivingLicenseNumber;
            employee.DrivingLicenseExpirationDate = parameters.DrivingLicenseExpirationDate;
            employee.WorkVisaNumber = parameters.WorkVisaNumber;
            employee.WorkVisaExpirationDate = parameters.WorkVisaExpirationDate;
            employee.PPSNumber = parameters.PPSNumber;
            employee.PassportNumber = parameters.PassportNumber;
            employee.HasCompanyVehicle = parameters.HasCompanyVehicle;
            employee.CompanyVehicleRegistration = parameters.CompanyVehicleRegistration;
            employee.CompanyVehicleType = parameters.CompanyVehicleType;
            employee.Site = parameters.Site;
            employee.OrganisationalUnitId = parameters.OrganisationalUnitId;
            employee.JobTitle = parameters.JobTitle;
            employee.EmploymentStatus = parameters.EmploymentStatus;
            employee.EmployeeReference = parameters.EmployeeReference;
            employee.CompanyId = parameters.ClientId;
            employee.NotificationType = MapNotificationType(parameters.NotificationType);
            employee.NotificationFrequecy = parameters.NotificationFrequency;
        }

        private static NotificationType MapNotificationType(NotificationTypeParameters notification)
        {
            switch (notification)
            {
                case NotificationTypeParameters.Daily: return NotificationType.Daily;
                case NotificationTypeParameters.Weekly: return NotificationType.Weekly;
                case NotificationTypeParameters.Monthly: return NotificationType.Monthly;
                default: return NotificationType.Daily;
            }
        }

        public virtual bool HasEmail
        {
            get
            {
                if (!ContactDetails.Any())
                {
                    return false;
                }

                return !string.IsNullOrEmpty(ContactDetails[0].Email);
            }
        }

        public virtual bool CanChangeEmail
        {
            get
            {
                if (User != null && User.IsRegistered == true)
                {
                    return false;
                }

                return true;
            }
        }

        private SiteStructureElement GetRiskAssessmentSiteOrDefault()
        {
            SiteStructureElement riskAssessorSite = null;
            if (this.RiskAssessor != null)
            {
                riskAssessorSite = this.RiskAssessor.Site;
            }
            else
            {
                if (User != null && !User.Deleted)
                {
                    riskAssessorSite = User.Site;
                }
                else
                {
                    riskAssessorSite = Site;
                }
            }
            return riskAssessorSite;
        }

        public virtual bool IsRiskAssessor
        {
            get { return RiskAssessor != null && !RiskAssessor.Deleted; }
        }

        public virtual void UpdateRiskAssessorDetails(bool enableRiskAssessor,
            bool doNotSendTaskOverdueNotifications,
            bool doNotSendTaskCompletedNotifications,
            bool doNotSendReviewDueNotifications,
            UserForAuditing user)
        {
            var riskAssessorSite = GetRiskAssessmentSiteOrDefault();

            UpdateRiskAssessorDetails(enableRiskAssessor, false, riskAssessorSite, doNotSendTaskOverdueNotifications, doNotSendTaskCompletedNotifications, doNotSendReviewDueNotifications, user);
        }

        public virtual void UpdateRiskAssessorDetails(bool enableRiskAssessor,bool hasAccessToAllSites, SiteStructureElement riskAssessorSite, 
            bool doNotSendTaskOverdueNotifications,
            bool doNotSendTaskCompletedNotifications,
            bool doNotSendReviewDueNotifications,
            UserForAuditing user)
        {
            if (hasAccessToAllSites)
            {
                riskAssessorSite = null;
            }

            if (enableRiskAssessor)
            {
                if (!RiskAssessors.Any())
                {
                    var riskAssessor = RiskAssessor.Create(this, riskAssessorSite, doNotSendTaskOverdueNotifications, doNotSendTaskCompletedNotifications, doNotSendReviewDueNotifications, hasAccessToAllSites, user);
                    RiskAssessors.Add(riskAssessor);
                }
                else if (RiskAssessors.All(x=> x.Deleted))  //if all deleted then restore one
                {
                    var deletedRiskAssessor = RiskAssessors.First(x => x.Deleted);
                    deletedRiskAssessor.ReinstateFromDelete(user);
                }

                RiskAssessor.Update(riskAssessorSite, hasAccessToAllSites, doNotSendReviewDueNotifications, doNotSendTaskOverdueNotifications, doNotSendTaskCompletedNotifications, user);
            }
            else if (RiskAssessor != null)
            {
                RiskAssessor.MarkForDelete(user);
            }

            SetLastModifiedDetails(user);
        }

        public virtual void SetEmail(string email, UserForAuditing user)
        {
            var now = DateTime.Now;

            if (!ContactDetails.Any())
            {
                ContactDetails.Add(new EmployeeContactDetail());
                ContactDetails[0].Employee = this;
                ContactDetails[0].CreatedOn = now;
                ContactDetails[0].CreatedBy = user;
                LastModifiedOn = now;
                LastModifiedBy = user;
            }

            ContactDetails[0].Email = email;
            ContactDetails[0].LastModifiedBy = user;
            ContactDetails[0].LastModifiedOn = DateTime.Now;
        }

        public virtual string GetEmail()
        {
            if (HasEmail == false)
            {
                return string.Empty;
            }

            return ContactDetails[0].Email;
        }

        public override void MarkForDelete(UserForAuditing user)
        {
            if(User != null)
            {
                User.Delete(user);
            }
            base.MarkForDelete(user);
        }

        public virtual bool DoesWantToBeNotifiedOn(DateTime notificationDateTime)
        {
            if (NotificationType == NotificationType.Daily)
                return true;

            if (NotificationType == NotificationType.Weekly)
            {
                if (NotificationFrequecy.HasValue)
                {
                    return (int)notificationDateTime.DayOfWeek == NotificationFrequecy.Value;
                }
            }

            if (NotificationType == NotificationType.Monthly)
            {
                if (NotificationFrequecy.HasValue)
                {
                    return (int)notificationDateTime.Day == NotificationFrequecy.Value;
                }
            }

            return false;
        }
    }
}