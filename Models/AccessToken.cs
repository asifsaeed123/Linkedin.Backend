using Newtonsoft.Json;

/**
 * POJO to encapsulate access token from 2/3-legged LinkedIn OAuth 2.0  flow.
 */
[JsonObject(MemberSerialization.OptIn)]
public sealed class Token
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public string ExpiresIn { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("refresh_token_expires_in")]
    public string RefreshTokenExpiresIn { get; set; }

    public Token()
    {
    }
}
