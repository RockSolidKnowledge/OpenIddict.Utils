namespace Rsk.OpenIddict.Utils.Constants;

public static class AdminUiConstants
{
    public const string LegacyApplicationPropertyBagUrnPrefix = "urn:com:rocksolidknowledge:adminui:application:property:";
    public const string LegacyApplicationPropertyClaims = $"{LegacyApplicationPropertyBagUrnPrefix}claims";
    public const string LegacyApplicationAdminUiClientType = $"{LegacyApplicationPropertyBagUrnPrefix}type";

    public const string LegacyScopePropertyBagUrnPrefix = "urn:com:rocksolidknowledge:adminui:scope:property:";
    public const string LegacyScopePropertyClaims = $"{LegacyScopePropertyBagUrnPrefix}claims";
    
    public const string ApplicationPropertyBagUrnPrefix = "urn:com:rocksolidknowledge:application:property:";
    public const string ApplicationPropertyClaims = $"{ApplicationPropertyBagUrnPrefix}claims";
    public const string ApplicationAdminUiClientType = $"{ApplicationPropertyBagUrnPrefix}type";

    public const string ScopePropertyBagUrnPrefix = "urn:com:rocksolidknowledge:scope:property:";
    public const string ScopePropertyClaims = $"{ScopePropertyBagUrnPrefix}claims";
}