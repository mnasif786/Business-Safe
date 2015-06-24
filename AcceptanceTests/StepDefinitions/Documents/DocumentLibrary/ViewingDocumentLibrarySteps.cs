using System.Collections.Generic;
using System.Linq;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Documents.DocumentLibrary
{
    [Binding]
    public class ViewingDocumentLibrarySteps : BaseSteps
    {
        [Then(@"the link to view a document's href is encrypted")]
        public void ThenTheLinkToViewADocumentSHrefIsEncrypted()
        {
            IEnumerable<Link> viewDocumentLinks = WebBrowser.Current.Links.Filter(Find.ByClass("ViewDocumentIconLink"));
            foreach(var link in viewDocumentLinks)
            {
                var keyValues = link.Url.Substring(link.Url.LastIndexOf('?') + 1).Split('&');
                var keyValueDictionary = new Dictionary<string, string>();

                foreach(var keyValue in keyValues)
                {
                    var parts = keyValue.Split('=');
                    keyValueDictionary.Add(parts[0], parts[1]);
                    Assert.That(!string.IsNullOrEmpty(keyValueDictionary["enc"]), "The queryString is not being encrypted");
                    try
                    {
                        Assert.That(string.IsNullOrEmpty(keyValueDictionary["documentId"]), "The documentId is being displayed");
                    }catch(KeyNotFoundException) {}
                }
            }
        }

        [When(@"I clicked on a view document link")]
        public void WhenIClickedOnAViewDocumentLink()
        {
            IEnumerable<Link> viewDocumentLinks = WebBrowser.Current.Links.Filter(Find.ByClass("ViewDocumentIconLink"));
            viewDocumentLinks.First().Click();
        }
        
        [Then(@"that document is displayed in a new tab")]
        public void ThenThatDocumentIsDisplayedInANewTab()
        {
            Assert.That(WebBrowser.Current.ContainsText("hello world!"));
        }

    }
}
