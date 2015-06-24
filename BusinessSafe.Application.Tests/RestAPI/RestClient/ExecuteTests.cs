using System.Net;

using BusinessSafe.Application.RestAPI;

using Moq;
using NUnit.Framework;
using BusinessSafe.Application.DataTransferObjects;
using RestSharp;
using System;

namespace BusinessSafe.Application.Tests.RestAPI.RestClient
{
    [TestFixture]
    [Category("Unit")]
    public class ExecuteTests
    {
        [Test]
        public void Given_that_execute_method_is_called_Then_correct_response_is_returned()
        {   
            //Given
            var restClientMock = new Mock<RestSharp.RestClient>();
            restClientMock.Setup(rc => rc.Execute<CompanyDetailsDto>(It.IsAny<RestRequest>())).Returns(
                (RestResponse<CompanyDetailsDto>)new RestResponse {StatusCode = HttpStatusCode.OK});

            var target = new RestClientAPI(restClientMock.Object);
            
            //When
            var result =  target.Execute<CompanyDetailsDto>(CreateCompanyRequest(-1));

            //Then
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Given_that_execute_method_is_called_with_unspecified_status_code_Then_throws_exception()
        {
            //Given
            var restClientMock = new Mock<RestSharp.RestClient>();
            restClientMock.Setup(rc => rc.Execute<CompanyDetailsDto>(It.IsAny<RestRequest>())).Returns(
                (RestResponse<CompanyDetailsDto>)new RestResponse { StatusCode = HttpStatusCode.MethodNotAllowed });

            var target = new RestClientAPI(restClientMock.Object);

            //When
            
            //Then
            Assert.Throws<Exception>(()=>target.Execute<CompanyDetailsDto>(CreateCompanyRequest(-1)),
                string.Concat("Unspecified exception. Status code: ", HttpStatusCode.MethodNotAllowed.ToString()));
        }

        [Test]
        public void Given_that_execute_method_is_called_with_not_found_status_code_Then_throws_exception()
        {
            //Given
            var restClientMock = new Mock<RestSharp.RestClient>();
            restClientMock.Setup(rc => rc.Execute<CompanyDetailsDto>(It.IsAny<RestRequest>())).Returns(
                (RestResponse<CompanyDetailsDto>)new RestResponse { StatusCode = HttpStatusCode.NotFound });

            var target = new RestClientAPI(restClientMock.Object);

            //When

            //Then
            Assert.Throws<Exception>(() => target.Execute<CompanyDetailsDto>(CreateCompanyRequest(-1)),
                "Client not fount!");
        }
        
        private static RestRequest CreateCompanyRequest(int companyId)
        {
            var request = new RestRequest("Company/{id}", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };
            request.AddUrlSegment("id", companyId.ToString());
            return request;
        }
    }


}
