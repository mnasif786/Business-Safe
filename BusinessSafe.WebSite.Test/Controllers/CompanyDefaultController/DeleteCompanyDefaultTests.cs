using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyDefaultController
{
    [TestFixture]
    public class DeleteCompanyDefaultTests
    {
         private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<INonEmployeeService> _nonEmployeeService;
        private Mock<ICompanyDefaultsTaskFactory> _companyDefaultsTaskFactory;
        private Mock<ISuppliersService> _suppliersService;
        private Mock<IRiskAssessorService> _riskAssessorService;
    
        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _nonEmployeeService = new Mock<INonEmployeeService>();
            _companyDefaultsTaskFactory = new Mock<ICompanyDefaultsTaskFactory>();
            _suppliersService = new Mock<ISuppliersService>();
            _riskAssessorService = new Mock<IRiskAssessorService>();
     }

        [Test]
        public void Given_valid_viewmodel_When_delete_company_default_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateCompanyDefaultsController();

            var viewModel = new DeleteCompanyDefaultViewModel()
            {
                CompanyId = 100,
                CompanyDefaultId = 3,
                CompanyDefaultType = "Hazards",
            };

            var mockDeleteTask = new Mock<ICompanyDefaultDeleteTask>();
            _companyDefaultsTaskFactory
                .Setup(x => x.CreateMarkForDeletedTask(viewModel.CompanyDefaultType))
                .Returns(mockDeleteTask.Object);

            var userId = target.CurrentUser.UserId;
           
            // When
            target.DeleteCompanyDefault(viewModel);

            // Then
            _companyDefaultsTaskFactory.VerifyAll();
            mockDeleteTask.Verify(y=>y.Execute(viewModel.CompanyDefaultId, viewModel.CompanyId, userId), Times.Once());
        }

        [Test]
        public void Given_default_deleted_successfully_Then_should_return_correct_result()
        {
             // Given
            var target = CreateCompanyDefaultsController();

            var viewModel = new DeleteCompanyDefaultViewModel()
            {
                CompanyId = 100,
                CompanyDefaultId = 3,
                CompanyDefaultType = "Hazards",
            };

            var mockDeleteTask = new Mock<ICompanyDefaultDeleteTask>();
            _companyDefaultsTaskFactory
                .Setup(x => x.CreateMarkForDeletedTask(viewModel.CompanyDefaultType))
                .Returns(mockDeleteTask.Object);

            // When
            var result = target.DeleteCompanyDefault(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Id = 3, Success = True }"));
        }

        private CompanyDefaultsController CreateCompanyDefaultsController()
        {
            var result = new CompanyDefaultsController(
                _nonEmployeeService.Object, 
                _companyDefaultService.Object,
                _companyDefaultsTaskFactory.Object, 
                _suppliersService.Object,
                null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
