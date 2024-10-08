using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.TeamsFx;
using OrderingApp.Data.Models;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly TeamsUserCredential _teamsUserCredential;
        private readonly IConfiguration _configuration;
        private static readonly string[] _scope = { "User.Read" };
        public UserProfileService(TeamsUserCredential teamsUserCredential, IConfiguration configuration)
        {
            _teamsUserCredential = teamsUserCredential;
            _configuration = configuration;
        }

        public async Task<Guid> GetUserProfileIdAsync()
        {
            Guid id;
            var user = await GetUserProfileAsync();
            if (Guid.TryParse(user.Id, out id))
                return id;
            else
                throw new WrongProfileCredentialsException("We can't load profile credentials");
        }

        public async Task<User> GetUserProfileAsync()
        {
            var tokenCredential = await GetOnBehalfOfCredential();
            var graphClient = GetGraphServiceClient(tokenCredential);
            return await graphClient.Me.GetAsync();
        }

        private GraphServiceClient GetGraphServiceClient(TokenCredential tokenCredential)
        {
            var client = new GraphServiceClient(tokenCredential, _scope);
            return client;
        }

        private async Task<OnBehalfOfCredential> GetOnBehalfOfCredential()
        {
            var config = _configuration.Get<ConfigOptions>();
            var tenantId = config.TeamsFx.Authentication.OAuthAuthority.Remove(0, "https://login.microsoftonline.com/".Length);
            AccessToken ssoToken = await _teamsUserCredential.GetTokenAsync(new TokenRequestContext(null), new CancellationToken());
            return new OnBehalfOfCredential(
                tenantId,
                config.TeamsFx.Authentication.ClientId,
                config.TeamsFx.Authentication.ClientSecret,
                ssoToken.Token
            );
        }
    }
}