using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Sites
{
    [Binding]
    public class SitesSteps: BaseSteps
    {

        [Given(@"I have no sites for company with id '(.*)'")]
        public void GivenIHaveNoSitesForCompanyWithId(long companyId)
        {
        }
        

        [Given(@"I have navigated to site structure page")]
        public void GivenIHaveNavigatedToSiteStructurePage()
        {
            WebBrowser.Driver.Navigate("Sites"); 
        }

        [Given(@"I have entered '(.*)' into site group name text field")]
        public void GivenIHaveEnteredNewSiteGroupIntoSiteGroupNameTextField(string siteGroupName)
        {
            if (siteGroupName == string.Empty) return;
            Thread.Sleep(500);
            var uniqueSiteGroupName = string.Format("{0}{1}", siteGroupName, DateTime.Now.ToLongTimeString());
            var form = WebBrowser.Current.Form(Find.ById(x => x == "SiteGroup"));
            var siteGroupNameTextBox = form.TextField(Find.ById(x => x == "Name"));
            siteGroupNameTextBox.Clear();
            siteGroupNameTextBox.AppendText(uniqueSiteGroupName);
            ScenarioContextHelpers.SetSiteGroupName(uniqueSiteGroupName);
        }

        [Given(@"I have selected '(.*)' as link to site for site group")]
        public void GivenIHaveSelectedHeadOfficeAsLinkToSiteForSiteGroup(string site)
        {
            Thread.Sleep(500);
            var form = WebBrowser.Current.Form(Find.ById(x => x == "SiteGroup"));

            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
               .CreateSQLQuery("SELECT Id FROM SiteStructureElement WHERE Name = '" + site + "' AND ClientId = " + ScenarioContextHelpers.GetCompanyId() + "")
               .UniqueResult<long>();

            var dropDownList = form.TextField(Find.ById("GroupLinkToSiteId"));
            dropDownList.Clear();
            dropDownList.AppendText(result.ToString());

            ScenarioContextHelpers.SetLinkToId(result);
        }

        [Given(@"I have clicked on the '(.*)' linked site")]
        public void GivenIHaveClickedOnTheNewarkLinkedSite(string linkedSite)
        {
            ScenarioContextHelpers.SetSiteName(linkedSite);
            var linkedSites = WebBrowser.Current.Divs.Where(x => x.ClassName == "linked-site").ToList();

            foreach (var site in linkedSites)
            {
                if (site.InnerHtml == ScenarioContextHelpers.GetSiteName())
                {
                    site.Click();
                    break;
                }
            }
           
        }


        [Given(@"I have clicked on the first unlinked site")]
        public void GivenIHaveClickedOnTheFirstUnlinkedSite()
        {
            var firstUnlinkedSite = WebBrowser.Current.Divs.First(x => x.ClassName == "unlinked-site");
            firstUnlinkedSite.Click();
        }

        [Given(@"I have entered '(.*)' into site name text field")]
        public void GivenIHaveEnteredNewSiteIntoSiteNameTextField(string site)
        {
            if (site == string.Empty) return;
            Thread.Sleep(500);
            var uniqueSiteName = string.Format("{0}{1}", site, DateTime.Now.ToLongTimeString());
            var form = WebBrowser.Current.Form(Find.ById(x => x == "SiteDetails"));
            var siteGroupNameTextBox = form.TextField(Find.ById(x => x == "Name"));
            siteGroupNameTextBox.Clear();
            siteGroupNameTextBox.AppendText(uniqueSiteName);
            ScenarioContextHelpers.SetSiteName(uniqueSiteName);
        }

        [Given(@"I have site group called '(.*)'")]
        public void GivenIHaveSiteGroupCalledSiteGroupToDelete(string siteGroupName)
        {
            CommandButtonSteps.PressButton("AddSiteGroupLink");
            GivenIHaveEnteredNewSiteGroupIntoSiteGroupNameTextField(siteGroupName);
            GivenIHaveSelectedHeadOfficeAsLinkToSiteForSiteGroup("Main Site");
	        WhenISelectSave();
        }

        [Given(@"I have selected site group called '(.*)'")]
        public void GivenIHaveSelectedSiteGroupCalledSiteGroupToDelete(string siteGroupToDelete)
        {
            var siteGroups = WebBrowser.Current.Divs.Where(x => x.ClassName == "linked-site siteGroupLabel");

            foreach (var siteGroup in siteGroups)
            {
                if (siteGroup.InnerHtml == ScenarioContextHelpers.GetSiteGroupName())
                {
                    siteGroup.Click();
                }
            }
        }
        

        [When(@"I select save group")]
        public void WhenISelectSave()
        {
            var button = WebBrowser.Current.Button(Find.ById(x => x == "SaveSiteGroupButton"));   
            button.Click();
        }

        [Then(@"the site group should be created and linked to correct site '(.*)'")]
        public void ThenTheSiteGroupShouldBeCreatedAndLinkedToCorrectSite(long siteId)
        {
            var siteGroupName = ScenarioContextHelpers.GetSiteGroupName();
            var parentSite = WebBrowser.Current.Divs
                .Filter(Find.ByClass("linked-site"))
                .Single(x => x.GetAttributeValue("data-id") == siteId.ToString(CultureInfo.InvariantCulture));

            var childrenContainer = parentSite.NextSibling;

            Assert.That(childrenContainer.InnerHtml.Contains(siteGroupName));
        }

        [Then(@"I should remain on the site group details")]
        public void ThenIShouldRemainOnTheSiteGroupDetails()
        {
            Assert.True(WebBrowser.Current.Form(Find.ById("SiteGroup")).Exists);
        }
        

        [Then(@"the site should be moved to new parent '(.*)'")]
        [Then(@"the site should be created and linked to correct site '(.*)'")]
        public void ThenTheSiteShouldBeCreatedAndLinkedToCorrectSite(long siteId)
        {
            var siteName = ScenarioContextHelpers.GetSiteName();
            var parentSite = WebBrowser.Current.Divs
                .Filter(Find.ByClass("linked-site"))
                .Single(x => x.GetAttributeValue("data-id") == siteId.ToString(CultureInfo.InvariantCulture));

            var childrenContainer = parentSite.NextSibling;

            Assert.That(childrenContainer.InnerHtml.Contains(siteName));
        }

        [Then(@"the site should be moved to new parent site group '(.*)'")]
        public void ThenTheSiteShouldBeMovedToNewParentSiteGroup(long siteId)
        {
            SleepIfEnvironmentIsPotentiallySlow(4000);
            var siteName = ScenarioContextHelpers.GetSiteName();
            var parentSite = WebBrowser.Current.Divs
                .Filter(Find.ByClass("linked-site siteGroupLabel"))
                .Single(x => x.GetAttributeValue("data-id") == siteId.ToString(CultureInfo.InvariantCulture));

            var childrenContainer = parentSite.NextSibling;

            Assert.That(childrenContainer.InnerHtml.Contains(siteName));
        }

        [Then(@"the site group should be deleted")]
        public void ThenTheSiteGroupShouldBeDeleted()
        {
            Thread.Sleep(4000);
            var siteGroupName = ScenarioContextHelpers.GetSiteGroupName();
            var siteLinkId = ScenarioContextHelpers.GetLinkToId();

            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
                .CreateSQLQuery("SELECT Count(*) FROM SiteStructureElement WHERE Name = '" + siteGroupName + "' AND ParentId = " + siteLinkId + " AND SiteType = 2 AND Deleted = 0")
                .UniqueResult<int>();

            Assert.That(result, Is.EqualTo(0));
        }
        
        [Then(@"the should get validation message")]
        public void ThenTheShouldGetValidationMessage()
        {
            var validationErrors = WebBrowser.Current.Div(d => d.ClassName == "validation-summary-errors alert alert-error");
            Assert.True(validationErrors.Exists, "validation-summary-errors div could not be found");

            var errorDict = new Dictionary<string, int>();
            foreach(Element elem in validationErrors.Elements)
            {
                if(errorDict.ContainsKey(elem.InnerHtml))
                {
                    errorDict[elem.InnerHtml]++;
                }else
                {
                    errorDict.Add(elem.InnerHtml, 1);
                }
            }
            foreach (var key in errorDict.Keys)
            {
                Assert.That(errorDict[key], Is.EqualTo(1));
            }
        }

        [Then(@"should open new window with '(.*)'")]
        public void ThenShouldOpenNewWindowWithUrl(string url)
        {
            Assert.True(TryGetNewBrowser(url));
        }

        private bool TryGetNewBrowser(string title)
        {
            try
            {
                IE.AttachToIE(Find.ByUrl(title));
                return true;
            }
            catch (WatiN.Core.Exceptions.IENotFoundException)
            {
                return false;
            }
        }
    }
}
