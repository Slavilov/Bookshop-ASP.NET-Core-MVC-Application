using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve, // Handles circular references
            WriteIndented = true // Optional: Makes JSON output more readable
        };
        session.SetString(key, JsonSerializer.Serialize(value, options));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        if (value == null) return default;

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve, // Handles circular references
            WriteIndented = true // Optional: consistent with serialization formatting
        };

        return JsonSerializer.Deserialize<T>(value, options);
    }
}
