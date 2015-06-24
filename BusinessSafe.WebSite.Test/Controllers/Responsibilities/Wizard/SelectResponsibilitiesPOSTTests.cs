using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.Wizard
{
    [TestFixture]
    public class SelectResponsibilitiesPOSTTests
    {
        private WizardController _target;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_array_of_longs_posted_When_SelectResponsibilities_Then_redirect_to_GenerateResponsibilities_action()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.SelectResponsibilities(new long[3]);
            var redirectResult = result as RedirectToRouteResult;

            // Then
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            Assert.That(redirectResult.RouteValues["Action"], Is.EqualTo("GenerateResponsibilities"));
        }

        [Test]
        public void Given_empty_array_of_longs_posted_When_SelectResponsibilities_Then_redirect_to_SelectResponsibilities_action()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.SelectResponsibilities(new long[] { });
            var redirectResult = result as RedirectToRouteResult;

            // Then
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            Assert.That(redirectResult.RouteValues["Action"], Is.EqualTo("SelectResponsibilities"));
        }

        [Test]
        public void Given_null_array_of_longs_posted_When_SelectResponsibilities_Then_redirect_to_SelectResponsibilities_action()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.SelectResponsibilities(new long[0]);
            var redirectResult = result as RedirectToRouteResult;

            // Then
            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            Assert.That(redirectResult.RouteValues["Action"], Is.EqualTo("SelectResponsibilities"));
        }

        [Test]
        public void Given_empty_array_of_longs_posted_When_SelectResponsibilities_Then_put_message_in_tempdata()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.SelectResponsibilities(new long[] { }) as RedirectToRouteResult;

            // Then
            Assert.That(_target.TempData["alertMessage"], Is.EqualTo("Please select at least one Responsibility to generate"));
        }

        private WizardController GetTarget()
        {
            var controller = new WizardController(null,null, null, null, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
