using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.WebSite.Contracts;
using NUnit.Framework;
using StructureMap;

namespace BusinessSafe.WebSite.Tests
{
    [TestFixture]
    public class WebRegistryTests
    {
        [Test]
        public void Given_bootstrapper_been_configured_When_run_Then_should_get_all_required_types()
        {
            // Arrange
            // Act
            //ObjectFactory.Initialize(cfg => cfg.AddRegistry<WebSiteRegistry>());
            
            // Assert
            //Assert.That(ObjectFactory.GetInstance<IControllerActivator>(), Is.Not.Null);
            //Assert.That(ObjectFactory.GetInstance<ITaskService>(), Is.Not.Null);
            //Assert.That(ObjectFactory.GetInstance<IResponsibilityPlannerViewModelFactory>(), Is.Not.Null);
            //Assert.That(ObjectFactory.GetInstance<ISiteStructureViewModelFactory>(), Is.Not.Null);
            //Assert.That(ObjectFactory.GetInstance<ISiteDetailsViewModelFactory>(), Is.Not.Null);
        }
    }
}
