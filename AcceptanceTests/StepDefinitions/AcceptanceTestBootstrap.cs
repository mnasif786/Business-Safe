using BusinessSafe.WebSite;
using StructureMap;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    public static class AcceptanceTestBootstrap
    {
        public static void Run()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<WebsiteRegistry>());
        }
    }
}