using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.VisitRequest;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Application.Implementations.Documents;
using BusinessSafe.Application.Implementations.VisitRequest;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Infrastructure.Security;
using BusinessSafe.WebSite.Areas.AccidentReports.Controllers;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.Areas.ActionPlans.Factories;
using BusinessSafe.WebSite.Areas.ActionPlans.Tasks;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.SqlReports.Factories;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.TaskList.Factories;
using BusinessSafe.WebSite.Areas.Users.Factories;
using BusinessSafe.WebSite.Areas.VisitRequest.Factories;
using BusinessSafe.WebSite.ClientDocumentService;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Controllers.AutoMappers;
using BusinessSafe.WebSite.DocumentLibraryService;
using BusinessSafe.WebSite.DocumentSubTypeService;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Factories.HazardousSubstances;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.PeninsulaOnline;
using BusinessSafe.WebSite.ServiceCompositionGateways;
using BusinessSafe.WebSite.ServiceProxies;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using BusinessSafe.WebSite.StreamingDocumentLibraryService;
using BusinessSafe.WebSite.WebsiteMoqs;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using StructureMap.Configuration.DSL;
using IDocumentTypeService = BusinessSafe.WebSite.DocumentTypeService.IDocumentTypeService;
using IPremisesInformationViewModelFactory = BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories.IPremisesInformationViewModelFactory;
using ISearchRiskAssessmentViewModelFactory = BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories.ISearchRiskAssessmentViewModelFactory;
using ISearchAccidentRecordViewModelFactory = BusinessSafe.WebSite.Areas.AccidentReports.Factories.ISearchAccidentRecordViewModelFactory;

using PremisesInformationViewModelFactory = BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories.PremisesInformationViewModelFactory;
using SearchRiskAssessmentViewModelFactory = BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories.SearchRiskAssessmentViewModelFactory;
using SearchAccidentRecordViewModelFactory = BusinessSafe.WebSite.Areas.AccidentReports.Factories.SearchAccidentRecordViewModelFactory;

namespace BusinessSafe.WebSite
{
    public class WebsiteRegistry : Registry
    {
        public WebsiteRegistry()
        {
            ObjectFactory.Container.Configure(x =>
            {
                x.ForSingletonOf<IBusinessSafeSessionFactory>().Use<BusinessSafeSessionFactory>();
                x.For<IBusinessSafeSessionManager>().HybridHttpOrThreadLocalScoped().Use<BusinessSafeSessionManager>();
                x.For<INonEmployeeSaveTask>().Use<NonEmployeeSaveTask>();

                #region view model factories
                x.For<ISelectResponsibilitiesViewModelFactory>().HttpContextScoped().Use<SelectResponsibilitiesViewModelFactory>();
                x.For<IGenerateResponsibilitiesViewModelFactory>().HttpContextScoped().Use<GenerateResponsibilitiesViewModelFactory>();
                x.For<ITaskListViewModelFactory>().HttpContextScoped().Use<TaskListViewModelFactory>();
                x.For<ISqlReportsViewModelFactory>().HttpContextScoped().Use<SqlReportsViewModelFactory>();              
                x.For<ISiteStructureViewModelFactory>().HttpContextScoped().Use<SiteStructureViewModelFactory>();
                x.For<ISiteDetailsViewModelFactory>().HttpContextScoped().Use<SiteDetailsViewModelFactory>();
                x.For<IEmployeeViewModelFactory>().HttpContextScoped().Use<EmployeeViewModelFactory>();
                x.For<IEmployeeSearchViewModelFactory>().Use<EmployeeSearchViewModelFactory>();
                x.For<ISiteGroupViewModelFactory>().Use<SiteGroupViewModelFactory>();
                x.For<ICompanyDefaultsTaskFactory>().Use<CompanyDefaultsTaskFactory>();
                x.For<IUserRolesViewModelFactory>().Use<UserRolesViewModelFactory>();
                x.For<IUserRolePermissionsViewModelFactory>().Use<UserRolePermissionsViewModelFactory>();
                x.For<IAddUsersViewModelFactory>().Use<AddUsersViewModelFactory>();
                x.For<IUserSearchViewModelFactory>().Use<UserSearchViewModelFactory>();
                x.For<IViewUserViewModelFactory>().Use<ViewUserViewModelFactory>();
                x.For<IAddedDocumentsLibraryViewModelFactory>().Use<AddedDocumentsLibraryViewModelFactory>();
                x.For<ISearchRiskAssessmentViewModelFactory>().Use<SearchRiskAssessmentViewModelFactory>();
                x.For<IDocumentsViewModelFactory>().Use<DocumentsViewModelFactory>();
                x.For<IExistingDocumentsViewModelFactory>().Use<ExistingDocumentsViewModelFactory>();
                x.For<ISearchRiskAssessmentViewModelFactory>().Use<SearchRiskAssessmentViewModelFactory>();
                x.For<ISearchAccidentRecordViewModelFactory>().Use<SearchAccidentRecordViewModelFactory>();
                x.For<ICompleteReviewViewModelFactory>().Use<CompleteReviewViewModelFactory>();
                x.For<IDocumentLibraryViewModelFactory>().Use<DocumentLibraryViewModelFactory>();
                x.For<IInventoryViewModelFactory>().Use<InventoryViewModelFactory>();
                x.For<ISearchRiskAssessmentsViewModelFactory>().Use<SearchRiskAssessmentsViewModelFactory>();
                x.For<IHazardousSubstanceDescriptionViewModelFactory>().Use<HazardousSubstanceDescriptionViewModelFactory>();
                x.For<IHazardousSubstanceViewModelFactory>().Use<HazardousSubstanceViewModelFactory>();
                x.For<IAssessmentViewModelFactory>().Use<AssessmentViewModelFactory>();
                x.For<ICreateRiskAssessmentViewModelFactory>().Use<CreateRiskAssessmentViewModelFactory>();
                x.For<ISearchRiskAssessmentsViewModelFactory>().Use<SearchRiskAssessmentsViewModelFactory>();
                x.For<IControlMeasuresViewModelFactory>().Use<ControlMeasuresViewModelFactory>();
                x.For<IViewEmergencyContactViewModelFactory>().Use<ViewEmergencyContactViewModelFactory>();
                x.For<IGeneralRiskAssessmentReviewsViewModelFactory>().Use<GeneralRiskAssessmentReviewsViewModelFactory>();
                x.For<IRiskAssessmentReviewsViewModelFactory>().Use<RiskAssessmentReviewsViewModelFactory>();
                x.For<IHazardousSubstanceRiskAssessmentReviewsViewModelFactory>().Use<HazardousSubstanceRiskAssessmentReviewsViewModelFactory>();
                x.For<IPersonalRiskAsessmentReviewsViewModelFactory>().Use<PersonalRiskAsessmentReviewsViewModelFactory>();
                x.For<IEmployeeChecklistGeneratorViewModelFactory>().Use<EmployeeChecklistGeneratorViewModelFactory>();
                x.For<IChecklistManagerViewModelFactory>().Use<ChecklistManagerViewModelFactory>();
                x.For<IAccidentRecordDistributionListModelFactory>().Use<AccidentRecordDistributionListModelFactory>();

                x.For<IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>().Use
                    <EditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>();

                x.For<IEditFurtherControlMeasureTaskViewModelFactory>().Use
                    <EditFurtherControlMeasureTaskViewModelFactory>();

                x.For<IRiskAssessmentHazardSummaryViewModelFactory>().Use
                    <RiskAssessmentHazardSummaryViewModelFactory>();

                x.For<IAddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>()
                    .Use<AddHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>();

                x.For < IAddFurtherControlMeasureTaskViewModelFactory>()
                    .Use<AddFurtherControlMeasureTaskViewModelFactory>();

                x.For<IViewFurtherControlMeasureTaskViewModelFactory>().Use
                    <ViewFurtherControlMeasureTaskViewModelFactory>();

                x.For<IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory>().Use
                    <ViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory>();

                x.For<ICompleteFurtherControlMeasureTaskViewModelFactory>().Use
                    <CompleteFurtherControlMeasureTaskViewModelFactory>();

                x.For<ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>().Use
                    <CompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>();

                x.For<IReassignFurtherControlMeasureTaskViewModelFactory>().Use
                    <ReassignFurtherControlMeasureTaskViewModelFactory>();

                x.For<IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>().Use
                    <ReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>();

                x.For<ISqlReportExecutionServiceFacade>().HttpContextScoped().Use<SqlReportExecutionServiceFacade>();

                x.For<ISearchPersonalRiskAssessmentsViewModelFactory>().Use<SearchPersonalRiskAssessmentsViewModelFactory>();

                x.For<IPremisesInformationViewModelFactory>().Use<PremisesInformationViewModelFactory>();
                x.For<IEditGeneralRiskAssessmentSummaryViewModelFactory>().Use<EditGeneralRiskAssessmentSummaryViewModelFactory>();
                x.For<IEditPersonalRiskAssessmentSummaryViewModelFactory>().Use<EditPersonalRiskAssessmentSummaryViewModelFactory>();
                x.For<IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory>().Use<EditHazardousSubstanceRiskAssessmentSummaryViewModelFactory>();
                x.For<IEmployeeChecklistSummaryViewModelFactory>().Use<EmployeeChecklistSummaryViewModelFactory>();

                x.For<IFireRiskAssessmentReviewsViewModelFactory>().Use<FireRiskAssessmentReviewsViewModelFactory>();
                

                x.For<Areas.FireRiskAssessments.Factories.ISearchRiskAssessmentViewModelFactory>().Use<Areas.FireRiskAssessments.Factories.SearchRiskAssessmentViewModelFactory>();
                x.For<IEditFireRiskAssessmentSummaryViewModelFactory>().Use<EditFireRiskAssessmentSummaryViewModelFactory>();

                x.For<Areas.FireRiskAssessments.Factories.IPremisesInformationViewModelFactory>().Use
                    <Areas.FireRiskAssessments.Factories.PremisesInformationViewModelFactory>();

                x.For<IFireRiskAssessmentChecklistViewModelFactory>().Use<FireRiskAssessmentChecklistViewModelFactory>();
                x.For<ISignificantFindingViewModelFactory>().Use<SignificantFindingViewModelFactory>();

                x.For<IAddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory>().Use<AddFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory>();
                x.For<IEditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory>().Use<EditFireRiskAssessmentFurtherControlMeasureTaskViewModelFactory>();
                x.For<IAddEditRiskAssessorViewModelFactory>().Use<AddEditRiskAssessorViewModelFactory>();
                x.For<IRiskAssessorsDefaultAddEditViewModelFactory>().Use<RiskAssessorsDefaultAddEditViewModelFactory>();
                x.For<ISearchResponsibilityViewModelFactory>().Use<SearchResponsibilityViewModelFactory>();
                x.For<IResponsibilityViewModelFactory>().Use<ResponsibilityViewModelFactory>();

                x.For<ICreateUpdateResponsibilityTaskViewModelFactory>().Use
                    <CreateUpdateResponsibilityTaskViewModelFactory>();

                x.For<ICompleteResponsibilityTaskViewModelFactory>().Use
                    <CompleteResponsibilityTaskViewModelFactory>();


                x.For<ICompleteActionTaskViewModelFactory>().Use<CompleteActionTaskViewModelFactory>();

                x.For<IViewResponsibilityTaskViewModelFactory>().Use<ViewResponsibilityTaskViewModelFactory>();
                x.For<IViewActionTaskViewModelFactory>().Use<ViewActionTaskViewModelFactory>();                
                x.For<IReassignResponsibilityTaskViewModelFactory>().Use<ReassignResponsibilityTaskViewModelFactory>();
                x.For<IGenerateResponsibilityTasksViewModelFactory>().Use<GenerateResponsibilityTasksViewModelFactory>();
                x.For<ISelectedEmployeeViewModelFactory>().Use<SelectedEmployeeViewModelFactory>();
                x.For<IAccidentDetailsViewModelFactory>().Use<AccidentDetailsViewModelFactory>();
                x.For<IInjuredPersonViewModelFactory>().Use<InjuredPersonViewModelFactory>();
                x.For<IAccidentSummaryViewModelFactory>().Use<AccidentSummmaryViewModelFactory>();
                x.For<IAccidentRecordOverviewViewModelFactory>().Use<AccidentRecordOverviewViewModelFactory>();
                x.For<IInjuryDetailsViewModelFactory>().Use<InjuryDetailsViewModelFactory>();
                x.For<INextStepsViewModelFactory>().Use<NextStepsViewModelFactory>();
                x.For<ISearchActionPlanViewModelFactory>().Use<SearchActionPlanViewModelFactory>();
                x.For<ISearchActionViewModelFactory>().Use<SearchActionViewModelFactory>();
                x.For<IAssignActionPlanTaskCommand>().Use<AssignActionPlanTaskCommand>();
                x.For<IReassignActionTaskViewModelFactory>().Use<ReassignActionTaskViewModelFactory>();
                x.For<IVisitRequestViewModelFactory>().Use<VisitRequestViewModelFactory>();
                
                #endregion

                x.For<IDocumentRequestMapper>().Use<DocumentRequestMapper>();
                x.For<INewRegistrationRequestService>().Use(new NewRegistrationRequestServiceClient());
                x.For<IDocumentLibraryService>().Use(new DocumentLibraryServiceClient("DocumentLibraryService"));
                x.For<IStreamingDocumentLibraryService>().Use(new StreamingDocumentLibraryServiceClient("StreamingDocumentLibraryService"));
                x.For<IDocumentTypeService>().Use(new DocumentTypeServiceFactory().Create());
                x.For<IClientDocumentService>().Use(new ClientDocumentServiceFactory().Create);
                x.For<IStreamingClientDocumentService>().Use(new StreamingClientDocumentServiceFactory().Create());
                x.For<IDocumentSubTypeService>().Use(new DocumentSubTypeServiceFactory().Create());
                x.For<IAddedDocumentsService>().Use<AddedDocumentsService>();
                x.For<IDocHandlerDocumentTypeService>().Use<DocHandlerDocumentTypeService>();
                x.For<IImpersonator>().Use<Impersonator>();
                x.For<ICacheHelper>().Use<CacheHelper>();
                x.For<IVirtualPathUtilityWrapper>().Use<VirtualPathUtilityWrapper>();
                x.For<IClientService>().Use<ClientService>();
                x.For<ISiteUpdateCompositionGateway>().Use<SiteUpdateCompositionGateway>();
                x.For<ISqlReportExecutionServiceFacade>().Use(new SqlReportsFactory().Create());
                x.For<IDocumentLibraryUploader>().Use<DocumentLibraryUploader>();
                x.For<ISaveFileStreamHelper>().Use<SaveFileStreamHelper>();
                x.For<IDocumentParameterHelper>().Use<DocumentParameterHelper>();
                x.For<IReviewAuditDocumentHelper>().Use<ReviewAuditDocumentHelper>();
                x.For<ICustomPrincipalFactory>().Use<CustomPrincipalFactory>();
                x.For<ILogOut>().Use<BusinessSafeLogOut>();
                x.For<IBusinessSafeSessionManagerFactory>().Use<BusinessSafeSessionManagerFactory>();
                x.For<IUserRegistrationService>().Use<PeninsulaOnlineRegistrationService>();

                x.For<IActionTaskService>().Use<ActionTaskService>();
                x.For<IVisitRequestService>().Use<VisitRequestService>();
                

                x.AddRegistry<ApplicationRegistry>();
            });
        }
    }
}