using System;
using System.Configuration;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Contracts.MultiHazardRiskAssessment;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Contracts.Users;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.AccidentRecords;
using BusinessSafe.Application.Implementations.ActionPlan;
using BusinessSafe.Application.Implementations.Checklists;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Implementations.Documents;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Implementations.HazardousSubstanceInventory;
using BusinessSafe.Application.Implementations.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.Implementations.MultiHazardRiskAssessment;
using BusinessSafe.Application.Implementations.PersonalRiskAssessments;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Application.Implementations.TaskList;
using BusinessSafe.Application.Implementations.Users;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.InfrastructureContracts.Email;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Infrastructure.Logging;
using BusinessSafe.Infrastructure.Security;
using RestSharp;
using StructureMap.Configuration.DSL;

namespace BusinessSafe.Application.Common
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Log4NetHelper.Logger.Info("ApplicationRegistry Begin");

            var baseUrl = ConfigurationManager.AppSettings["ClientDetailsServices.Rest.BaseUrl"];
            Log4NetHelper.Logger.Debug(string.Format("ClientDetailsService baseurl is {0}", baseUrl));

            var connectionString = GetConnectionString("BusinessSafe");

            Configure(x =>
                          {
                              For<IRolesService>().Use<RolesService>();
                              For<IEmployeeService>().Use<EmployeeService>();
                              For<IEmployeeEmergencyContactDetailService>().Use<EmployeeEmergencyContactDetailService>();
                              For<ILookupService>().Use<LookupService>();
                              For<ISiteGroupService>().Use<SiteGroupService>();
                              For<ITaskService>().Use<TaskService>();
                              For<ISiteService>().Use<SiteService>();
                              For<INonEmployeeService>().Use<NonEmployeeService>();
                              For<ICompanyDetailsService>().Use<CompanyDetailsService>();
                              For<IBusinessSafeCompanyDetailService>().Use<BusinessSafeCompanyDetailService>();
                              For<IGeneralRiskAssessmentService>().Use<GeneralRiskAssessmentService>();
                              For<IPersonalRiskAssessmentService>().Use<PersonalRiskAssessmentService>();
                              For<IDocumentService>().Use<DocumentService>();
                              For<IGeneralRiskAssessmentAttachmentService>().Use<GeneralRiskAssessmentAttachmentService>();
                              For<IRiskAssessmentLookupService>().Use<RiskAssessmentLookupService>();
                              For<IRiskAssessmentHazardService>().Use<RiskAssessmentHazardService>();
                              For<IRiskAssessmentReviewService>().Use<RiskAssessmentReviewService>();
                              For<IHazardousSubstanceRiskAssessmentService>().Use<HazardousSubstanceRiskAssessmentService>();
                              For<IHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService>().Use<HazardousSubstanceRiskAssessmentFurtherControlMeasureTaskService>();
                              For<IFurtherControlMeasureTaskService>().Use<FurtherControlMeasureTaskService>();
                              For<IDoesNonEmployeeAlreadyExistGuard>().Use<DoesNonEmployeeAlreadyExistGuard>();
                              For<IDoesCompanyDefaultAlreadyExistGuard>().Use<DoesCompanyDefaultAlreadyExistGuard>();
                              For<ITemplateEngine>().Use<TemplateEngine>();
                              For<IRestClientAPI>().Use(new RestClientAPI(new RestClient(baseUrl)));
                              For<ICompanyDefaultService>().Use<CompanyDefaultService>();
                              For<IUserService>().Use<UserService>();
                              For<IAuthenticationTokenService>().Use<AuthenticationTokenService>();
                              For<IApplicationTokenService>().Use<ApplicationTokenService>();
                              For<IRegisterEmployeeAsUserParametersMapper>().Use<RegisterEmployeeAsUserParametersMapper>();
                              For<IEmployeeParametersMapper>().Use<EmployeeParametersMapper>();
                              For<IEmployeeContactDetailsParametersMapper>().Use<EmployeeContactDetailsParametersMapper>();
                              For<IEmergencyContactDetailsParametersMapper>().Use<EmergencyContactDetailsParametersMapper>();
                              For<IRegisterEmployeeAsUserParametersMapper>().Use
                                  <RegisterEmployeeAsUserParametersMapper>();
                              For<IPeninsulaLog>().Use<SqlPeninsulaLog>()
                                  .WithProperty("connectionString")
                                  .EqualTo(connectionString);
                              For<IUserLoginProvider>().Use<UserLoginProvider>();
                              For<IDocumentService>().Use<DocumentService>();
                              For<IDocumentTypeService>().Use<DocumentTypeService>();
                              For<IHazardousSubstancesService>().Use<HazardousSubstancesService>();
                              For<ISuppliersService>().Use<SuppliersService>();
                              For<IRiskPhraseService>().Use<RiskPhraseService>();
                              For<ISafetyPhraseService>().Use<SafetyPhraseService>();
                              For<IPictogramService>().Use<PictogramService>();
                              For<IControlSystemService>().Use<ControlSystemService>();
                              For<IEmployeeEmergencyContactDetailService>().Use<EmployeeEmergencyContactDetailService>();
                              For<IRiskAssessmentService>().Use<RiskAssessmentService>();
                              For<IEmailTemplateService>().Use<EmailTemplateService>();
                              For<IRiskAssessmentAttachmentService>().Use<RiskAssessmentAttachmentService>();
                              For<IMultiHazardRiskAssessmentService>().Use<MultiHazardRiskAssessmentService>();
                              For<IMultiHazardRiskAssessmentAttachmentService>().Use<MultiHazardRiskAssessmentAttachmentService>();
                              For<IEmployeeChecklistService>().Use<EmployeeChecklistService>();
                              For<IChecklistService>().Use<ChecklistService>();
                              For<IEmployeeChecklistEmailService>().Use<EmployeeChecklistEmailService>();
                              For<IFireRiskAssessmentService>().Use<FireRiskAssessmentService>();
                              For<IFireRiskAssessmentAttachmentService>().Use<FireRiskAssessmentAttachmentService>();
                              For<IFireRiskAssessmentChecklistService>().Use<FireRiskAssessmentChecklistService>();
                              For<ISignificantFindingService>().Use<SignificantFindingService>();
                              For<IFireRiskAssessmentFurtherControlMeasureTaskService>().Use<FireRiskAssessmentFurtherControlMeasureTaskService>();
                              For<IRiskAssessorService>().Use<RiskAssessorService>();
                              For<IResponsibilitiesService>().Use<ResponsibilitiesService>();
                              For<IResponsibilityTaskService>().Use<ResponsibilityTaskService>();
                              For<IActionTaskService>().Use<ActionTaskService>();
                              For<ITaskListService>().Use<TaskListService>();
                              For<IStatutoryResponsibilityTemplateService>().Use<StatutoryResponsibilityTemplateService>();
                              For<IStatutoryResponsibilityTaskTemplateService>().Use<StatutoryResponsibilityTaskTemplateService>();
                              For<IAccidentRecordService>().Use<AccidentRecordService>();
                              For<IAccidentTypeService>().Use<AccidentTypeService>();
                              For<ICauseOfAccidentService>().Use<CauseOfAccidentService>();
                              For<IInjuryService>().Use<InjuryService>();
                              For<IInjuryDetailsService>().Use<InjuryDetailsService>();
                              For<IBodyPartService>().Use<BodyPartService>();
                              For<IEmail>().Use<FakeEmail>();
                              For<IActionPlanService>().Use<ActionPlanService>();
                              For<IActionService>().Use<ActionService>();
                              For<IEmployeeNotificationsService>().Use<EmployeeNotificationsService>();

                              x.ImportRegistry(typeof(DataRegistry));
                          });

            Log4NetHelper.Logger.Info("ApplicationRegistry Complete");
        }

        private static string GetConnectionString(string databaseName)
        {
            if (ConfigurationManager.ConnectionStrings[databaseName] == null)
            {
                throw new Exception(string.Format("Connection string not found for {0}", databaseName));
            }

            return ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
        }
    }
}