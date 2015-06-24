using BusinessSafe.Application.Implementations.Specification;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Specifications
{
    [TestFixture]
    public class UniqueRiskAssessmentReferenceSpecificationTests
    {
        public Mock<IRiskAssessmentRepository> _riskAssessmentRepository;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
        }

        [Test]
        public void Given_a_reference_exists_when_IsSatisfiedBy_then_return_false()
        {
            //given
            _riskAssessmentRepository.Setup(x =>
                x.DoesAssessmentExistWithTheSameReference < GeneralRiskAssessment>(It.IsAny<long>()
                                                         , It.IsAny<string>()
                                                         , It.IsAny<long?>())).Returns(true);
            var target =  GetTarget();
            var riskAss = new CreateRiskAssessmentRequest()
                              {
                                  Title = "Title this is",
                                  Reference = "reference here",
                                  CompanyId = 45698
                              };  

            //when
            var result = target.IsSatisfiedBy(riskAss);

            //then
            Assert.IsFalse(result);

        }

        [Test]
        public void Given_a_reference_doesnt_exists_when_IsSatisfiedBy_then_return_true()
        {
            //given
            _riskAssessmentRepository.Setup(x =>
                x.DoesAssessmentExistWithTheSameReference < GeneralRiskAssessment>(It.IsAny<long>()
                                               , It.IsAny<string>()
                                               , It.IsAny<long?>())).Returns(false);
            var target = GetTarget();

            var riskAss = new CreateRiskAssessmentRequest()
            {
                Title = "Title this is",
                Reference = "reference here",
                CompanyId = 45698
            };  
            

            //when
            var result = target.IsSatisfiedBy(riskAss);

            //then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_any_scenario_when_IsSatisfiedBy_then_repository_access_is_correct()
        {
            //given
            var target = GetTarget();
            var riskAss = new CreateRiskAssessmentRequest()
            {
                Title = "Title this is",
                Reference = "reference here",
                CompanyId = 45698
            };  
            

            //when
            target.IsSatisfiedBy(riskAss);

            //then
            _riskAssessmentRepository.Verify(x =>
                                             x.DoesAssessmentExistWithTheSameReference < GeneralRiskAssessment>(riskAss.CompanyId
                                                                                       , riskAss.Reference
                                                                                       , 0));


        }

        private UniqueRiskAssessmentReferenceSpecification<GeneralRiskAssessment> GetTarget()
        {
            return new UniqueRiskAssessmentReferenceSpecification<GeneralRiskAssessment>(_riskAssessmentRepository.Object);
        }
    }
}
