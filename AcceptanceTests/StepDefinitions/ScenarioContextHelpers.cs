using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment;
using TechTalk.SpecFlow;
using TableRow = WatiN.Core.TableRow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    public static class ScenarioContextHelpers
    {
        public static long GetResponsibilityTaskId()
        {
            var initiateSessionRequest = (long) ScenarioContext.Current["responsibilitytaskid"];
            return initiateSessionRequest;
        }

        public static void SetResponsibilityTaskId(long  responsibilityTaskId)
        {
            if (ScenarioContext.Current.ContainsKey("responsibilitytaskid"))
                ScenarioContext.Current["responsibilitytaskid"] = responsibilityTaskId;
            else
                ScenarioContext.Current.Add("responsibilitytaskid", responsibilityTaskId);
        }

        public static long GetFirstCreatedResponsibilityTaskId()
        {
            var createdResponsibilityTaskIds = ScenarioContext.Current["AddedResponsibilityTasks"] as List<long>;
            return createdResponsibilityTaskIds.OrderBy(x => x).First();
        }

        public static long GetSecondCreatedResponsibilityTaskId()
        {
            var createdResponsibilityTaskIds = ScenarioContext.Current["AddedResponsibilityTasks"] as List<long>;
            return createdResponsibilityTaskIds.OrderBy(x => x).Skip(1).Take(1).First();
        }


        public static void SetSiteAddressId(long siteaddressid)
        {
            if (ScenarioContext.Current.ContainsKey("siteaddressid"))
                ScenarioContext.Current["siteaddressid"] = siteaddressid;
            else
                ScenarioContext.Current.Add("siteaddressid", siteaddressid);
        }

        public static long GetSiteAddressId()
        {
            var siteAddressId = (long)ScenarioContext.Current["siteaddressid"];
            return siteAddressId;
        }

        public static void AddedCompanyDefault(string addeddefault)
        {
            if (ScenarioContext.Current.ContainsKey("addeddefault"))
                ScenarioContext.Current["addeddefault"] = addeddefault;
            else
                ScenarioContext.Current.Add("addeddefault", addeddefault);
        }

        public static string GetAddedDefault()
        {
            var addeddefault = (string)ScenarioContext.Current["addeddefault"];
            return addeddefault;
        }

        public static void SetCompanyId(long companyId)
        {
            if (ScenarioContext.Current.ContainsKey("companyId"))
                ScenarioContext.Current["companyId"] = companyId;
            else
                ScenarioContext.Current.Add("companyId", companyId);
        }

        public static long GetCompanyId()
        {
            var companyId = (long)ScenarioContext.Current["companyId"];
            return companyId;
        }

        public static void SetLinkToId(long linkid)
        {
            if (ScenarioContext.Current.ContainsKey("linkid"))
                ScenarioContext.Current["linkid"] = linkid;
            else
                ScenarioContext.Current.Add("linkid", linkid);
        }

        public static long GetLinkToId()
        {
            var linkid = (long)ScenarioContext.Current["linkid"];
            return linkid;
        }

        public static void SetSiteGroupName(string siteGroupName)
        {
            if (ScenarioContext.Current.ContainsKey("siteGroupName"))
                ScenarioContext.Current["siteGroupName"] = siteGroupName;
            else
                ScenarioContext.Current.Add("siteGroupName", siteGroupName);
        }

        public static string GetSiteGroupName()
        {
            var siteGroupName = (string)ScenarioContext.Current["siteGroupName"];
            return siteGroupName;
        }

        public static string GetSiteName()
        {
            var siteName = (string)ScenarioContext.Current["siteName"];
            return siteName;
        }

        public static void SetSiteName(string siteName)
        {
            if (ScenarioContext.Current.ContainsKey("siteName"))
                ScenarioContext.Current["siteName"] = siteName;
            else
                ScenarioContext.Current.Add("siteName", siteName);
        }

        public static void SetSearchedNonEmployee(string searchTerm)
        {
            if (ScenarioContext.Current.ContainsKey("searchTerm"))
                ScenarioContext.Current["searchTerm"] = searchTerm;
            else
                ScenarioContext.Current.Add("searchTerm", searchTerm);
        }
        public static string GetSearchedNonEmployee()
        {
            var searchTerm = (string)ScenarioContext.Current["searchTerm"];
            return searchTerm;
        }

        public static void SetAddingNewNonEmployee(NonEmployeeCreating nonEmployeeCreating)
        {
            if (ScenarioContext.Current.ContainsKey("nonEmployeeCreating"))
                ScenarioContext.Current["nonEmployeeCreating"] = nonEmployeeCreating;
            else
                ScenarioContext.Current.Add("nonEmployeeCreating", nonEmployeeCreating);
        }

        public static NonEmployeeCreating GetAddingNewNonEmployee()
        {
            var nonEmployeeCreating = (NonEmployeeCreating)ScenarioContext.Current["nonEmployeeCreating"];
            return nonEmployeeCreating;
        }

        public static void SetCompanyDefaultFormWorkingOnName(string companyDefaultsFormName)
        {
            if (ScenarioContext.Current.ContainsKey("companyDefaultsFormName"))
                ScenarioContext.Current["companyDefaultsFormName"] = companyDefaultsFormName;
            else
                ScenarioContext.Current.Add("companyDefaultsFormName", companyDefaultsFormName);
        }

        public static string GetCompanyDefaultFormWorkingOnName()
        {
            var companyDefaultsFormName = (string)ScenarioContext.Current["companyDefaultsFormName"];
            return companyDefaultsFormName;
        }

        public static void SetEmployeeCreatingUpdating(dynamic creatingEmployee)
        {
            if (ScenarioContext.Current.ContainsKey("creatingEmployee"))
                ScenarioContext.Current["creatingEmployee"] = creatingEmployee;
            else
                ScenarioContext.Current.Add("creatingEmployee", creatingEmployee);
        }

        public static dynamic GetEmployeeCreatingUpdating()
        {
            var creatingEmployee = (dynamic)ScenarioContext.Current["creatingEmployee"];
            return creatingEmployee;
        }

        public static void SetCreatedEmployees(List<ExpandoObject> createdEmployees)
        {
            if (ScenarioContext.Current.ContainsKey("createdEmployees"))
                ScenarioContext.Current["createdEmployees"] = createdEmployees;
            else
                ScenarioContext.Current.Add("createdEmployees", createdEmployees);
        }

        public static IEnumerable<ExpandoObject> GetCreatedEmployees()
        {
            var createdEmployees = (List<ExpandoObject>)ScenarioContext.Current["createdEmployees"];
            return createdEmployees;
        }

        public static void SetUserRolePermissions(List<string> permissions)
        {
            if (ScenarioContext.Current.ContainsKey("permissions"))
                ScenarioContext.Current["permissions"] = permissions;
            else
                ScenarioContext.Current.Add("permissions", permissions);
        }

        public static List<string> GetUserRolePermissions()
        {
            if(ScenarioContext.Current.ContainsKey("permissions"))
                return (List<string>)ScenarioContext.Current["permissions"];
            var permissions = new List<string>();
            SetUserRolePermissions(permissions);
            return permissions;
        }

        public static void SetHazardAddingToRiskAssessment(dynamic hazard)
        {
            if (ScenarioContext.Current.ContainsKey("hazard"))
                ScenarioContext.Current["hazard"] = hazard;
            else
                ScenarioContext.Current.Add("hazard", hazard);
        }

        public static dynamic GetHazardAddingToRiskAssessment()
        {
            var hazard = (dynamic)ScenarioContext.Current["hazard"];
            return hazard;
        }

        public static void SetFurtherActionTaskAddingToRiskAssessment(dynamic furtherActionTaskAdding)
        {
            if (ScenarioContext.Current.ContainsKey("furtherActionTaskAdding"))
                ScenarioContext.Current["furtherActionTaskAdding"] = furtherActionTaskAdding;
            else
                ScenarioContext.Current.Add("furtherActionTaskAdding", furtherActionTaskAdding);
        }

        public static dynamic GetFurtherActionTaskAddingToRiskAssessment()
        {
            var hazard = (dynamic)ScenarioContext.Current["furtherActionTaskAdding"];
            return hazard;
        }

        public static void SetFurtherActionTaskRow(TableRow furtherActionTaskRow)
        {
            if (ScenarioContext.Current.ContainsKey("furtherActionTaskRow"))
                ScenarioContext.Current["furtherActionTaskRow"] = furtherActionTaskRow;
            else
                ScenarioContext.Current.Add("furtherActionTaskRow", furtherActionTaskRow);
        }

        public static TableRow GetFurtherActionTaskRow()
        {
            var furtherActionTaskRow = (TableRow)ScenarioContext.Current["furtherActionTaskRow"];
            return furtherActionTaskRow;
        }
    }
}