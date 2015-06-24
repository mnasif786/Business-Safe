using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.Wizard
{
    [TestFixture]
    public class GenerateResponsibilitiesGETTests
    {
        private WizardController _target;
        private Mock<IGenerateResponsibilitiesViewModelFactory> _generateResponsibilitiesViewModelFactory;
        private Mock<IGenerateResponsibilityTasksViewModelFactory> _generateResponsibilityTasksViewModelFactory;
        private GenerateResponsibilitiesViewModel _returnedViewModel;
        private List<long> _allowedSites;
        
        private long[] _selectedResponsibilityIds;

        [SetUp]
        public void Setup()
        {
            _allowedSites = new List<long> { 16585L, 43456L, 757589L };
            _selectedResponsibilityIds = new[] { 123L, 456L, 789L };

            _returnedViewModel = new GenerateResponsibilitiesViewModel();

            _generateResponsibilitiesViewModelFactory = new Mock<IGenerateResponsibilitiesViewModelFactory>();

            _generateResponsibilitiesViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(_returnedViewModel);

            _generateResponsibilitiesViewModelFactory
                .Setup(x => x.WithResponsibilityTemplateIds(It.IsAny<long[]>()))
                .Returns(_generateResponsibilitiesViewModelFactory.Object);

            _generateResponsibilitiesViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_generateResponsibilitiesViewModelFactory.Object);

            _generateResponsibilitiesViewModelFactory
                .Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
                .Returns(_generateResponsibilitiesViewModelFactory.Object);

            _generateResponsibilityTasksViewModelFactory = new Mock<IGenerateResponsibilityTasksViewModelFactory>();

            _generateResponsibilityTasksViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_generateResponsibilityTasksViewModelFactory.Object);
        }

        [Test]
        public void Given_array_of_responsibility_ids_When_GenerateResponsibilities_Then_return_ViewResult()
        {
            // Given
            _target = GetTarget();
            _target.TempData["selectedResponsibilities"] = _selectedResponsibilityIds;

            // When
            var result = _target.GenerateResponsibilities(string.Join(",", _selectedResponsibilityIds));

            var viewResult = result as ViewResult;

            // Then
            Assert.That(result,Is.InstanceOf<ViewResult>());
            Assert.That(viewResult.ViewName == String.Empty);
        }

        [Test]
        public void Given_array_of_responsibility_ids_When_GenerateResponsibilities_Then_pass_array_to_view_model_factory()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.GenerateResponsibilities(string.Join(",", _selectedResponsibilityIds));

            // Then
            _generateResponsibilitiesViewModelFactory.Verify(x => x.WithResponsibilityTemplateIds(_selectedResponsibilityIds));
        }

        [Test]
        public void Given_no_responsibility_ids_When_GenerateResponsibilities_Then_pass_null_to_view_model_factory()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.GenerateResponsibilities(string.Empty);

            // Then
            _generateResponsibilitiesViewModelFactory.Verify(x => x.WithResponsibilityTemplateIds(It.IsAny<long[]>()), Times.Never());
        }

        [Test]
        public void Given_array_of_responsibility_ids_When_GenerateResponsibilities_Then_pass_current_users_companyId_to_view_model_factory()
        {
            // Given
            _target = GetTarget();
            _target.TempData["selectedResponsibilities"] = _selectedResponsibilityIds;

            // When
            var result = _target.GenerateResponsibilities(string.Join(",", _selectedResponsibilityIds));

            // Then
            _generateResponsibilitiesViewModelFactory.Verify(x => x.WithCompanyId(TestControllerHelpers.CompanyIdAssigned));
        }

        [Test]
        public void Given_array_of_responsibility_ids_When_GenerateResponsibilities_Then_pass_current_users_allowed_sites_to_view_model_factory()
        {
            // Given
            _target = GetTarget();
            _target.TempData["selectedResponsibilities"] = _selectedResponsibilityIds;

            // When
            var result = _target.GenerateResponsibilities(string.Join(",", _selectedResponsibilityIds));

            // Then
            _generateResponsibilitiesViewModelFactory.Verify(x => x.WithAllowedSiteIds(_allowedSites));
        }

        [Test]
        public void Given_array_of_responsibility_ids_When_GenerateResponsibilities_Then_pass_model_from_factory_to_view()
        {
            // Given
            _target = GetTarget();
            _target.TempData["selectedResponsibilities"] = _selectedResponsibilityIds;

            // When
            var result = _target.GenerateResponsibilities(string.Join(",", _selectedResponsibilityIds));
            var viewResult = result as ViewResult;

            // Then
            Assert.That(viewResult.Model, Is.EqualTo(_returnedViewModel));
        }

        [Test]
        public void Given_null_array_of_longs_posted_When_GenerateResponsibilities_Then_redirect_to_SelectResponsibilities_action()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.GenerateResponsibilities(It.IsAny<string>());
            var redirectResult = result as RedirectToRouteResult;

            // Then
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            Assert.That(redirectResult.RouteValues["Action"], Is.EqualTo("SelectResponsibilities"));
        }

        [Test]
        public void Given_empty_array_of_longs_posted_When_GenerateResponsibilities_Then_put_message_in_tempdata()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.GenerateResponsibilities(It.IsAny<string>()) as RedirectToRouteResult;

            // Then
            Assert.That(_target.TempData["alertMessage"], Is.EqualTo("Please select at least one Responsibility to generate"));
        }

        private WizardController GetTarget()
        {
            var controller = new WizardController(null, _generateResponsibilitiesViewModelFactory.Object,
                                                  _generateResponsibilityTasksViewModelFactory.Object, null, null, null);
            return TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(controller, _allowedSites);
        }
    }
}
