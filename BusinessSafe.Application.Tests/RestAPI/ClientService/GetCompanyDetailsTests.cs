using System;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Application.RestAPI.Responses;
using NUnit.Framework;
using Moq;
using RestSharp;

namespace BusinessSafe.Application.Tests.RestAPI.ClientService
{
    [TestFixture]
    [Category("Unit")]
    public class GetCompanyDetailsTests
    {

        [Test]
        public void Given_that_GetCompanyDetails_method_is_called_Then_the_correct_request_is_sent_in()
        {
            //Given
            var restClientSpy = new RestClientAPISpy();
            var target = new Application.RestAPI.ClientService(restClientSpy);

            //When
            target.GetCompanyDetails(default(int));

            //Then
            Assert.That(restClientSpy.Request, Is.Not.Null);
        }

        [Test]
        public void Given_that_non_existant_companyId_is_passed_in_Then_exception_is_thrown()
        {
            //Given
            var restClientAPIClientSpy = new Mock<IRestClientAPI>();

            var target = new Application.RestAPI.ClientService(restClientAPIClientSpy.Object);

            //Then
            Assert.Throws<Exception>(() => target.GetCompanyDetails(-1), "Company not found!");
        }

        [Test]
        public void Given_that_GetSite_method_is_called_Then_the_correct_request_is_sent_in()
        {
            //Given
            var restClientSpy = new RestClientAPISpy();            
            var target = new Application.RestAPI.ClientService(restClientSpy);
            
            //When
            target.GetSite(default(int), default(int));

            //Then
            Assert.That(restClientSpy.Request, Is.Not.Null);
        }


        [Test]
        public void Given_that_non_existant_siteId_is_passed_in_Then_exception_is_thrown()
        {
            //Given
            var restClientAPIClientSpy = new Mock<IRestClientAPI>();

            var target = new Application.RestAPI.ClientService(restClientAPIClientSpy.Object);

            //Then
            Assert.Throws<Exception>(() => target.GetSite(-1, -1), "Company not found!");
        }

        [Test]
        public void Given_that_GetSites_method_is_called_Then_the_correct_request_is_sent_in()
        {
            //Given
            var restClientSpy = new RestClientAPISpy();
            var target = new Application.RestAPI.ClientService(restClientSpy);

            //When
            target.GetSites(default(int));

            //Then
            Assert.That(restClientSpy.Request, Is.Not.Null);
        }


        [Test]
        public void Given_that_non_existant_siteId_is_passed_in_When_GetSites_called_Then_exception_is_thrown()
        {
            //Given
            var restClientAPIClientSpy = new Mock<IRestClientAPI>();

            var target = new Application.RestAPI.ClientService(restClientAPIClientSpy.Object);

            //Then
            Assert.Throws<Exception>(() => target.GetSites(-1), "Company not found!");
        }


    }

    public class RestClientAPISpy : IRestClientAPI
    {

        public RestRequest Request { get; private set; }

        public T Execute<T>(RestRequest request) where T : new()
        {
            Request = request;
            return new T();
        }


    }
}
