using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    public class Add : HazardousSubtanceInventoryTest
    {
        [SetUp]
        public void SetUp()
        {
            base.Setup();
            hazardousSubstanceViewModelFactory
              .Setup(x => x.GetViewModel())
              .Returns(new AddEditHazardousSubstanceViewModel());
        }

        [Test]
        public void Add_returns_view()
        {
            // Given

            // When
            var result = target.Add();

            // Then
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Add_calls_correct_method()
        {
            // Given
           

            // When
            var result = target.Add();

            // Then
            hazardousSubstanceViewModelFactory.VerifyAll();
        }

       
       
    }
}
