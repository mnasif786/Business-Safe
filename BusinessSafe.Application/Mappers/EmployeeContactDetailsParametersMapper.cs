using BusinessSafe.Application.Request;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Mappers
{
    public interface IEmployeeContactDetailsParametersMapper
    {
        AddUpdateEmployeeContactDetailsParameters Map(SaveEmployeeRequest request);
    }

    public class EmployeeContactDetailsParametersMapper : IEmployeeContactDetailsParametersMapper
    {
        private readonly ICountriesRepository _countriesRepository;
        
        public EmployeeContactDetailsParametersMapper(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        public AddUpdateEmployeeContactDetailsParameters Map(SaveEmployeeRequest request)
        {
            var result = new AddUpdateEmployeeContactDetailsParameters
                                                       {
                                                           Id = request.ContactDetailId,
                                                           Address1 = request.Address1,
                                                           Address2 = request.Address2,
                                                           Address3 = request.Address3,
                                                           Town = request.Town,
                                                           County = request.County,
                                                           PostCode = request.Postcode,
                                                           Telephone1 = request.Telephone,
                                                           Telephone2 = request.Mobile,
                                                           Email = request.Email
                                                       };

            if (request.CountryId != default(int))
            {
                result.Country = _countriesRepository.LoadById(request.CountryId);
            }
            return result;
        }
    }
}