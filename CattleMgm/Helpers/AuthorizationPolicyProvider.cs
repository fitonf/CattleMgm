using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CattleMgm.Helpers
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _Authorization;
        private readonly IConfiguration _configuration;
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> AuthOptions, IConfiguration configuration) : base(AuthOptions)
        {
            _Authorization = AuthOptions.Value;
            _configuration = configuration;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);
            string[] claims = policyName.Split(":");


            if (policy == null)
            {
                policy = new AuthorizationPolicyBuilder().
                            RequireClaim(claims[0], claims[1]).Build();
                _Authorization.AddPolicy(policyName, policy);
            }

            return policy;
        }
    }
}
