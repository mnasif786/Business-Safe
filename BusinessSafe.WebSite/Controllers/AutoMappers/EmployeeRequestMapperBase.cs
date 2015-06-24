using System.Collections.Generic;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;

namespace BusinessSafe.WebSite.Controllers.AutoMappers
{
    public class EmployeeRequestMapperBase
    {
        protected IEnumerable<CreateEmergencyContactRequest> MapEmergencyContactDetails(EmployeeViewModel viewModel)
        {
            var result = new List<CreateEmergencyContactRequest>();

            foreach (var request in viewModel.EmergencyContactDetails)
            {
                var addEmergencyContactRequest = new CreateEmergencyContactRequest()
                                                     {
                                                         Title = request.Title, 
                                                         Forename = request.Forename, 
                                                         Surname = request.Surname, 
                                                         Relationship = request.Relationship, 
                                                         WorkTelephone = request.WorkTelephone, 
                                                         HomeTelephone = request.HomeTelephone, 
                                                         PreferredContactNumber = request.PreferredContactNumber, 
                                                         MobileTelephone = request.MobileTelephone,
                                                         SameAddressAsEmployee = request.SameAddressAsEmployee,
                                                         Address1 = request.Address1,
                                                         Address2 = request.Address2,
                                                         Address3 = request.Address3,
                                                         Town = request.Town,
                                                         County = request.County,
                                                         CountryId = request.EmergencyContactCountryId.GetValueOrDefault(),
                                                         PostCode = request.PostCode
                                                     };

                result.Add(addEmergencyContactRequest);
            }

            return result;
        }
    }
}