using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mailjet.Client;

public class MailjetRequest
{
    // Mailjet resource string
    public required ResourceInfo Resource { get; set; }

    // Resource ID
    public ResourceId? ResourceId { get; set; }

    // Resource action ID
    public long? ActionId { get; set; }

    // Every filter should be string values. So Integer will be cast into Strings
    public IDictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();

    // The request body is a JsonObject that will be cast into a String before the call
    public JsonObject Body { get; set; } = new JsonObject();

    public MailjetRequest Filter(string key, string value)
    {
        Filters.Add(key, value);
        return this;
    }

    public MailjetRequest Filter(string key, int value)
    {
        return Filter(key, value.ToString());
    }

    public MailjetRequest Property(string key, object? value)
    {
        Body[key] = JsonValue.Create(value);
        return this;
    }

    public MailjetRequest Property(string key, JsonNode? value)
    {
        Body[key] = value;
        return this;
    }

    public string BuildUrl()
    {
        string? resourceId = ResourceId?.Id;
        string? actionId = ActionId?.ToString();
        string url = Resource.BuildUrl(resourceId, actionId);
        return UrlHelper.AddQuerryString(url, Filters);
    }

    public override string ToString()
    {
        var jObject = new JsonObject
        {
            ["Resource"] = JsonSerializer.SerializeToNode(Resource),
            ["ResourceId"] = ResourceId?.Id,
            ["ActionID"] = ActionId?.ToString(),
            ["Filters"] = JsonSerializer.SerializeToNode(Filters),
            ["Body"] = Body.DeepClone()
        };

        return jObject.ToJsonString();
    }
}
