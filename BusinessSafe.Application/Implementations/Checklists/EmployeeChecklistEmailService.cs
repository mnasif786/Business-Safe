using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Checklists
{
    public class EmployeeChecklistEmailService : IEmployeeChecklistEmailService
    {
        private readonly IEmployeeChecklistEmailRepository _employeeChecklistEmailRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IChecklistRepository _checklistRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IPeninsulaLog _log;
        private readonly IEmployeeChecklistRepository _employeeChecklistRepository;
        private readonly IPersonalRiskAssessmentRepository _riskAssessmentRepository;

        public EmployeeChecklistEmailService(
            IEmployeeChecklistEmailRepository employeeChecklistEmailRepository,
            IEmployeeRepository employeeRepository,
            IChecklistRepository checklistRepository,
            IUserForAuditingRepository userForAuditingRepository,
            IPeninsulaLog log,
            IEmployeeChecklistRepository employeeChecklistRepository,
            IPersonalRiskAssessmentRepository riskAssessmentRepository)
        {
            _employeeChecklistEmailRepository = employeeChecklistEmailRepository;
            _employeeRepository = employeeRepository;
            _checklistRepository = checklistRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _log = log;
            _employeeChecklistRepository = employeeChecklistRepository;
            _riskAssessmentRepository = riskAssessmentRepository;
        }

        public IEnumerable<EmployeeChecklistEmailDto> GetByIds(IList<Guid> employeeChecklistEmailIds)
        {
            _log.Add(employeeChecklistEmailIds);

            try
            {
                var employeeChecklistEmails = _employeeChecklistEmailRepository.GetByIds(employeeChecklistEmailIds);
                return new EmployeeChecklistEmailDtoMapper().Map(employeeChecklistEmails);
            }
            catch (Exception e)
            {
                _log.Add(e);
                throw;
            }
        }

        public IList<Guid> Generate(GenerateEmployeeChecklistEmailRequest request)
        {
            _log.Add(request);

            try
            {
                var generatingUser = _userForAuditingRepository.GetById(request.GeneratingUserId);
                var checklists = _checklistRepository.GetByIds(request.ChecklistIds).ToList();
                var riskAssessment = _riskAssessmentRepository.GetById(request.RiskAssessmentId);
                var employees = _employeeRepository.GetByIds(request.RequestEmployees.Select(x => x.EmployeeId).ToList());
                var existingReferenceParameters = _employeeChecklistRepository.GetExistingReferencesForPrefixes(employees.Select(x => x.PrefixForEmployeeChecklists)).ToList();

                var employeesParameters =
                    request.RequestEmployees.Select(requestEmployee => new EmployeesWithNewEmailsParameters
                                                             {
                                                                 Employee = employees.Single(x => x.Id == requestEmployee.EmployeeId),
                                                                 NewEmail = requestEmployee.NewEmail
                                                             }).ToList();

                var employeeChecklistEmails = EmployeeChecklistEmail.Generate(
                    employeesParameters, 
                    checklists, 
                    request.Message, 
                    generatingUser, 
                    riskAssessment, 
                    request.SendCompletedChecklistNotificationEmail, 
                    request.CompletionDueDateForChecklists, 
                    request.CompletionNotificationEmailAddress,
                    existingReferenceParameters);

                foreach (var employeeChecklistEmail in employeeChecklistEmails)
                {
                    _employeeChecklistEmailRepository.Save(employeeChecklistEmail);
                }

                _employeeChecklistEmailRepository.Flush();
                return employeeChecklistEmails.Select(employeeChecklistEmail => employeeChecklistEmail.Id).ToList();
            }
            catch (Exception e)
            {
                _log.Add(e);
                throw;
            }
        }

        public EmployeeChecklistEmail Regenerate(ResendEmployeeChecklistEmailRequest request)
        {
            _log.Add(request);

            try
            {
                var generatingUser = _userForAuditingRepository.GetById(request.ResendUserId);
                var employeeChecklist = _employeeChecklistRepository.GetByIdAndRiskAssessmentId(request.EmployeeChecklistId, request.RiskAssessmentId);
                var regenerateEmployeeChecklistEmail = EmployeeChecklistEmail.Generate(employeeChecklist, generatingUser);
                _employeeChecklistEmailRepository.Save(regenerateEmployeeChecklistEmail);
                _employeeChecklistEmailRepository.Flush();

                return regenerateEmployeeChecklistEmail;

            }
            catch (Exception e)
            {
                _log.Add(e);
                throw;
            }
        }
    }
}
