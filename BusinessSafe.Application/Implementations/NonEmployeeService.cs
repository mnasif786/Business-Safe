using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations
{
    public class NonEmployeeService : INonEmployeeService
    {
        private readonly INonEmployeeRepository _nonEmployeeRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;


        public NonEmployeeService(
                                  INonEmployeeRepository nonEmployeeRepository,
                                  IUserForAuditingRepository userForAuditingRepository,
                                  IPeninsulaLog log)
        {
            _nonEmployeeRepository = nonEmployeeRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
        }

        public NonEmployeeDto GetNonEmployee(long nonEmployeeId, long companyId)
        {
            _log.Add(new object[] {nonEmployeeId, companyId});

            try
            {
                var nonEmployee = _nonEmployeeRepository.GetByIdAndCompanyId(nonEmployeeId, companyId);
                return new NonEmployeeDtoMapper().Map(nonEmployee);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

        }

        public void MarkNonEmployeeAsDeleted(MarkNonEmployeeAsDeletedRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var nonEmployee = _nonEmployeeRepository.GetByIdAndCompanyId(request.NonEmployeeId, request.CompanyId);
                nonEmployee.MarkForDelete(user);

                _nonEmployeeRepository.SaveOrUpdate(nonEmployee);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
          

        }

        public IEnumerable<NonEmployeeDto> GetAllNonEmployeesForCompany(long companyId)
        {
            _log.Add(companyId);

            try
            {
                var nonEmployees = _nonEmployeeRepository.GetAllNonEmployeesForCompany(companyId);
                return nonEmployees
                                .Select(new NonEmployeeDtoMapper().Map)
                                .ToList();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

           

        }

        public CompanyDefaultSaveResponse SaveNonEmployee(SaveNonEmployeeRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

                NonEmployee nonEmployee;
                if (request.Id == 0)
                {
                    nonEmployee = NonEmployee.Create(request.Name, request.Position, request.NonEmployeeCompanyName, request.CompanyId, user);
                }
                else
                {
                    nonEmployee = _nonEmployeeRepository.GetByIdAndCompanyId(request.Id, request.CompanyId);
                    nonEmployee.Update(request.Name, request.Position, request.NonEmployeeCompanyName, user);
                }

                _nonEmployeeRepository.SaveOrUpdate(nonEmployee);


                return CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(nonEmployee.Id);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }

           

        }
    }
}
