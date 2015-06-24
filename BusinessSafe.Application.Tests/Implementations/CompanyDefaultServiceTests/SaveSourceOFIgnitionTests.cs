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
    public class SaveSourceOFIgnitionTests
    {
        private Mock<ISourceOfIgnitionRepository> _sourceOFIgnitionRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private UserForAuditing _user;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _sourceOFIgnitionRepository = new Mock<ISourceOfIgnitionRepository>();
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

            _sourceOFIgnitionRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<SourceOfIgnition>()))
                .Callback<SourceOfIgnition>(par => par.Id = 1);

            _userRepository
                .Setup(x => x.GetById(request.UserId))
                .Returns(_user);

            //When
            target.SaveSourceOfIgnition(request);

            //Then
            _sourceOFIgnitionRepository.Verify(tp => tp.SaveOrUpdate(It.IsAny<SourceOfIgnition>()), Times.Once());

        }


        [Test]
        public void Given_valid_request_When_save_Then_should_return_correct_value()
        {
            //Given
            var target = CreateCompanyDefaultService();
            var request = SaveCompanyDefaultRequestBuilder.Create().WithRunMatchCheck(true).Build();

            _sourceOFIgnitionRepository
                .Setup(rr => rr.SaveOrUpdate(It.IsAny<SourceOfIgnition>()))
                .Callback<SourceOfIgnition>(par => par.Id = 1);

            _userRepository
                .Setup(x => x.GetById(request.UserId))
                .Returns(_user);

            //When
            var result = target.SaveSourceOfIgnition(request);

            //Then
            Assert.That(result, Is.GreaterThan(0));
        }


        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, null, _userRepository.Object, null, null, null, _sourceOFIgnitionRepository.Object, null, null, null, _log.Object);
            return target;
        }
    }
}