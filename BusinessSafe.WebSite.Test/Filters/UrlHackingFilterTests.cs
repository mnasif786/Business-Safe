using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Filters;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Filters
{
    [TestFixture]
    public class UrlHackingFilterTests
    {
        [Test]
        public void Given_not_got_a_valid_custom_principle_When_OnActionExecuting_Then_should_throw_401_not_authorised_exception()
        {
            // Given
            var fakePrincipal = new FakePrincipal();
            var actionParameters = new Dictionary<string, object> { { "companyId", 1000 } };
            
            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(fakePrincipal),
                ActionParameters = actionParameters
            };

            var urlHackingFilter = new UrlHackingFilter();


            // When
            urlHackingFilter.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.TypeOf<HttpUnauthorizedResult>());

        }

        [Test]
        public void Given_not_got_a_companyid_When_OnActionExecuting_Then_should_have_result_of_null()
        {
            // Given
            var userDto = new UserDto()
            {
                Permissions = new string[] { }
            };
            var customPrincipal = CreateCustomPrincipal(userDto);
            var actionParameters = new Dictionary<string, object>();
            
            var filterContext = new ActionExecutingContext
                                    {
                                        HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal),
                                        ActionParameters = actionParameters
                                    };

            var urlHackingFilter = new UrlHackingFilter();


            // When
            urlHackingFilter.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.Null);

        }

        [Test]
        public void Given_a_companyid_but_not_correct_for_custom_principal_When_OnActionExecuting_Then_should_throw_401_not_authorised_exception()
        {
            // Given
            const int userCompanyId = 1;
            const int urlCompanyId = 9999;

            var userDto = new UserDto()
            {
                CompanyId = userCompanyId,
                Permissions = new List<string>()
            };

            var customPrincipal = CreateCustomPrincipal(userDto);
            var actionParameters = new Dictionary<string, object>
                                       {
                                           {"companyId", urlCompanyId}
                                       };

            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal),
                ActionParameters = actionParameters
            };

            var urlHackingFilter = new UrlHackingFilter();


            // When
            urlHackingFilter.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.TypeOf<HttpUnauthorizedResult>());

        }

        private static CustomPrincipal CreateCustomPrincipal(UserDto userDto)
        {
            var customPrincipal = new CustomPrincipal(userDto, new CompanyDto());
            return customPrincipal;
        }

        [Test]
        public void Given_a_companyid_and_matches_custom_principal_company_id_When_OnActionExecuting_Then_should_return_null()
        {
            // Given
            const int userCompanyId = 1;
            const int urlCompanyId = 1;
            var userDto = new UserDto()
            {
                CompanyId = userCompanyId,
                Permissions = new string[] { }
            };
            var customPrincipal = CreateCustomPrincipal(userDto);
            var actionParameters = new Dictionary<string, object>
                                       {
                                           {"companyId", urlCompanyId}
                                       };

            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal),
                ActionParameters = actionParameters
            };

            var urlHackingFilter = new UrlHackingFilter();

            // When
            urlHackingFilter.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.Null);
        }

        
        // url does have the corrct company id
    }
}