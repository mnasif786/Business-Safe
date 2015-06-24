using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Controllers;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyDefaults.RiskAssessorTests
{
    [TestFixture]
    public class MarkRiskAssessorAsDeletedTests
    {
        private Mock<IRiskAssessorService> _riskAssessorService;
        private long _companyId;
        private long _riskAssessorId;
        
        [SetUp]
        public void Setup()
        {
            _riskAssessorService = new Mock<IRiskAssessorService>();
            _companyId = 200L;
            _riskAssessorId = 100L;
        }

        [Test]
        public void When_MarkRiskAssessorAsDeleted_Then_should_call_correct_methods()
        {
            // Given
            var target = GetTarget();

            // When
            target.MarkDeleted(_companyId, _riskAssessorId);

            // Then
            _riskAssessorService
                .Verify(x => x.MarkDeleted(It.Is<MarkRiskAssessorAsDeletedAndUndeletedRequest>(
                    y => y.CompanyId == _companyId && 
                         y.RiskAssessorId == _riskAssessorId)));
        }

        [Test]
        public void When_MarkRiskAssessorAsDeleted_Then_should_return_correct_result()
        {
            // Given
            var target = GetTarget();

            // When
            dynamic result = target.MarkDeleted(_companyId, _riskAssessorId);

            // Then
            Assert.That(result.Data.Success, Is.True);
        }

        [TestCase(0L, 0L)]
        [TestCase(-1L, -1L)]
        [TestCase(888L, 0)]
        [TestCase(0, 17L)]
        public void Given_no_company_id_and_risk_assessor_id_When_MarkDeleted_Then_throws_exception(long companyId, long riskAssessorId)
        {
            // Given
            var target = GetTarget();

            // When

            // Then
            var result = Assert.Throws<ArgumentException>(() => target.MarkDeleted(companyId, riskAssessorId));
            Assert.That(result.Message, Is.EqualTo("Invalid riskAssessorId or companyId when trying to mark risk assessor as deleted."));
        }

        private RiskAssessorController GetTarget()
        {
            var controller = new RiskAssessorController(null, _riskAssessorService.Object, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}