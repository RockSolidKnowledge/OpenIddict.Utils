using System.Collections.Immutable;
using System.Text.Json;
using OpenIddict.Abstractions;
using Rsk.OpenIddict.Utils.Constants;
using Rsk.OpenIddict.Utils.Models;

namespace Rsk.OpenIddict.Utils.Extensions;

public static class ApplicationManagerExtensionMethods
{
    /// <summary>
    /// Gets the Claims from the properties of the application stored under the AdminUI key.
    /// </summary>
    /// <param name="applicationManager"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public static async Task<List<AdminUIClaim>> GetClaimsFromProperties(this IOpenIddictApplicationManager applicationManager, object scope)
    {
        ImmutableDictionary<string, JsonElement> properties = await applicationManager.GetPropertiesAsync(scope);

        return properties.TryGetValue(AdminUiConstants.ApplicationPropertyClaims, out var claimsJson) ?
            claimsJson.Deserialize<List<AdminUIClaim>>(new JsonSerializerOptions {PropertyNameCaseInsensitive = true}) ?? [] : [];
    }
    
    /// <summary>
    /// Returns the claims from the properties of the application stored under the AdminUI key, grouped by Claim type.
    /// </summary>
    /// <param name="applicationManager"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public static async Task<IDictionary<string, ImmutableArray<string>>> GetClaimValuesDictionary(this IOpenIddictApplicationManager applicationManager, object scope)
    {
        var claimsList = await GetClaimsFromProperties(applicationManager, scope);

        return claimsList.GroupBy(c => c.Type)
            .ToDictionary(x => x.Key, 
                x => x.Select(g => g.Value).ToImmutableArray());
    }
}