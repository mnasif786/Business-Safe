using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using BusinessSafe.AcceptanceTests.StepHelpers;
using StructureMap;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.CompanyDefaults
{
    [Binding]
    public class AddRiskAssessorSteps : BaseSteps
    {
        [Given("Javascript for selecting employee has fired")]
        [When("Javascript for selecting employee has fired")]
        public void JavaScriptForSelectingEmployeeHasFired()
        {
            WebBrowser.Current.Eval("new addEditRiskAssessor.updateJobAndSite(null, null);");
        }

        [Given(@"We have the following risk assessors:")]
        public void GivenWeHaveTheFollowingRiskAssessors(TechTalk.SpecFlow.Table table)
        {
            const string insertSql = "INSERT INTO [BusinessSafe].[dbo].[RiskAssessor] (" +
                                     "[EmployeeId], " +
                                     "[HasAccessToAllSites], " +
                                     "[SiteId], " +
                                     "[DoNotSendTaskOverdueNotifications], " +
                                     "[DoNotSendTaskCompletedNotifications], " +
                                     "[DoNotSendReviewDueNotification], " +
                                     "[CreatedBy], " +
                                     "[CreatedOn], " +
                                     "[LastModifiedBy], " +
                                     "[LastModifiedOn], " +
                                     "[Deleted] ) " +
                                     "VALUES (" +
                                     ":EmployeeId, " +
                                     ":HasAccessToAllSites, " +
                                     ":SiteId, " +
                                     ":DoNotSendTaskOverdueNotifications, " +
                                     ":DoNotSendTaskCompletedNotifications, " +
                                     ":DoNotSendReviewDueNotification, " +
                                     ":CreatedBy, " +
                                     ":CreatedOn, " +
                                     ":LastModifiedBy, " +
                                     ":LastModifiedOn, " +
                                     ":Deleted ) ";

            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();

            foreach (var row in table.Rows)
            {
                session
                   .CreateSQLQuery(insertSql)
                   .SetParameter("EmployeeId", row["EmployeeId"])
                   .SetParameter("HasAccessToAllSites", row["HasAccessToAllSites"])
                   .SetParameter("SiteId", row["SiteId"])
                   .SetParameter("DoNotSendTaskOverdueNotifications", row["DoNotSendTaskOverdueNotifications"])
                   .SetParameter("DoNotSendTaskCompletedNotifications", row["DoNotSendTaskCompletedNotifications"])
                   .SetParameter("DoNotSendReviewDueNotification", row["DoNotSendReviewDueNotification"])
                   .SetParameter("CreatedBy", new Guid("16ac58fb-4ea4-4482-ac3d-000d607af67c"))
                   .SetParameter("CreatedOn", DateTime.Now)
                   .SetParameter("LastModifiedBy", new Guid("16ac58fb-4ea4-4482-ac3d-000d607af67c"))
                   .SetParameter("LastModifiedOn", DateTime.Now)
                   .SetParameter("Deleted", row["Deleted"])
                   .UniqueResult<long>();

                session.Flush();
            }
        }

        [When(@"I click on the reinstate button for the first deleted user")]
        public void WhenIClickOnTheReinstateButtonForTheFirstDeletedUser()
        {
            var reinstateButtons = WebBrowser.Current.Links.Filter(Find.ByClass("icon-share reinstate-risk-assessor"));
            foreach (Link link in reinstateButtons)
            {
                link.Click();
                break;
            }
        }

        [Given("I click on show deleted button")]
        [When("I click on show deleted button")]
        public void IClickOnTheShowDeletedButton()
        {
            var button = WebBrowser.Current.Button(Find.ByClass("btn showDeletedRiskAssessors"));
            button.Click();
        }

        [Given("I click on show active button")]
        [When("I click on show active button")]
        public void IClickOnTheShowActiveButton()
        {
            var button = WebBrowser.Current.Button(Find.ByClass("btn showActiveRiskAssessors"));
            button.Click();
        }
    }
}
