using System;

namespace Services
{

    /**
     * Constants file
     */
    public static class ApiUrls
    {
        public const string LI_FIND_AD_ACCOUNTS_FOR_USER_ENDPOINT = "https://api.linkedin.com/v2/adAccountUsersV2?q=authenticatedUser&oauth2_access_token=";
        public const string LI_FIND_USER_ROLES_ENDPOINT = "https://api.linkedin.com/v2/organizationAcls?q=roleAssignee&oauth2_access_token=";
        public const string LI_ME_ENDPOINT = "https://api.linkedin.com/v2/me?oauth2_access_token=";
        public const string TOKEN_INTROSPECTION_ERROR_MESSAGE = "Error introspecting token, service is not initiated";
        public const string SAMPLE_APP_BASE = "java-sample-application";
        public const string SAMPLE_APP_VERSION = "version 1.0";

        public enum AppName
        {
            OAuth,
            Marketing
        }

        public static readonly string USER_AGENT_OAUTH_VALUE = $"{SAMPLE_APP_BASE} ({SAMPLE_APP_VERSION}, {AppName.OAuth})";
        public static readonly string USER_AGENT_LMS_VALUE = $"{SAMPLE_APP_BASE} ({SAMPLE_APP_VERSION}, {AppName.Marketing})";


        public const string AUTHORIZE_URL = "https://www.linkedin.com/oauth/v2/authorization";
        public const string REQUEST_TOKEN_URL = "https://www.linkedin.com/oauth/v2/accessToken";
        public const string TOKEN = "token";
        public const string CLIENT_SECRET = "client_secret";
        public const string CLIENT_ID = "client_id";
        public const string REFRESH_TOKEN = "refresh_token";
        public const string CODE = "code";
        public const string REDIRECT_URI = "redirect_uri";
        public const string GRANT_TYPE = "grant_type";
        public const string TOKEN_INTROSPECTION_URL = "https://www.linkedin.com/oauth/v2/introspectToken";
        public const int RESPONSE_CODE = 200;
        public const int PORT = 8000;

        public enum GrantType
        {
            CLIENT_CREDENTIALS,
            AUTHORIZATION_CODE,
            REFRESH_TOKEN
        }

        public static string GetGrantType(this GrantType grantType)
        {
            switch (grantType)
            {
                case GrantType.CLIENT_CREDENTIALS:
                    return "client_credentials";
                case GrantType.AUTHORIZATION_CODE:
                    return "authorization_code";
                case GrantType.REFRESH_TOKEN:
                    return "refresh_token";
                default:
                    throw new ArgumentOutOfRangeException(nameof(grantType), grantType, "Invalid grant type");
            }
        }

    }

}