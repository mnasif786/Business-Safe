
using System;

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    public class GetResponsibilityTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepo;
        private Mock<IPeninsulaLog> _log;
        private Responsibility _responsibility;

        private const long _responsibilityId = 1234L;
        private const long _companyId = 5678L;

        private ResponsibilitiesService _target;

        [SetUp]
        public void Setup()
        {
            _responsibilityRepo = new Mock<IResponsibilityRepository>();
            _responsibility = Responsibility.Create(
                _companyId,
                new ResponsibilityCategory() { },
                "title",
                "description",
                new Site(),
                new ResponsibilityReason(),
                new Employee(),
                TaskReoccurringType.Annually, null,
                new UserForAuditing());

            _responsibilityRepo
                .Setup(x => x.GetByIdAndCompanyId(_responsibilityId, _companyId))
                .Returns(_responsibility);

            _log = new Mock<IPeninsulaLog>();

        }

        [Test]
        public void Given_responsibility_When_GetResponsibility_Then_responsibilityDto()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.GetResponsibility(_responsibilityId, _companyId);

            // Then
            Assert.IsInstanceOf<ResponsibilityDto>(result);
            Assert.That(result.Id, Is.EqualTo(_responsibility.Id));
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
        }

        [Test]
        public void Given_responsibility_not_found_When_CanBeDeleted_Then_log_and_throw_exception()
        {
            // Given
            _responsibilityRepo
                .Setup(x => x.GetByIdAndCompanyId(_responsibilityId, _companyId));

            _target = GetTarget();

            // When
            // Then
            var e = Assert.Throws<ResponsibilityNotFoundException>(() => _target.GetResponsibility(_responsibilityId, _companyId));
            _log.Verify(x => x.Add(e));
        }

        private ResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepo.Object, null, null, null, null, null, null, null, null, null, _log.Object);
        }
    }
}
