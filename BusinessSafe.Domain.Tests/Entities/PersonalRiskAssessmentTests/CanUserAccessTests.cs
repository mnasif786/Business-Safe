
using System;
using System.Collections.Generic;

using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PersonalRiskAssessmentTests
{
    [TestFixture]
    public class CanUserAccessTests
    {
        [Test]
        public void Given_Not_Sensitive_When_CanUserAccess_Then_return_true()
        {
            // Given
            var target = new PersonalRiskAssessment { Sensitive = false };

            // When
            var result = target.CanUserAccess(It.IsAny<Guid>());

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_Sensitive_and_created_by_current_user_return_true()
        {
            var currentUser = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment { Sensitive = true, CreatedBy = new UserForAuditing() { Id = currentUser } };

            // When
            var result = target.CanUserAccess(currentUser);

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_Sensitive_and_not_created_by_current_user_return_false()
        {
            var currentUser = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment { Sensitive = true, CreatedBy = new UserForAuditing() { Id = currentUser } };

            // When
            var result = target.CanUserAccess(Guid.NewGuid());

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_Sensitive_and_risk_assessor_is_current_user_return_true()
        {
            var currentUserId = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment
            {
                Sensitive = true,
                RiskAssessor = new RiskAssessor()
                {
                    Employee = new Employee()
                    {
                        User = new User()
                        {
                            Id = currentUserId
                        }
                    }
                }
            };

            // When
            var result = target.CanUserAccess(currentUserId);

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_Sensitive_and_risk_assessor_is_not_current_user_return_false()
        {
            var currentUserId = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment
            {
                Sensitive = true,
                RiskAssessor = new RiskAssessor()
                {
                    Employee = new Employee()
                    {
                        User = new User()
                        {
                            Id = Guid.NewGuid()
                        }
                    }
                }
            };

            // When
            var result = target.CanUserAccess(currentUserId);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_Sensitive_and_reviewer_is_current_user_return_true()
        {
            var currentUserId = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment
            {
                Sensitive = true,
                Reviews = new List<RiskAssessmentReview>
                          {
                              new RiskAssessmentReview { 
                                  CreatedOn = DateTime.Now,
                                  ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = currentUserId }
                                }
                              },
                              new RiskAssessmentReview { 
                                  CreatedOn = DateTime.Now.AddDays(-5),
                                  ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = Guid.NewGuid() }
                                }
                              }
                          }
            };

            // When
            var result = target.CanUserAccess(currentUserId);

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_Sensitive_and_reviewer_is_not_current_user_return_false()
        {
            var currentUserId = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment
            {
                Sensitive = true,
                Reviews = new List<RiskAssessmentReview>
                          {
                              new RiskAssessmentReview { 
                                  ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = Guid.NewGuid() }
                                }
                              }
                          }
            };

            // When
            var result = target.CanUserAccess(currentUserId);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_Sensitive_and_reviewer_is_not_user_account_return_false()
        {
            var currentUserId = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment
            {
                Sensitive = true,
                Reviews = new List<RiskAssessmentReview>
                          {
                              new RiskAssessmentReview { 
                                  ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = Guid.NewGuid() }
                                }
                              }
                          }
            };

            // When
            var result = target.CanUserAccess(currentUserId);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_Sensitive_and_reviewer_is_user_but_review_deleted_return_false()
        {
            var currentUserId = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment
            {
                Sensitive = true,
                Reviews = new List<RiskAssessmentReview>
                          {
                              new RiskAssessmentReview { 
                                  ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = currentUserId }
                                    
                                },
                                Deleted = true
                              }
                          }
            };

            // When
            var result = target.CanUserAccess(currentUserId);

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_Sensitive_and_last_review_reviewer_is_not_user_return_false()
        {
            var currentUserId = Guid.NewGuid();

            // Given
            var target = new PersonalRiskAssessment
            {
                Sensitive = true,
                Reviews = new List<RiskAssessmentReview>
                          {
                              new RiskAssessmentReview { 
                                CreatedOn = DateTime.Now.AddDays(-5),
                                ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = currentUserId }
                                }
                              },
                              new RiskAssessmentReview { 
                                CreatedOn = DateTime.Now.AddDays(-3),
                                ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = currentUserId }
                                }
                              },
                              new RiskAssessmentReview { 
                                CreatedOn = DateTime.Now.AddDays(-1),
                                ReviewAssignedTo = new Employee()
                                {
                                    User = new User() { Id = Guid.NewGuid() }
                                }
                              }
                          }
            };

            // When
            var result = target.CanUserAccess(currentUserId);

            // Then
            Assert.IsFalse(result);
        }
    }


}
