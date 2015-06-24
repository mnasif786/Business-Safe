using System.Collections.Generic;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.FireRiskAssessments
{
    public class SignificantFindingService : ISignificantFindingService
    {
        private readonly IUserForAuditingRepository _userForAuditingRepository;
        private readonly IFireAnswerRepository _fireAnswerRepository;

        public SignificantFindingService(
            IUserForAuditingRepository userForAuditingRepository,
            IFireAnswerRepository fireAnswerRepository
            )
        {
            _userForAuditingRepository = userForAuditingRepository;
            _fireAnswerRepository = fireAnswerRepository;
        }

        public void MarkSignificantFindingAsDeleted(MarkSignificantFindingAsDeletedRequest request)
        {
            var user = _userForAuditingRepository.GetByIdAndCompanyId(request.UserId, request.CompanyId);

            var fireAnswer = _fireAnswerRepository.GetByChecklistIdAndQuestionId(request.FireChecklistId,
                                                                                 request.FireQuestionId);
            if (fireAnswer.SignificantFinding == null)
            {
                throw new TryingToDeleteSignificantFindingFromFireAnswer(fireAnswer.Id);
            }
            fireAnswer.SignificantFinding.MarkForDelete(user);

            _fireAnswerRepository.SaveOrUpdate(fireAnswer);
        }
    }
}
