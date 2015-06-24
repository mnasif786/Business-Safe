using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    public class RiskAssessmentImplementation: RiskAssessment
    {

        public override string PreFix
        {
            get { throw new NotImplementedException(); }
        }

        public override bool HasUndeletedTasks()
        {
            throw new NotImplementedException();
        }

        public override bool HasUncompletedTasks()
        {
            throw new NotImplementedException();
        }

        public override bool HasAnyReviews()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<FurtherControlMeasureTask> GetAllUncompleteFurtherControlMeasureTasks()
        {
            throw new NotImplementedException();
        }

        public Boolean IsDifferentRiskAssessor(RiskAssessor riskAssessor)
        {
            return base.IsDifferentRiskAssessor(riskAssessor);
        }

        public override RiskAssessment Copy(string newTitle, string newReference, UserForAuditing user)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class IsDifferentRiskAssessorTests
    {
        [Test]
        public void Given_current_risk_assessor_is_null_and_compared_to_null_value_When_IsDifferentRiskAssessor_then_return_false()
        {
            //given
            var riskAss = new RiskAssessmentImplementation();

            var result = riskAss.IsDifferentRiskAssessor(null);

            Assert.IsFalse(result);
        }

        [Test]
        public void Given_current_risk_assessor_is_not_null_and_compared_to_null_value_When_IsDifferentRiskAssessor_then_return_true()
        {
            //given
            var riskAss = new RiskAssessmentImplementation();
            riskAss.RiskAssessor = new RiskAssessor() {Id = 123};

            var result = riskAss.IsDifferentRiskAssessor(null);

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_current_risk_assessor_is_null_and_compared_to_risk_assessor_When_IsDifferentRiskAssessor_then_return_true()
        {
            //given
            var riskAss = new RiskAssessmentImplementation();
            riskAss.RiskAssessor = null;

            var result = riskAss.IsDifferentRiskAssessor(new RiskAssessor(){Id =96345});

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_current_risk_assessor_is_not_null_and_compared_to_different_risk_assessor_When_IsDifferentRiskAssessor_then_return_true()
        {
            //given
            var riskAss = new RiskAssessmentImplementation();
            riskAss.RiskAssessor = new RiskAssessor() { Id = 14 };

            var result = riskAss.IsDifferentRiskAssessor(new RiskAssessor() { Id = 96345 });

            Assert.IsTrue(result);
        }

        [Test]
        public void Given_current_risk_assessor_is_not_null_and_compared_to_same_risk_assessor_When_IsDifferentRiskAssessor_then_return_false()
        {
            //given
            var riskAss = new RiskAssessmentImplementation();
            riskAss.RiskAssessor = new RiskAssessor() { Id = 14 };

            var result = riskAss.IsDifferentRiskAssessor(new RiskAssessor() { Id = 14 });

            Assert.IsFalse(result);
        }
    }
}
