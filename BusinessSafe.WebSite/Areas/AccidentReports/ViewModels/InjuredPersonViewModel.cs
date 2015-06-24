using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.ViewModels;
using BusinessSafe.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using RestSharp.Extensions;

namespace BusinessSafe.WebSite.Areas.AccidentReports.ViewModels
{
    public class InjuredPersonViewModel : IValidatableObject
    {
        public long AccidentRecordId { get; set; }
        public long CompanyId { get; set; }
        public IEnumerable<AutoCompleteViewModel> Employees { get; set; }
        public string Employee { get; set; }
        public Guid? EmployeeId { get; set; }
        public PersonInvolvedEnum? PersonInvolvedType { get; set; }
        public string PersonInvolvedOtherDescription { get; set; }
        public string PersonInvolvedOtherDescriptionOther { get; set; }
        public int? PersonInvolvedOtherDescriptionId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public IEnumerable<AutoCompleteViewModel> Countries { get; set; }
        public string County { get; set; }
        public int? CountryId { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string ContactNo { get; set; }
        public string Occupation { get; set; }
        public long? SiteId { get; set; }
        public string Site { get; set; }
        public bool NextStepsVisible { get; set; }
        public IEnumerable<AutoCompleteViewModel> OthersInvolved { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!PersonInvolvedType.HasValue)
            {
                results.Add(new ValidationResult("Please select who was involved in the accident"));
            }

            if (PersonInvolvedType == PersonInvolvedEnum.Employee)
            {
                if (!EmployeeId.HasValue)
                {
                    results.Add(new ValidationResult("Employee is required", new[] { "Employee" }));
                }
            }

            if (PersonInvolvedType == PersonInvolvedEnum.Visitor
                || PersonInvolvedType == PersonInvolvedEnum.PersonAtWork
                || PersonInvolvedType == PersonInvolvedEnum.Other)
            {
                if (Forename == null)
                {
                    results.Add(new ValidationResult("Forename is required", new[] { "Forename" }));
                }

                if (Surname == null)
                {
                    results.Add(new ValidationResult("Surname is required", new[] { "Surname" }));
                }
            }

            if (PersonInvolvedType == PersonInvolvedEnum.Other)
            {
                if (!PersonInvolvedOtherDescriptionId.HasValue)
                {
                    results.Add(new ValidationResult("Other Person details are required", new[] { "PersonInvolvedOtherDescription" }));
                }

                // 9 is other, should have made it an enum
                if (PersonInvolvedOtherDescriptionId == 9 && !PersonInvolvedOtherDescriptionOther.HasValue())
                {
                    results.Add(new ValidationResult("Other Person details are required", new[] { "PersonInvolvedOtherDescriptionOther" }));
                }
            }

            return results;
        }
    }
}