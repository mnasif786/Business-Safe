using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.EmployeeViewModelTests
{
    [TestFixture]
    public class EmployeeViewModelTests
    {
        [Test]
        public void given_employee_model_has_no_site_or_role_values_then_no_validation_errors()
        {
            var employeeViewModel = new EmployeeViewModel()
            {
                UserRoleId = null,
                UserSiteGroupId = null,
                UserSiteGroups = null,
                UserPermissionsApplyToAllSites = false
            };

            var validationContext = new ValidationContext(employeeViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(employeeViewModel, validationContext, validationResults);
        }


        [Test]
        public void given_employee_model_has_no_user_role_but_site_group_then_validation_error()
        {
            var employeeViewModel = new EmployeeViewModel()
            {
                UserRoleId = null,
                UserSiteGroupId = 1,
                UserSiteGroups = null,
                UserPermissionsApplyToAllSites = false,
                Forename = "name",
                Surname = "name",
                Sex = "M",
                SiteId = 1
            };

            var validationContext = new ValidationContext(employeeViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(employeeViewModel, validationContext, validationResults);

            Assert.That(validationResults.Count > 0);
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The User Role must be selected for the user"));
        }

        //[Test]
        //public void given_employee_model_has_no_Site_Id_then_validation_error()
        //{
        //    var employeeViewModel = new EmployeeViewModel()
        //    {
        //        UserRoleId = null,
        //        UserSiteGroupId = 1,
        //        UserSiteGroups = null,
        //        UserPermissionsApplyToAllSites = false,
        //        Forename = "name",
        //        Surname = "name",
        //        Sex = "M"
        //    };

        //    var validationContext = new ValidationContext(employeeViewModel, null, null);
        //    var validationResults = new List<ValidationResult>();
        //    Validator.TryValidateObject(employeeViewModel, validationContext, validationResults);

        //    Assert.That(validationResults.Count > 0);
        //    Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Site is required"));
        //}
    }
}
