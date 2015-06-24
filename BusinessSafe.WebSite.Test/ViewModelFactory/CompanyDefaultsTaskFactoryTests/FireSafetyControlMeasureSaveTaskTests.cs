using System;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Tasks;
using BusinessSafe.WebSite.Tests.Builder;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.CompanyDefaultsTaskFactoryTests
{
    [TestFixture]
    [Category("Unit")]
    public class FireSafetyControlMeasureSaveTaskTests
    {
        private Mock<ICompanyDefaultService> _companyDefaultService;
        private Mock<IDoesCompanyDefaultAlreadyExistGuard> _doesCompanyDefaultAlreadyExistGuard;
        private Guid _userId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            _companyDefaultService = new Mock<ICompanyDefaultService>();
            _doesCompanyDefaultAlreadyExistGuard = new Mock<IDoesCompanyDefaultAlreadyExistGuard>();
        }

       
        [Test]
        public void When_executes_Then_should_call_SaveFireSafetyControlMeasure_on_service()
        {
            //Given
            var saveTask = CreateSaveTask();

            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();

            //When
            saveTask.Execute(request, _userId);

            //Then
            _companyDefaultService.Verify(cds => cds.SaveFireSafetyControlMeasure(It.IsAny<SaveCompanyDefaultRequest>()), Times.Once());
        }


        [Test]
        public void When_executes_Then_should_return_correct_result()
        {
            //Given
            var saveTask = CreateSaveTask();

            const long expectedId = 2;
            _companyDefaultService.Setup(x => x.SaveFireSafetyControlMeasure(It.IsAny<SaveCompanyDefaultRequest>())).Returns(expectedId);

            var request = SaveCompanyDefaultViewModelBuilder.Create().Build();

            //When
            var result = saveTask.Execute(request, _userId);

            //Then
            Assert.That(result.Id, Is.EqualTo(2));
        }
        
        private FireSafetyControlMeasureSaveTask CreateSaveTask()
        {
            var saveTask = new FireSafetyControlMeasureSaveTask(_companyDefaultService.Object, _doesCompanyDefaultAlreadyExistGuard.Object);
            return saveTask;
        }
    }
}