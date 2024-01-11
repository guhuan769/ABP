using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace MicroClassroom.Identity.IdentityServer;

public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IApiResourceRepository _apiResourceRepository;
    private readonly IApiScopeRepository _apiScopeRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IPermissionDataSeeder _permissionDataSeeder;
    private readonly IConfiguration _configuration;
    private readonly ICurrentTenant _currentTenant;

    public IdentityServerDataSeedContributor(
        IClientRepository clientRepository,
        IApiResourceRepository apiResourceRepository,
        IApiScopeRepository apiScopeRepository,
        IIdentityResourceDataSeeder identityResourceDataSeeder,
        IGuidGenerator guidGenerator,
        IPermissionDataSeeder permissionDataSeeder,
        IConfiguration configuration,
        ICurrentTenant currentTenant)
    {
        _clientRepository = clientRepository;
        _apiResourceRepository = apiResourceRepository;
        _apiScopeRepository = apiScopeRepository;
        _identityResourceDataSeeder = identityResourceDataSeeder;
        _guidGenerator = guidGenerator;
        _permissionDataSeeder = permissionDataSeeder;
        _configuration = configuration;
        _currentTenant = currentTenant;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _identityResourceDataSeeder.CreateStandardResourcesAsync();
            await CreateApiResourcesAsync();
            await CreateApiScopesAsync();
            await CreateClientsAsync();
        }
    }

    private async Task CreateApiScopesAsync()
    {
        await CreateApiScopeAsync("IdentityService");
        await CreateApiScopeAsync("EnterpriseService");
        await CreateApiScopeAsync("CustomerService");
        await CreateApiScopeAsync("PublicGateway");
        await CreateApiScopeAsync("BackendAdminGateway");
    }

    private async Task CreateApiResourcesAsync()
    {
        var commonApiUserClaims = new[]
        {
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role"
            };

        await CreateApiResourceAsync("IdentityService", commonApiUserClaims);
        await CreateApiResourceAsync("EnterpriseService", commonApiUserClaims);
        await CreateApiResourceAsync("CustomerService", commonApiUserClaims);
        await CreateApiResourceAsync("PublicGateway", commonApiUserClaims);
        await CreateApiResourceAsync("BackendAdminGateway", commonApiUserClaims);
    }

    private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
    {
        var apiResource = await _apiResourceRepository.FindByNameAsync(name);
        if (apiResource == null)
        {
            apiResource = await _apiResourceRepository.InsertAsync(
                new ApiResource(
                    _guidGenerator.Create(),
                    name,
                    name + " API"
                ),
                autoSave: true
            );
        }

        foreach (var claim in claims)
        {
            if (apiResource.FindClaim(claim) == null)
            {
                apiResource.AddUserClaim(claim);
            }
        }

        return await _apiResourceRepository.UpdateAsync(apiResource);
    }

    private async Task<ApiScope> CreateApiScopeAsync(string name)
    {
        var apiScope = await _apiScopeRepository.FindByNameAsync(name);
        if (apiScope == null)
        {
            apiScope = await _apiScopeRepository.InsertAsync(
                new ApiScope(
                    _guidGenerator.Create(),
                    name,
                    name + " API"
                ),
                autoSave: true
            );
        }

        return apiScope;
    }

    private async Task CreateClientsAsync()
    {
        var commonScopes = new[]
        {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address"
            };

        var configurationSection = _configuration.GetSection("IdentityServer:Clients");
        var corsOrigins = _configuration.GetValue<string>("App:CorsOrigins");

        // PublicApp Client
        var publicApppClientId = configurationSection["PublicApp:ClientId"];
        if (!publicApppClientId.IsNullOrWhiteSpace())
        {
            var rootUrl = configurationSection["PublicApp:RootUrl"].TrimEnd('/');
            await CreateClientAsync(
                name: publicApppClientId,
                clientUri: rootUrl,
                scopes: commonScopes.Union(new[] {
                    "IdentityService",
                    "EnterpriseService",
                    "CustomerService",
                    "PublicGateway"
                }),
                grantTypes: new[] { "hybrid" },
                secret: configurationSection["PublicApp:ClientSecret"]?.Sha256(),
                requireClientSecret: false,
                redirectUri: $"{rootUrl}/signin-oidc",
                postLogoutRedirectUri: $"{rootUrl}/signout-callback-oidc",
                corsOrigins: corsOrigins.Split(',').ToArray()
            );
        }

        // BackendAdminApp Client
        var backendAdminAppClientId = configurationSection["BackendAdminApp:ClientId"];
        if (!backendAdminAppClientId.IsNullOrWhiteSpace())
        {
            var rootUrl = configurationSection["BackendAdminApp:RootUrl"].TrimEnd('/');
            await CreateClientAsync(
                name: backendAdminAppClientId,
                clientUri: rootUrl,
                scopes: commonScopes.Union(new[] {
                    "IdentityService",
                    "EnterpriseService",
                    "CustomerService",
                    "BackendAdminGateway"
                }),
                grantTypes: new[] { "hybrid" },
                secret: configurationSection["BackendAdminApp:ClientSecret"]?.Sha256(),
                requireClientSecret: false,
                redirectUri: $"{rootUrl}/signin-oidc",
                postLogoutRedirectUri: $"{rootUrl}/signout-callback-oidc",
                corsOrigins: corsOrigins.Split(',').ToArray()
            );
        }

        // Enterprise Swagger Client
        var enterpriseSwaggerClientId = configurationSection["Enterprise_Swagger:ClientId"];
        if (!enterpriseSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["Enterprise_Swagger:RootUrl"].TrimEnd('/');

            await CreateClientAsync(
                name: enterpriseSwaggerClientId,
                clientUri: swaggerRootUrl,
                scopes: commonScopes.Union(new[] { "EnterpriseService" }),
                grantTypes: new[] { "authorization_code" },
                secret: configurationSection["Enterprise_Swagger:ClientSecret"]?.Sha256(),
                requireClientSecret: false,
                redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                corsOrigins: corsOrigins.Split(',').ToArray()
            );
        }

        // Customer Swagger Client
        var customerSwaggerClientId = configurationSection["Customer_Swagger:ClientId"];
        if (!customerSwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["Customer_Swagger:RootUrl"].TrimEnd('/');

            await CreateClientAsync(
                name: customerSwaggerClientId,
                clientUri: swaggerRootUrl,
                scopes: commonScopes.Union(new[] { "CustomerService" }),
                grantTypes: new[] { "authorization_code" },
                secret: configurationSection["Customer_Swagger:ClientSecret"]?.Sha256(),
                requireClientSecret: false,
                redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                corsOrigins: corsOrigins.Split(',').ToArray()
            );
        }

        // Identity Swagger Client
        var identitySwaggerClientId = configurationSection["Identity_Swagger:ClientId"];
        if (!identitySwaggerClientId.IsNullOrWhiteSpace())
        {
            var swaggerRootUrl = configurationSection["Identity_Swagger:RootUrl"].TrimEnd('/');

            await CreateClientAsync(
                name: identitySwaggerClientId,
                clientUri: swaggerRootUrl,
                scopes: commonScopes.Union(new[] { "IdentityService" }),
                grantTypes: new[] { "authorization_code" },
                secret: configurationSection["Identity_Swagger:ClientSecret"]?.Sha256(),
                requireClientSecret: false,
                redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                corsOrigins: corsOrigins.Split(',').ToArray()
            );
        }
    }

    private async Task<Client> CreateClientAsync(
        string name,
        IEnumerable<string> scopes,
        IEnumerable<string> grantTypes,
        string clientUri = null,
        string secret = null,
        string redirectUri = null,
        string postLogoutRedirectUri = null,
        string frontChannelLogoutUri = null,
        bool requireClientSecret = true,
        bool requirePkce = false,
        IEnumerable<string> permissions = null,
        IEnumerable<string> corsOrigins = null)
    {
        var client = await _clientRepository.FindByClientIdAsync(name);
        if (client == null)
        {
            client = await _clientRepository.InsertAsync(
                new Client(
                    _guidGenerator.Create(),
                    name
                )
                {
                    ClientName = name,
                    ClientUri = clientUri,
                    ProtocolType = "oidc",
                    Description = name,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    AbsoluteRefreshTokenLifetime = 31536000, //365 days
                    AccessTokenLifetime = 31536000, //365 days
                    AuthorizationCodeLifetime = 300,
                    IdentityTokenLifetime = 300,
                    RequireConsent = false,
                    FrontChannelLogoutUri = frontChannelLogoutUri,
                    RequireClientSecret = requireClientSecret,
                    RequirePkce = requirePkce
                },
                autoSave: true
            );
        }

        if (client.ClientUri != clientUri)
        {
            client.ClientUri = clientUri;
        }

        foreach (var scope in scopes)
        {
            if (client.FindScope(scope) == null)
            {
                client.AddScope(scope);
            }
        }

        foreach (var grantType in grantTypes)
        {
            if (client.FindGrantType(grantType) == null)
            {
                client.AddGrantType(grantType);
            }
        }

        if (!secret.IsNullOrEmpty())
        {
            if (client.FindSecret(secret) == null)
            {
                client.AddSecret(secret);
            }
        }

        if (redirectUri != null)
        {
            if (client.FindRedirectUri(redirectUri) == null)
            {
                client.AddRedirectUri(redirectUri);
            }
        }

        if (postLogoutRedirectUri != null)
        {
            if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
            {
                client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
            }
        }

        if (permissions != null)
        {
            await _permissionDataSeeder.SeedAsync(
                ClientPermissionValueProvider.ProviderName,
                name,
                permissions,
                null
            );
        }

        if (corsOrigins != null)
        {
            foreach (var origin in corsOrigins)
            {
                if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                {
                    client.AddCorsOrigin(origin);
                }
            }
        }

        return await _clientRepository.UpdateAsync(client);
    }
}
