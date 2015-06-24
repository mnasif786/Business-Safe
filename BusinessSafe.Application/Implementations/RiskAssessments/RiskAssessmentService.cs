using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.RiskAssessments
{
    public class RiskAssessmentService : IRiskAssessmentService
    {
        private readonly IPeninsulaLog _log;
        private readonly IRiskAssessmentRepository _riskAssessmentRepository;
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly ITaskService _taskService;
        public RiskAssessmentService(
            IPeninsulaLog log, 
            IRiskAssessmentRepository riskAssessmentRepository, 
            IUserForAuditingRepository userForAuditingRepository,
            ITaskService taskService)
        {
            _log = log;
            _riskAssessmentRepository = riskAssessmentRepository;
            _userForAuditingRepository = userForAuditingRepository;
            _taskService = taskService;
        }

        public RiskAssessmentDto GetByIdAndCompanyId(long riskAssessmentId, long companyId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });
            try
            {
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
                return new RiskAssessmentDtoMapper().MapWithEmployeesAndNonEmployeesAndSiteAndRiskAssessor(riskAssessment);
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }
       
        public bool HasUncompletedTasks(long companyId, long riskAssessmentId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            try
            {
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
                return riskAssessment.HasUncompletedTasks();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public bool HasUndeletedTasks(long companyId, long riskAssessmentId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            try
            {
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);

                return riskAssessment.HasUndeletedTasks();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }
        public DateTime? GetDefaultDateOfNextReviewById(long riskAssessmentId)
        {
            try
            {
                var riskAssessment = _riskAssessmentRepository.GetById(riskAssessmentId);
                return riskAssessment.GetDefaultDateOfNextReview();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkRiskAssessmentAsDraft(MarkRiskAssessmentAsDraftRequest request)
        {
            _log.Add();

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                
                riskAssessment.MarkAsDraft(user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkRiskAssessmentAsLive(MarkRiskAssessmentAsLiveRequest request)
        {
            _log.Add();

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                
                riskAssessment.MarkAsLive(user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkRiskAssessmentAsDeleted(MarkRiskAssessmentAsDeletedRequest request)
        {
            _log.Add();

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
                riskAssessment.MarkForDelete(user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void ReinstateRiskAssessmentAsNotDeleted(ReinstateRiskAssessmentAsDeletedRequest request)
        {
            _log.Add();

            try
            {
                var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyIdIncludeDeleted(request.RiskAssessmentId, request.CompanyId);
                riskAssessment.ReinstateRiskAssessmentAsNotDeleted(user);

                _riskAssessmentRepository.SaveOrUpdate(riskAssessment);

            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public bool CanMarkRiskAssessmentAsLive(long companyId, long riskAssessmentId)
        {
            _log.Add(new object[] { riskAssessmentId, companyId });

            try
            {
                var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(riskAssessmentId, companyId);
                return riskAssessment.HasAnyReviews();
            }
            catch (Exception ex)
            {
                _log.Add(ex);
                throw;
            }
        }

        public void MarkAllRealtedUncompletedTasksAsNoLongerRequired(MarkRiskAssessmentTasksAsNoLongerRequiredRequest request)
        {
            var riskAssessment = _riskAssessmentRepository.GetByIdAndCompanyId(request.RiskAssessmentId, request.CompanyId);
            var furtherControlMeasureTasks = riskAssessment.GetAllUncompleteFurtherControlMeasureTasks();
            foreach (var task in furtherControlMeasureTasks)
            {
                _taskService.MarkTaskAsNoLongerRequired(new MarkTaskAsNoLongerRequiredRequest()
                {
                    CompanyId = request.CompanyId,
                    TaskId = task.Id,
                    UserId = request.UserId
                });
            }
        }
    }
}
