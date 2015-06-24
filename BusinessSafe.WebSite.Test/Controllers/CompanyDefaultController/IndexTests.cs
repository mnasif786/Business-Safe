using System.Collections.Generic;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyDefaultControllerTests
{
    [TestFixture]
    public class IndexTests
    {
        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<INonEmployeeService> _nonEmployeeService;
        private Mock<ICompanyDefaultsTaskFactory> _companyDefaultsTaskFactory;
        private Mock<ISuppliersService> _suppliersService;
        private Mock<ISiteService> _siteService;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _nonEmployeeService = new Mock<INonEmployeeService>();
            _companyDefaultsTaskFactory = new Mock<ICompanyDefaultsTaskFactory>();
            _suppliersService = new Mock<ISuppliersService>();
            _siteService = new Mock<ISiteService>();
            _companyId = 200;
        }

        [Test]
        public void When_Index_get_Then_should_call_the_correct_methods()
        {
            // Given
            var target = CreateCompanyDefaultsController();

            var hazards = new List<CompanyDefaultDto>();
            _companyDefaultService.Setup(x => x.GetAllHazardsForCompany(_companyId)).Returns(hazards);

            var peopleAtRisk = new List<CompanyDefaultDto>();
            _companyDefaultService.Setup(x => x.GetAllPeopleAtRiskForCompany(_companyId)).Returns(peopleAtRisk);

            var nonEmployees = new NonEmployeeDto[]{};
            _nonEmployeeService.Setup(x => x.GetAllNonEmployeesForCompany(_companyId)).Returns(nonEmployees);

            var suppliers = new List<SupplierDto>();
            _suppliersService.Setup(x => x.GetForCompany(_companyId)).Returns(suppliers);

            // When
            target.Index(_companyId);

            // Then
            _nonEmployeeService.VerifyAll();
            _companyDefaultService.VerifyAll();
            _suppliersService.VerifyAll();
        }

        private CompanyDefaultsController CreateCompanyDefaultsController()
        {
            var result = new CompanyDefaultsController(
                _nonEmployeeService.Object, 
                _companyDefaultService.Object, 
                _companyDefaultsTaskFactory.Object, 
                _suppliersService.Object,
                _siteService.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
