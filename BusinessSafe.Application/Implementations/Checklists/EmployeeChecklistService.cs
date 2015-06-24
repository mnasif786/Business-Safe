using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request.Checklists;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.ParameterClasses;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Checklists
{
    public class EmployeeChecklistService : IEmployeeChecklistService
    {
        private readonly IUserForAuditingRepository _auditedUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeChecklistRepository _employeeChecklistRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IPeninsulaLog _log;

        public EmployeeChecklistService(
            IUserForAuditingRepository auditedUserRepository,
            IEmployeeChecklistRepository employeeChecklistRepository,
            IQuestionRepository questionRepository,
            IPeninsulaLog peninsulaLog, IUserRepository userRepository)
        {
            _auditedUserRepository = auditedUserRepository;
            _employeeChecklistRepository = employeeChecklistRepository;
            _questionRepository = questionRepository;
            _log = peninsulaLog;
            _userRepository = userRepository;
        }

        public EmployeeChecklistDto GetById(Guid id)
        {
            _log.Add(new object[] { id });

            try
            {
                var employeeChecklist = _employeeChecklistRepository.GetById(id);
                return new EmployeeChecklistDtoMapper().Map(employeeChecklist);
            }
            catch (Exception e)
            {
                _log.Add(e);
                throw;
            }
        }

        public EmployeeChecklistDto GetWithCompletedOnEmployeesBehalfBy(Guid id)
        {
            _log.Add(new object[] { id });

            try
            {
                var employeeChecklist = _employeeChecklistRepository.GetById(id);
                return new EmployeeChecklistDtoMapper().MapWithCompletedOnEmployeesBehalfBy(employeeChecklist);
            }
            catch (Exception e)
            {
                _log.Add(e);
                throw;
            }
        }

        public IEnumerable<EmployeeChecklistDto> GetByPersonalRiskAssessmentId(long riskAssessmentId)
        {
            _log.Add(new object[] { riskAssessmentId });

            try
            {
                var employeeChecklist = _employeeChecklistRepository.GetByPersonalRiskAssessmentId(riskAssessmentId);
                return new EmployeeChecklistDtoMapper().Map(employeeChecklist);
            }
            catch (Exception e)
            {
                _log.Add(e);
                throw;
            }
        }

        public bool UserIsAuthentic(AuthenticateUserRequest request)
        {
            _log.Add(request);

            try
            {
                var empChecklist = _employeeChecklistRepository.GetById(request.ChecklistId);
                return empChecklist.Password == request.Password;
            }
            catch (Exception e)
            {
                _log.Add(e);
                throw;
            }
        }

        public void Save(SaveEmployeeChecklistRequest request)
        {
            _log.Add(request);

            try
            {
                var employeeChecklist = _employeeChecklistRepository.GetById(request.EmployeeChecklistId);
                var nsbSystemUser = _auditedUserRepository.GetSystemUser();
                var submitAnswerParameterClasses = new List<SubmitPersonalAnswerParameters>();

                foreach (var submitAnswerRequest in request.Answers)
                {
                    var question = _questionRepository.GetById(submitAnswerRequest.QuestionId);

                    submitAnswerParameterClasses.Add(new SubmitPersonalAnswerParameters
                                                         {
                                                             AdditionalInfo = submitAnswerRequest.AdditionalInfo,
                                                             BooleanResponse = submitAnswerRequest.BooleanResponse,
                                                             Question = question
                                                         });
                }

                employeeChecklist.Submit(submitAnswerParameterClasses, nsbSystemUser);
                _employeeChecklistRepository.Save(employeeChecklist);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public ValidationMessageCollection ValidateComplete(CompleteEmployeeChecklistRequest request)
        {
            try
            {
                var employeeChecklist = _employeeChecklistRepository.GetById(request.EmployeeChecklistId);
                var validationMessages = employeeChecklist.ValidateComplete();
                return validationMessages;
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void Complete(CompleteEmployeeChecklistRequest request)
        {
            //verify that all questions have been entered, then throw exception?????
            try
            {
                var empChecklist = _employeeChecklistRepository.GetById(request.EmployeeChecklistId);


                if (empChecklist == null)
                {
                    throw new Exception(string.Format("Employee checklist not found with id:{0}", request.EmployeeChecklistId.ToString()));
                }

                if (empChecklist.CompletedDate.HasValue) return;


                var systemUser = _auditedUserRepository.GetSystemUser();
                var submitAnswerParameterClasses = new List<SubmitPersonalAnswerParameters>();

                // TODO : Can we do something better than this
                var completedOnEmployeesBehalfBy = request.CompletedOnEmployeesBehalfBy.HasValue
                                                       ? _userRepository.GetById(request.CompletedOnEmployeesBehalfBy.Value)
                                                       : null;

       

                foreach (var submitAnswerRequest in request.Answers)
                {
                    var question = _questionRepository.GetById(submitAnswerRequest.QuestionId);

                    submitAnswerParameterClasses.Add(new SubmitPersonalAnswerParameters
                    {
                        AdditionalInfo = submitAnswerRequest.AdditionalInfo,
                        BooleanResponse = submitAnswerRequest.BooleanResponse,
                        Question = question
                    });
                }

                empChecklist.Complete(submitAnswerParameterClasses, completedOnEmployeesBehalfBy, systemUser, request.CompletedDate);

                _employeeChecklistRepository.SaveOrUpdate(empChecklist);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void ToggleFurtherActionRequired(Guid employeeChecklistId, bool isRequired, Guid assessingUserId)
        {
            var empChecklist = _employeeChecklistRepository.GetById(employeeChecklistId);
            var user = _auditedUserRepository.GetById(assessingUserId);

            empChecklist.SetIsFurtherActionRequired(isRequired, user);

            _employeeChecklistRepository.SaveOrUpdate(empChecklist);
        }
    }
}
