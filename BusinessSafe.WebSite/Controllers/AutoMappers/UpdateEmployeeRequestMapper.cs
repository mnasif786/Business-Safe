using System;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class UpdateEmployeeRequestMapper : EmployeeRequestMapperBase
    {
      
        public UpdateEmployeeRequest Map(EmployeeViewModel viewModel, Guid userId)
        {
            var result = new UpdateEmployeeRequest()
            {
                EmployeeId = viewModel.EmployeeId.GetValueOrDefault(),
                EmployeeReference = viewModel.EmployeeReference,
                Title = (string.IsNullOrEmpty(viewModel.NameTitle) || viewModel.NameTitle.ToLower().Contains("select") ? string.Empty : viewModel.NameTitle),
                Forename = viewModel.Forename,
                Surname = viewModel.Surname,
                MiddleName = viewModel.MiddleName,
                PreviousSurname = viewModel.PreviousSurname,
                Sex = viewModel.Sex,
                DateOfBirth = viewModel.DateOfBirth.GetValueOrDefault(),
                NationalityId = viewModel.NationalityId,
                HasDisability = viewModel.HasDisability,
                DisabilityDescription = viewModel.DisabilityDescription,
                SiteId = viewModel.SiteId,
                JobTitle = viewModel.JobTitle,
                EmploymentStatusId = viewModel.EmploymentStatusId,
                NINumber = viewModel.NINumber,
                PassportNumber = viewModel.PassportNumber,
                PPSNumber = viewModel.PPSNumber,
                WorkVisaNumber = viewModel.WorkVisaNumber,
                WorkVisaExpirationDate = viewModel.WorkVisaExpirationDate.GetValueOrDefault(),
                DrivingLicenseNumber = viewModel.DrivingLicenseNumber,
                DrivingLicenseExpirationDate = viewModel.DrivingLicenseExpirationDate.GetValueOrDefault(),
                HasCompanyVehicle = viewModel.HasCompanyVehicle,
                CompanyVehicleTypeId = viewModel.CompanyVehicleTypeId,
                CompanyVehicleRegistration = viewModel.CompanyVehicleRegistration,
                Address1 = viewModel.Address1,
                Address2 = viewModel.Address2,
                Address3 = viewModel.Address3,
                Town = viewModel.Town,
                County = viewModel.County,
                CountryId = viewModel.CountryId.GetValueOrDefault(),
                Postcode = viewModel.Postcode,
                Telephone = viewModel.Telephone,
                Mobile = viewModel.Mobile,
                Email = viewModel.Email,
                UserId = userId,
                CompanyId = viewModel.CompanyId,
                ContactDetailId = viewModel.ContactDetailId,
                IsRiskAssessor = viewModel.IsRiskAssessor,
                DoNotSendTaskOverdueNotifications = viewModel.DoNotSendTaskOverdueNotifications,
                DoNotSendTaskCompletedNotifications = viewModel.DoNotSendTaskCompletedNotifications,
                DoNotSendReviewDueNotification = viewModel.DoNotSendReviewDueNotification,
                RiskAssessorHasAccessToAllSites = viewModel.RiskAssessorHasAccessToAllSites,
                RiskAssessorSiteId = viewModel.RiskAssessorSiteId.HasValue ? viewModel.RiskAssessorSiteId.Value : 0,
                NotificationType = MapNotificationType(viewModel.NotificationType),
                NotificiationFrequency = viewModel.NotificationFrequency

            };
            result.UserId = userId;
            return result;
        }

        private NotificationTypeDto MapNotificationType(NotificationTypeViewModel notification)
        {
            switch (notification)
            {
                case NotificationTypeViewModel.Daily: return NotificationTypeDto.Daily;
                case NotificationTypeViewModel.Weekly: return NotificationTypeDto.Weekly;
                case NotificationTypeViewModel.Monthly: return NotificationTypeDto.Monthly;
                default: return NotificationTypeDto.Daily;
            }
        }
    }
}