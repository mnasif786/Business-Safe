using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.CompanyDefaults.RiskAssessorTests
{
    [TestFixture]
    public class MarkRiskAssessorAsUndeletedTests
    {
        private Mock<IRiskAssessorService> _riskAssessorService;
        private long _companyId;
        private long _riskAssessorId;

        [SetUp]
        public void Setup()
        {
            _riskAssessorService = new Mock<IRiskAssessorService>();
            _companyId = 123456L;
            _riskAssessorId = 888L;
        }

        [Test]
        public void When_MarkUndeleted_Then_should_calls_service()
        {
            // Given
            var target = GetTarget();

            // When
            target.MarkUndeleted(_companyId, _riskAssessorId);

            // Then
            _riskAssessorService
                .Verify(x => x.MarkUndeleted(It.Is<MarkRiskAssessorAsDeletedAndUndeletedRequest>(
                    y => y.CompanyId == _companyId &&
                         y.RiskAssessorId == _riskAssessorId)));
        }

        [Test]
        public void When_Markundeleted_Then_return_json_with_success_true()
        {
            // Given
            var target = GetTarget();

            // When
            dynamic result = target.MarkUndeleted(_companyId, _riskAssessorId);

            // Then
            Assert.IsTrue(result.Data.Success);
        }

        [TestCase(0L, 0L)]
        [TestCase(-1L, -1L)]
        [TestCase(888L, 0)]
        [TestCase(0, 17L)]
        public void Given_no_company_id_and_risk_assessor_id_When_Markundeleted_Then_throws_exception(long companyId, long riskAssessorId)
        {
            // Given
            var target = GetTarget();

            // When

            // Then
            var result = Assert.Throws<ArgumentException>(() => target.MarkUndeleted(companyId, riskAssessorId));
            Assert.That(result.Message, Is.EqualTo("Invalid riskAssessorId or companyId when trying to mark risk assessor as reinstated."));
        }

        private RiskAssessorController GetTarget()
        {
            var controller = new RiskAssessorController(null, _riskAssessorService.Object, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}
