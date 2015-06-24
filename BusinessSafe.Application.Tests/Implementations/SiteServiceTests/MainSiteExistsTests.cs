using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
{
    [TestFixture]
    public class MainSiteExistsTests
    {
        private Mock<ISiteStructureElementRepository> _siteStructureElementRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userRepository;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _siteStructureElementRepository = new Mock<ISiteStructureElementRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _log = new Mock<IPeninsulaLog>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _companyId = 234L;
        }

        [Test]
        public void Given_Sites_in_BSO_When_MainSiteExists_Then_Return_True()
        {
            // Given
            _siteStructureElementRepository.Setup(x => x.GetByCompanyId(_companyId)).Returns(
                new List<SiteStructureElement>() { new Site() });

            // When
            var result = GetTarget().MainSiteExists(_companyId);

            // Then
            _siteStructureElementRepository.Verify(x => x.GetByCompanyId(_companyId));
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_No_Sites_in_BSO_When_MainSiteExists_Then_Return_False()
        {
            // Given
            _siteStructureElementRepository.Setup(x => x.GetByCompanyId(_companyId)).Returns(
                new List<SiteStructureElement>());

            // When
            var result = GetTarget().MainSiteExists(_companyId);

            // Then
            _siteStructureElementRepository.Verify(x => x.GetByCompanyId(_companyId));
            Assert.IsFalse(result);
        }

        public SiteService GetTarget()
        {
            return new SiteService(
                _siteRepository.Object,
                _siteStructureElementRepository.Object,
                _userRepository.Object,
                null);
        }
    }
}
