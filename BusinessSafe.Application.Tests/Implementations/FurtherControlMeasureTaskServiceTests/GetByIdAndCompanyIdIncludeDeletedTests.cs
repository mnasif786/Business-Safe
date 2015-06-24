using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FurtherControlMeasureTaskServiceTests
{
    [TestFixture]
    public class GetByIdAndCompanyIdIncludeDeletedTests
    {
        private Mock<IFurtherControlMeasureTasksRepository> _furtherControlMeasureTasksRepository;
        private Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask> task;
        private Mock<IUserRepository> _userRepo;

        private long _id;

        [SetUp]
        public void Setup()
        {
            _id = 10L;

            task=new Mock<MultiHazardRiskAssessmentFurtherControlMeasureTask>();
            task
                .SetupGet(x=>x.Self)
                .Returns(new MultiHazardRiskAssessmentFurtherControlMeasureTask());

            task
                .SetupGet(x => x.RiskAssessment)
                .Returns(new GeneralRiskAssessment());

            task
                .SetupGet(x => x.Category)
                .Returns(new TaskCategory());

            _furtherControlMeasureTasksRepository = new Mock<IFurtherControlMeasureTasksRepository>();
            _furtherControlMeasureTasksRepository
                .Setup(x => x.GetByIdIncludeDeleted(_id))
                .Returns(task.Object);

            _userRepo = new Mock<IUserRepository>();
        }

        [Test]
        public void When_GetByIdAndCompanyIdIncludeDeleted_calls_correct_methods()
        {
            // Given
            var target = GetTarget();

            // When
            target.GetByIdIncludeDeleted(_id);

            // Then
            _furtherControlMeasureTasksRepository.VerifyAll();
        }

        [Test]
        public void When_GetByIdAndCompanyIdIncludeDeleted_returns_correct_result()
        {
            // Given
            var target = GetTarget();

            // When
            var result=target.GetByIdIncludeDeleted(_id);

            // Then
            Assert.IsInstanceOf<MultiHazardRiskAssessmentFurtherControlMeasureTaskDto>(result);
        }

        private IFurtherControlMeasureTaskService GetTarget()
        {
            return new FurtherControlMeasureTaskService
                (
                    null,
                    _furtherControlMeasureTasksRepository.Object,
                    null,
                    null,
                    null,
                    null,
                    null, _userRepo.Object, null, null
                );
        }
    }
}
