using System;

using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Company
{
    public class BusinessSafeCompanyDetailService : IBusinessSafeCompanyDetailService
    {
        private readonly IBusinessSafeCompanyDetailRepository _businessSafeCompanyDetailRepository;
        private readonly IEmployeeForAuditingRepository _employeeForAuditingRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;

        private readonly IPeninsulaLog _log;

        public BusinessSafeCompanyDetailService(IBusinessSafeCompanyDetailRepository businessSafeCompanyDetailRepository,
            IEmployeeForAuditingRepository employeeRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IPeninsulaLog log)
        {
            _businessSafeCompanyDetailRepository = businessSafeCompanyDetailRepository;
            _employeeForAuditingRepository = employeeRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
        }

        public BusinessSafeCompanyDetailDto Get(long companyId)
        {
            var companyDetails = _businessSafeCompanyDetailRepository.GetByCompanyId(companyId);
            return companyDetails == null ? null : new BusinessSafeCompanyDetailDtoMapper().Map(companyDetails);
        }

        public UpdateCompanyDetailsResponse UpdateBusinessSafeContact(CompanyDetailsRequest request)
        {
            try
            {
                var employee = GetEmployee(request);

                if (employee != null)
                {
                    var companyDetails = SetCompanyDetails(request, employee);

                    _businessSafeCompanyDetailRepository.SaveOrUpdate(companyDetails);
                }

                return new UpdateCompanyDetailsResponse
                {
                    Success = true
                };

            }
            catch (NullReferenceException)
            {
                var exception = new BusinessSafeCompanyDetailsNotFoundException(request.Id);
                _log.Add(exception);
                throw exception;
            }
            catch (Exception e)
            {
                _log.Add(e);
                throw e;
            }
        }

        private BusinessSafeCompanyDetail SetCompanyDetails(CompanyDetailsRequest request, EmployeeForAuditing employee)
        {
            BusinessSafeCompanyDetail company = null;
            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.Id);

                company = _businessSafeCompanyDetailRepository.GetByCompanyId(request.Id);

                if (company == null)
                {
                    company = BusinessSafeCompanyDetail.Create(request.Id, employee, user);
                }
                else
                {
                    company.BusinessSafeContactEmployee = employee;    
                }
            }
            catch (Exception e)
            {
                _log.Add(e);
            }

            return company;
        }

        private EmployeeForAuditing GetEmployee(CompanyDetailsRequest request)
        {
            EmployeeForAuditing employee = null;
            employee = _employeeForAuditingRepository.GetByIdAndCompanyIdWithoutChecking(request.NewCompanyDetails.BusinessSafeContactId, request.Id);
            return employee;
        }
    }
}
