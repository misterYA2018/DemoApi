using RestSharp;

namespace Tests
{
    public class TestBase
    {
        public RestClient client = new RestClient("https://petstore.swagger.io");

        public RestResponse Get(string url)
        {
            var request = new RestRequest(url, Method.Get);
            return client.Execute(request);
        }

        public RestResponse Post(string url, string body, ContentType contentType = null)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddBody(body, contentType);

            return client.Execute(request);
        }
    }
}
