using System;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    public class Save : HazardousSubtanceInventoryTest
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();

            hazardousSubstancesService.Setup(x => x.Add(It.IsAny<AddHazardousSubstanceRequest>()));

        }

        [Test]
        public void Save_passes_view_model_to_service()
        {
            // Given
            var saveHazardousSubstanceViewModel = new AddHazardousSubstanceRequest()
            {
                Name = "My HS",
                SdsDate = DateTime.Now
            };

            // When
            var result = target.Add(saveHazardousSubstanceViewModel);

            // Then
            hazardousSubstancesService.Verify(x => x.Add(saveHazardousSubstanceViewModel), Times.Once());
        }


        [Test]
        public void Save_attaches_current_user_id_and_users_companyId_view_model_to_service()
        {
            // Given
            var passedSaveHazardousSubstanceRequest = new AddHazardousSubstanceRequest();

            hazardousSubstancesService
                .Setup(x => x.Add(It.IsAny<AddHazardousSubstanceRequest>()))
                .Callback<AddHazardousSubstanceRequest>(y => passedSaveHazardousSubstanceRequest = y);

            var addHazardousSubstanceRequest = new AddHazardousSubstanceRequest()
            {
                Name = "My HS",
                SdsDate = DateTime.Now
            };

            // When
            var result = target.Add(addHazardousSubstanceRequest);

            // Then
            hazardousSubstancesService.Verify(x => x.Add(addHazardousSubstanceRequest), Times.Once());
            Assert.That(passedSaveHazardousSubstanceRequest.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(passedSaveHazardousSubstanceRequest.CompanyId, Is.GreaterThan(0));
        }

        [Test]
        public void Save_returns_correct_result()
        {
            // Given
            var saveHazardousSubstanceViewModel = new AddHazardousSubstanceRequest()
            {
                Name = "My HS",
                SdsDate = DateTime.Now
            };

            // When
            var result = target.Add(saveHazardousSubstanceViewModel) as RedirectToRouteResult;

            
            // Then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Inventory"));
            Assert.That(result.RouteValues.Values.Where(x => x.ToString() == "newHazardousSubstanceId"), Is.Not.Null);
        }

        [Test]
        public void Invalid_model_sent_to_save_displays_form_with_errors()
        {
            // Given
            var saveHazardousSubstanceViewModel = new AddHazardousSubstanceRequest()
            {
            };

            target.ModelState.AddModelError("", "fake error");

            // When
            var result = target.Add(saveHazardousSubstanceViewModel) as ViewResult;

            // Then
            hazardousSubstancesService.Verify(x => x.Add(saveHazardousSubstanceViewModel), Times.Never());
            Assert.That(result.ViewName, Is.EqualTo("Add"));
        }
    }
}
