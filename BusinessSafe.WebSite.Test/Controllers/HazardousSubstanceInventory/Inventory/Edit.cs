using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    public class Edit : HazardousSubtanceInventoryTest
    {
        private long _companyId = 1;
        private long _hazardousSubstanceId = 1;

        [SetUp]
        public void SetUp()
        {
            base.Setup();

            hazardousSubstanceViewModelFactory
               .Setup(x => x.WithHazardousSubstanceId(It.IsAny<long>()))
               .Returns(hazardousSubstanceViewModelFactory.Object);

            hazardousSubstanceViewModelFactory
              .Setup(x => x.GetViewModel())
              .Returns(new AddEditHazardousSubstanceViewModel());
            
        }

        [Test]
        public void Edit_returns_view()
        {
            // Given
            // When
            var result = target.Edit(_companyId, _hazardousSubstanceId);

            // Then
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Edit_returns_correct_viewmodel()
        {
            // Given
            // When
            var result = target.Edit(_companyId, _hazardousSubstanceId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<AddEditHazardousSubstanceViewModel>());
        }

        [Test]
        public void Given_Id_Edit_calls_correct_methods()
        {
            // Given

            // When
            var result = target.Edit(_companyId, _hazardousSubstanceId) as ViewResult;

            // Then
            hazardousSubstanceViewModelFactory.VerifyAll();
        }

    }
}