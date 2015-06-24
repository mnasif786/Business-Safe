using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDefaultServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class SaveHazardTests
    {
        private Mock<IHazardRepository> _hazardRepository;
        private Mock<IHazardTypeRepository> _hazardTypeRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();
            _hazardRepository = new Mock<IHazardRepository>();
            _hazardTypeRepository = new Mock<IHazardTypeRepository>();
            _user = new UserForAuditing();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_saving_hazard_Then_should_call_correct_methods()
        {
            
            //Given
            var target = CreateCompanyDefaultService();
            var request = SaveCompanyHazardDefaultRequestBuilder
                            .Create()
                            .WithRunMatchCheck(true)
                            .WithApplicableRiskAssessmentTypes(new [] {1, 2})
                            .Build();

            _hazardRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<Hazard>()))
                .Callback<Hazard>(par => par.Id = 1);

            _userRepository
                .Setup(x => x.GetById(request.UserId))
                .Returns(_user);

            _hazardTypeRepository.Setup(x => x.LoadById(It.IsAny<int>())).Returns(new HazardType());

            //When
            target.SaveHazard(request);

            //Then
            _hazardRepository.Verify(tp => tp.SaveOrUpdate(It.IsAny<Hazard>()), Times.Once());

        }

        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(_hazardRepository.Object, null, _userRepository.Object, _hazardTypeRepository.Object, null, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}
