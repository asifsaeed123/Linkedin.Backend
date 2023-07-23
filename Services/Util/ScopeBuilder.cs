using System.Collections.Generic;
using System.Text;

namespace Services.Util
{
    public class ScopeBuilder
    {
        private readonly HashSet<string> scopes = new HashSet<string>();

        public ScopeBuilder(string scope)
        {
            WithScope(scope);
        }

        public ScopeBuilder(params string[] scopes)
        {
            WithScopes(scopes);
        }

        public ScopeBuilder(IEnumerable<string> scopes)
        {
            WithScopes(scopes);
        }

        public ScopeBuilder WithScope(string scope)
        {
            scopes.Add(scope);
            return this;
        }

        public ScopeBuilder WithScopes(params string[] scopes)
        {
            foreach (var scope in scopes)
            {
                this.scopes.Add(scope);
            }
            return this;
        }

        public ScopeBuilder WithScopes(IEnumerable<string> scopes)
        {
            foreach (var scope in scopes)
            {
                this.scopes.Add(scope);
            }
            return this;
        }

        public string Build()
        {
            var scopeBuilder = new StringBuilder();
            foreach (var scope in scopes)
            {
                scopeBuilder.Append(" ").Append(scope);
            }
            return scopeBuilder.Length > 0 ? scopeBuilder.ToString(1, scopeBuilder.Length - 1) : "";
        }
    }
}
