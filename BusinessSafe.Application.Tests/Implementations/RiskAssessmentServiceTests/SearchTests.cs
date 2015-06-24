using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.GeneralRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentServiceTests
{
    [TestFixture]
    public class SearchTests
    {
        private Mock<IGeneralRiskAssessmentRepository> _riskAssessmentRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IGeneralRiskAssessmentRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_Task_Service_is_initialized_with_spy_repository_When_Search_is_called_Then_repo_search_called_accordingly()
        {
            //Given
            var riskAssessmentService = CreateRiskAssessmentService();
            var request = new SearchRiskAssessmentsRequest()
            {
                Title = "title",
                CompanyId = 123,
                CreatedFrom = DateTime.Now.AddDays(-1),
                CreatedTo = DateTime.Now,
                ShowArchived = true,
                ShowDeleted = false
            };

            _riskAssessmentRepository.Setup(rr => rr.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>())).Returns(new GeneralRiskAssessment());
            _riskAssessmentRepository
                .Setup(rr => rr.Search(It.IsAny<string>(), It.IsAny<long>(), null, It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<long?>(), It.IsAny<long?>()
                    , It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<RiskAssessmentOrderByColumn>(), It.IsAny<OrderByDirection>()))
                .Returns(new List<GeneralRiskAssessment>());

            //When
            riskAssessmentService.Search(request);

            //Then
            _riskAssessmentRepository.Verify(x => x.Search(request.Title, request.CompanyId, null, request.CreatedFrom, request.CreatedTo, null, null, request.CurrentUserId, request.ShowDeleted, request.ShowArchived, It.IsAny<int>(), It.IsAny<int>(), request.OrderBy, request.OrderByDirection), Times.Once());
        }

        [Test]
        public void Given_search_criteria_contains_user_id_and_user_does_not_have_permission_for_all_sites_When_search_called_correct_methods_called()
        {
            //Given
            var companyId = 346L;
            var createdFrom = DateTime.Now;
            var createdTo = DateTime.Now;
            var userId = Guid.NewGuid();
            var title = "title";

            _riskAssessmentRepository
                .Setup(x => x.Search(title, companyId,
                    It.Is<List<long>>(y => y.Count == 3
                                           && y.Contains(345L)
                                           && y.Contains(346L)
                                           && y.Contains(347L)),
                    createdFrom,
                    createdTo,
                    null,
                    null,
                    userId, false, false, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<RiskAssessmentOrderByColumn>(), It.IsAny<OrderByDirection>()))
                .Returns(new List<GeneralRiskAssessment>());

            var riskAssessmentService = CreateRiskAssessmentService();

            var request = new SearchRiskAssessmentsRequest
                              {
                                  Title = title,
                                  CompanyId = companyId,
                                  CreatedFrom = createdFrom,
                                  CreatedTo = createdTo,
                                  AllowedSiteIds = new List<long>() { 345L, 346L, 347L},
                                  ShowArchived = false,
                                  ShowDeleted = false,
                                  CurrentUserId = userId
                              };

            //When
            riskAssessmentService.Search(request);

            //Then
            _riskAssessmentRepository.Verify(x => x.Search(title, companyId, null, createdFrom, createdTo, null, null, userId, false, false, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<RiskAssessmentOrderByColumn>(), It.IsAny<OrderByDirection>()), Times.Never());
        }

        [Test]
        public void Given_search_criteria_contains_user_id_and_user_has_permission_for_all_sites_When_search_called_correct_methods_called()
        {
            //Given
            var companyId = 346L;
            var createdFrom = DateTime.Now;
            var createdTo = DateTime.Now;
            var userId = Guid.NewGuid();
            var title = "title";

            _riskAssessmentRepository
                .Setup(x => x.Search(title, companyId,
                    null,
                    createdFrom,
                    createdTo,
                    null,
                    null,
                    userId, false, false, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<RiskAssessmentOrderByColumn>(), It.IsAny<OrderByDirection>()))
                .Returns(new List<GeneralRiskAssessment>());

            var riskAssessmentService = CreateRiskAssessmentService();

            var request = new SearchRiskAssessmentsRequest
            {
                Title = title,
                CompanyId = companyId,
                CreatedFrom = createdFrom,
                CreatedTo = createdTo,
                AllowedSiteIds = null,
                ShowArchived = false,
                ShowDeleted = false,
                CurrentUserId = userId
            };

            //When
            riskAssessmentService.Search(request);

            //Then
            _riskAssessmentRepository.Verify(x => x.Search(title, companyId, It.Is<List<long>>(y => y != null), createdFrom, createdTo, null, null, userId, false, false, It.IsAny<int>(), It.IsAny<int>(), It.IsAny<RiskAssessmentOrderByColumn>(), It.IsAny<OrderByDirection>()), Times.Never());
        }

        private GeneralRiskAssessmentService CreateRiskAssessmentService()
        {
            var riskAssessmentService = new GeneralRiskAssessmentService(
                _riskAssessmentRepository.Object, 
                null, 
                _userRepository.Object, 
                null, 
                _log.Object, 
                null,
                null);

            return riskAssessmentService;
        }

    }
}
