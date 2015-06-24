using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Mappers
{

    public interface IEmergencyContactDetailsParametersMapper
    {
        IEnumerable<EmergencyContactDetailParameters> Map(SaveEmployeeRequest request);
        EmergencyContactDetailParameters Map(SaveEmergencyContactBaseRequest request, Employee employee);
    }

    public class EmergencyContactDetailsParametersMapper : IEmergencyContactDetailsParametersMapper
    {
        private readonly ICountriesRepository _countriesRepository;

        public EmergencyContactDetailsParametersMapper(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public IEnumerable<EmergencyContactDetailParameters> Map(SaveEmployeeRequest request)
        {
            var result = new List<EmergencyContactDetailParameters>();
            foreach (var emergencyContactRequest in request.EmergencyContactDetails)
            {
                var emergencyContactDetailParameters = new EmergencyContactDetailParameters
                                                           {

                                                               Title = emergencyContactRequest.Title,
                                                               Forename = emergencyContactRequest.Forename,
                                                               Surname = emergencyContactRequest.Surname,
                                                               Relationship = emergencyContactRequest.Relationship,
                                                               Telephone1 = emergencyContactRequest.WorkTelephone,
                                                               Telephone2 = emergencyContactRequest.HomeTelephone,
                                                               Telephone3 = emergencyContactRequest.MobileTelephone,
                                                               PreferedTelephone = emergencyContactRequest.PreferredContactNumber,
                                                               Address1 = emergencyContactRequest.Address1,
                                                               Address2 = emergencyContactRequest.Address2,
                                                               Address3 = emergencyContactRequest.Address3,
                                                               County = emergencyContactRequest.County,
                                                               PostCode = emergencyContactRequest.PostCode
                                                           };

                if (emergencyContactRequest.CountryId != 0)
                {
                    emergencyContactDetailParameters.Country = _countriesRepository.GetById(emergencyContactRequest.CountryId);
                }

                result.Add(emergencyContactDetailParameters);
            }
            return result;
        }

        public EmergencyContactDetailParameters Map(SaveEmergencyContactBaseRequest request, Employee employee)
        {
            var result = new EmergencyContactDetailParameters
                {
                    EmergencyContactId =  request.EmergencyContactId,
                    Title = request.Title,
                    Forename = request.Forename,
                    Surname = request.Surname,
                    Relationship = request.Relationship,
                    SameAddressAsEmployee = request.SameAddressAsEmployee,
                    Telephone1 = request.WorkTelephone,
                    Telephone2 = request.HomeTelephone,
                    Telephone3 = request.MobileTelephone,
                    PreferedTelephone = request.PreferredContactNumber,
                    Address1 = request.Address1,
                    Address2 = request.Address2,
                    Address3 = request.Address3,
                    Town = request.Town,
                    County = request.County,
                    PostCode = !string.IsNullOrEmpty(request.PostCode) ? request.PostCode.ToUpper(): string.Empty,
                    Country = request.CountryId != 0 ? _countriesRepository.GetById(request.CountryId): null
                };

            return result;
        }
    }
}