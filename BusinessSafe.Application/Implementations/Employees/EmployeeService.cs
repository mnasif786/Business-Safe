using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.Data.Queries;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation.Results;
using NHibernate.Util;
using NServiceBus;
using Peninsula.Online.Messages.Commands;

namespace BusinessSafe.Application.Implementations.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeParametersMapper _employeeParametersMapper;
        private readonly IEmployeeContactDetailsParametersMapper _contactDetailsParametersMapper;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IRegisterEmployeeAsUserParametersMapper _registerEmployeeAsUserParametersMapper;
        private readonly IPeninsulaLog _log;
        private readonly ITaskService _taskService;
        private readonly IBus _bus;
        private readonly IGetEmployeeNamesQuery _getEmployeeNamesQuery;
        private readonly ISiteStructureElementRepository _siteStructureElementRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeParametersMapper employeeParametersMapper
            , IEmployeeContactDetailsParametersMapper contactDetailsParametersMapper
            , IUserForAuditingRepository userForAuditingRepository
            , IRegisterEmployeeAsUserParametersMapper registerEmployeeAsUserParametersMapper, ITaskService taskService
            , IPeninsulaLog log
            , IBus bus
            , IGetEmployeeNamesQuery getEmployeeNamesQuery
            , ISiteStructureElementRepository siteStructureElementRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeParametersMapper = employeeParametersMapper;
            _contactDetailsParametersMapper = contactDetailsParametersMapper;
            _userForAuditingRepository = userForAuditingRepository;
            _registerEmployeeAsUserParametersMapper = registerEmployeeAsUserParametersMapper;
            _log = log;
            _taskService = taskService;
            _bus = bus;
            _getEmployeeNamesQuery = getEmployeeNamesQuery;
            _siteStructureElementRepository = siteStructureElementRepository;
        }

        public IEnumerable<EmployeeDto> GetAll(long companyId)
        {
            _log.Add(new object[] { companyId });

            try
            {
                var employees = _employeeRepository.Search(companyId, null, null, null, new long[] { }, false, 0, true, false, "Surname", true);
                //return new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(employees);
                return new EmployeeDtoMapper().Map(employees);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public IEnumerable<EmployeeDto> GetEmployeesForSearchTerm(string searchTerm, long companyId, int pageLimit)
        {
            _log.Add(new object[] { searchTerm, companyId, pageLimit });

            var employees = _employeeRepository.GetByTermSearch(searchTerm, companyId, pageLimit);
            return employees.Select(x => new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetails(x)).ToList();
        }

        public List<EmployeeName> GetEmployeeNames(long companyId)
        {
            var employees = _getEmployeeNamesQuery.Execute(companyId);
            return employees.ToList();
        }

        public EmployeeDto GetEmployee(Guid employeeId, long companyId)
        {
            var employee = _employeeRepository.GetByIdAndCompanyId(employeeId, companyId);
            return new EmployeeDtoMapper().MapWithContactDetailsAndEmergencyContactDetailsAndUser(employee);
        }

        public AddEmployeeResponse Add(AddEmployeeRequest request)
        {
            _log.Add(request);

            try
            {

                var user = _userForAuditingRepository.GetById(request.UserId);
                var employeeParameters = GetAddingEmployeeParameters(request);
                var employee = Employee.Create(employeeParameters, user);
                var contactDetailsParameters = GetEmployeeContactDetailParameters(request);
                var contactDetails = EmployeeContactDetail.Create(contactDetailsParameters, user, employee);

                employee.AddContactDetails(contactDetails);
                _employeeRepository.SaveOrUpdate(employee);

                return new AddEmployeeResponse
                           {
                               Success = true,
                               EmployeeId = employee.Id
                           };
            }
            catch (InvalidAddUpdateEmployeeContactDetailsParameters ex)
            {
                return new AddEmployeeResponse
                {
                    Success = false,
                    EmployeeId = Guid.Empty,
                    Errors = ex.Errors
                };
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public UpdateEmployeeResponse Update(UpdateEmployeeRequest request)
        {
            _log.Add(request);

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
                var employeeParameters = GetUpdatingEmployeeParameters(request);
                var contactDetailsParameters = GetEmployeeContactDetailParameters(request);
                var riskAssessorSite = request.RiskAssessorSiteId > 0 ? _siteStructureElementRepository.GetByIdAndCompanyId(request.RiskAssessorSiteId, request.CompanyId) : null;

                // to prevent the dreaded,  "was not processed by flush()",  nhibernate error, load the risk assessor site
                if (employee.RiskAssessor != null && employee.RiskAssessor.Site != null)
                {
                    _siteStructureElementRepository.Initialize(employee.RiskAssessor.Site);
                }

                employee.Update(employeeParameters, contactDetailsParameters, user);
                employee.UpdateRiskAssessorDetails(request.IsRiskAssessor, request.RiskAssessorHasAccessToAllSites, riskAssessorSite, request.DoNotSendTaskOverdueNotifications, request.DoNotSendTaskCompletedNotifications, 
                    request.DoNotSendReviewDueNotification, user);

                _employeeRepository.SaveOrUpdate(employee);

                if (CanSendUpdateRegistrationRequest(request, employee))
                {
                    _bus.Send(new UpdateUserRegistration
                    {
                        UserId = employee.User.Id,
                        ActioningUserId = user.Id,
                        SecurityAnswer = String.IsNullOrEmpty(request.Telephone) ? request.Mobile : request.Telephone,
                        Email = request.Email
                    });
                }
                return new UpdateEmployeeResponse
                {
                    Success = true
                };

            }
            catch (InvalidAddUpdateEmployeeContactDetailsParameters ex)
            {
                return new UpdateEmployeeResponse
                {
                    Success = false,
                    Errors = ex.Errors
                };
            }

        }

        private static bool CanSendUpdateRegistrationRequest(UpdateEmployeeRequest request, Employee employee)
        {
            bool sendUpdateRegistrationRequest = (request.Telephone != (employee.ContactDetails.Any()
                ? employee.ContactDetails[0].Telephone1 : null))
                                                 ||
                                                 (request.Email != (employee.ContactDetails.Any()
                                                     ? employee.ContactDetails[0].Email : null))
                                                 ||
                                                 (request.Mobile != (employee.ContactDetails.Any()
                                                     ? employee.ContactDetails[0].Telephone2 : null));


            // check employee has user associated otherwise exception thrown
            if (employee.User != null && sendUpdateRegistrationRequest == true)
            {
                sendUpdateRegistrationRequest = employee.User.IsRegistered != true && employee.User.Deleted != true;
            }
            else
            {
                // don't update is user details arent added
                sendUpdateRegistrationRequest = false;
            }
            return sendUpdateRegistrationRequest;
        }

        public void UpdateEmailAddress(UpdateEmployeeEmailAddressRequest request)
        {

            var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
            var currentUser = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);
            employee.SetEmail(request.Email, currentUser);
            _employeeRepository.SaveOrUpdate(employee);
        }

        public void UpdateOnlineRegistrationDetails(UpdateOnlineRegistrationDetailsRequest request)
        {
            var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
            var currentUser = _userForAuditingRepository.GetByIdAndCompanyId(request.CurrentUserId, request.CompanyId);
         
            var employeeContactDetails = new AddUpdateEmployeeContactDetailsParameters()
            {
                Id = employee.ContactDetails.Any() ? employee.ContactDetails.Last().Id : 1,
                Email = request.Email,
                Telephone1 = request.Telephone,
                Telephone2 = request.Mobile
            };

            employee.UpdateContactDetails(employeeContactDetails, currentUser);
            _employeeRepository.SaveOrUpdate(employee);
        }

        public List<EmployeeDto> Search(SearchEmployeesRequest request)
        {
            _log.Add(request);

            try
            {
                IEnumerable<Employee> employees = _employeeRepository.Search(request.CompanyId,
                                                                             request.EmployeeReferenceLike,
                                                                             request.ForenameLike,
                                                                             request.SurnameLike,
                                                                             request.SiteIds,
                                                                             request.ShowDeleted,
                                                                             request.MaximumResults,
                                                                             request.IncludeSiteless,
                                                                             request.ExcludeWithActiveUser,
                                                                             null,
                                                                             true);


                return employees.Select(new EmployeeDtoMapper().MapWithNationalityAndContactDetailsAndEmergencyContactDetailsAndUser).ToList();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkEmployeeAsDeleted(MarkEmployeeAsDeletedRequest request)
        {
            bool hasEmployeeGotOutstandingTasks = _taskService.HasEmployeeGotOutstandingTasks(request.EmployeeId, request.CompanyId);
            if (hasEmployeeGotOutstandingTasks)
            {
                throw new TryingToDeleteEmployeeWithOutstandingTasksException(request.EmployeeId);
            }

            var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
            employee.MarkForDelete(user);
            _employeeRepository.SaveOrUpdate(employee);

            if (employee.User == null) return;
            var deleteUserInPeninsulaOnlineCommand = new Peninsula.Online.Messages.Commands.DeleteUser() {ActioningUserId = request.UserId, UserId = employee.User.Id};
            _bus.Send(deleteUserInPeninsulaOnlineCommand);
        }

        public void ReinstateEmployeeAsNotDeleted(ReinstateEmployeeAsNotDeleteRequest request)
        {
            _log.Add(request);

            try
            {

                var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                employee.ReinstateEmployeeAsNotDeleted(user);
                _employeeRepository.SaveOrUpdate(employee);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        private AddUpdateEmployeeParameters GetUpdatingEmployeeParameters(UpdateEmployeeRequest addEmployeeRequest)
        {
            return _employeeParametersMapper.Map(addEmployeeRequest);
        }

        private AddUpdateEmployeeParameters GetAddingEmployeeParameters(AddEmployeeRequest addEmployeeRequest)
        {
            return _employeeParametersMapper.Map(addEmployeeRequest);
        }

        private AddUpdateEmployeeContactDetailsParameters GetEmployeeContactDetailParameters(SaveEmployeeRequest saveEmployeeRequest)
        {
            return _contactDetailsParametersMapper.Map(saveEmployeeRequest);
        }

        public ValidationResult ValidateRegisterAsUser(CreateEmployeeAsUserRequest request)
        {
            _log.Add(request);

            try
            {
                var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
                var parameters = _registerEmployeeAsUserParametersMapper.Map(request);
                return employee.ValidateRegisterAsUser(parameters);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void CreateUser(CreateEmployeeAsUserRequest request)
        {
            _log.Add(request);

            var employee = _employeeRepository.GetByIdAndCompanyId(request.EmployeeId, request.CompanyId);
            var parameters = _registerEmployeeAsUserParametersMapper.Map(request);
            employee.CreateUser(parameters);
            _employeeRepository.SaveOrUpdate(employee);

        }

       
    }
}