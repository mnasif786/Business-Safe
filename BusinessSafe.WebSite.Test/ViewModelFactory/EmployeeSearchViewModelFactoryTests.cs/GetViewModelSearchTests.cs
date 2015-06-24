using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NHibernate;
using NHibernate.Collection.Generic;
using NHibernate.Impl;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.EmployeeSearchViewModelFactoryTests.cs
{
    [TestFixture]
    public class GetViewModelSearchTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;
        private Mock<ILookupService> _lookupService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        
        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _siteService = new Mock<ISiteService>();
            _lookupService = new Mock<ILookupService>(); 
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();

            _employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Returns( new List<EmployeeDto>());

            _lookupService
                .Setup(x => x.GetEmploymentStatuses())
                .Returns( new List<LookupDto>());
                
            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns( new List<SiteDto>());

             var session = new Mock<ISession>();
            _businessSafeSessionManager
                .SetupGet(x => x.Session)
                .Returns(session.Object);
             
        }

        [Test]
        public void Give_Search_for_employees_When_Current_Employee_is_in_list_Then_Show_delete_Button_is_false_for_current_employee()
        {
            var currentUser = new Mock<ICustomPrincipal>();          
            Guid currentUserId = Guid.NewGuid();
           
            currentUser
                .Setup(x => x.UserId)
                .Returns(currentUserId);

            _employeeService
               .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
               .Returns( new List<EmployeeDto>(){ new EmployeeDto(){ User = new UserDto() {Id = currentUserId} } });

            EmployeeSearchViewModelFactory factory = GetTarget();
            EmployeeSearchViewModel model = factory
                                                .WithAllowedSites(new List<long>())
                                                .WithCurrentUser(currentUser.Object)
                                                .GetViewModel();

            Assert.AreEqual(1, model.Employees.Count );
            Assert.IsFalse( model.Employees[0].ShowDeleteButton);
        }

        private EmployeeSearchViewModelFactory GetTarget()
        {
            return new EmployeeSearchViewModelFactory( _employeeService.Object, _siteService.Object, _lookupService.Object, _businessSafeSessionManager.Object  );
        }
    }
}




