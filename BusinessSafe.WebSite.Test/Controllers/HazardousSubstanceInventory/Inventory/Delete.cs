using System.Web.Mvc;

using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using BusinessSafe.WebSite.ViewModels;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    [Category("Unit")]
    public class CanDeleteHazardousSubstanceTests : HazardousSubtanceInventoryTest
    {
        private long _companyId;
        private long _hazardousSubstanceId;


        [SetUp]
        public void SetUp()
        {
            _companyId = 1;
            _hazardousSubstanceId = 2;

            base.Setup();
        }

        [Test]
        public void Given_request_which_cant_be_deleted_When_CanDeleteHazardousSubstance_Then_should_return_correct_result()
        {
            //Given
            var viewModel = new CanDeleteHazardousSubstanceViewModel()
                                {
                                    CompanyId = _companyId,
                                    HazardousSubstanceId = _hazardousSubstanceId
                                };

            hazardousSubstancesService
                .Setup(x => x.HasHazardousSubstanceGotRiskAssessments(viewModel.HazardousSubstanceId, viewModel.CompanyId))
                .Returns(true);

            //Get
            var result = target.CanDeleteHazardousSubstance(viewModel) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("CanDeleteHazardousSubstance = False"));
        }

        [Test]
        public void Given_request_which_can_be_deleted_When_CanDeleteHazardousSubstance_Then_should_return_correct_result()
        {
            //Given
            var viewModel = new CanDeleteHazardousSubstanceViewModel()
            {
                CompanyId = _companyId,
                HazardousSubstanceId = _hazardousSubstanceId
            };

            hazardousSubstancesService
                .Setup(x => x.HasHazardousSubstanceGotRiskAssessments(viewModel.HazardousSubstanceId, viewModel.CompanyId))
                .Returns(false);

            //Get
            var result = target.CanDeleteHazardousSubstance(viewModel) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("CanDeleteHazardousSubstance = True"));
        }


        [Test]
        public void Given_valid_request_When_Delete_Then_should_call_correct_methods()
        {
            // Given
            var viewModel = new CanDeleteHazardousSubstanceViewModel()
            {
                CompanyId = _companyId,
                HazardousSubstanceId = _hazardousSubstanceId
            };

            hazardousSubstancesService
               .Setup(x => x.HasHazardousSubstanceGotRiskAssessments(viewModel.HazardousSubstanceId, viewModel.CompanyId))
               .Returns(false);


            // When
            target.CanDeleteHazardousSubstance(viewModel);

            // Then
            hazardousSubstancesService.VerifyAll();
        }
    }
}