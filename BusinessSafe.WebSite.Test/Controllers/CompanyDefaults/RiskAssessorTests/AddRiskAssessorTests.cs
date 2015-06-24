using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyDefaults.RiskAssessorTests
{
    [TestFixture]
    public class AddRiskAssessorTests
    {
        private Mock<IAddEditRiskAssessorViewModelFactory> _addEditRiskAssessorViewModelFactory;
        private Mock<IRiskAssessorService> _riskAssessorService;
        private const long _newRiskAssessorId = 230680L;

        [SetUp]
        public void Setup()
        {
            _addEditRiskAssessorViewModelFactory = new Mock<IAddEditRiskAssessorViewModelFactory>();
            _riskAssessorService = new Mock<IRiskAssessorService>();
            _riskAssessorService.Setup(x => x.Create(It.IsAny<CreateEditRiskAssessorRequest>())).Returns(_newRiskAssessorId);
            _riskAssessorService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new RiskAssessorDto()
                         {
                             Id = 1234L,
                             Employee = new EmployeeDto()
                                        {
                                            FullName = "Rick Assessor"
                                        },
                            Site = new SiteDto()
                         });
        }

        [Test]
        public void When_Add_Then_return_json()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                Site = "Magaluf",
                SiteId = 1234L,
                EmployeeId = Guid.NewGuid()
            };
            var target = GetTarget();

            // When
            var result = target.Add(viewModel);

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void When_Add_Then_request_service_to_create_new_riskAssessor()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                Site = "Magaluf",
                SiteId = 1234L,
                EmployeeId = Guid.NewGuid()
            };
            var target = GetTarget();

            // When
            target.Add(viewModel);

            // Then
            _riskAssessorService.Verify(x => x.Create(It.Is<CreateEditRiskAssessorRequest>(
                y => 
                    y.CompanyId == TestControllerHelpers.CompanyIdAssigned &&
                    y.CreatingUserId == TestControllerHelpers.UserIdAssigned &&
                    y.DoNotSendReviewDueNotification == viewModel.DoNotSendReviewDueNotification &&
                    y.DoNotSendTaskCompletedNotifications == viewModel.DoNotSendTaskCompletedNotifications &&
                    y.DoNotSendTaskOverdueNotifications == viewModel.DoNotSendTaskOverdueNotifications &&
                    y.DoNotSendDueTomorrowNotification == viewModel.DoNotSendTaskDueTomorrowNotification &&
                    y.EmployeeId == viewModel.EmployeeId
            )));
        }

        [Test]
        public void When_Add_Then_return_new_riskAssessor_info()
        {
            // Given
            _riskAssessorService
                .Setup(x => x.GetByIdAndCompanyId(_newRiskAssessorId, TestControllerHelpers.CompanyIdAssigned))
                .Returns(new RiskAssessorDto()
                {
                    Id = _newRiskAssessorId,
                    Employee = new EmployeeDto()
                    {
                        Forename = "Jimbo",
                        Surname = "Jones"
                    },
                    Site = new SiteDto()
                });

            var viewModel = new AddEditRiskAssessorViewModel()
            {
                Site = "Magaluf",
                SiteId = 1234L,
                EmployeeId = Guid.NewGuid()
            };
            var target = GetTarget();

            // When
            dynamic result = target.Add(viewModel);

            // Then
            Assert.That(result.Data.Forename, Is.EqualTo("Jimbo"));
            Assert.That(result.Data.Surname, Is.EqualTo("Jones"));
            Assert.That(result.Data.RiskAssessorId, Is.EqualTo(_newRiskAssessorId));
        }

        [Test]
        public void Given_invalid_model_submission_When_Add_Then_return_errors_as_json()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                EmployeeId = Guid.NewGuid(),
                Site = "a site",
                SiteId = 1234L
            };
            var target = GetTarget();

            // When
            target.ModelState.AddModelError("error key", "error message");
            dynamic result = target.Add(viewModel);

            // Then
            Assert.That(bool.Parse(result.Data.Success), Is.False);
        }

        [Test]
        public void Given_no_site_id_and_no_site_name_When_Add_Then_return_error()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                EmployeeId = Guid.NewGuid(),
                Site = string.Empty,
                SiteId = null
            };
            var target = GetTarget();

            // When
            dynamic result = target.Add(viewModel);

            // Then
            Assert.That(bool.Parse(result.Data.Success), Is.False);
        }

        [Test]
        public void Given_no_employee_id_When_Add_Then_return_error()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                EmployeeId = Guid.NewGuid(),
                Site = string.Empty,
                SiteId = null
            };
            var target = GetTarget();

            // When
            dynamic result = target.Add(viewModel);

            // Then
            Assert.That(bool.Parse(result.Data.Success), Is.False);
        }

        private RiskAssessorController GetTarget()
        {
            var controller = new RiskAssessorController(_addEditRiskAssessorViewModelFactory.Object, _riskAssessorService.Object, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
