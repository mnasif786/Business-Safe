using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ControlMeasures
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IPersonalRiskAssessmentService> _riskAssessmentService;
        private const long _companyId = 500L;
        private const long _riskAssessmentId = 888L;
        private ControlMeasuresController _target;
        private Guid _currentUserId;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            var riskAssessment = new PersonalRiskAssessmentDto();

            _target = GetTarget();
            _currentUserId = _target.CurrentUser.UserId;

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessmentWithHazards(_riskAssessmentId, _companyId, _currentUserId))
                .Returns(riskAssessment);
        }

        [Test]
        public void When_get_control_measures_Then_should_return_index_view()
        {
            // Given
            
            // When
            var result = _target.Index(_companyId, _riskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void When_get_control_measures_Then_should_return_ControlMeasuresViewModel()
        {
            // Given

            // When
            var result = _target.Index(_companyId, _riskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.TypeOf<ControlMeasuresViewModel>());

        }

        [Test]
        public void When_get_control_measures_Then_should_call_correct_methods()
        {
            // Given

            // When
            _target.Index(_companyId, _riskAssessmentId);

            // Then
            _riskAssessmentService.VerifyAll();

        }


        private ControlMeasuresController GetTarget()
        {
            var result = new ControlMeasuresController(_riskAssessmentService.Object,null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}