using System.Configuration;

using MvcContrib.TestHelper.WatiN;

using TechTalk.SpecFlow;

using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepHelpers
{
    public static class WebBrowser
    {
        public static IE Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    ScenarioContext.Current["browser"] = new IE();
                }
                return ScenarioContext.Current["browser"] as IE;
            }
        }

        public static WatinDriver Driver
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browserDriver"))
                {
                    ScenarioContext.Current["browserDriver"] = new WatinDriver(Current , ConfigurationManager.AppSettings["baseURL"]);
                }
                return ScenarioContext.Current["browserDriver"] as WatinDriver;
            }
        }



    }
}