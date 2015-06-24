using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;

namespace BusinessSafe.WebSite.Areas.Company.Tasks
{
    public class NonEmployeeSaveTask : INonEmployeeSaveTask
    {
        private readonly INonEmployeeService _nonEmployeeService;
        private readonly IDoesNonEmployeeAlreadyExistGuard _nonEmployeeDefaultAlreadyExistGuard;

        public NonEmployeeSaveTask(INonEmployeeService nonEmployeeService, IDoesNonEmployeeAlreadyExistGuard nonEmployeeDefaultAlreadyExistGuard)
        {
            _nonEmployeeService = nonEmployeeService;
            _nonEmployeeDefaultAlreadyExistGuard = nonEmployeeDefaultAlreadyExistGuard;
        }

        public CompanyDefaultSaveResponse Execute(SaveNonEmployeeRequest request)
        {
            if (request.RunMatchCheck)
            {
                var nonEmployeeDefaultAlreadyExistGuard = new GuardDefaultExistsRequest(request.Name, request.Id, request.CompanyId);
                var existResult = _nonEmployeeDefaultAlreadyExistGuard.Execute(nonEmployeeDefaultAlreadyExistGuard);

                if (existResult.Exists)
                {
                    return CompanyDefaultSaveResponse.CompanyDefaultMatches(existResult.MatchingResults);
                }
            }

            return _nonEmployeeService.SaveNonEmployee(request);
        }
    }
}