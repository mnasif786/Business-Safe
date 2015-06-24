using BusinessSafe.AcceptanceTests.StepHelpers;
using StructureMap.Configuration.DSL;

namespace BusinessSafe.AcceptanceTests
{
    public class SutRegistry : Registry
    {
        public SutRegistry()
        {
            For<ICurrentTime>().Add(new CurrentTime());
        }         
    }
}