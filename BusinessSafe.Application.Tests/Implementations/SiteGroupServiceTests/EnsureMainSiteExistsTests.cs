using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.Sites;
using NUnit.Framework;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Domain.InfrastructureContracts.Logging;

namespace BusinessSafe.Application.Tests.Implementations.SiteGroupServiceTests
{
    [TestFixture]
    public class EnsureMainSiteExistsTests
    {
        private Mock<ISiteStructureElementRepository> _siteStructureElementRepository;
        private Mock<ISiteRepository> _siteRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userRepository;
        private long _companyId;
        private long _mainPeninsulaSiteId;

        [SetUp]
        public void Setup()
        {
            _siteStructureElementRepository = new Mock<ISiteStructureElementRepository>();
            _siteRepository = new Mock<ISiteRepository>();
            _log = new Mock<IPeninsulaLog>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _companyId = 234L;
            _mainPeninsulaSiteId = 2431324L;
        }

        [Test]
        public void Given_no_sites_exist_forComapany_When_EnsureMainSiteExists_is_called_then_site_is_created()
        {
            //Given
            _siteStructureElementRepository
                .Setup(x => x.GetByCompanyId(_companyId))
                .Returns(new List<SiteStructureElement>());

            //When
            GetTarget().CreateMainSite(_companyId, _mainPeninsulaSiteId);

            //Then
            _siteStructureElementRepository.Verify(x => x.GetByCompanyId(_companyId), Times.Once());

            _siteRepository.Verify(x => x.Save(It.Is<Site>(
                y => y.SiteId == _mainPeninsulaSiteId
                && y.Parent == null
                && y.ClientId == _companyId
                && y.Name == "Main Site"
                && y.Reference == "Main Site Reference"
                )),
                Times.Once());
        }

        [Test]
        public void Given_sites_exist_forComapany_When_EnsureMainSiteExists_is_called_then_no_site_is_created()
        {
            //Given
            _siteStructureElementRepository
                .Setup(x => x.GetByCompanyId(_companyId))
                .Returns(new List<SiteStructureElement> { new Site() });

            //When
            GetTarget().CreateMainSite(_companyId, _mainPeninsulaSiteId);

            //Then
            _siteStructureElementRepository.Verify(x => x.GetByCompanyId(_companyId), Times.Once());

            _siteRepository.Verify(x => x.Save(It.IsAny<Site>()),
                Times.Never());
        }

        public ISiteService GetTarget()
        {
            return new SiteService(
                _siteRepository.Object, 
                _siteStructureElementRepository.Object, 
                _userRepository.Object,null);
        }
    }
}
