using System;

using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Employees
{
    public class EmployeeEmergencyContactDetailService : IEmployeeEmergencyContactDetailService
    {
        private readonly IEmployeeEmergencyContactDetailsRepository _employeeEmergencyContactDetailsRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmergencyContactDetailsParametersMapper _emergencyContactDetailsParametersMapper;
        private readonly IPeninsulaLog _log;
        private readonly IUserForAuditingRepository _userForAuditingRepository;

        public EmployeeEmergencyContactDetailService(
            IEmployeeEmergencyContactDetailsRepository employeeEmergencyContactDetailsRepository,
            IEmployeeRepository employeeRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IEmergencyContactDetailsParametersMapper emergencyContactDetailsParametersMapper,
            IPeninsulaLog log)
        {
            _employeeEmergencyContactDetailsRepository = employeeEmergencyContactDetailsRepository;
            _employeeRepository = employeeRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _emergencyContactDetailsParametersMapper = emergencyContactDetailsParametersMapper;
            _log = log;
        }

        public void CreateEmergencyContactDetailForEmployee(CreateEmergencyContactRequest request)
        {
            _log.Add(request);
            try
            {

                var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

                var emergencyContactDetailsParameter = _emergencyContactDetailsParametersMapper.Map(request, employee);
                var emergencyContactDetail = EmployeeEmergencyContactDetail.Create(emergencyContactDetailsParameter, user);

                employee.AddEmergencyContact(emergencyContactDetail, user);

                _employeeRepository.SaveOrUpdate(employee);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void UpdateEmergencyContactDetailForEmployee(UpdateEmergencyContactRequest request)
        {
            _log.Add(request);

            try
            {

                var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

                var emergencyContactDetailsParameter = _emergencyContactDetailsParametersMapper.Map(request, employee);
                employee.UpdateEmergencyContact(emergencyContactDetailsParameter, user);

                _employeeRepository.SaveOrUpdate(employee);


            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkEmployeeEmergencyContactAsDeleted(MarkEmployeeEmergencyContactAsDeletedRequest request)
        {
            _log.Add(request);

            try
            {

                var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);


                employee.MarkEmergencyContactForDelete(request.EmergencyContactId, user);


                _employeeRepository.SaveOrUpdate(employee);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public EmployeeEmergencyContactDetailDto GetByIdAndCompanyId(int id, long companyId)
        {
            _log.Add(new object[] { id, companyId });

            var employeeEmergencyContactDetail = _employeeEmergencyContactDetailsRepository.GetByIdAndCompanyId(id, companyId);
            var employeeEmergencyContactDetailDto = new EmployeeEmergencyContactDetailDtoMapper().Map(employeeEmergencyContactDetail);
            return employeeEmergencyContactDetailDto;
        }
    }
}