using System.Text.Json;

namespace Rsk.OpenIddict.Utils.Extensions;

public static class JsonElementExtensions
{
    public static List<T> DeserialiseTo<T>(this JsonElement jsonElement)
    {
        return jsonElement.Deserialize<List<T>>(new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true }) ?? [];
    }
}