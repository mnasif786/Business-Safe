using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Application.Contracts.RiskAssessments;

namespace BusinessSafe.WebSite.Factories
{
    public class AddFurtherControlMeasureTaskViewModelFactory : IAddFurtherControlMeasureTaskViewModelFactory
    {
        private readonly IRiskAssessmentHazardSummaryViewModelFactory _riskAssessmentHazardSummaryViewModelFactory;
        private readonly IRiskAssessmentHazardService _riskAssessmentHazardService;
        private long _companyId;
        private long _riskAssessmentHazardId;
        private FurtherControlMeasureTaskCategoryEnum _furtherControlMeasureTaskCategory;

        public AddFurtherControlMeasureTaskViewModelFactory(
            IRiskAssessmentHazardSummaryViewModelFactory riskAssessmentHazardSummaryViewModelFactory,
            IRiskAssessmentHazardService riskAssessmentHazardService)
        {
            _riskAssessmentHazardSummaryViewModelFactory = riskAssessmentHazardSummaryViewModelFactory;
            _riskAssessmentHazardService = riskAssessmentHazardService;
        }

        public AddRiskAssessmentFurtherControlMeasureTaskViewModel GetViewModel()
        {
            var riskAssessmentHazard = _riskAssessmentHazardService.GetByIdAndCompanyId(_riskAssessmentHazardId, _companyId);

            var viewModel = new AddRiskAssessmentFurtherControlMeasureTaskViewModel()
                                    {
                                        HazardSummary = _riskAssessmentHazardSummaryViewModelFactory
                                            .WithCompanyId(_companyId)
                                            .WithRiskAssessmentHazardId(_riskAssessmentHazardId)
                                            .GetViewModel(),
                                        CompanyId = _companyId,
                                        RiskAssessmentHazardId = _riskAssessmentHazardId,
                                        ExistingDocuments = new ExistingDocumentsViewModel()
                                        {
                                            DocumentTypeId = (int)DocumentTypeEnum.GRADocumentType
                                        },
                                        TaskReoccurringTypes = new TaskReoccurringType().ToSelectList(),
                                        FurtherControlMeasureTaskCategory = _furtherControlMeasureTaskCategory,
                                        DoNotSendTaskCompletedNotification = riskAssessmentHazard.RiskAssessment.RiskAssessor == null ? false : riskAssessmentHazard.RiskAssessment.RiskAssessor.DoNotSendTaskCompletedNotifications,
                                        DoNotSendTaskOverdueNotification = riskAssessmentHazard.RiskAssessment.RiskAssessor == null ? false : riskAssessmentHazard.RiskAssessment.RiskAssessor.DoNotSendTaskOverdueNotifications
                                    };


            
            return viewModel;
        }

        public IAddFurtherControlMeasureTaskViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAddFurtherControlMeasureTaskViewModelFactory WithRiskAssessmentHazardId(long riskAssessmentHazardId)
        {
            _riskAssessmentHazardId = riskAssessmentHazardId;
            return this;
        }

        public IAddFurtherControlMeasureTaskViewModelFactory WithFurtherControlMeasureTaskCategory(FurtherControlMeasureTaskCategoryEnum furtherControlMeasureTaskCategory)
        {
            _furtherControlMeasureTaskCategory = furtherControlMeasureTaskCategory;
            return this;
        }
    }
}