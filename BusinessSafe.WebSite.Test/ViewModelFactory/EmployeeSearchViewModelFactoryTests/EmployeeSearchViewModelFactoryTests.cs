using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.EmployeeSearchViewModelFactoryTests
{
    [TestFixture]
    public class EmployeeSearchViewModelFactoryTests
    {
        private Mock<IEmployeeService> _employeeService;
        private Mock<ISiteService> _siteService;
        private Mock<ILookupService> _lookupService;

        [SetUp]
        public void Setup()
        {
            _employeeService = new Mock<IEmployeeService>();
            _employeeService.Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>())).Returns(new List<EmployeeDto>());

            _siteService = new Mock<ISiteService>();
            _siteService.Setup(x => x.Search(It.IsAny<SearchSitesRequest>())).Returns(new List<SiteDto>());

            _lookupService = new Mock<ILookupService>();
            _lookupService.Setup(x => x.GetEmploymentStatuses()).Returns(new List<LookupDto>());
        }

        [Test]
        public void When_GetViewModel_Then_Calls_SiteService_Take()
        {
            // Given
            var target = new EmployeeSearchViewModelFactory(
                _employeeService.Object,
                _siteService.Object,
                _lookupService.Object);
            const int companyId = 100;
            var searchSitesRequest = new SearchSitesRequest()
                                         {
                                             CompanyId = companyId,

                                         };

            // When
            target.WithCompanyId(companyId).w.GetViewModel();

            // Then
            _siteService.Verify(x => x.Search(searchSitesRequest));
        }
    }
}
