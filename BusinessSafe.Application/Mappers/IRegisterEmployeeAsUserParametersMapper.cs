using BusinessSafe.Application.Request;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Application.Mappers
{
    public interface IRegisterEmployeeAsUserParametersMapper
    {
        RegisterEmployeeAsUserParameters Map(CreateEmployeeAsUserRequest request);
    }
}