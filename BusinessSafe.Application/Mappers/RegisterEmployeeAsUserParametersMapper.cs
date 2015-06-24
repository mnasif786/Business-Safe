using System;
using System.Linq;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Mappers
{
    public class RegisterEmployeeAsUserParametersMapper : IRegisterEmployeeAsUserParametersMapper
    {
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ISiteStructureElementRepository _siteRepository;
        private readonly IRoleRepository _roleRepository;

        public RegisterEmployeeAsUserParametersMapper(
            IUserForAuditingRepository userForAuditingRepository,
            ISiteStructureElementRepository siteRepository,
            IRoleRepository roleRepository)
        {
            _userForAuditingRepository = userForAuditingRepository;
            _siteRepository = siteRepository;
            _roleRepository = roleRepository;
        }

        public RegisterEmployeeAsUserParameters Map(CreateEmployeeAsUserRequest request)
        {
            var parameters = new RegisterEmployeeAsUserParameters
                                 {
                                     NewUserId = request.NewUserId,
                                     CompanyId = request.CompanyId
                                 };
            parameters.ActioningUser = _userForAuditingRepository.GetByIdAndCompanyId(request.ActioningUserId, request.CompanyId);
            if (request.MainSiteId != default(long)) parameters.MainSite = _siteRepository.GetByIdAndCompanyId(request.MainSiteId, request.CompanyId);
            if (request.RoleId != default(Guid)) parameters.Role = _roleRepository.GetByIdAndCompanyId(request.RoleId, request.CompanyId);
            if (request.PermissionsForAllSites)
                parameters.Site = _siteRepository.GetByCompanyId(request.CompanyId).First(x => x.IsMainSite);
            else
            {
                if (request.SiteId != default(long)) parameters.Site = _siteRepository.GetByIdAndCompanyId(request.SiteId, request.CompanyId);
            }

            return parameters;
        }
    }
}
