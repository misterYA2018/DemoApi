using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Tests.Models;

namespace Tests
{
    [TestFixture]
    public class LoginUserTests : TestBase
    {
        private static readonly string _correctUserName = "TestUser1";
        private static readonly string _correctPassword = "Test_pass1!";

        [Test]
        public void LoginUser_withCorrectCredential_200Ok()
        {
            var response = LoginUser(_correctUserName, _correctPassword);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            CheckBodyForSuccessResponse(response);
        }

        [TestCase("TestUser", "", TestName = "LoginUser_WithEmptyPassword_400BadRequest")]
        [TestCase("", "Password1!", TestName = "LoginUser_WithEmptyLogin_400BadRequest")]
        [TestCase("", "", TestName = "LoginUser_WithEmptyLoginAndPassword_400BadRequest")]
        public void IncorrectCredentialTests(string name, string password)
        {
            var response = LoginUser(name, password);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            //Добавить проверку тела ответа. Сейчас ответ такой же, 
            // как при успешном логине и ожидаемый формат тела ошибки не известен
        }


        private RestResponse LoginUser(string name, string password)
        {
            return Get($"v2/user/login?username={name}&password={password}");
        }

        private void CheckBodyForSuccessResponse(RestResponse response, int expectedCode = 200, 
            string expectedType = "unknown", string expectedMsgRegex = "logged in user session:\\d{13}") 
        {

            UserLoginResponse loginResponse = JsonConvert.DeserializeObject<UserLoginResponse>(response.Content);

            Assert.AreEqual(expectedCode, loginResponse.Code);
            Assert.AreEqual(expectedType, loginResponse.Type);

            Assert.That(loginResponse.Message, Does.Match(expectedMsgRegex));
        }
    }
}
