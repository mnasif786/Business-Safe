//using System;
//using System.Web.Mvc;

//using BusinessSafe.Application.DataTransferObjects;
//using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
//using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
//using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

//using Moq;

//using NUnit.Framework;

//namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.EmployeeChecklistSummaryTests
//{
//    [TestFixture]
//    public class IndexTests
//    {
//        private Mock<IEmployeeChecklistSummaryViewModelFactory> _viewModelFactory;
//        private EmployeeChecklistSummaryController _target;

//        [SetUp]
//        public void Setup()
//        {
//            _viewModelFactory = new Mock<IEmployeeChecklistSummaryViewModelFactory>();

//            _viewModelFactory
//                .Setup(x => x.WithEmployeeChecklistId(It.IsAny<Guid>()))
//                .Returns(_viewModelFactory.Object);

//            _viewModelFactory
//                .Setup(x => x.GetViewModel())
//                .Returns(new EmployeeChecklistSummaryViewModel());

//            _target = GetTarget();
//        }

//        [Test]
//        public void Correct_viewModel_returned_from_ViewEmployeeChecklistEmail()
//        {
//            // Given
//            var employeeChecklistId = Guid.NewGuid();
//            const string expectedViewName = "_EmployeeChecklistEmailSummary";

//            // When
//            var result = _target.Index(employeeChecklistId) as PartialViewResult;
//            var model = result.Model as EmployeeChecklistSummaryViewModel;

//            // Then
//            //Assert.That(result.Model, Is.InstanceOf<EmployeeChecklistEmailSummaryDto>());
//            Assert.That(result.ViewName, Is.EqualTo(expectedViewName));

//            _viewModelFactory.Verify(x => x.WithEmployeeChecklistId(employeeChecklistId));
//            _viewModelFactory.Verify(x => x.GetViewModel());
//        }

//        private EmployeeChecklistSummaryController GetTarget()
//        {
//            var controller = new EmployeeChecklistSummaryController(_viewModelFactory.Object);
//            return TestControllerHelpers.AddUserToController(controller);
//        }
//    }
//}
