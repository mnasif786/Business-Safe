using System;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

//namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessmentReview
//{
    //This has been moved to the general/personal risk assessment revioew controllers.
    //[TestFixture]
    //public class CreateTests : BaseRiskAssessmentReviewTest
    //{
        //[Test]
        //public void Given_that_add_is_called_When_required_fields_are_empty_Then_error_message_returned()
        //{
        //    //Given
        //    var target = GetTarget();

        //    //When
        //    target.ModelState.AddModelError("ReviewDate", "we need this !!!");
        //    target.ModelState.AddModelError("ReviewingEmployeeId", "we need this !!!");

        //    var result = target.SaveRiskAssessmentReview(new AddEditRiskAssessmentReviewViewModel());
        //    var jsonResult = result.Data.ToString();

        //    //Then
        //    Assert.That(jsonResult, Contains.Substring("Success = False"));
        //}

        //[Test]
        //public void Given_that_add_is_called_When_all_required_fields_are_provided_Then_should_return_success_message()
        //{
        //    //Given
        //    var target = GetTarget();
        //    var request = new AddEditRiskAssessmentReviewViewModel() { CompanyId = 1, RiskAssessmentId = 1, ReviewDate = "31/12/2012", ReviewingEmployeeId = Guid.NewGuid() };
            
        //    //When
        //    var result = target.SaveRiskAssessmentReview(request);
        //    var jsonResult = result.Data.ToString();

        //    //Then
        //    Assert.That(jsonResult, Contains.Substring("Success = True"));
            
        //}

        //[Test]
        //public void Given_that_add_is_called_When_all_required_fields_are_provided_Then_the_review_service_should_be_requested_to_add_review_request()
        //{
        //    //Given
        //    var createdAddReviewRequest = new AddRiskAssessmentReviewRequest();

        //    _riskAssessmentReviewService.Setup(
        //        x => x.Add(It.IsAny<AddRiskAssessmentReviewRequest>())).Callback<AddRiskAssessmentReviewRequest>(x => createdAddReviewRequest = x);
        //    var target = GetTarget();
        //    var model = new AddEditRiskAssessmentReviewViewModel() { CompanyId = 1, RiskAssessmentId = 1, ReviewDate = "31/12/2012", ReviewingEmployeeId = Guid.NewGuid() };

        //    var expectedAddReviewRequest = new AddRiskAssessmentReviewRequest()
        //    {
        //        CompanyId = model.CompanyId,
        //        RiskAssessmentId = model.RiskAssessmentId,
        //        ReviewDate = DateTime.Parse(model.ReviewDate),
        //        ReviewingEmployeeId = model.ReviewingEmployeeId
        //    };

        //    //When
        //    var result = target.SaveRiskAssessmentReview(model);

        //    //Then
        //    _riskAssessmentReviewService.Verify(x => x.Add(It.IsAny<AddRiskAssessmentReviewRequest>()), Times.Once());
        //    Assert.That(createdAddReviewRequest.CompanyId, Is.EqualTo(expectedAddReviewRequest.CompanyId));
        //    Assert.That(createdAddReviewRequest.RiskAssessmentId, Is.EqualTo(expectedAddReviewRequest.RiskAssessmentId));
        //    Assert.That(createdAddReviewRequest.ReviewDate, Is.EqualTo(expectedAddReviewRequest.ReviewDate));
        //    Assert.That(createdAddReviewRequest.ReviewingEmployeeId, Is.EqualTo(expectedAddReviewRequest.ReviewingEmployeeId));
        //}

        //[Test]
        //public void Given_that_add_is_called_When_required_fields_are_empty_Then_add_review_request_is_not_called()
        //{
        //    //Given
        //    var target = GetTarget();
        //    _riskAssessmentReviewService.Setup(
        //        x => x.Add(It.IsAny<AddRiskAssessmentReviewRequest>()));

            

        //    //When
        //    target.ModelState.AddModelError("ReviewDate", "we need this !!!");
        //    target.ModelState.AddModelError("ReviewingEmployeeId", "we need this !!!");

        //    var result = target.SaveRiskAssessmentReview(new AddEditRiskAssessmentReviewViewModel());

        //    //Then
        //    _riskAssessmentReviewService.Verify(x => x.Add(It.IsAny<AddRiskAssessmentReviewRequest>()), Times.Never());
        //}

        //[Test]
        //public void Given_that_add_is_called_When_required_fields_are_provided_Then_should_add_assigning_user_id_to_request()
        //{

        //    //Given
        //    var createdAddReviewRequest = new AddRiskAssessmentReviewRequest();

        //    _riskAssessmentReviewService.Setup(
        //        x => x.Add(It.IsAny<AddRiskAssessmentReviewRequest>())).Callback<AddRiskAssessmentReviewRequest>(x => createdAddReviewRequest = x);
        //    var target = GetTarget();
        //    var model = new AddEditRiskAssessmentReviewViewModel() { CompanyId = 1, RiskAssessmentId = 1, ReviewDate = "31/12/2012", ReviewingEmployeeId = Guid.NewGuid() };

        //    //When
        //    var result = target.SaveRiskAssessmentReview(model);

        //    //Then
        //    _riskAssessmentReviewService.Verify(x => x.Add(It.IsAny<AddRiskAssessmentReviewRequest>()), Times.Once());
        //    Assert.That(createdAddReviewRequest.AssigningUserId, Is.InstanceOf<Guid>());
        //    Assert.That(createdAddReviewRequest.AssigningUserId, Is.Not.Null);
        //}
//    }
//}