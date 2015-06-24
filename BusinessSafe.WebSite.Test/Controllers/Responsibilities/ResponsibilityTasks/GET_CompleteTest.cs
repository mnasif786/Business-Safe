using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities.ResponsibilityTasks
{
    [TestFixture]
    [Category("Unit")]
    public class GET_CompleteTest
    {
        private const long CompanyId = 12312L;
        private const long ResponsibilityId = 1L;
        private const long ResponsibilityTaskId = 72L;
        
        private Mock<ICompleteResponsibilityTaskViewModelFactory> _completeResponsibilityTaskViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _completeResponsibilityTaskViewModelFactory =
                new Mock<ICompleteResponsibilityTaskViewModelFactory>();

            _completeResponsibilityTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_completeResponsibilityTaskViewModelFactory.Object);

            _completeResponsibilityTaskViewModelFactory
                .Setup(x => x.WithResponsibilityTaskId(ResponsibilityTaskId))
                .Returns(_completeResponsibilityTaskViewModelFactory.Object);

            _completeResponsibilityTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new CompleteResponsibilityTaskViewModel());
        }

        [Test]
        public void When_View_called_Then_correct_methods_are_called()
        {
            var controller = GetTarget();
            controller.Complete(CompanyId ,ResponsibilityTaskId);
            _completeResponsibilityTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _completeResponsibilityTaskViewModelFactory.Verify(x => x.WithResponsibilityTaskId(ResponsibilityTaskId), Times.Once());
            _completeResponsibilityTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        [Test]
        public void When_View__called_Then_corrct_view_model_is_of_correct_type()
        {
            var controller = GetTarget();
            var result = controller.Complete(CompanyId, ResponsibilityTaskId);
            Assert.That(result.Model, Is.InstanceOf<CompleteResponsibilityTaskViewModel>());
        }
        

        private ResponsibilityController GetTarget()
        {
            return new ResponsibilityController(null,
                null,
                null,
                null,
                null,
                null,
                _completeResponsibilityTaskViewModelFactory.Object, null,
                null, 
                null,
                null);
        }
    }
}
