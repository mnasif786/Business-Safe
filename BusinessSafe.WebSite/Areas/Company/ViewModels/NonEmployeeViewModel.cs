using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class NonEmployeeViewModel
    {
        public NonEmployeeViewModel(PremisesInformationViewModel model)
        {
            if (model != null)
            {
                CompanyId = model.CompanyId;
                RiskAssessmentId = model.RiskAssessmentId;
            }
            FormName = "addNewNonEmployeeForm";
        }

        public NonEmployeeViewModel(CompanyDefaultsViewModel model)
        {
            if (model != null)
            {
                CompanyId = model.CompanyId;
            }
            FormName = "addNewNonEmployeeForm";
        }
        public NonEmployeeViewModel(PersonalRiskAssessments.ViewModels.PremisesInformationViewModel model)
        {
            if (model != null)
            {
                CompanyId = model.CompanyId;
            }
            FormName = "addNewNonEmployeeForm";
        }

        public NonEmployeeViewModel(AccidentRecordDistributionListViewModel model)
        {
            if (model != null)
            {
                CompanyId = model.CompanyId;
            }
            FormName = "addNewNonEmployeeForm";
        }


        public NonEmployeeViewModel(NonEmployeeDto nonEmployeeDto, long companyId)
        {
            NonEmployeeId = nonEmployeeDto.Id;
            Name = nonEmployeeDto.Name;
            Position = nonEmployeeDto.Position;
            Company = nonEmployeeDto.Company;
            RiskAssessmentId = 0;
            CompanyId = companyId;
            FormName = "editNewNonEmployeeForm";
        }

        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }

        public long NonEmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }

        public string FormName { get; set; }

        public string SaveNonEmployeeButtonId
        {
            get { return NonEmployeeId == 0 ? "createNonEmployeeBtn" : "updateNonEmployeeBtn"; }
        }

        public string SaveNonEmployeeButtonText
        {
            get { return NonEmployeeId == 0 ? "Create" : "Update"; }
        }
        
        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return true;
            //This may change - now someone without company default add access can add a non employee.
            //return user.IsInRole(NonEmployeeId == 0 ? Permissions.AddCompanyDefaults.ToString() : Permissions.EditCompanyDefaults.ToString());
        }
    }
}