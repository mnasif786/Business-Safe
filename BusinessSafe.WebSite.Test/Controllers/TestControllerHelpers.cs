using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;

namespace BusinessSafe.WebSite.Tests.Controllers
{
    public static class TestControllerHelpers
    {
        private const long _companyId = 1;
        private const string _userEmail = "TestControllerUser@pbstest.com";
        private const string _fullname = "Test Controller User";
        private static Guid _userId = Guid.NewGuid();

        public static long CompanyIdAssigned { get { return _companyId; } }
        public static string EmailAssigned { get { return _userEmail; } }
        public static Guid UserIdAssigned { get { return _userId; } }
        public static string UserFullNameAssigned { get { return _fullname; } }

        public static T AddUserToController<T>(T controller)
        {
            var result = controller as Controller;
            var customPrinciple = new CustomPrincipal(new UserDto()
            {
                Id = _userId,
                CompanyId = _companyId,
                Permissions = new string[] { "EditGeneralandHazardousSubstancesRiskAssessments" },
                AllowedSites = new List<long>() {123,435 },
                Employee = new EmployeeDto()
                           {
                               MainContactDetails = new EmployeeContactDetailDto { Email = _userEmail },
                               FullName = _fullname
                           }
            }, new CompanyDto()); 
            
            MockContext<T>(result, customPrinciple);

            return controller;
        }

        public static T AddUserWithGetSitesFilterToController<T>(T controller)
        {
            var result = controller as Controller;
            var customPrinciple = new Mock<CustomPrincipal>();
            customPrinciple.Setup(x => x.CompanyId).Returns(1);
            customPrinciple.Setup(x => x.UserId).Returns(Guid.NewGuid());
            customPrinciple.Setup(x => x.GetSitesFilter()).Returns(new List<long>());

            MockContext<T>(result, customPrinciple.Object);
            return controller;
        }

        public static T AddUserWithPopulatedGetSitesFilterToController<T>(T controller)
        {
            var result = controller as Controller;
            var customPrinciple = new CustomPrincipal(new UserDto()
            {
                Id = _userId,
                CompanyId = 1,
                Permissions = new string[] { },
                AllowedSites = new List<long>() { 1234 }
            }, new CompanyDto());

            MockContext<T>(result, customPrinciple);
            return controller;
        }

        public static T AddUserWithDefinableAllowedSitesToController<T>(T controller, IList<long> allowedSites)
        {
            var result = controller as Controller;
            var customPrinciple = new CustomPrincipal(new UserDto()
            {
                Id = _userId,
                CompanyId = 1,
                Permissions = new string[] { },
                AllowedSites = allowedSites
            }, new CompanyDto());

            MockContext<T>(result, customPrinciple);
            return controller;
        }

        private static void MockContext<T>(Controller result, CustomPrincipal customPrincipal)
        {
            var contextMock = new Mock<HttpContextBase>();
            contextMock
                .SetupGet(ctx => ctx.User)
                .Returns(customPrincipal);

            var controllerContextMock = new Mock<ControllerContext>();
            controllerContextMock
                .SetupGet(con => con.HttpContext)
                .Returns(contextMock.Object);
            

            result.ControllerContext = controllerContextMock.Object;
        }
    }
}