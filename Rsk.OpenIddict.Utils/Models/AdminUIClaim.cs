namespace Rsk.OpenIddict.Utils.Models;

public class AdminUIClaim(string type, string value)
{
    public string Type { get; } = type;
    public string Value { get; } = value;
}