using System.Web;

namespace Services.Util
{

    public class AuthorizationUrlBuilder
    {
        private string state;
        private IDictionary<string, string> additionalParams;
        private readonly LinkedInOAuthService oauth20Service;

        public AuthorizationUrlBuilder(LinkedInOAuthService oauth20Service)
        {
            this.oauth20Service = oauth20Service;
        }

        public AuthorizationUrlBuilder State(string state)
        {
            this.state = state;
            return this;
        }

        public AuthorizationUrlBuilder AdditionalParams(IDictionary<string, string> additionalParams)
        {
            this.additionalParams = additionalParams;
            return this;
        }

        public string Build()
        {
            var queryParams = new Dictionary<string, string>
        {
            { "response_type", "code" },
            { "client_id", oauth20Service.GetApiKey() },
            { "redirect_uri", oauth20Service.GetRedirectUri() },
            { "state", state },
            { "scope", Uri.EscapeDataString(oauth20Service.GetScope()) }
        };

            if (additionalParams != null)
            {
                foreach (var kvp in additionalParams)
                {
                    queryParams[kvp.Key] = kvp.Value;
                }
            }

            var queryString = string.Join("&", queryParams
                .Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

            return $"{ApiUrls.AUTHORIZE_URL}?{queryString}";
        }
    }

}
