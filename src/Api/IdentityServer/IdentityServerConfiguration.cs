using FoodVault.Modules.UserAccess.Application.Contracts;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace FoodVault.Api.IdentityServer
{
    /// <summary>
    /// Identity server configuration
    /// </summary>
    internal class IdentityServerConfiguration
    {
        /// <summary>
        /// Gets all available APIs.
        /// </summary>
        /// <returns>An enumerable of ApiResources.</returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("foodvault.api", "FoodVault API")
                {
                    Scopes = {
                        "foodvault.api.read",
                        "foodvault.api.write"
                    },
                    UserClaims = {
                        CustomClaimTypes.Roles
                    }
                }
            };
        }

        /// <summary>
        /// Gets all available ApiScopes.
        /// </summary>
        /// <returns>An enumerable of ApiScopes.</returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("foodvault.api.read"),
                new ApiScope("foodvault.api.write")
            };
        }

        /// <summary>
        /// Gets all available identity resources.
        /// </summary>
        /// <returns>An enumarble of IdentityResources.</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(CustomClaimTypes.Roles, new List<string>
                {
                    CustomClaimTypes.Roles
                })
            };
        }

        /// <summary>
        /// Gets all available clients.
        /// </summary>
        /// <returns>An enumerable of clients.</returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {
                        new Secret("dummy".Sha256())
                    },
                    AllowedScopes = {
                        "foodvault.api.read",
                        "foodvault.api.write",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },
            };
        }
    }
}
