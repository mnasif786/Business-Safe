using System;
using System.Web.Mvc;

using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceInventory.Inventory
{
    [TestFixture]
    [Category("Unit")]
    public class MarkHazardousSubstanceAsDeletedTests : HazardousSubtanceInventoryTest
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
        public void Given_invalid_request_companyId_not_set_When_Delete_Then_should_throw_correct_exception()
        {
            //Given
            _companyId = 0;

            //Get
            //Then
            Assert.Throws<ArgumentException>(() => target.MarkHazardousSubstanceAsDeleted(_companyId, _hazardousSubstanceId));

        }

        [Test]
        public void Given_invalid_request_documentId_not_set_When_Delete_Then_should_throw_correct_exception()
        {
            //Given
            //When
            //Then
            Assert.Throws<ArgumentException>(() => target.MarkHazardousSubstanceAsDeleted(_companyId, 0));

        }

        [Test]
        public void Given_valid_request_When_Delete_Then_should_return_correct_result()
        {
            //Given
            //Get
            var result = target.MarkHazardousSubstanceAsDeleted(_companyId, _hazardousSubstanceId) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("Success = True"));
        }


        [Test]
        public void Given_valid_request_When_Delete_Then_should_call_correct_methods()
        {
            // Given
            // When
            target.MarkHazardousSubstanceAsDeleted(_companyId, _hazardousSubstanceId);

            // Then
            hazardousSubstancesService.Verify(x => x.MarkForDelete(It.Is<MarkHazardousSubstanceAsDeleteRequest>(r => r.CompanyId == _companyId &&
                                                                                                          r.HazardousSubstanceId == _hazardousSubstanceId &&
                                                                                                          r.UserId == target.CurrentUser.UserId)));
        }
    }
}