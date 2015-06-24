using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessorServiceTests
{
    [TestFixture]
    public class SearchTests
    {
        public Mock<IRiskAssessorRepository> _riskAssessorRepository;
        public Mock<ISiteStructureElementRepository> _siteStructureRepository;
        public Mock<IPeninsulaLog> _log;
        public IEnumerable<RiskAssessor> _riskAssessors;

        [SetUp]
        public void Setup()
        {
            _riskAssessors = new List<RiskAssessor>
            {
                new RiskAssessor() {Employee = new Employee(), Site = new Site()},
                new RiskAssessor() {Employee = new Employee(), Site = new Site()},
                new RiskAssessor() {Employee = new Employee(), Site = new Site()}
            };

            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();

            _riskAssessorRepository
                .Setup(x => x.Search(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<int>(), false, false, It.IsAny<bool>()))
                .Returns(() => _riskAssessors);

            _riskAssessorRepository
                .Setup(x => x.Search(It.IsAny<string>(), It.IsAny<long[]>(), It.IsAny<long>(), It.IsAny<int>(), false, false, It.IsAny<bool>()))
                .Returns(() => _riskAssessors);


            _siteStructureRepository = new Mock<ISiteStructureElementRepository>();
            _siteStructureRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns<long, long>((id, companyId) => new Site() {Id = id});
        }

        [Test]
        public void Given_request_When_Search_Then_returns_correct_results()
        {
            // Given
            var target = GetTarget();
            var request = new SearchRiskAssessorRequest
                          {
                              SearchTerm = "search term",
                              CompanyId = 17L,
                              SiteId = 888L,
                              MaximumResults = 10
                          };

            // When
            var result = target.Search(request);

            // Then
            Assert.That(result.Count(), Is.EqualTo(_riskAssessors.Count()));
            _riskAssessorRepository.Verify(x => x.Search(
                It.Is<string>(y => y == request.SearchTerm),
                It.IsAny<long[]>(),
                It.Is<long>(y => y == request.CompanyId),
                It.Is<int>(y => y == request.MaximumResults),
                It.Is<bool>(y => y == request.IncludeDeleted),
                It.Is<bool>(y => y == request.ExcludeActive),
                false
            ));
        }

        private RiskAssessorService GetTarget()
        {
            return new RiskAssessorService(
                _riskAssessorRepository.Object,
                null,
                null,
                null,
                null, _siteStructureRepository.Object, null);
        }
    }
}
