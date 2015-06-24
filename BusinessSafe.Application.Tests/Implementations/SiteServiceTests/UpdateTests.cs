using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Implementations;
using BusinessSafe.Application.Implementations.Sites;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Application.Tests.Builder;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Email;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using FluentValidation;
using Moq;
using NUnit.Framework;
using System;

namespace BusinessSafe.Application.Tests.Implementations.SiteServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        private Mock<ISiteRepository> _siteAddressRepository;
        private Mock<ISiteStructureElementRepository> _siteRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _siteAddressRepository = new Mock<ISiteRepository>();
            _siteRepository = new Mock<ISiteStructureElementRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
            _siteRepository.Setup(sr => sr.GetById(It.IsAny<long>())).Returns(Site.Create(20, null, 200, "Parent", "Ref", "", new UserForAuditing()));
            _userRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>())).Returns(new UserForAuditing());
            _log = new Mock<IPeninsulaLog>();
        }


        [Test]
        public void Given_that_update_us_called_Then_site_address_is_saved()
        {
            //Given            
            var target = CreateSiteService();
            _siteAddressRepository.Setup(srs => srs.SaveOrUpdate(It.IsAny<Site>())).Callback<Site>(par => par.Id = 1);

            //When
            target.CreateUpdate(SiteAddressRequestBuilder.Create().WithName("some name").Build());

            //Then
            _siteAddressRepository.Verify(srs => srs.SaveOrUpdate(It.IsAny<Site>()), Times.Once());
        }

        [Test]
        public void Given_that_name_is_not_set_When_site_address_request_is_updated_Then_error_is_returned()
        {
            //Given
            var target = CreateSiteService();

            //When
            TestDelegate testDel = () => target.CreateUpdate(SiteAddressRequestBuilder.Create().WithName(string.Empty).Build());

            //Then
            Assert.Throws<ValidationException>(testDel, "Name is required");
        }

        [TestCase(100)]
        [TestCase(200)]
        public void Given_that_site_address_is_saved_When_id_is_returned_Then_id_is_correct(long expectedIdToReturn)
        {
            //Given                       
            _siteAddressRepository.Setup(srs => srs.SaveOrUpdate(It.IsAny<Site>())).Callback<Site>(par => par.Id = expectedIdToReturn);
            var target = CreateSiteService();

            //When
            var result = target.CreateUpdate(SiteAddressRequestBuilder.Create().WithName("some name").Build());

            //Then
            Assert.That(result, Is.EqualTo(expectedIdToReturn));
        }

        //[Test]
        //public void Given_that_address_line_one_is_changed_When_site_address_saved_Then_email_is_sent()
        //{
        //    //Given
        //    const long id = 10;
        //    const long companyId = 100;
        //    const long parentId = 1;

        //    _siteAddressRepository.Setup(srs => srs.SaveOrUpdate(It.IsAny<Site>())).Callback<Site>(par => par.Id = 1);
        //    _siteAddressRepository.Setup(srs => srs.GetById(id)).Returns(Site.Create(20, Site.Create(null, null, companyId, "Parent", "this is parent", new UserForAuditing()), 200, "Parent", "Ref", new UserForAuditing()));

        //    var target = CreateSiteService();

        //    //When
        //    target.CreateUpdate(SiteAddressRequestBuilder.Create().WithParentId(parentId).WithId(id).WithSiteId(id).WithClientId(companyId).WithName("some name").WithAddressLine1("The address one").Build());

        //    //Then
        //    _email.Verify(mec => mec.Send(), Times.Once());
        //}

        //[Test]
        //public void Given_that_address_line_one_is_not_changed_When_site_address_saved_Then_email_not_sent()
        //{
        //    //Given
        //    const long id = 10;
        //    const long companyId = 100;            
        //    _clientService.Setup(csm => csm.GetSite(companyId, id)).Returns(SiteAddressDtoBuilder.Create().WithAddressLine1("It is not changed").Build());
        //    _siteAddressRepository.Setup(srs => srs.SaveOrUpdate(It.IsAny<Site>())).Callback<Site>(par => par.Id = 1);
        //    _siteAddressRepository.Setup(srs => srs.GetById(id)).Returns(Site.Create(20, Site.Create(null, null, companyId, "Parent", "this is parent", new UserForAuditing()), 200, "Parent", "Ref", new UserForAuditing()));

        //    var target = CreateSiteService();

        //    //When
        //    target.CreateUpdate(SiteAddressRequestBuilder.Create().WithId(id).WithClientId(companyId).WithName("some name").WithAddressLine1("It is not changed").Build());

        //    //Then
        //    _email.Verify(mec => mec.Send(), Times.Never());
        //}

        //[Test]
        //public void Given_that_address_line_one_is_changed_When_site_address_saved_Then_email_template_is_retrieved()
        //{
        //    //Given
        //    const long id = 10;
        //    const long companyId = 100;          
        //    _emailTemplateRepository.Setup(mec => mec.GetByEmailTemplateName(EmailTemplateName.SiteAddressChangeNotification)).Returns(EmailTemplate.Create("Test", "Test", "body test"));
        //    _clientService.Setup(csm => csm.GetSite(companyId, id)).Returns(SiteAddressDtoBuilder.Create().WithAddressLine1("it is changed").Build());
        //    _siteAddressRepository.Setup(srs => srs.SaveOrUpdate(It.IsAny<Site>())).Callback<Site>(par => par.Id = 1);
        //    _siteAddressRepository.Setup(srs => srs.GetById(id)).Returns(Site.Create(20, Site.Create(null, null, companyId, "Parent", "this is parent", new UserForAuditing()), 200, "Parent", "Ref", new UserForAuditing()));

        //    var target = CreateSiteService();

        //    //When
        //    target.CreateUpdate(SiteAddressRequestBuilder.Create().WithId(id).WithSiteId(id).WithClientId(companyId).WithName("some name").WithAddressLine1("The address one").Build());

        //    //Then
        //    _emailTemplateRepository.Verify(mec => mec.GetByEmailTemplateName(EmailTemplateName.SiteAddressChangeNotification), Times.Once());
        //    _templateEngine.Verify(mec => mec.Render(It.IsAny<CompanyDetails>(), It.IsAny<string>()), Times.Once());
        //}

        //[Test]
        //public void Given_that_address_line_one_is_not_changed_When_site_address_saved_Then_email_template_is_not_retrieved()
        //{
        //    //Given
        //    const long id = 10;
        //    const long companyId = 100;
        //    _clientService.Setup(csm => csm.GetSite(id, companyId)).Returns(SiteAddressDtoBuilder.Create().WithAddressLine1("It is not changed").Build());
        //    _siteAddressRepository.Setup(srs => srs.SaveOrUpdate(It.IsAny<Site>())).Callback<Site>(par => par.Id = 1);
        //    _siteAddressRepository.Setup(srs => srs.GetById(id)).Returns(Site.Create(20, Site.Create(null, null, companyId, "Parent", "this is parent", new UserForAuditing()), 200, "Parent", "Ref", new UserForAuditing()));

        //    var target = CreateSiteService();

        //    //When
        //    target.CreateUpdate(SiteAddressRequestBuilder.Create().WithId(id).WithClientId(companyId).WithName("some name").WithAddressLine1("It is not changed").Build());

        //    //Then
        //    _emailTemplateRepository.Verify(mec => mec.GetByEmailTemplateName(It.IsAny<EmailTemplateName>()), Times.Never());
        //    _templateEngine.Verify(mec => mec.Render(It.IsAny<CompanyDetails>(), It.IsAny<string>()), Times.Never());
        //}

        //[Test]
        //public void Given_that_only_site_address_information_is_changed_Then_return_value_is_true_for_address_information_is_changed_flag()
        //{
        //    //Given
        //    const long id = 10;
        //    const long companyId = 100;
        //    var target = CreateSiteService();
        //    _clientService.Setup(csm => csm.GetSite(companyId, id)).Returns(SiteAddressDtoBuilder.Create().WithAddressLine1("It is not changed").Build());
        //    _siteAddressRepository.Setup(srs => srs.GetById(id)).Returns(Site.Create(20, Site.Create(null, null, companyId, "Parent", "this is parent", new UserForAuditing()), 200, "some name", "Ref", new UserForAuditing()));            

        //    //When
        //    var result = target.CreateUpdate(SiteAddressRequestBuilder.Create().WithId(id).WithSiteId(id).WithClientId(companyId).WithName("some name").WithReference("Ref").WithLinkToSiteGroupId(5).WithAddressLine1("It is changed").Build());

        //    //Then
        //    Assert.That(result.AddressInformationChange, Is.True);
        //}

        //[Test]
        //public void Given_that_only_site_details_is_changed_Then_return_value_is_false_for_address_information_is_changed_flag()
        //{
        //    //Given
        //    const long id = 10;
        //    const long companyId = 100;
        //    var target = CreateSiteService();
        //    _clientService.Setup(csm => csm.GetSite(companyId, id)).Returns(SiteAddressDtoBuilder.Create().WithAddressLine1("It is not changed").Build());
        //    _siteAddressRepository.Setup(srs => srs.GetById(id)).Returns(Site.Create(20, Site.Create(null, null, companyId, "Parent", "this is parent", new UserForAuditing()), 200, "Parent", "Ref", new UserForAuditing()));

        //    //When
        //    var result = target.CreateUpdate(SiteAddressRequestBuilder.Create().WithId(id).WithSiteId(id).WithName("some name").WithClientId(companyId).WithAddressLine1("It is not changed").Build());

        //    //Then
        //    Assert.That(result.AddressInformationChange, Is.False);
        //}

        //[Test]
        //public void Given_that_site_details_and_site_address_is_changed_Then_return_value_is_false_for_address_information_is_changed_flag()
        //{
        //    //Given
        //    const long id = 10;
        //    const long companyId = 100;
        //    var target = CreateSiteService();
        //    _clientService.Setup(csm => csm.GetSite(companyId, id)).Returns(SiteAddressDtoBuilder.Create().WithAddressLine1("It is not changed").Build());
        //    _siteAddressRepository.Setup(srs => srs.GetById(id)).Returns(Site.Create(20, Site.Create(null, null, companyId, "Parent", "this is parent", new UserForAuditing()), companyId, "Parent", "Ref", new UserForAuditing()));

        //    //When
        //    var result = target.CreateUpdate(SiteAddressRequestBuilder.Create().WithId(id).WithSiteId(id).WithName("some name").WithClientId(companyId).WithAddressLine1("It is not changed 2").Build());

        //    //Then
        //    //Assert.That(result.AddressInformationChange, Is.True);
        //}

        private SiteService CreateSiteService()
        {
            var target = new SiteService(_siteAddressRepository.Object, _siteRepository.Object, _userRepository.Object,null);
            return target;
        }
    }
}
