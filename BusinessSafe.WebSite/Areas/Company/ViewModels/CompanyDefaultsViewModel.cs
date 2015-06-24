using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.ViewModels
{
    public class CompanyDefaultsViewModel
    {
        public DefaultAddEdit OrganisationalUnitClassificationsViewModel { get; set; }
        public DefaultAddEdit SpecialistSuppliersViewModel { get; set; }
        public DefaultAddEdit EmployeeGroupsViewModel { get; set; }
        public DefaultAddEdit PeopleAtRiskViewModel { get; set; }
        public DefaultAddEdit HazardsViewModel { get; set; }
        public DefaultAddEdit NonEmployeesViewModel { get; set; }
        public long CompanyId { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }

        public CompanyDefaultsViewModel(IList<Defaults> organisationalUnitclassifications,
                                        IEnumerable<SupplierDto> suppliers,
                                        IList<Defaults> employeeGroups,
                                        IEnumerable<NonEmployeeDto> nonEmployees,
                                        IEnumerable<CompanyDefaultDto> hazards,
                                        IEnumerable<CompanyDefaultDto> peopleAtRisk)
        {
            OrganisationalUnitClassificationsViewModel = new OrganisationUnitClassificationDefaultAddEdit(organisationalUnitclassifications);

            EmployeeGroupsViewModel = new EmployeeGroupsDefaultAddEdit(employeeGroups);

            var hazardsDefaults = hazards.Select(dto => new Defaults() { Id = dto.Id.ToString(), Name = dto.Name, IsSystemDefault = dto.IsSystemDefault }).ToList();
            HazardsViewModel = new HazardsDefaultAddEdit(hazardsDefaults);

            var peopleDefaults = peopleAtRisk.Select(dto => new Defaults() { Id = dto.Id.ToString(), Name = dto.Name, IsSystemDefault = dto.IsSystemDefault }).ToList();
            PeopleAtRiskViewModel = new PeopleAtRiskDefaultAddEdit(peopleDefaults);

            var nonEmployeeDefaults = nonEmployees.Select(dto => new Defaults() { Id = dto.Id.ToString(), Name = string.Format("{0}, {1}, {2}", dto.Name, dto.Company, dto.Position) }).ToList();
            NonEmployeesViewModel = new NonEmployeesDefaultAddEdit(nonEmployeeDefaults);

            var suppliersDefaults = suppliers.Select(dto => new Defaults() { Id = dto.Id.ToString(), Name = dto.Name, IsSystemDefault = dto.IsSystemDefault }).ToList();
            SpecialistSuppliersViewModel = new SpecialistSuppliersDefaultAddEdit(suppliersDefaults);
        }
    }
}