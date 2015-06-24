using System.Collections.Generic;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Suppliers
{
    [TestFixture]
    [Category("Unit")]
    public class CanDeleteSupplierTests
    {
        private Mock<IHazardousSubstancesService> _hazardousSubstanceService;
        private long supplierId;

        [SetUp]
        public void SetUp()
        {
            _hazardousSubstanceService = new Mock<IHazardousSubstancesService>();
            supplierId = 200;

        }

        [Test]
        public void Given_a_supplied_supplier_id_When_CanDeleteSupplier_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateController();
            var companyId = target.CurrentUser.CompanyId;

            //When
            target.CanDeleteSupplier(supplierId);

            //Then            
            _hazardousSubstanceService.Verify(x => x.HasSupplierGotHazardousSubstances(supplierId, companyId));
        }
        
        private SupplierController CreateController()
        {
            var result = new SupplierController(null, _hazardousSubstanceService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
