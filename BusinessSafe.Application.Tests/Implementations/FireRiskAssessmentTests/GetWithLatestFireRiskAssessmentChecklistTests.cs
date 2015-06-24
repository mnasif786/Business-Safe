using System.Collections.Generic;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class GetWithLatestFireRiskAssessmentChecklistTests
    {
        private Mock<IFireRiskAssessmentRepository> _riskAssessmentRepo;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<object>()));

            _riskAssessmentRepo = new Mock<IFireRiskAssessmentRepository>();
            _riskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(123L, 345L))
                .Returns(new FireRiskAssessment
                             {
                                 FireRiskAssessmentChecklists = new List<FireRiskAssessmentChecklist>
                                                                    {
                                                                        new FireRiskAssessmentChecklist
                                                                            {
                                                                                Checklist = new Checklist
                                                                                                {
                                                                                                    Sections = new List<Section>
                                                                                                                   {
                                                                                                                       new Section
                                                                                                                           {
                                                                                                                               Questions = new List<Question>()
                                                                                                                           }
                                                                                                                   }
                                                                                                }
                                                                            }
                                                                    }
                             });
        }

        [Test]
        public void When_GetWithLatestFireRiskAssessmentChecklist_called_Then_correct_methods_are_called()
        {
            GetTarget().GetWithLatestFireRiskAssessmentChecklist(123L, 345L);
            _riskAssessmentRepo.Verify(x => x.GetByIdAndCompanyId(123L, 345L));
        }

        private FireRiskAssessmentService GetTarget()
        {
            return new FireRiskAssessmentService(
                _riskAssessmentRepo.Object,
                null,
                null,
                null,
                null, _log.Object, null,null, null, null);
        }
    }
}
