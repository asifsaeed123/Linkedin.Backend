using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Util;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Linkedin.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkedInOAuthController : ControllerBase
    {
        private const string API_URL = "https://api.linkedin.com/rest/adTargetingFacets";
        private const string X_RESTLI_PROTOCOL_VERSION = "2.0.0";
        private const string LINKEDIN_VERSION = "202201";

        private readonly IConfiguration configuration;

        public LinkedInOAuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("login")]
        public async Task<IActionResult> OAuth([FromQuery(Name = "code")] string? code)
        {



            // Get the values from the configuration
            string apiKey = configuration["clientId"];
            string apiSecret = configuration["clientSecret"];
            string scope = configuration["scope"];
            string redirectUri = configuration["redirectUri"];

            // Split the scope string into an array of individual scopes
            string[] scopesArray = scope.Split(',');

            var service = new LinkedInOAuthService.LinkedInOAuthServiceBuilder()
                                .ApiKey(apiKey)
                                .ApiSecret(apiSecret)
                                .DefaultScope(new ScopeBuilder(scopesArray).Build())
                                .Callback(redirectUri)
                                .Build();

            if (!string.IsNullOrEmpty(code))
            {
                var result = await service.GetAccessToken3Legged(code);

                return Ok(result);
            }

            string secretState = "secret" + new Random().Next(999_999);

            // Build the authorization URL using the LinkedInOAuthService instance
            string authorizationUrl = service.CreateAuthorizationUrlBuilder()
                                             .State(secretState)
                                             .Build();

            return Ok(authorizationUrl);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile(string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, ApiUrls.LI_ME_ENDPOINT);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return Content(responseBody, "application/json");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode((int)ex.StatusCode);
            }
        }


        [HttpGet("GetAdTargetingEntities")]
        public async Task<IActionResult> GetAdTargetingEntities(string accessToken)
        {

            string apiEndpoint = "https://api.linkedin.com/v2/dotnet?start=10&count=10";

            using (HttpClient httpClient = new HttpClient())
            {
                // Set the Authorization header with the access token
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                try
                {
                    // Send the GET request and retrieve the response
                    HttpResponseMessage response = await httpClient.GetAsync(apiEndpoint);
                    response.EnsureSuccessStatusCode();

                    // Read the response content as a string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                }
                catch (HttpRequestException ex)
                {
                   return  BadRequest();
                }
            }

        }

    }
}
