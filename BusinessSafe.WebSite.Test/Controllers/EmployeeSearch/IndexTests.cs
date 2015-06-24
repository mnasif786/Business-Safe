using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Employees.Controllers;
using BusinessSafe.WebSite.Areas.Employees.Factories;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.EmployeeSearch
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        
        private Mock<IEmployeeSearchViewModelFactory> _employeeSearchViewModelFactory;
        private const int _companyId = 100;
        private List<long> _allowedSites;

        [SetUp]
        public void Setup()
        {
            _allowedSites = new List<long>() { 123, 456, 789};

            _employeeSearchViewModelFactory = new Mock<IEmployeeSearchViewModelFactory>();
            
            _employeeSearchViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_employeeSearchViewModelFactory.Object);
        }

        [Test]
        public void Given_get_When_search_employees_Then_should_return_correct_view()
        {
            // Given
            _employeeSearchViewModelFactory
                .Setup(x => x.WithEmployeeReference(It.IsAny<string>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithForeName(It.IsAny<string>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithSurname(It.IsAny<string>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithAllowedSites(It.IsAny<List<long>>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithShowDeleted(false))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
             .Setup(x => x.WithCurrentUser(It.IsAny<IPrincipal>()))
             .Returns(_employeeSearchViewModelFactory.Object);

            
            var controller = CreateEmployeeSearchControllerWithUserAndFactory(_employeeSearchViewModelFactory.Object);

            // When
            var result = controller.Index(_companyId, null) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void Given_get_When_search_employees_Then_should_return_correct_viewmodel()
        {
            // Given
            var expectedViewModel = EmployeeSearchViewModelBuilder
                                            .Create()
                                            .Build();

            
            _employeeSearchViewModelFactory
                .Setup(x => x.WithEmployeeReference(It.IsAny<string>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithAllowedSites(It.IsAny<List<long>>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithForeName(It.IsAny<string>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithSurname(It.IsAny<string>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithShowDeleted(false))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
              .Setup(x => x.WithCurrentUser(It.IsAny<IPrincipal>()))
              .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                           .Setup(x => x.GetViewModel())
                           .Returns(expectedViewModel);

            var controller = CreateEmployeeSearchControllerWithUserAndFactory(_employeeSearchViewModelFactory.Object);

            // When
            var result = controller.Index(_companyId, null) as ViewResult;

            // Then
            Assert.That(result.ViewData.Model, Is.EqualTo(expectedViewModel));
        }

        [Test]
        public void Given_get_When_search_employees_Then_should_callcorrect_viewmodel()
        {
            // Given

            var expectedViewModel = EmployeeSearchViewModelBuilder
                                            .Create()
                                            .Build();


            const string employeeReference = "Reference";
            _employeeSearchViewModelFactory
                .Setup(x => x.WithEmployeeReference(employeeReference))
                .Returns(_employeeSearchViewModelFactory.Object);

            const string forename = "Forname";
            _employeeSearchViewModelFactory
                .Setup(x => x.WithForeName(forename))
                .Returns(_employeeSearchViewModelFactory.Object);

            const string surname = "Surname";
            _employeeSearchViewModelFactory
                .Setup(x => x.WithSurname(surname))
                .Returns(_employeeSearchViewModelFactory.Object);

            const int siteId = 1;
            _employeeSearchViewModelFactory
                .Setup(x => x.WithSiteId(siteId))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithAllowedSites(It.IsAny<List<long>>()))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
                .Setup(x => x.WithShowDeleted(false))
                .Returns(_employeeSearchViewModelFactory.Object);

            _employeeSearchViewModelFactory
             .Setup(x => x.WithCurrentUser(It.IsAny<IPrincipal>()))
             .Returns(_employeeSearchViewModelFactory.Object);


            _employeeSearchViewModelFactory
                           .Setup(x => x.GetViewModel())
                           .Returns(expectedViewModel);

            var controller = CreateEmployeeSearchControllerWithUserAndFactory(_employeeSearchViewModelFactory.Object);

            // When
            controller.Index(_companyId, employeeReference, forename,surname,siteId);

            // Then
            _employeeSearchViewModelFactory.VerifyAll();
        }

        // stinky tests as testing two things, but required to ensure allowed sites are used in site retrieval
        [Test]
        public void When_GetViewModel_Then_populate_viewmodel_from_allowed_sites()
        {
            // Given
            var employeeService = new Mock<IEmployeeService>();
            employeeService.Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>())).Returns(new List<EmployeeDto>());

            var passedSearchSitesRequest = new SearchSitesRequest();
            var siteService = new Mock<ISiteService>();
            siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Callback<SearchSitesRequest>(y => passedSearchSitesRequest = y)
                .Returns(new List<SiteDto>());

            var lookupService = new Mock<ILookupService>();
            lookupService.Setup(x => x.GetEmploymentStatuses()).Returns(new List<LookupDto>());

            var sessionManager = new Mock<IBusinessSafeSessionManager>();
            var session = new Mock<ISession>();
            sessionManager.SetupGet(x => x.Session).Returns(session.Object);

            var controller = CreateEmployeeSearchControllerWithUserAndFactory(new EmployeeSearchViewModelFactory(employeeService.Object, siteService.Object, lookupService.Object, sessionManager.Object));

            const string employeeReference = "Reference";
            const string forename = "Forname";
            const string surname = "Surname";
            const int siteId = 1;

            // When
            controller.Index(_companyId, employeeReference, forename, surname, siteId);

            // Then
            siteService.Verify(x => x.Search(It.IsAny<SearchSitesRequest>()));
            Assert.That(passedSearchSitesRequest.CompanyId, Is.EqualTo(_companyId));
            Assert.That(passedSearchSitesRequest.PageLimit, Is.EqualTo(100));
            Assert.That(passedSearchSitesRequest.AllowedSiteIds, Is.EqualTo(_allowedSites));
        }

        [Test]
        public void Given_GetViewModel_When_no_specific_siteId_requested_Then_Employee_Search_only_requests_those_from_allowed_sites()
        {
            // Given
            var siteService = new Mock<ISiteService>();
            siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(new List<SiteDto>());

            var passedSearchEmployeesRequest = new SearchEmployeesRequest();
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Callback<SearchEmployeesRequest>(y => passedSearchEmployeesRequest = y)
                .Returns(new List<EmployeeDto>());

            var lookupService = new Mock<ILookupService>();
            lookupService.Setup(x => x.GetEmploymentStatuses()).Returns(new List<LookupDto>());

            var sessionManager = new Mock<IBusinessSafeSessionManager>();
            var session = new Mock<ISession>();
            sessionManager.SetupGet(x => x.Session).Returns(session.Object);

            var controller = CreateEmployeeSearchControllerWithUserAndFactory(new EmployeeSearchViewModelFactory(employeeService.Object, siteService.Object, lookupService.Object, sessionManager.Object));

            const string employeeReference = "Reference";
            const string forename = "Forname";
            const string surname = "Surname";

            // When
            controller.Index(_companyId, employeeReference, forename, surname);

            // Then
            Assert.That(passedSearchEmployeesRequest.SiteIds, Is.EqualTo(_allowedSites));
            Assert.That(passedSearchEmployeesRequest.IncludeSiteless, Is.True);
        }

        [Test]
        public void Given_GetViewModel_When_specific_siteId_requested_Then_Employee_Search_only_requests_requested_siteId()
        {
            // Given
            var siteService = new Mock<ISiteService>();
            siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(new List<SiteDto>());

            var passedSearchEmployeesRequest = new SearchEmployeesRequest();
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(x => x.Search(It.IsAny<SearchEmployeesRequest>()))
                .Callback<SearchEmployeesRequest>(y => passedSearchEmployeesRequest = y)
                .Returns(new List<EmployeeDto>());

            var lookupService = new Mock<ILookupService>();
            lookupService.Setup(x => x.GetEmploymentStatuses()).Returns(new List<LookupDto>());

            var sessionManager = new Mock<IBusinessSafeSessionManager>();
            var session = new Mock<ISession>();
            sessionManager.SetupGet(x => x.Session).Returns(session.Object);

            var controller = CreateEmployeeSearchControllerWithUserAndFactory(new EmployeeSearchViewModelFactory(employeeService.Object, siteService.Object, lookupService.Object, sessionManager.Object));

            const string employeeReference = "Reference";
            const string forename = "Forname";
            const string surname = "Surname";
            const long siteId = 100;

            // When
            controller.Index(_companyId, employeeReference, forename, surname, siteId);

            // Then
            Assert.That(passedSearchEmployeesRequest.SiteIds.Length, Is.EqualTo(1));
            Assert.That(passedSearchEmployeesRequest.SiteIds[0], Is.EqualTo(siteId));
            Assert.That(passedSearchEmployeesRequest.IncludeSiteless, Is.False);
        }




        
        private EmployeeSearchController CreateEmployeeSearchControllerWithUserAndFactory(IEmployeeSearchViewModelFactory viewModelFactory)
        {
            var controller = new EmployeeSearchController(viewModelFactory, null);
            return TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(controller, _allowedSites);
        }
    }
}