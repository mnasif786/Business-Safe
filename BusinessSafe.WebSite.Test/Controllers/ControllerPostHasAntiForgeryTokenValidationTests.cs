using System;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers
{
    [TestFixture]
    [Category("Unit")]
    public class ControllerPostHasAntiForgeryTokenValidationTests
    {
        [Test]
        public void All_controllers_post_methods_should_have_validate_antiforgery_attribute()
        {
            var controllers = typeof(MvcApplication).Assembly.GetTypes().Where(typeof(IController).IsAssignableFrom);
            foreach (var controller in controllers)
            {
                var postActions = controller.GetMethods()
                                        .Where(m => !m.ContainsGenericParameters)
                                        .Where(m => !m.IsDefined(typeof(ChildActionOnlyAttribute), true))
                                        .Where(m => !m.IsDefined(typeof(NonActionAttribute), true))
                                        .Where(m => !m.GetParameters().Any(p => p.IsOut || p.ParameterType.IsByRef))
                                        .Where(m => m.IsDefined(typeof(HttpPostAttribute), true));

                foreach (var action in postActions)
                {
                    //CSRF XOR AntiForgery
                    Assert.IsTrue(action.IsDefined(typeof(AllowCsrfAttacksAttribute), true) != action.IsDefined(typeof(ValidateAntiForgeryTokenAttribute), true),
                                    string.Format("{0} on {2}.{1} is [HttpPost] but not [ValidateAntiForgeryToken]", action.Name, controller.Name, controller.Namespace ));
                }
            }
        }
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AllowCsrfAttacksAttribute : Attribute { }