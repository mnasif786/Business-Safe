using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Mappers
{
    [TestFixture]
    public class HazardousSubstanceRiskAssessmentDtoMapperTests
    {

        [Test]
        public void Given_hazard_substances_risk_assessment_with_any_mapping_then_created_by_is_not_null()
        {
            //Given
            var hazardousSubstanceRiskAssessment = new HazardousSubstanceRiskAssessment();
            hazardousSubstanceRiskAssessment.CreatedBy = new UserForAuditing
                                    {
                                        Id = Guid.NewGuid(),
                                        Employee =
                                            new EmployeeForAuditing() {Id = Guid.NewGuid(), Forename = "testf", Surname = "testS"}
                                    };
            var target = new HazardousSubstanceRiskAssessmentDtoMapper();

            //When
            var result = target.Map(hazardousSubstanceRiskAssessment);

            //Act
            Assert.IsNotNull(result.CreatedBy);
            
        }

        [Test]
        public void Given_hazard_substances_risk_assessment_has_reviews_with_mapping_then_reviews_are_populated()
        {
            //Given
            var hazardousSubstanceRiskAssessment = new HazardousSubstanceRiskAssessment();
            hazardousSubstanceRiskAssessment.Reviews = new List<RiskAssessmentReview>(){ new RiskAssessmentReview(){Id = 123123}};
            var target = new HazardousSubstanceRiskAssessmentDtoMapper();

            //When
            var result = target.Map(hazardousSubstanceRiskAssessment);

            //Act
            Assert.IsNotNull(result.Reviews);
            Assert.IsTrue(result.Reviews.Any());
            Assert.AreEqual(hazardousSubstanceRiskAssessment.Reviews.First().Id, result.Reviews.First().Id);
            

        }
    }
}
