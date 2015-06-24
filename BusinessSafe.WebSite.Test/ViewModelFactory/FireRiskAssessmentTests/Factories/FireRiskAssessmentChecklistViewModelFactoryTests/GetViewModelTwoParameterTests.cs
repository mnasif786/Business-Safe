using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;

using FluentValidation.Results;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories.FireRiskAssessmentChecklistViewModelFactoryTests
{
    [TestFixture]
    public class GetViewModelTwoParameterTests
    {

        /* todo: Can't figure out how to mock this all out with the extension methods
         * 
         * */

        //private FireRiskAssessmentChecklistViewModelFactory _target;

        //private Mock<SectionViewModel> _mockSectionViewModel;
        //private Mock<IFireRiskAssessmentChecklistViewModel> _mockViewModel;

        //private List<ValidationFailure> _errors;

        //[SetUp]
        //public void Setup()
        //{
        //    _target = new FireRiskAssessmentChecklistViewModelFactory(null, null);

        //    _mockSectionViewModel = new Mock<SectionViewModel>();
        //    _mockSectionViewModel.Setup(x => x.MarkAsInvalid());

        //    _mockViewModel = new Mock<IFireRiskAssessmentChecklistViewModel>();
        //    _mockViewModel
        //        .Setup(x => x.MarkAsInvalid());
        //    _mockViewModel
        //        .Setup(x => x.GetSectionViewModel(It.IsAny<int>()))
        //        .Returns(_mockSectionViewModel.Object);

        //    _errors = new List<ValidationFailure>();
        //}

        //[Test]
        //public void When_GetViewModel_Then_should_return_viewModel()
        //{
        //    // Given
        //    var passedInModel = new FireRiskAssessmentChecklistViewModel()
        //    {
        //        CompanyId = 1234L,
        //        FireRiskAssessmentChecklistId = 5678L,
        //        RiskAssessmentId = 4968L
        //    };

        //    // When
        //    var result = _target.GetViewModel(passedInModel, _errors);

        //    // Then
        //    Assert.That(result, Is.EqualTo(passedInModel));
        //}

        //[Test]
        //public void Given_has_errors_When_GetViewModel_Then_should_mark_viewModel_as_invalid()
        //{
        //    // Given
        //    _errors.Add(new ValidationFailure("123", "error"));

        //    // When
        //    _target.GetViewModel(_mockViewModel.Object, _errors);

        //    // Then
        //    _mockViewModel.Verify(x => x.MarkAsInvalid());
        //}

        //[Test]
        //public void Given_has_errors_When_GetViewModel_Then_should_mark_corresponding_section_as_invalid()
        //{
        //    // Given
        //    _errors.Add(new ValidationFailure("123", "error"));

        //    // When
        //    _target.GetViewModel(_mockViewModel.Object, _errors);

        //    // Then
        //    _mockSectionViewModel.Verify(x => x.MarkAsInvalid());
        //}
    }
}