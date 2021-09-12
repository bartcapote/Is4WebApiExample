using System.Collections.Generic;
using IdentityServer4.Models;

namespace Server
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource // Iss 3
                {
                    Name = "my.scope",
                    UserClaims =
                    {
                        "my.claim"
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource>()
            {
                new ApiResource("MyApiOne", new string[] { "my.api.claim" }) // Iss 2 adding an array of claims to attach them to JWT
                {
                    Scopes = {"Blob"} // TODO Iss 1 this adds "aud" claim to JWT (api resource grouping) https://docs.identityserver.io/en/latest/topics/resources.html
                }
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>()
            {
                //new Client
                //{
                //    ClientId = "my_client_id",
                //    ClientSecrets = { new Secret("client_secret".ToSha256()) },

                //    AllowedGrantTypes = GrantTypes.ClientCredentials,

                //    AllowedScopes = { "MyApiOne" }
                //},
                new Client()
                {
                    ClientId = "client_id_js",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:44366/home/signin" },
                    AllowedCorsOrigins = { "https://localhost:44366" },

                    AllowedScopes =
                    {
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        "MyApiOne",
                        "Blob" // TODO Iss1
                    },

                    AllowAccessTokensViaBrowser = true,
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>()
            {
                new ApiScope("MyApiOne"),
                new ApiScope("Blob") // TODO Iss1
            };
    }
}
