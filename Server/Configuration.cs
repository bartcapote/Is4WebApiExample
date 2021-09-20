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
                new Client()
                {
                    ClientId = "client_id_js",
                    RequireClientSecret = false, // PKCE
                    AllowedGrantTypes = GrantTypes.Code, // code instead of implicit because of PKCE
                    RequirePkce = true, // defaults to true?

                    RedirectUris = { "https://localhost:44366/signin" },
                    PostLogoutRedirectUris = { "https://localhost:44366/" },
                    AllowedCorsOrigins = { "https://localhost:44366" },

                    AllowedScopes =
                    {
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        "MyApiOne",
                        "my.api.claim",
                        "my.scope", // Iss 3
                        "Blob" // TODO Iss1
                    },

                    AccessTokenLifetime = 50,

                    AllowAccessTokensViaBrowser = true,
                    //AlwaysIncludeUserClaimsInIdToken = true // allows to pass user claims to id token hence bloats it with data. not sure if whether allowing scope manually in allowed scopes adds a round trip? doesn't seem like it.
                }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>()
            {
                new ApiScope("MyApiOne"),
                new ApiScope("my.api.claim", displayName: "This is a custom user claim / api scope"),
                new ApiScope("Blob") // TODO Iss1
            };
    }
}
