using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class DialogConfirmationSteps : BaseSteps
    {
        [Given(@"I select '(.*)' on confirmation")]
        [When(@"I select '(.*)' on confirmation")]
        public void WhenISelectYesOnConfirmation(string confirmation)
        {
            SleepIfEnvironmentIsPotentiallySlow(8000);
            var dialog = WebBrowser.Current.Div(Find.ByClass("ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable"));
            Assert.That(dialog.Exists, "dialog does not exist");

            if (confirmation.ToLower() == "yes")
            {
                string jsClickConfirmButtonScript = "var clickConfirm = function(){var dialog = $('.ui-dialog-buttonset'); $('button:first', dialog).click(); }; clickConfirm();";
                WebBrowser.Current.Eval(jsClickConfirmButtonScript);
            }
            else
            {
                string jsClickCancelButtonScript = "var clickCancel = function(){var dialog = $('.ui-dialog-buttonset'); $('button', dialog).filter(':last').click(); }; clickCancel();";
                WebBrowser.Current.Eval(jsClickCancelButtonScript);
            }
            WebBrowser.Current.WaitForComplete();
        }


    }
}