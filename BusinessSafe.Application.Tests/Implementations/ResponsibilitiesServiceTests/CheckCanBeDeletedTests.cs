using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Implementations.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.ResponsibilitiesServiceTests
{
    [TestFixture]
    public class CheckCanBeDeletedTests
    {
        private Mock<IResponsibilityRepository> _responsibilityRepo;
        private Mock<IPeninsulaLog> _log;
        private Mock<Responsibility> _responsibility;

        private const long _responsibilityId = 1234L;
        private const long _companyId = 5678L;

        private ResponsibilitiesService _target;

        [SetUp]
        public void Setup()
        {
            _responsibilityRepo = new Mock<IResponsibilityRepository>();
            _responsibility = new Mock<Responsibility>();
            _responsibility
                .Setup(x => x.HasUndeletedTasks())
                .Returns(true);

            _responsibilityRepo
                .Setup(x => x.GetByIdAndCompanyId(_responsibilityId, _companyId))
                .Returns(_responsibility.Object);

            _log = new Mock<IPeninsulaLog>();

        }

        [Test]
        public void Given_responsibility_can_be_deleted_When_CanBeDeleted_Then_return_true()
        {
            // Given
            _target = GetTarget();

            // When
            var result = _target.HasUndeletedTasks(_responsibilityId, _companyId);

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_responsibility_can_not_be_deleted_When_CanBeDeleted_Then_return_false()
        {
            // Given
            _responsibility
                .Setup(x => x.HasUndeletedTasks())
                .Returns(false);

            _target = GetTarget();

            // When
            var result = _target.HasUndeletedTasks(_responsibilityId, _companyId);

            // Then
            Assert.IsFalse(result);
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
            var e = Assert.Throws<ResponsibilityNotFoundException>(() => _target.HasUndeletedTasks(_responsibilityId, _companyId));
            _log.Verify(x => x.Add(e));
        }

        private ResponsibilitiesService GetTarget()
        {
            return new ResponsibilitiesService(_responsibilityRepo.Object, null, null, null, null, null, null, null, null, null, _log.Object);
        }
    }
}
