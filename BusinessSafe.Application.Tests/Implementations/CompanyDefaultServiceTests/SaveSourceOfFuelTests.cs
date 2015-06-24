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
    public class SaveSourceOfFuelTests
    {
        private Mock<ISourceOfFuelRepository> _sourceOfFuelRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _sourceOfFuelRepository = new Mock<ISourceOfFuelRepository>();
            _user = new UserForAuditing();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_valid_request_When_save_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateCompanyDefaultService();
            var request = SaveCompanyDefaultRequestBuilder.Create().WithRunMatchCheck(true).Build();

            _sourceOfFuelRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<SourceOfFuel>()))
                .Callback<SourceOfFuel>(par => par.Id = 1);

            _userRepository
                .Setup(x => x.GetById(request.UserId))
                .Returns(_user);

            //When
            target.SaveSourceOfFuel(request);

            //Then
            _sourceOfFuelRepository.Verify(tp => tp.SaveOrUpdate(It.IsAny<SourceOfFuel>()), Times.Once());

        }


        [Test]
        public void Given_valid_request_When_save_Then_should_return_correct_value()
        {
            //Given
            var target = CreateCompanyDefaultService();
            var request = SaveCompanyDefaultRequestBuilder.Create().WithRunMatchCheck(true).Build();

            _sourceOfFuelRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<SourceOfFuel>()))
                .Callback<SourceOfFuel>(par => par.Id = 1);

            _userRepository
                .Setup(x => x.GetById(request.UserId))
                .Returns(_user);

            //When
            var result = target.SaveSourceOfFuel(request);

            //Then
            Assert.That(result, Is.GreaterThan(0));
        }


        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, null, _userRepository.Object, null, null, null, null, _sourceOfFuelRepository.Object, null, null, _log.Object);
            return target;
        }
    }
}