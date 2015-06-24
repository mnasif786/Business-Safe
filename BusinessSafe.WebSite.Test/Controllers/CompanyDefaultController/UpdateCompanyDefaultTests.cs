using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using Moq;
using NUnit.Framework;


namespace BusinessSafe.WebSite.Tests.Controllers.CompanyDefaultController
{
    [TestFixture]
    public class UpdateCompanyDefaultTests
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
        public void Given_invalid_viewmodel_no_default_value_When_UpdateCompanyDefault_Then_should_return_correct_result()
        {
            // Given
            var target = CreateCompanyDefaultsController();
            var viewModel = new SaveCompanyDefaultViewModel()
            {
                CompanyDefaultValue = ""
            };

            // When
            var result = target.UpdateCompanyDefaults(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = False, Message = Value must be provided. }"));
        }

        [Test]
        public void Given_valid_viewmodel_When_UpdateCompanyDefault_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateCompanyDefaultsController();
            var userId = target.CurrentUser.UserId;
            var viewModel = new SaveCompanyDefaultViewModel()
            {
                CompanyDefaultId = 100,
                CompanyDefaultValue = "Some Value",
                CompanyDefaultType = "Hazards"
            };

            var mockSaveTask = new Mock<ICompanyDefaultsSaveTask>();
            _companyDefaultsTaskFactory
                .Setup(x => x.CreateSaveTask(viewModel.CompanyDefaultType))
                .Returns(mockSaveTask.Object);

            mockSaveTask
                .Setup(x => x.Execute(It.Is<SaveCompanyDefaultViewModel>(y => y.CompanyDefaultValue == viewModel.CompanyDefaultValue &&
                                                                                     y.CompanyDefaultId == viewModel.CompanyDefaultId &&
                                                                                     y.CompanyId == viewModel.CompanyId &&
                                                                                     y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                     y.RunMatchCheck == viewModel.RunMatchCheck), userId))
                .Returns(new CompanyDefaultSaveResponse());


            // When
            target.UpdateCompanyDefaults(viewModel);

            // Then
            _companyDefaultsTaskFactory.VerifyAll();
            mockSaveTask.VerifyAll();
        }

        [Test]
        public void Given_save_task_fails_When_UpdateCompanyDefault_Then_should_call_correct_result()
        {
            // Given
            var target = CreateCompanyDefaultsController();
            var userId = target.CurrentUser.UserId;
            var viewModel = new SaveCompanyDefaultViewModel()
            {
                CompanyDefaultId = 100,
                CompanyDefaultValue = "Some Value",
                CompanyDefaultType = "Hazards"
            };

            var mockSaveTask = new Mock<ICompanyDefaultsSaveTask>();
            _companyDefaultsTaskFactory
                .Setup(x => x.CreateSaveTask(viewModel.CompanyDefaultType))
                .Returns(mockSaveTask.Object);

            mockSaveTask
                .Setup(x => x.Execute(It.Is<SaveCompanyDefaultViewModel>(y => y.CompanyDefaultValue == viewModel.CompanyDefaultValue &&
                                                                                     y.CompanyDefaultId == viewModel.CompanyDefaultId &&
                                                                                     y.CompanyId == viewModel.CompanyId &&
                                                                                     y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                     y.RunMatchCheck == viewModel.RunMatchCheck), userId))
                .Returns(new CompanyDefaultSaveResponse());


            // When
            var result = target.UpdateCompanyDefaults(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("Success = False"));
        }

        [Test]
        public void Given_save_task_succeeds_When_UpdateCompanyDefault_Then_should_call_correct_result()
        {
            // Given
            var target = CreateCompanyDefaultsController();
            var userId = target.CurrentUser.UserId;
            var viewModel = new SaveCompanyDefaultViewModel()
            {
                CompanyDefaultId = 100,
                CompanyDefaultValue = "Some Value",
                CompanyDefaultType = "Hazards"
            };

            var mockSaveTask = new Mock<ICompanyDefaultsSaveTask>();
            _companyDefaultsTaskFactory
                .Setup(x => x.CreateSaveTask(viewModel.CompanyDefaultType))
                .Returns(mockSaveTask.Object);

            mockSaveTask
                .Setup(x => x.Execute(It.Is<SaveCompanyDefaultViewModel>(y => y.CompanyDefaultValue == viewModel.CompanyDefaultValue &&
                                                                                     y.CompanyId == viewModel.CompanyId &&
                                                                                     y.CompanyDefaultId == viewModel.CompanyDefaultId &&
                                                                                     y.RiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                     y.RunMatchCheck == viewModel.RunMatchCheck), userId))
                .Returns(CompanyDefaultSaveResponse.CreateSavedSuccessfullyResponse(100));


            // When
            var result = target.UpdateCompanyDefaults(viewModel) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ Success = True, Id = 100 }"));
        }

        [Test]
        public void Given_valid_request_When_GetDefaultRiskAssessmentTypes_Then_should_call_correct_methods()
        {
            // Given
            const long companyId = 100;
            const long companyDefaultId = 3;

            var target = CreateCompanyDefaultsController();
            _companyDefaultService
                .Setup(x => x.GetHazardForCompany(companyId, companyDefaultId))
                .Returns(new CompanyHazardDto());

            // When
            target.GetDefaultRiskAssessmentTypes(companyId, companyDefaultId);

            // Then
            _companyDefaultService.VerifyAll();
        }

        [Test]
        public void Given_valid_request_When_GetDefaultRiskAssessmentTypes_Then_should_return_correct_result()
        {
            // Given
            const long companyId = 100;
            const long companyDefaultId = 3;

            var target = CreateCompanyDefaultsController();
            _companyDefaultService
                .Setup(x => x.GetHazardForCompany(companyId, companyDefaultId))
                .Returns(new CompanyHazardDto()
                             {
                                 IsGra=true,
                                 IsPra=true
                             });

            // When
            var result = target.GetDefaultRiskAssessmentTypes(companyId, companyDefaultId) as JsonResult;

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ IsGra = True, IsPra = True, IsFra = False, Success = True }"));
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
