using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers
{
    [TestFixture]
    [Category("Unit")]
    public class BaseControllerAttributesTests
    {

        [Test]
        public void All_controllers_should_derive_from_base_controller()
        {
            //Given
            var businessSafeController = Assembly
                                                .GetAssembly(typeof(BaseController))
                                                .GetTypes()
                                                .Where(type => type.IsSubclassOf(typeof(Controller)))
                                                .ToList();

            //When
            //Then
            foreach (var controller in businessSafeController
                .Where(
                    controller => controller.Name != "BaseController" &&
                    controller.Name != "AutoLogInFromPeninsulaController" &&
                    controller.Name != "BusinessSafeMailerBaseController" &&
                    controller.Name != "HelpController" &&
                    controller.Name != "HealthStatusController"))
            {
                Assert.That(controller.BaseType.Name, Is.EqualTo("BaseController"));
            }
        }

        [Test]
        public void Base_controller_should_have_authorize_attribute_action_filter()
        {
            //Given
            var businessSafeController = Assembly
                                            .GetAssembly(typeof(BaseController))
                                            .GetTypes()
                                            .First(type => type.Name == "BaseController");

            //When
            //Then
            var customAttributes = businessSafeController.GetCustomAttributes(false);
            Assert.That(customAttributes.Length, Is.EqualTo(2));
            Assert.That(customAttributes.Count(x => x.GetType() == typeof(UrlHackingFilter)),Is.EqualTo(1));
            Assert.That(customAttributes.Count(x => x.GetType() == typeof(AuthorizeAttribute)), Is.EqualTo(1));
        }
    }
}