using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Services;
using Services.Util;
using static Services.ApiUrls;

public class LinkedInOAuthService
{
    private readonly string redirectUri;
    private readonly string apiKey;
    private readonly string apiSecret;
    private readonly string scope;

    private LinkedInOAuthService(LinkedInOAuthServiceBuilder oauthServiceBuilder)
    {
        redirectUri = oauthServiceBuilder.redirectUri;
        apiKey = oauthServiceBuilder.apiKey;
        apiSecret = oauthServiceBuilder.apiSecret;
        scope = oauthServiceBuilder.scope;
    }

    public string GetRedirectUri()
    {
        return redirectUri;
    }

    public string GetApiKey()
    {
        return apiKey;
    }

    public string GetApiSecret()
    {
        return apiSecret;
    }

    public string GetScope()
    {
        return scope;
    }

    public AuthorizationUrlBuilder CreateAuthorizationUrlBuilder()
    {
        return new AuthorizationUrlBuilder(this);
    }

    public async Task<Token> GetAccessToken3Legged(string code)
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", GrantType.AUTHORIZATION_CODE.GetGrantType() },
            { "code", code },
            { "redirect_uri", redirectUri },
            { "client_id", apiKey },
            { "client_secret", apiSecret }
        };

        var content = new FormUrlEncodedContent(parameters);
        var httpClient = new HttpClient();

        //httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(USER_AGENT_OAUTH_VALUE));

        var response = await httpClient.PostAsync(ApiUrls.REQUEST_TOKEN_URL, content);

        var data = await response.Content.ReadAsStringAsync();

        var result = await Deserialize<Token>(data);

        return result;
    }

    public HttpContent GetAccessTokenFromRefreshToken(string refreshToken)
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", GrantType.REFRESH_TOKEN.GetGrantType() },
            { "refresh_token", refreshToken },
            { "client_id", apiKey },
            { "client_secret", apiSecret }
        };

        var content = new FormUrlEncodedContent(parameters);
        return content;
    }

    public HttpContent GetAccessToken2Legged()
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", GrantType.CLIENT_CREDENTIALS.GetGrantType() },
            { "client_id", apiKey },
            { "client_secret", apiSecret }
        };

        var content = new FormUrlEncodedContent(parameters);
        return content;
    }

    public HttpContent IntrospectToken(string token)
    {
        var parameters = new Dictionary<string, string>
        {
            { "client_id", apiKey },
            { "client_secret", apiSecret },
            { "token", token }
        };

        var content = new FormUrlEncodedContent(parameters);
        return content;
    }

    public async Task<T> Deserialize<T>(string json)
    {
        // Use JsonConvert.DeserializeObject<T>() to deserialize JSON data to the specified type T.
        return JsonConvert.DeserializeObject<T>(json);
    }
    public class LinkedInOAuthServiceBuilder
    {
        public string redirectUri;
        public string apiKey;
        public string apiSecret;
        public string scope;

        public LinkedInOAuthServiceBuilder ApiKey(string apiKey)
        {
            Preconditions.CheckEmptyString(apiKey, "Invalid Api key");
            this.apiKey = apiKey;
            return this;
        }

        public LinkedInOAuthServiceBuilder ApiSecret(string apiSecret)
        {
            Preconditions.CheckEmptyString(apiSecret, "Invalid Api secret");
            this.apiSecret = apiSecret;
            return this;
        }

        public LinkedInOAuthServiceBuilder Callback(string callback)
        {
            redirectUri = callback;
            return this;
        }

        private LinkedInOAuthServiceBuilder SetScope(string scope)
        {
            Preconditions.CheckEmptyString(scope, "Invalid OAuth scope");
            this.scope = scope;
            return this;
        }

        public LinkedInOAuthServiceBuilder DefaultScope(ScopeBuilder scopeBuilder)
        {
            return SetScope(scopeBuilder.Build());
        }

        public LinkedInOAuthServiceBuilder DefaultScope(string scope)
        {
            return SetScope(scope);
        }

        public LinkedInOAuthService Build()
        {
            return new LinkedInOAuthService(this);
        }
    }
}
