using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Extensions;
using System.Linq;

namespace BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories
{
    public class AddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory : IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory
    {
        private long _companyId;
        private long _riskAssessmentId;
        private long _significantFindingId;

        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;
        
        public AddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory(IFireRiskAssessmentService fireRiskAssessmentService)
        {
            _fireRiskAssessmentService = fireRiskAssessmentService;
        }

        public IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory WithSignificantFindingId(long significantFindingId)
        {
            _significantFindingId = significantFindingId;
            return this;
        }

        public AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var fireRiskAssessment = _fireRiskAssessmentService.GetRiskAssessment(_riskAssessmentId, _companyId);

            var viewModel = new AddEditFireRiskAssessmentFurtherControlMeasureTaskViewModel()
                                {
                                    CompanyId = _companyId,
                                    RiskAssessmentId = _riskAssessmentId,
                                    SignificantFindingId = _significantFindingId,
                                    ExistingDocuments = new ExistingDocumentsViewModel()
                                                            {
                                                                DocumentTypeId = (int)DocumentTypeEnum.FRADocumentType
                                                            },
                                    
                                    TaskReoccurringTypes = new TaskReoccurringType().ToSelectList(),
                                    DoNotSendTaskCompletedNotification = fireRiskAssessment.RiskAssessor == null ? false : fireRiskAssessment.RiskAssessor.DoNotSendTaskCompletedNotifications,
                                    DoNotSendTaskOverdueNotification = fireRiskAssessment.RiskAssessor == null ? false : fireRiskAssessment.RiskAssessor.DoNotSendTaskOverdueNotifications,
                                    Description = GetSignificantFindingAdditionalInfo(fireRiskAssessment,_significantFindingId)
                                    
                                };
            return viewModel;
        }

        private string GetSignificantFindingAdditionalInfo(FireRiskAssessmentDto fireRiskAssessment, long significantFindingId)
        {
            if (fireRiskAssessment.SignificantFindings != null
                && fireRiskAssessment.SignificantFindings.Any(x => x.Id == significantFindingId))
            {
                return fireRiskAssessment.SignificantFindings.First(x => x.Id == significantFindingId).FireAnswer.AdditionalInfo;
            }
            else
            {
                return "";
            }
            
        }
    }
}