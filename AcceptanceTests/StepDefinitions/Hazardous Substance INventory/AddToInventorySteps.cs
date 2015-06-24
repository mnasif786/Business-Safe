using System.Data.SqlClient;
using System.Text;
using System.Threading;
using NUnit.Framework;
using TechTalk.SpecFlow;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.HazardousSubstances.Inventory
{
    [Binding]
    public class AddToInventorySteps : BaseSteps
    {
        [BeforeScenario("createsHazardousSubstance")]
        [AfterScenario("createsHazardousSubstance")]
        public void TearDown()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var sql = new StringBuilder("DELETE [HazardousSubstance] WHERE id > 6 ");

                runSQLCommand(sql.ToString(), conn);
            }
        }

        [Given(@"I am on the add hazardous substance page for company '(.*)'")]
        public void GivenIAmOnTheAddHazardousSubstancePageForCompany(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("HazardousSubstanceInventory/Inventory/Add", companyId));
        }

        [Given(@"I have selected the option label '(.*)' from multi-select control '(.*)'")]
        [When(@"I have selected the option label '(.*)' from multi-select control '(.*)'")]
        public Element GivenIHaveSelectedOptionLabelFromMultiSelectControl(string optionLabelText, string nameOfControl)
        {
            Thread.Sleep(2000);

            var container = WebBrowser.Current.Div(Find.ById(nameOfControl+"-multi-select"));
            var item = container.Element(Find.ByText(optionLabelText));
            item.Click();

            Thread.Sleep(500);

            var addToSelect = container.Button(Find.ByClass("add-to-selected btn"));
            addToSelect.Click();
            Thread.Sleep(500);

            return item;
        }

        [Given(@"I selected the Global identification standard")]
        public void GivenISelectedTheGlobalIdentificationStandard()
        {
            var radioButton = WebBrowser.Current.RadioButton(Find.ByValue("Global"));
            radioButton.Click();
        }

        [Given(@"I have selected Global standard")]
        public void IHaveSelectedGlobalStandard()
        {
            var global = WebBrowser.Current.Element(Find.ByValue("Global"));
            global.Click();
        }

        [Then(@"I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            Thread.Sleep(1000);
            const string expectedUrl = "/hazardoussubstanceinventory?";
            Assert.That(WebBrowser.Current.Url.ToLower(), Contains.Substring(expectedUrl));
        }

        [Then(@"I should be redirected to the create hazardous substance risk assessment page")]
        public void ThenIShouldBeRedirectedToTheCReateHazardousSubstanceRiskAssessmentPage()
        {
            Thread.Sleep(1000);
            const string expectedUrl = "/HazardousSubstanceRiskAssessments/RiskAssessment/Create?";
            Assert.That(WebBrowser.Current.Url.ToLower(), Contains.Substring(expectedUrl.ToLower()));
        }

        [Then(@"the new hazardous substance '(.*)' should be visible")]
        public void ThenTheNewHazardousSubstanceShouldBeVisible(string title)
        {
            var inventoryGrid = WebBrowser.Current.Divs.Filter(Find.ById("HazardousSubstancesGrid")).First();
            var found = false;

            foreach (TableCell cell in inventoryGrid.TableCells)
            {
                if (cell.InnerHtml.Contains(title))
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found, string.Format("Could not find hazardous substance '{0}'", title));
        }

        [Given(@"I press the edit icon for hazardous substance with name '(.*)'")]
        public void GivenIPressTheEditIconForHazardousSubstanceWithName(string name)
        {
            var tableContainer = WebBrowser.Current.Div(Find.ById("HazardousSubstancesGrid"));
            var table = tableContainer.Tables.First();
            var links = table.Links.Filter(Find.ByClass("editIcon icon-edit"));
            var linkClicked = false;

            foreach (Element link in links)
            {
                var row = link.Parent.Parent;
                if (row.InnerHtml.Contains(name))
                {
                    link.Click();
                    linkClicked = true;
                    break;
                }
            }
            Assert.IsTrue(linkClicked, string.Format("Could not find edit link for {0}", name));
        }

        [Given(@"I have deselected option label '(.*)' from multi-select control '(.*)'")]
        [When(@"I have deselected option label '(.*)' from multi-select control '(.*)'")]
        public void GivenIHaveDeselectedRiskPhrase(string optionLabel, string controlName)
        {
            var container = WebBrowser.Current.Div(Find.ById(controlName + "-multi-select"));
            var selectedItems = container.SelectLists.First();
            try
            {
                selectedItems.Select(optionLabel);
                Thread.Sleep(500);
                var addToSelect = container.Button(Find.ByClass("remove-from-selected btn"));
                addToSelect.Click();
                Thread.Sleep(500);
            }catch
            {
            }
        }
    }
}
