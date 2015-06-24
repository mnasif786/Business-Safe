using System.Linq;

using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Company
{
    public interface IDoesNonEmployeeAlreadyExistGuard
    {
        GuardDefaultExistsReponse Execute(GuardDefaultExistsRequest request);
    }


    public class DoesNonEmployeeAlreadyExistGuard: IDoesNonEmployeeAlreadyExistGuard
    {
        private readonly INonEmployeeRepository _nonEmployeeRepository;

        public DoesNonEmployeeAlreadyExistGuard(INonEmployeeRepository nonEmployeeRepository)
        {
            _nonEmployeeRepository = nonEmployeeRepository;
        }

        public GuardDefaultExistsReponse Execute(GuardDefaultExistsRequest request)
        {
            var nonEmployees = _nonEmployeeRepository.GetAllByNameSearch(request.Name, request.ExcludeId, request.CompanyId);
            if (!nonEmployees.Any())
            {
                return GuardDefaultExistsReponse.NoMatches;
            }
                
            var matches = nonEmployees
                                    .OrderBy(x => x.Name)
                                    .Select(nonEmployee => string.Format("{0}, {1}, {2}", nonEmployee.Name, nonEmployee.Company, nonEmployee.Position))
                                    .ToList();
            return GuardDefaultExistsReponse.MatchesExist(matches);
        }
    }
}