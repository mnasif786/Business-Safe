using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstanceRiskAssessementTests
{
    [TestFixture]
    public class CreateRiskAssessmentViewModelFactoryTests
    {
        private CreateRiskAssessmentViewModelFactory _target;
        private Mock<IHazardousSubstancesService> _hazardousSubstanceService;

        [SetUp]
        public void Setup()
        {
            _hazardousSubstanceService = new Mock<IHazardousSubstancesService>();
            _hazardousSubstanceService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new HazardousSubstanceDto());

            _hazardousSubstanceService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(new List<HazardousSubstanceDto>());

            _target = new CreateRiskAssessmentViewModelFactory(_hazardousSubstanceService.Object);
        }

        [Test]
        public void When_WithNewHazardousSubstanceId_Then_title_is_retrieved_from_service()
        {
            // Given
            var returnedHazSub = new HazardousSubstanceDto() { Name = "haz sub 01" };
            var companyId = 1234;
            var hazardousSubstanceId = 5678;

            _hazardousSubstanceService
                .Setup(x => x.GetByIdAndCompanyId(hazardousSubstanceId, companyId))
                .Returns(returnedHazSub);
            
            // When
            var result = _target.WithCompanyId(companyId).WithNewHazardousSubstanceId(hazardousSubstanceId).GetViewModel();

            // Then
            _hazardousSubstanceService.Verify(x => x.GetByIdAndCompanyId(hazardousSubstanceId, companyId));
            Assert.That(result.Title, Is.EqualTo(returnedHazSub.Name));
        }

        [Test]
        public void When_get_create_Then_hazardous_substances_are_retrieved_from_service()
        {
            // Given

            // When            
            var result = _target.WithCompanyId(1234).GetViewModel();
            
            // Then
            _hazardousSubstanceService.Verify(x => x.Search(It.Is<SearchHazardousSubstancesRequest>(y => y.CompanyId == 1234)), Times.Once());
        }

        [Test]
        public void When_GetViewModel_Then_hazardous_substances_are_put_in_view_model_and_prepends_please_select_empty_option()
        {
            // Given
            var returnedHazardousSubstances = new List<HazardousSubstanceDto>()
                                              {
                                                  new HazardousSubstanceDto() { Id = 1, Name = "hazsub 01"},
                                                  new HazardousSubstanceDto() { Id = 2, Name = "hazsub 02" },
                                                  new HazardousSubstanceDto() { Id = 3, Name = "hazsub 03" },
                                                  new HazardousSubstanceDto() { Id = 4, Name = "hazsub 04" }
                                              };
            _hazardousSubstanceService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(returnedHazardousSubstances);

            // When
            var result = _target.WithCompanyId(1234).GetViewModel();

            // Then
            Assert.That(result.HazardousSubstances.Count(), Is.EqualTo(5));
            Assert.That(result.HazardousSubstances.First().value, Is.EqualTo(string.Empty));
            Assert.That(result.HazardousSubstances.First().label, Is.EqualTo("--Select Option--"));
            for (var i = 1; i < returnedHazardousSubstances.Count; i++)
            {
                Assert.That(result.HazardousSubstances.Skip(i).Take(1).First().label, Is.EqualTo(returnedHazardousSubstances.ElementAt(i - 1).Name));
                Assert.That(result.HazardousSubstances.Skip(i).Take(1).First().value, Is.EqualTo(returnedHazardousSubstances.ElementAt(i - 1).Id.ToString()));
            }
        }

        [Test]
        public void When_GetViewModel_called_with_CreateRiskAssessmentViewModel_Then_existing_values_are_retained()
        {
            // Given
            var returnedHazardousSubstances = new List<HazardousSubstanceDto>()
                                              {
                                                  new HazardousSubstanceDto() { Id = 1, Name = "hazsub 01"},
                                                  new HazardousSubstanceDto() { Id = 2, Name = "hazsub 02" },
                                                  new HazardousSubstanceDto() { Id = 3, Name = "hazsub 03" },
                                                  new HazardousSubstanceDto() { Id = 4, Name = "hazsub 04" }
                                              };

            var createRiskAssessmentViewModel = new CreateRiskAssessmentViewModel()
            {
                CompanyId = 1234,
                IsCreateDraft = false,
                NewHazardousSubstanceId = 5678,
                Reference = "My Ref",
                Title = "My Title"
            };

            _hazardousSubstanceService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(returnedHazardousSubstances);

            // When
            var result = _target.WithCompanyId(1234).GetViewModel(createRiskAssessmentViewModel) as CreateRiskAssessmentViewModel;

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(createRiskAssessmentViewModel.CompanyId));
            Assert.That(result.IsCreateDraft, Is.EqualTo(createRiskAssessmentViewModel.IsCreateDraft));
            Assert.That(result.NewHazardousSubstanceId, Is.EqualTo(createRiskAssessmentViewModel.NewHazardousSubstanceId));
            Assert.That(result.Reference, Is.EqualTo(createRiskAssessmentViewModel.Reference));
            Assert.That(result.Title, Is.EqualTo(createRiskAssessmentViewModel.Title));
        }

        [Test]
        public void When_GetViewModel_called_with_CreateRiskAssessmentViewModel_Then_hazardous_substances_are_put_in_view_model()
        {
            // Given
            var returnedHazardousSubstances = new List<HazardousSubstanceDto>()
                                              {
                                                  new HazardousSubstanceDto() { Id = 1, Name = "hazsub 01"},
                                                  new HazardousSubstanceDto() { Id = 2, Name = "hazsub 02" },
                                                  new HazardousSubstanceDto() { Id = 3, Name = "hazsub 03" },
                                                  new HazardousSubstanceDto() { Id = 4, Name = "hazsub 04" }
                                              };

            var createRiskAssessmentViewModel = new CreateRiskAssessmentViewModel()
                                                {

                                                };

            _hazardousSubstanceService
                .Setup(x => x.Search(It.IsAny<SearchHazardousSubstancesRequest>()))
                .Returns(returnedHazardousSubstances);

            // When
            var result = _target.WithCompanyId(1234).GetViewModel(createRiskAssessmentViewModel) as CreateRiskAssessmentViewModel;

            // Then
            Assert.That(result.HazardousSubstances.Count(), Is.EqualTo(5));
            for (var i = 1; i < returnedHazardousSubstances.Count; i++)
            {
                Assert.That(result.HazardousSubstances.ElementAt(i).label, Is.EqualTo(returnedHazardousSubstances.ElementAt(i - 1).Name));
                Assert.That(result.HazardousSubstances.ElementAt(i).value, Is.EqualTo(returnedHazardousSubstances.ElementAt(i - 1).Id.ToString()));
            }
        }
    }
}
