using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Calculator.Api;
using Xunit;

namespace Calculator.IntegrationTests.Fixtures
{
    public class ControllerFixture : IDisposable
    {
        public HttpClient Client { get; }

        private HttpServer Server { get; }

        public ControllerFixture()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            Server = new HttpServer(config);
            Client = CreateClient(Server);
        }

        private static HttpClient CreateClient(HttpServer server)
        {
            var client = new HttpClient(server);
            client.BaseAddress=new Uri("http://localhost");
            client.DefaultRequestHeaders.Add("Accept", new[] { "application/json" });
            return client;
        }

        public void Dispose()
        {
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}