using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Tests.Models;

namespace Tests
{
    [TestFixture]
    public class GetUserByNameTests : TestBase
    {
        [Test]
        public void GetUserByName_withExistUsername_200Ok()
        {
            User expectedUser = new User()
            {
                Id = 9223372036854744349,
                Username = "user1",
                FirstName = "Mike",
                LastName = "Shal",
                Email = "shal@gmail.com",
                Password = "1234",
                Phone = "1234567891",
                UserStatus = 0
            };

            var response = GetUserByName("user1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            CheckBodyForSuccessResponse(response, expectedUser);
        }

        [Test]
        public void GetUserByName_withNotExistUsername_404NotFound()
        {
            var response = GetUserByName("notExistUser");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            CheckBodyForErrorResponse(response, 1, "error", "User not found");
        }

        [Test]
        public void GetUserByName_withoutUsername_405MethodNotAllowed()
        {
            var response = GetUserByName("");

            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        private RestResponse GetUserByName(string name)
        {
            return Get($"v2/user/{name}");
        }

        private void CheckBodyForSuccessResponse(RestResponse response, User expectedUser) 
        {
            string expected = JsonConvert.SerializeObject(expectedUser);
            Assert.AreEqual(expected, response.Content);
        }

        private void CheckBodyForErrorResponse(RestResponse response, int expectedCode,
            string expectedType, string expectedMsg)
        {
            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);

            Assert.AreEqual(expectedCode, errorResponse.Code);
            Assert.AreEqual(expectedType, errorResponse.Type);
            Assert.AreEqual(expectedMsg, errorResponse.Message);
        }
    }
}