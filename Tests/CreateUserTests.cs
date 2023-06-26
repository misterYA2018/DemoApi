using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Tests.Models;

namespace Tests
{
    [TestFixture]
    public class CreateUserTests : TestBase
    {
        [Test]
        public void CreateUser_withCorrectFields_200Ok()
        {
            var user = new User()
            {
                Id = 1,
                Username = "Test Username",
                FirstName = "Test FirstName",
                LastName = "Test LastName",
                Email = "Test Email",
                Password = "Test Password",
                Phone = "Test Phone",
                UserStatus = 2,
            };

            var response = CreateUser(user);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            CheckBodyForSuccessResponse(response, expectedMsg: user.Id.ToString());
        }


        private RestResponse CreateUser(string body)
        {
            return Post($"/v2/user", body, ContentType.Json);
        }

        private RestResponse CreateUser(User userCreateRequest)
        {
            return CreateUser(JsonConvert.SerializeObject(userCreateRequest));
        }

        private void CheckBodyForSuccessResponse(RestResponse response, int expectedCode = 200, 
            string expectedType = "unknown", string expectedMsg = "") 
        {

            UserCreateResponse createResponse = JsonConvert.DeserializeObject<UserCreateResponse>(response.Content);

            Assert.AreEqual(expectedCode, createResponse.Code);
            Assert.AreEqual(expectedType, createResponse.Type);
            Assert.AreEqual(expectedMsg, createResponse.Message);
        }
    }
}
