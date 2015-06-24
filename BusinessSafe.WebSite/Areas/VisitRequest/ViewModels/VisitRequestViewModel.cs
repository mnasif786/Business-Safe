using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.ViewModels;
using Iesi.Collections;
using NHibernate.Linq;

namespace BusinessSafe.WebSite.Areas.VisitRequest.ViewModels
{
    public class VisitRequestViewModel
    {
        [Required(ErrorMessage = "Name of person to visit is required")]
        public string PersonNameToVisit { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        public string ContactNumber  { get; set; }
        [Required(ErrorMessage = "Contact email address is required")]
        public string EmailAddress  { get; set; }
        [Required(ErrorMessage = "Site is required")]
        public long? SiteId { get; set; }
        [Required(ErrorMessage = "Visit From date is required")]
        public string VisitFrom { get; set; }
        [Required(ErrorMessage = "Visit To date is required")]
        public string VisitTo { get; set; }
        public string VisitTimePreference { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        [Required(ErrorMessage = "Comments are required")]
        public string Comments { get; set; }
        public string CompanyName { get; set; }
        public string CAN { get; set; }
        public string SiteName { get; set; }
        public string Postcode { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (!SiteId.HasValue)
            {
                validationResults.Add(new ValidationResult("Site is rquired"));
            }

            if (Regex.Match(EmailAddress, @"^[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$").Success == false)
            {
                validationResults.Add(new ValidationResult("Contact email address must be provided in valid format", new[] { "EmailAddress" }));
            }

            DateTime visitFrom;
            bool isValidVisitFrom = DateTime.TryParse(VisitFrom, out visitFrom);
            if (!isValidVisitFrom)
            {
                validationResults.Add(new ValidationResult("Visit From must be provided in valid format", new[] { "VisitFrom" }));
            }


            DateTime visitTo;
            bool isvalidVisitTo = DateTime.TryParse(VisitTo, out visitTo);
            if (!isvalidVisitTo)
            {
                validationResults.Add(new ValidationResult("Visit To must be provided in valid format", new[] { "VisitTo" }));
            }

            if (isValidVisitFrom && isvalidVisitTo && visitFrom.Date < DateTime.Now.Date)
            {
                validationResults.Add(new ValidationResult("Visit From date cannot be in the past", new[] { "VisitFrom" }));
            }
            else if (isValidVisitFrom && isvalidVisitTo && visitTo.Date < DateTime.Now.Date)
            {
                validationResults.Add(new ValidationResult("Visit To date cannot be in the past", new[] { "VisitTo" }));
            }
            else if (isValidVisitFrom && isvalidVisitTo && visitFrom.Date > visitTo.Date )
            {
                validationResults.Add(new ValidationResult("Visit To date cannot be earlier than From date", new[] { "VisitTo" }));
            }

            return validationResults;
        }
        
        public virtual void  Validate(ValidationContext validationContext, ModelStateDictionary modelState)
        {

            var validationResults = Validate(new ValidationContext(this, null, null));

            var errorList = validationResults as IList<ValidationResult> ?? validationResults.ToList();
            foreach (var error in errorList)
                foreach (var memberName in error.MemberNames)
                    modelState.AddModelError(memberName, error.ErrorMessage);
        }

        
    }

        //Company name 
        //contact name contact email
        //contact phone
        //VistiDate To
        //VistiDate From
        //VisitTime (am/pm?)
        //Comments
        //Sitename
        //site postcode

        //UserId = CurrentUser.UserId,
}