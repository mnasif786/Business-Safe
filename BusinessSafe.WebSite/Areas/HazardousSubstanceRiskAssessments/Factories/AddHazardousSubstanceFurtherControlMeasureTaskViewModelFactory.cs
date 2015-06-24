using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Extensions;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public class AddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory : IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory
    {
        private long _companyId;
        private long _riskAssessmentId;
        private IHazardousSubstanceRiskAssessmentService _hazardousSubstanceRiskAssessmentService;

        public AddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory(IHazardousSubstanceRiskAssessmentService hazardousSubstanceRiskAssessmentService)
        {
            _hazardousSubstanceRiskAssessmentService = hazardousSubstanceRiskAssessmentService;
        }

        //public 

        public AddHazardousSubstanceFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var riskAssessment = _hazardousSubstanceRiskAssessmentService.GetRiskAssessment(_riskAssessmentId, _companyId);
            var viewModel = new AddHazardousSubstanceFurtherControlMeasureTaskViewModel()
                                {
                                    CompanyId = _companyId,
                                    RiskAssessmentId = _riskAssessmentId,
                                    ExistingDocuments = new ExistingDocumentsViewModel()
                                                            {
                                                                DocumentTypeId = (int) DocumentTypeEnum.HSRADocumentType
                                                            },
                                    TaskReoccurringTypes = new TaskReoccurringType().ToSelectList(),
                                    DoNotSendTaskCompletedNotification = riskAssessment.RiskAssessor == null ? false : riskAssessment.RiskAssessor.DoNotSendTaskCompletedNotifications,
                                    DoNotSendTaskOverdueNotification = riskAssessment.RiskAssessor == null ? false : riskAssessment.RiskAssessor.DoNotSendTaskOverdueNotifications
                                };

            return viewModel;
        }

        public IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithRiskAssessmentId(long riskAssessmentId)
        {
            _riskAssessmentId = riskAssessmentId;
            return this;
        }

        public IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory WithCanDeleteDocuments(bool canDeleteDocuments)
        {
            return this;
        }
    }
}