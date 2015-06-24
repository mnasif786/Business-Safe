using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.DocumentSubTypeService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;
using DocumentTypeDto = BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto;
using RoleDto = BusinessSafe.Application.DataTransferObjects.RoleDto;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.WebSite.ViewModels
{
    public class AutoCompleteViewModel
    {
        public string label { get; set; }
        public string value { get; set; }
        public string filterName { get; set; }
        public string filterValue { get; set; }

        public AutoCompleteViewModel(string label, string value)
        {
            this.value = value;
            this.label = label;
        }


        public static AutoCompleteViewModel ForRiskAssessor(RiskAssessorDto riskAssessorDto)
        {
            return new AutoCompleteViewModel(   string.Format( "{0} ( {1} )", riskAssessorDto.Employee.FullName, riskAssessorDto.Employee.JobTitle),
                                                riskAssessorDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForNonEmployee(NonEmployeeDto nonEmployeeDto)
        {
            return new AutoCompleteViewModel(string.Format("{0}, {1}, {2}", nonEmployeeDto.Name, nonEmployeeDto.Company, nonEmployeeDto.Position), nonEmployeeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForEmployee(EmployeeDto employeeDto)
        {
            return new AutoCompleteViewModel(string.Format("{0} ( {1} )", employeeDto.FullName, employeeDto.JobTitle), employeeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForEmployee(EmployeeName employeeDto)
        {
            return new AutoCompleteViewModel(string.Format("{0} ( {1} )", employeeDto.FullName, employeeDto.JobTitle), employeeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForEmployeeWithSite(EmployeeDto employeeDto)
        {
            return new AutoCompleteViewModel(string.Format("{0}, {1} ( {2} )", employeeDto.Surname, employeeDto.Forename, employeeDto.SiteName), employeeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForEmployeeNoJobTitle(EmployeeDto employeeDto)
        {
            return new AutoCompleteViewModel(employeeDto.FullName, employeeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForEmployeeNoJobTitle(EmployeeName employeeDto)
        {
            return new AutoCompleteViewModel(employeeDto.FullName, employeeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForLookUp(LookupDto lookupDTo)
        {
            return new AutoCompleteViewModel(lookupDTo.Name, lookupDTo.Id.ToString());
        }

        public static AutoCompleteViewModel ForHazardousSubstance(HazardousSubstanceDto hazardousSubstanceDto)
        {
            return new AutoCompleteViewModel(hazardousSubstanceDto.Name, hazardousSubstanceDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForSupplier(SupplierDto supplierDto)
        {
            return new AutoCompleteViewModel(supplierDto.Name, supplierDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForSite(SiteDto sitesDto)
        {
            return new AutoCompleteViewModel(sitesDto.Name, sitesDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForSiteGroup(SiteGroupDto siteGroupDto)
        {
            return new AutoCompleteViewModel(siteGroupDto.Name, siteGroupDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForTaskCategory(TaskCategoryDto taskCategory)
        {
            return new AutoCompleteViewModel(taskCategory.Category, taskCategory.Id.ToString());
        }

        public static AutoCompleteViewModel ForResponsibilityCategory(ResponsibilityCategoryDto responsibilityCategory)
        {
            return new AutoCompleteViewModel(responsibilityCategory.Category, responsibilityCategory.Id.ToString());
        }

        public static AutoCompleteViewModel ForResponsibilityReason(ResponsibilityReasonDto responsibilityReason)
        {
            return new AutoCompleteViewModel(responsibilityReason.Reason, responsibilityReason.Id.ToString());
        }

        public static AutoCompleteViewModel ForRole(RoleDto roleDto)
        {
            return new AutoCompleteViewModel(roleDto.Description, roleDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForDocumentType(DocumentTypeDto documentTypeDto)
        {
            return new AutoCompleteViewModel(documentTypeDto.Title, documentTypeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForDocumentSubType(DocumentSubTypeDto documentTypeDto)
        {
            return new AutoCompleteViewModel(documentTypeDto.Title, documentTypeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForDocumentType(Application.DataTransferObjects.DocumentTypeDto documentTypeDto)
        {
            return new AutoCompleteViewModel(documentTypeDto.Name, documentTypeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForDocumentType(DocumentSubTypeDto documentTypeDto)
        {
            return new AutoCompleteViewModel(documentTypeDto.Title, documentTypeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForTaskReoccurringType(TaskReoccurringType taskReoccurringType)
        {
            return new AutoCompleteViewModel(EnumHelper.GetEnumDescription(taskReoccurringType), ((int)taskReoccurringType).ToString(CultureInfo.InvariantCulture));
        }

        public static AutoCompleteViewModel ForEmployeeWithEmail(EmployeeDto employeeDto)
        {
            var emailLabel = employeeDto.MainContactDetails == null || string.IsNullOrEmpty(employeeDto.MainContactDetails.Email) ? string.Empty : string.Format(" ({0})", employeeDto.MainContactDetails.Email);
            return new AutoCompleteViewModel(string.Format("{0}{1}", employeeDto.FullName, emailLabel), employeeDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForAccidentType(AccidentTypeDto accidentTypeDto)
        {
            return new AutoCompleteViewModel(accidentTypeDto.Description, accidentTypeDto.Id.ToString());
        }

        public static IEnumerable<AutoCompleteViewModel> ForAccidentTypes(IEnumerable<AccidentTypeDto>  accidentTypeDtos)
        {
            const string lastItemDescription = "Another kind of accident";

            List<AccidentTypeDto> accidentTypes = accidentTypeDtos.Where(y => y.Description != lastItemDescription).OrderBy(x => x.Description).ToList();
            AccidentTypeDto anyAccidentType = accidentTypeDtos.Where(y => y.Description == lastItemDescription).FirstOrDefault();
            if (anyAccidentType != null)
            {
                accidentTypes.Add(anyAccidentType);
            }

            return accidentTypes.Select(ForAccidentType).AddDefaultOption(String.Empty);
        }

        public static AutoCompleteViewModel ForAccidentCauses(CauseOfAccidentDto causeOfAccidentDto)
        {
            return new AutoCompleteViewModel(causeOfAccidentDto.Description, causeOfAccidentDto.Id.ToString());
        }

        public static AutoCompleteViewModel ForSqlReportType(KeyValuePair<SqlReportHelper.ReportType, string> sqlReportType)
        {
            long sqlReportTypeId = (long)sqlReportType.Key;
            return new AutoCompleteViewModel( sqlReportType.Value, sqlReportTypeId.ToString() );
        }


        public static AutoCompleteViewModel ForCountry(CountryDto countryDto)
        {
            return new AutoCompleteViewModel(countryDto.Name, countryDto.Id.ToString());
        }

        public static IEnumerable<AutoCompleteViewModel> ForCountries(IEnumerable<CountryDto> countryDtos)
        {
            var countries = countryDtos.OrderBy(x => x.Name).ToList();

            if (countries.Any(x => x.Name == "Ireland"))
            {
                var ireland = countries.First(x => x.Name == "Ireland"); 
                countries.Remove(ireland);
                countries.Insert(0, ireland);
            }

            if (countries.Any(x => x.Name == "United Kingdom"))
            {
                var uk = countries.First(x => x.Name == "United Kingdom");
                countries.Remove(uk);
                countries.Insert(0, uk);
            }

            return countries.Select(ForCountry).AddDefaultOption();
        }

        public static AutoCompleteViewModel ForOthersInvolved(OthersInvolvedAccidentDetailsDto othersInvolvedDto)
        {
            return new AutoCompleteViewModel(othersInvolvedDto.Name, othersInvolvedDto.Id.ToString());
        }   
    }
}