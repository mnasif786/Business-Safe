using System;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.Models;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessmentReview
{
    public class BaseRiskAssessmentReviewTest
    {
        protected Mock<IRiskAssessmentService> _riskAssessmentService;
        protected Mock<IEmployeeService> _employeeService;
        protected Mock<IRiskAssessmentReviewService> _riskAssessmentReviewService;
        protected Mock<ICompleteReviewViewModelFactory> _reviewViewFactory;
        protected Mock<IReviewAuditDocumentHelper> _reviewAuditService;
        protected Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;
        protected Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        protected Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;
        protected Mock<IBus> _bus;

        [SetUp]
        public void SetUp()
        {
            _riskAssessmentService = new Mock<IRiskAssessmentService>();
            _employeeService = new Mock<IEmployeeService>();
            _riskAssessmentReviewService = new Mock<IRiskAssessmentReviewService>();
            _riskAssessmentReviewService
                .Setup(x => x.CompleteRiskAssessementReview(It.IsAny<CompleteRiskAssessmentReviewRequest>()));
            
            _reviewAuditService = new Mock<IReviewAuditDocumentHelper>();   
            _reviewViewFactory = new Mock<ICompleteReviewViewModelFactory>();

            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            _personalRiskAssessmentService.Setup(
                x => x.CanUserAccess(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<Guid>())).Returns(true);

            var reviewAdutiDocumentResult = new ReviewAuditDocumentResult();
            _reviewAuditService
                .Setup(x => x.CreateReviewAuditDocument(It.IsAny<RiskAssessmentType>(), It.IsAny<RiskAssessmentDto>()))
                .Returns(reviewAdutiDocumentResult);

            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _bus = new Mock<IBus>();
        }

        public  RiskAssessmentReviewController GetTarget()
        {
            var result = new RiskAssessmentReviewController(_riskAssessmentService.Object,
                _employeeService.Object, 
                _riskAssessmentReviewService.Object, 
                _reviewAuditService.Object,
                _fireRiskAssessmentService.Object,
                _businessSafeSessionManager.Object,
                _bus.Object);

            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
