using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyDefaultController
{
    [TestFixture]
    public class CanDeleteHazardTests
    {
        private Mock<ICompanyDefaultService> _companyDefaultService;
        
        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
        
        }

        [Test]
        public void When_CanDeleteHazard_Then_should_call_correct_methods()
        {
            // Given
            var target = CreateCompanyDefaultsController();
            const long hazardId = 300;
            const long companyId = 500;

            _companyDefaultService
                .Setup(x => x.CanDeleteHazard(hazardId, companyId))
                .Returns(true);

            // When
            target.CanDeleteHazard(hazardId, companyId);

            // Then
            _companyDefaultService.VerifyAll();
        }

        [Test]
        public void Given_cant_delete_hazard_in_use_When_CanDeleteHazard_Then_should_return_correct_result()
        {
            // Given
            var target = CreateCompanyDefaultsController();
            const long hazardId = 300;
            const long companyId = 500;

            _companyDefaultService
                .Setup(x => x.CanDeleteHazard(hazardId, companyId))
                .Returns(false);

            // When
            var result = target.CanDeleteHazard(hazardId, companyId);

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ CanDelete = False }"));
        }

        [Test]
        public void Given_can_delete_hazard_in_use_When_CanDeleteHazard_Then_should_return_correct_result()
        {
            // Given
            var target = CreateCompanyDefaultsController();
            const long hazardId = 300;
            const long companyId = 500;

            _companyDefaultService
                .Setup(x => x.CanDeleteHazard(hazardId, companyId))
                .Returns(true);

            // When
            var result = target.CanDeleteHazard(hazardId, companyId);

            // Then
            Assert.That(result.Data.ToString(), Contains.Substring("{ CanDelete = True }"));
        }


        private CompanyDefaultsController CreateCompanyDefaultsController()
        {
            var result = new CompanyDefaultsController(null, _companyDefaultService.Object, null,null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}