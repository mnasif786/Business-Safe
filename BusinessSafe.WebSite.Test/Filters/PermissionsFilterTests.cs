using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Filters;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Filters
{
    [TestFixture]
    public class PermissionsFilterTests
    {
        
        [Test]
        public void Given_not_got_a_valid_custom_principle_When_OnActionExecuting_Then_should_throw_401_not_authorised_exception()
        {
            // Given
            var fakePrincipal = new FakePrincipal();
            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(fakePrincipal)
            };

            var permissionFilterAttribute = new PermissionFilterAttribute(Permissions.ViewSiteDetails);


            // When
            permissionFilterAttribute.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.TypeOf<HttpUnauthorizedResult>());

        }

        [Test]
        public void Given_custom_principle_has_no_permissions_When_OnActionExecuting_Then_should_throw_401_not_authorised_exception()
        {
            // Given
            var userDto = new UserDto()
            {
                Permissions = new string[]{}
            };

            var customPrincipal = CreateCustomPrincipal(userDto);
            var filterContext = new ActionExecutingContext
                                    {
                                        HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal)
                                    };

            var permissionFilterAttribute = new PermissionFilterAttribute(Permissions.ViewSiteDetails);

            
            // When
            permissionFilterAttribute.OnActionExecuting(filterContext);
            
            // Then
            Assert.That(filterContext.Result, Is.TypeOf<HttpUnauthorizedResult>());
            
        }

        [Test]
        public void Given_custom_principle_has_permission_but_not_one_checking_When_OnActionExecuting_Then_should_throw_401_not_authorised_exception()
        {
            // Given
            var permissions = new string[] { Permissions.ViewCompanyDetails.ToString() };

            var userDto = new UserDto()
            {
                CompanyId = 0,
                Permissions = permissions
            };

            var customPrincipal = CreateCustomPrincipal(userDto);
            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal)
            };

            var permissionFilterAttribute = new PermissionFilterAttribute(Permissions.ViewSiteDetails);

            // When
            permissionFilterAttribute.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.TypeOf<HttpUnauthorizedResult>());
        }

        [Test]
        public void Given_custom_principal_has_valid_permission_When_OnActionExecuting_Then_should_return_null()
        {
            // Given
            var permissions = new string[] { Permissions.ViewCompanyDetails.ToString() };
            var userDto = new UserDto()
            {
                CompanyId = 0,
                Permissions = permissions
            };
            var customPrincipal = CreateCustomPrincipal(userDto);
            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal)
            };

            var permissionFilterAttribute = new PermissionFilterAttribute(Permissions.ViewCompanyDetails);

            // When
            permissionFilterAttribute.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.Null);
        }

        [Test]
        public void Given_custom_principal_has_valid_permission_and_got_more_than_one_permission_required_When_OnActionExecuting_Then_should_return_null()
        {
            // Given
            var permissions = new string[] { Permissions.ViewCompanyDetails.ToString(), Permissions.ViewSiteDetails.ToString() };
            var userDto = new UserDto()
            {
                CompanyId = 0,
                Permissions = permissions
            };
            var customPrincipal = CreateCustomPrincipal(userDto);
            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal)
            };

            var permissionFilterAttribute = new PermissionFilterAttribute(Permissions.ViewCompanyDetails, Permissions.ViewSiteDetails);

            // When
            permissionFilterAttribute.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.Null);
        }

        [Test]
        public void Given_custom_principal_has_invalid_permission_got_one_of_the_specified_two_When_OnActionExecuting_Then_should_throw_401_not_authorised_exception()
        {
            // Given
            var permissions = new string[] { Permissions.ViewCompanyDetails.ToString(), Permissions.AddEmployeeRecords.ToString() };
            var userDto = new UserDto()
            {
                CompanyId = 0,
                Permissions = permissions
            };

            var customPrincipal = CreateCustomPrincipal(userDto);
            var filterContext = new ActionExecutingContext
            {
                HttpContext = MvcMockHelpers.FakeHttpContext(customPrincipal)
            };

            var permissionFilterAttribute = new PermissionFilterAttribute(Permissions.ViewCompanyDetails, Permissions.ViewSiteDetails);

            // When
            permissionFilterAttribute.OnActionExecuting(filterContext);

            // Then
            Assert.That(filterContext.Result, Is.TypeOf<HttpUnauthorizedResult>());
        }

        private static CustomPrincipal CreateCustomPrincipal(UserDto userDto)
        {
            var customPrincipal = new CustomPrincipal(userDto, new CompanyDto());
            return customPrincipal;
        }
    }
}
