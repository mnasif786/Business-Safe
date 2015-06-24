using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.ResponsibilityTasks
{
    [TestFixture]
    [Category("Unit")]
    public class ReassignTest
    {
        private const long CompanyId = 12312L;
        private const long ResponsibilityTaskId = 72L;

        private Mock<IResponsibilityTaskService> _responsibilityTaskService;
        private Mock<IReassignResponsibilityTaskViewModelFactory> _reassignResponsibilityTaskViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _responsibilityTaskService = new Mock<IResponsibilityTaskService>();

            _reassignResponsibilityTaskViewModelFactory =
                new Mock<IReassignResponsibilityTaskViewModelFactory>();

            _reassignResponsibilityTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_reassignResponsibilityTaskViewModelFactory.Object);

            _reassignResponsibilityTaskViewModelFactory
                .Setup(x => x.WithResponsibilityTaskId(ResponsibilityTaskId))
                .Returns(_reassignResponsibilityTaskViewModelFactory.Object);

            _reassignResponsibilityTaskViewModelFactory
                .Setup(x => x.GetViewModel()).Returns(
                    new WebSite.Areas.Responsibilities.ViewModels.ReassignResponsibilityTaskViewModel());
        }

        [Test]
        public void When_View_called_Then_correct_methods_are_called()
        {
            var controller = GetTarget();
            controller.Reassign(CompanyId, ResponsibilityTaskId);
            _reassignResponsibilityTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _reassignResponsibilityTaskViewModelFactory.Verify(x => x.WithResponsibilityTaskId(ResponsibilityTaskId),Times.Once());
            _reassignResponsibilityTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        [Test]
        public void When_View__called_Then_corrct_view_model_is_of_correct_type()
        {
            var controller = GetTarget();
            var result = controller.Reassign(CompanyId, ResponsibilityTaskId);
            Assert.That(result.Model, Is.InstanceOf<ReassignResponsibilityTaskViewModel>());
        }

        [Test]
        public void When_Reassign_called_Then_correct_view_is_returned()
        {
            var controller = GetTarget();
            var result = controller.Reassign(CompanyId, ResponsibilityTaskId);
            Assert.That(result.ViewName, Is.EqualTo("_ReassignResponsibilityTask"));
        }

        private ResponsibilityController GetTarget()
        {
            return new ResponsibilityController(null,
                null,
                _responsibilityTaskService.Object,
                null,
                null,
                null,
                null,
                _reassignResponsibilityTaskViewModelFactory.Object,
                null, 
                null,
                null);
        }
    }
}
