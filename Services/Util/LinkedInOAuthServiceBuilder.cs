//using Services.Util;
//using System;

//namespace LinkedInOAuth.Service
//{
//    public class LinkedInOAuthServiceBuilder
//    {
//        private string redirectUri;
//        private string apiKey;
//        private string apiSecret;
//        private string scope;

//        public LinkedInOAuthServiceBuilder ApiKey(string apiKey)
//        {
//            Preconditions.CheckEmptyString(apiKey, "Invalid Api key");
//            this.apiKey = apiKey;
//            return this;
//        }

//        public LinkedInOAuthServiceBuilder ApiSecret(string apiSecret)
//        {
//            Preconditions.CheckEmptyString(apiSecret, "Invalid Api secret");
//            this.apiSecret = apiSecret;
//            return this;
//        }

//        public LinkedInOAuthServiceBuilder Callback(string callback)
//        {
//            this.redirectUri = callback;
//            return this;
//        }

//        private LinkedInOAuthServiceBuilder SetScope(string scope)
//        {
//            Preconditions.CheckEmptyString(scope, "Invalid OAuth scope");
//            this.scope = scope;
//            return this;
//        }

//        public LinkedInOAuthServiceBuilder DefaultScope(ScopeBuilder scopeBuilder)
//        {
//            return SetScope(scopeBuilder.Build());
//        }

//        public LinkedInOAuthServiceBuilder DefaultScope(string scope)
//        {
//            return SetScope(scope);
//        }

//        public LinkedInOAuthService Build()
//        {
//            LinkedInOAuthService baseService = new LinkedInOAuthService(this);
//            return baseService;
//        }
//    }
//}
