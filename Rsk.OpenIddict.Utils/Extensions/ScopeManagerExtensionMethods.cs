using System.Collections.Immutable;
using System.Text.Json;
using OpenIddict.Abstractions;
using Rsk.OpenIddict.Utils.Constants;

namespace Rsk.OpenIddict.Utils.Extensions;

public static class ScopeManagerExtensionMethods
{
    /// <summary>
    /// Gets the Claims from the properties of the scope stored under the AdminUI key.
    /// </summary>
    /// <param name="scopeManager"></param>
    /// <param name="scope"></param>
    /// <returns></returns>
    public static async Task<List<string>> GetClaimsFromProperties(this IOpenIddictScopeManager scopeManager, object scope)
    {
        ImmutableDictionary<string, JsonElement> properties = await scopeManager.GetPropertiesAsync(scope);
        
        return properties.TryGetValue(AdminUiConstants.ScopePropertyClaims, out var claimsJson) ?
            claimsJson.Deserialize<List<string>>() ?? [] : [];
    }
    
    /// <summary>
    /// Checks if all scopes in the request exist in the database.
    /// </summary>
    /// <param name="scopeManager"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task<bool> ScopesExist(this IOpenIddictScopeManager scopeManager, OpenIddictRequest request)
    {
        var scopes = request.GetScopes().ToList();
        
        foreach (var scope in scopes)
        {
            if (await scopeManager.FindByNameAsync(scope) == null)
            {
                return false;
            }
        }

        return true;
    }
}