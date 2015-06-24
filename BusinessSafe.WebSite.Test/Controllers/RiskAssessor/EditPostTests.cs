using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using NUnit.Framework;
using Moq;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessor
{
    [TestFixture]
    public class EditPostTests
    {
        private Mock<IAddEditRiskAssessorViewModelFactory> _addEditRiskAssessorViewModelFactory;
        private Mock<IRiskAssessorService> _riskAssessorService;
        private Mock<IEmployeeService> _employeeService;
        private const long _newRiskAssessorId = 230680L;
        private RiskAssessorDto _returnedRiskAssessor;

        [SetUp]
        public void Setup()
        {
            _addEditRiskAssessorViewModelFactory = new Mock<IAddEditRiskAssessorViewModelFactory>();
            _riskAssessorService = new Mock<IRiskAssessorService>();
            _employeeService = new Mock<IEmployeeService>();

            _returnedRiskAssessor = new RiskAssessorDto()
                                        {
                                            Id = 1234L,
                                            Employee = new EmployeeDto()
                                                           {
                                                               FullName = "Rick Assessor",
                                                               Forename = "Rick",
                                                               Surname = "Assessor"
                                                           },
                                            Site = new SiteDto()
                                                       {
                                                           Id = 1234L,
                                                           Name = "Aberystwyth"
                                                       }
                                        };

            _riskAssessorService.Setup(x => x.Update(It.IsAny<CreateEditRiskAssessorRequest>()));
            _riskAssessorService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_returnedRiskAssessor);
        }

        [Test]
        public void When_Edit_Then_return_json()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                Site = "Llandundo",
                SiteId = 1234L,
                EmployeeId = Guid.NewGuid(),
                RiskAssessorId = 555L
            };
            var target = GetTarget();

            // When
            var result = target.Edit(viewModel);

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
        }

        [Test]
        public void Given_invalid_model_submission_When_Add_Then_return_errors_as_json()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                EmployeeId = Guid.NewGuid(),
                Site = "a site",
                SiteId = 1234L,
                RiskAssessorId = 555L
            };
            var target = GetTarget();

            // When
            target.ModelState.AddModelError("error key", "error message");
            dynamic result = target.Edit(viewModel);

            // Then
            Assert.That(bool.Parse(result.Data.Success), Is.False);
        }

        [Test]
        public void Given_not_HasAccessToAllSites_and_no_site_name_When_Add_Then_return_error()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                EmployeeId = Guid.NewGuid(),
                HasAccessToAllSites = false,
                SiteId = null,
                RiskAssessorId = 555L
            };
            var target = GetTarget();

            // When
            dynamic result = target.Edit(viewModel);

            // Then
            Assert.That(bool.Parse(result.Data.Success), Is.False);
        }

        [Test]
        public void Given_no_employee_id_When_Add_Then_return_error()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                EmployeeId = null,
                Site = "Southport",
                SiteId = 888L,
                RiskAssessorId = 555L
            };
            var target = GetTarget();

            // When
            dynamic result = target.Edit(viewModel);

            // Then
            Assert.That(bool.Parse(result.Data.Success), Is.False);
        }

        [Test]
        public void Given_site_id_and_HasAccessToAllSites_When_Add_Then_return_error()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
            {
                EmployeeId = Guid.NewGuid(),
                HasAccessToAllSites = true,
                SiteId = 123L,
                RiskAssessorId = 555L
            };
            var target = GetTarget();

            // When
            dynamic result = target.Edit(viewModel);

            // Then
            Assert.That(bool.Parse(result.Data.Success), Is.False);
        }

        [Test]
        public void Given_site_selected_When_Edit_Then_site_id_and_risk_assessor_are_passed_to_service()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel()
                                {
                                    DoNotSendReviewDueNotification = true,
                                    DoNotSendTaskOverdueNotifications = true,
                                    DoNotSendTaskCompletedNotifications = true,
                                    DoNotSendTaskDueTomorrowNotification = true,
                                    EmployeeId = Guid.NewGuid(),
                                    SiteId = 888L,
                                    Site = "Vatican",
                                    HasAccessToAllSites = false,
                                    RiskAssessorId = 555L
                                };
            var target = GetTarget();

            // When
            var result = target.Edit(viewModel);

            // Then
            _riskAssessorService.Verify(x => x.Update(It.Is<CreateEditRiskAssessorRequest>(
                y =>
                    y.CompanyId == TestControllerHelpers.CompanyIdAssigned &&
                    y.CreatingUserId == TestControllerHelpers.UserIdAssigned &&
                    y.DoNotSendReviewDueNotification == viewModel.DoNotSendReviewDueNotification &&
                    y.DoNotSendTaskCompletedNotifications == viewModel.DoNotSendTaskCompletedNotifications &&
                    y.DoNotSendTaskOverdueNotifications == viewModel.DoNotSendTaskOverdueNotifications &&
                    y.EmployeeId == viewModel.EmployeeId &&
                    y.SiteId == viewModel.SiteId &&
                    y.RiskAssessorId == viewModel.RiskAssessorId
            )));
        }

        [Test]
        public void Given_no_riskAssessor_id_When_Edit_Then_throw_exception()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel
                                {
                                    DoNotSendReviewDueNotification = true,
                                    DoNotSendTaskOverdueNotifications = true,
                                    DoNotSendTaskCompletedNotifications = true,
                                    DoNotSendTaskDueTomorrowNotification = true,
                                    EmployeeId = Guid.NewGuid(),
                                    SiteId = 888L,
                                    Site = "Powys",
                                    HasAccessToAllSites = false,
                                    RiskAssessorId = (long?)null
                                };
            var target = GetTarget();

            // When


            // Then
            Assert.Throws<ArgumentNullException>(() => target.Edit(viewModel));
        }

        [Test]
        public void Given_site_selected_When_Edit_Then_return_RiskAssessor_details_in_json()
        {
            // Given
            var viewModel = new AddEditRiskAssessorViewModel
            {
                RiskAssessorId = 1234L,
                DoNotSendReviewDueNotification = true,
                DoNotSendTaskOverdueNotifications = true,
                DoNotSendTaskCompletedNotifications = true,
                DoNotSendTaskDueTomorrowNotification = true,
                EmployeeId = Guid.NewGuid(),
                SiteId = 888L,
                Site = "Vatican",
                HasAccessToAllSites = false,
            };
            var target = GetTarget();

            // When
            dynamic result = target.Edit(viewModel);

            // Then
            Assert.That(result.Data.Success, Is.True);
            Assert.That(result.Data.RiskAssessorId, Is.EqualTo(viewModel.RiskAssessorId.Value));
            Assert.That(result.Data.Forename, Is.EqualTo(_returnedRiskAssessor.Employee.Forename));
            Assert.That(result.Data.Surname, Is.EqualTo(_returnedRiskAssessor.Employee.Surname));
            Assert.That(result.Data.DoNotSendReviewDueNotification, Is.EqualTo(_returnedRiskAssessor.DoNotSendReviewDueNotification));
            Assert.That(result.Data.DoNotSendTaskCompletedNotifications, Is.EqualTo(_returnedRiskAssessor.DoNotSendTaskCompletedNotifications));
            Assert.That(result.Data.DoNotSendTaskOverdueNotifications, Is.EqualTo(_returnedRiskAssessor.DoNotSendTaskOverdueNotifications));
          
            Assert.That(result.Data.FormattedName, Is.EqualTo(_returnedRiskAssessor.Employee.FullName));
        }

        private RiskAssessorController GetTarget()
        {
            var controller = new RiskAssessorController(_addEditRiskAssessorViewModelFactory.Object, _riskAssessorService.Object, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
