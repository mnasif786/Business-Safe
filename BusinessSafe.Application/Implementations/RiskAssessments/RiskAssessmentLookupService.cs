using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class RiskAssessmentLookupService : IRiskAssessmentLookupService
    {

        private readonly IPeninsulaLog _log;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INonEmployeeRepository _nonEmployeeRepository;

        public RiskAssessmentLookupService(IRiskAssessmentRepository riskAssessmentRepository, IPeninsulaLog log, IEmployeeRepository employeeRepository, INonEmployeeRepository nonEmployeeRepository)
        {
            _riskAssessmentRepository = riskAssessmentRepository;
            _log = log;
            _employeeRepository = employeeRepository;
            _nonEmployeeRepository = nonEmployeeRepository;
        }

        public IEnumerable<NonEmployeeDto> SearchForNonEmployeesNotAttachedToRiskAssessment(NonEmployeesNotAttachedToRiskAssessmentSearchRequest request)
        {
            _log.Add(new object[] { request.SearchTerm, request.CompanyId });

            try
            {
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId,request.CompanyId);
                return GetNonEmployeesNotAttachedToRiskAssessment(request.SearchTerm, request.CompanyId, riskAssessment, 20);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }


        }

        public IEnumerable<EmployeeDto> SearchForEmployeesNotAttachedToRiskAssessment(EmployeesNotAttachedToRiskAssessmentSearchRequest request)
        {
            _log.Add(new object[] { request.SearchTerm,request.CompanyId });

            try
            {
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId,request.CompanyId);
                return GetEmployeesNotAttachedToRiskAssessment(request.SearchTerm,request.CompanyId,riskAssessment,request.PageLimit);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private IEnumerable<NonEmployeeDto> GetNonEmployeesNotAttachedToRiskAssessment(string searchTerm, long companyId, RiskAssessment riskAssessment, int pageLimit)
        {
            _log.Add(new object[] { searchTerm, companyId });

            try
            {
                var nonEmployees = _nonEmployeeRepository.GetByTermSearch(searchTerm, companyId, pageLimit);

                return nonEmployees
                    .Where(RiskAssessmentNotGotNonEmployeeAlreadyAttached(riskAssessment))
                    .Select(new NonEmployeeDtoMapper().Map)
                    .ToList();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private IEnumerable<EmployeeDto> GetEmployeesNotAttachedToRiskAssessment(string searchTerm, long companyId, RiskAssessment riskAssessment, int pageLimit)
        {
            _log.Add(new object[] { searchTerm, companyId, pageLimit });

            try
            {
                var employees = _employeeRepository.GetByTermSearch(searchTerm, companyId, pageLimit);
                return employees
                    .Select(x => new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(x))
                    .Where(RiskAssessmentNotGotEmployeeAlreadyAttached(riskAssessment))
                    .ToList();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private static Func<EmployeeDto, bool> RiskAssessmentNotGotEmployeeAlreadyAttached(RiskAssessment riskAssessment)
        {
            return x => riskAssessment.Employees.Select(z => z.Employee).Count(y => y.Id == x.Id) == 0;
        }

        private static Func<NonEmployee, bool> RiskAssessmentNotGotNonEmployeeAlreadyAttached(RiskAssessment riskAssessment)
        {
            return x => riskAssessment.NonEmployees.Select(z => z.NonEmployee).Count(y => y.Id == x.Id) == 0;
        }

    }
}