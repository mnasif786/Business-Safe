using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers
{
    [TestFixture]
    [Category("Unit")]
    public class ControllerPermissionsTests
    {
        public class ControllerAction
        {
            public string Controller { get; set; }
            public string Action { get; set; }
        }

        [Test]
        public void All_controllers_public_methods_should_have_permissions_attribute()
        {

            var actionsToIgnore = new List<ControllerAction>
                                      {
                                          new ControllerAction {Controller = "InventoryController", Action = "CanDeleteHazardousSubstance"},
                                          new ControllerAction {Controller = "NonEmployeeDefaultsController", Action = "CreateNonEmployee"},
                                          new ControllerAction {Controller = "EmployeesController", Action = "GetEmployees"},
                                          new ControllerAction {Controller = "EmployeesController", Action = "IsEmployeeAUser"},
                                          new ControllerAction {Controller = "EmployeesController", Action = "IsEmployeeAbleToCompleteReviewTask"},
                                          new ControllerAction {Controller = "SitesController", Action = "GetSites"},
                                          new ControllerAction {Controller = "SitesController", Action = "GetSitesAndSiteGroups"},
                                          new ControllerAction {Controller = "RiskAssessmentNonEmployeeController", Action = "GetNonEmployees"},
                                          new ControllerAction {Controller = "RiskAssessorController", Action = "Get"},
                                          new ControllerAction {Controller = "RiskAssessmentEmployeeController", Action = "GetEmployees"}
                                      };
            //Given
            var businessSafeControllers = Assembly
                                            .GetAssembly(typeof(BaseController))
                                            .GetTypes()
                                            .Where(x => x.Name != "HealthStatusController")
                                            .Where(t => typeof(Controller).IsAssignableFrom(t) && (t.Name != "AccountController" && t.Name != "AutoLogInFromPeninsulaController"));


            foreach (var businessSafeController in businessSafeControllers)
            {
                var actionMethods = businessSafeController
                                            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                            .Where(a => a.ReturnType == typeof (ActionResult) || a.ReturnType == typeof(JsonResult) || a.ReturnType == typeof(PartialViewResult))
                                            .ToList();
                foreach (var actionMethod in actionMethods)
                {
                    if (actionsToIgnore.Any(a => a.Controller == businessSafeController.Name && a.Action == actionMethod.Name)) continue;
                    var customAttributes = actionMethod.GetCustomAttributes(false);

                    // Then
                    Assert.That(businessSafeControllers.Count(), Is.GreaterThan(0));
                    Assert.That(customAttributes.Count(x => x.GetType() == typeof(PermissionFilterAttribute)), Is.EqualTo(1), "Controller " + businessSafeController.Name + " method not got permissions " + actionMethod.Name);
                }
            }
        }
    }
}