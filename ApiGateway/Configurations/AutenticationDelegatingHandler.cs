using Ocelot.Middleware;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ApiGateway.Configurations
{
    public class AutenticationDelegatingHandler : DelegatingHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        record ErrorToken(string Message);

        public AutenticationDelegatingHandler(HttpClient httpClient, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration["Customer_ServiceIp"]);

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (request.Headers.Authorization is null) {
                var json = JsonSerializer.Serialize(new { message = "Enter a bearer token" });
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                return response;
            }


            var authorizationHeader = request.Headers.Authorization;


            var token = authorizationHeader.Parameter;

            var IsValid = await _httpClient.GetFromJsonAsync<bool>($"/api/customer/validatetoken/{token}");

            if (!IsValid) {

                var json = JsonSerializer.Serialize(new {message="Token Invalid" });
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                return response;
            }
          
            return await base.SendAsync(request, cancellationToken);
        }





    }


}
