using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    public class CheckForDuplicatedSubstance : HazardousSubtanceInventoryTest
    {

        [SetUp]
        public new void Setup()
        {
            base.Setup();
        }

        [Test]
        public void Check_for_duplicated_substance_performs_search_on_service_to_check()
        {
            // Given 
            var passedSearchHazardousSubstancesRequest = new SearchHazardousSubstancesRequest();
            var checkForDuplicatedSubstanceRequest = new CheckForDuplicatedSubstanceRequest()
                                                     {
                                                         CompanyId = 1234,
                                                         NewSubstanceName = "magic beans"
                                                     };
            hazardousSubstancesService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(new List<HazardousSubstanceDto>())
                .Callback<SearchHazardousSubstancesRequest>(y => passedSearchHazardousSubstancesRequest = y);

            // When
            var result = target.CheckForDuplicatedSubstance(checkForDuplicatedSubstanceRequest);

            // Then
            hazardousSubstancesService.Verify(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()), Times.Once());
            Assert.That(passedSearchHazardousSubstancesRequest.CompanyId, Is.EqualTo(checkForDuplicatedSubstanceRequest.CompanyId));
            Assert.That(passedSearchHazardousSubstancesRequest.SubstanceNameLike, Is.EqualTo(checkForDuplicatedSubstanceRequest.NewSubstanceName));
        }

        [Test]
        public void Check_for_duplicated_substance_returns_JsonResult_of_matches_equals_zero_if_no_matches()
        {
            // Given 
            var checkForDuplicatedSubstanceRequest = new CheckForDuplicatedSubstanceRequest();

            // When
            var result = target.CheckForDuplicatedSubstance(checkForDuplicatedSubstanceRequest);

            // Then
        }

        [Test]
        public void If_matches_found_check_for_duplicated_substance_returns_partialview()
        {
            // Given 
            var searchResults = new List<HazardousSubstanceDto>()
                                {
                                    new HazardousSubstanceDto()
                                };
            hazardousSubstancesService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(searchResults);

            // When
            var result = target.CheckForDuplicatedSubstance(new CheckForDuplicatedSubstanceRequest());
            var viewResult = result as PartialViewResult;

            // Then
            Assert.That(result, Is.InstanceOf<PartialViewResult>());
            Assert.That(viewResult.Model, Is.EqualTo(searchResults));
        }

        [Test]
        public void If_no_matches_found_check_for_duplicated_substance_returns_jsonresult()
        {
            // Given 

            // When
            var result = target.CheckForDuplicatedSubstance(new CheckForDuplicatedSubstanceRequest());
            var jsonResult = result as JsonResult;
            var data = jsonResult.Data as bool?;

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
            Assert.That(data, Is.EqualTo(true));
        }
    }
}
