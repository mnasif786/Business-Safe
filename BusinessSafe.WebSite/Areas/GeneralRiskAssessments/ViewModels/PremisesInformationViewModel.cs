using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels
{
    public class PremisesInformationViewModel
    {
        public DateTime? PreviousAssessment { get; set; }
        [Required(ErrorMessage = "Location/Area/Department is required")]
        public string LocationAreaDepartment { get; set; }
        [Required(ErrorMessage = "Task/Process Description is required")]
        public string TaskProcessDescription { get; set; }
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<Tuple<long, string>> NonEmployees { get; set; }
        public IEnumerable<Tuple<Guid, string>> Employees { get; set; }
        

        public PremisesInformationViewModel()
        {
            Employees = new List<Tuple<Guid, string>>();
            NonEmployees = new List<Tuple<long, string>>();
        }

        public static PremisesInformationViewModel ForValidationErrors(PremisesInformationViewModel request, IEnumerable<RiskAssessmentNonEmployeeDto> nonEmployees)
        {
            return new PremisesInformationViewModel()
                       {
                           RiskAssessmentId = request.RiskAssessmentId,
                           CompanyId = request.CompanyId,
                           Title = request.Title,
                           Reference = request.Reference,
                           CreatedOn = request.CreatedOn,
                           LocationAreaDepartment = request.LocationAreaDepartment,
                           PreviousAssessment = request.PreviousAssessment,
                           TaskProcessDescription = request.TaskProcessDescription,
                           NonEmployees = nonEmployees != null ? GetNonEmployees(nonEmployees) : new List<Tuple<long, string>>()
                       };
        }


        public static PremisesInformationViewModel CreateFrom(GeneralRiskAssessmentDto riskAssessment)
        {
            return new PremisesInformationViewModel
                 {
                     CompanyId = riskAssessment.CompanyId,
                     RiskAssessmentId = riskAssessment.Id,
                     LocationAreaDepartment = riskAssessment.Location,
                     TaskProcessDescription = riskAssessment.TaskProcessDescription,
                     Title = riskAssessment.Title,
                     Reference = riskAssessment.Reference,
                     CreatedOn = riskAssessment.CreatedOn.GetValueOrDefault(),
                     NonEmployees = riskAssessment.NonEmployees != null ? GetNonEmployees(riskAssessment.NonEmployees) : new List<Tuple<long, string>>(),
                     Employees = riskAssessment.Employees != null ? GetEmployees(riskAssessment.Employees) : new List<Tuple<Guid, string>>()
                 };
        }


        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(RiskAssessmentId == 0 ? Permissions.AddGeneralandHazardousSubstancesRiskAssessments.ToString() : Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());
        }

        private static IEnumerable<Tuple<long, string>> GetNonEmployees(IEnumerable<RiskAssessmentNonEmployeeDto> nonEmployees)
        {
            return nonEmployees.Select(riskAssessmentNonEmployee => riskAssessmentNonEmployee.NonEmployee).Select(nonEmployee => new Tuple<long, string>(nonEmployee.Id, nonEmployee.FormattedName)).ToList();
        }

        private static IEnumerable<Tuple<Guid, string>> GetEmployees(IEnumerable<RiskAssessmentEmployeeDto> employees)
        {
            return employees.Select(riskAssessmentEmployee => riskAssessmentEmployee.Employee).Select(employee => new Tuple<Guid, string>(employee.Id, employee.FullName)).ToList();
        }
    }
}