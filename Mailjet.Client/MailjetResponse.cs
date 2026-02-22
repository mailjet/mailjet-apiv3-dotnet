using System.Text.Json;
using System.Text.Json.Nodes;

namespace Mailjet.Client;

public class MailjetResponse(bool isSuccessStatusCode, int statusCode, JsonObject content)
{
    public JsonObject Content { get; private set; } = content;
    public bool IsSuccessStatusCode { get; private set; } = isSuccessStatusCode;
    public int StatusCode { get; private set; } = statusCode;

    public int GetTotal()
    {
        if (!TryGetValue("Total", out int total))
        {
            total = 0;
        }
        return total;
    }

    public JsonArray GetData()
    {
        if (TryGetValue("Data", out JsonArray? result) && result != null)
            return result;
        if (TryGetValue("Sent", out result) && result != null)
            return result;
        if (TryGetValue("Messages", out result) && result != null)
            return result;
        return [Content.DeepClone()];
    }

    public int GetCount()
    {
        if (!TryGetValue("Count", out int count))
        {
            count = 0;
        }
        return count;
    }

    public string GetErrorInfo()
    {
        if (!TryGetValue(MailjetConstants.ErrorInfo, out string? errorInfo) || errorInfo == null)
        {
            errorInfo = string.Empty;
        }
        return errorInfo;
    }

    public string GetErrorMessage()
    {
        if (!TryGetValue("ErrorMessage", out string? errorMessage) || errorMessage == null)
        {
            errorMessage = string.Empty;
        }
        return errorMessage;
    }

    public bool TryGetValue<T>(string key, out T? value)
    {
        JsonNode? node = null;
        
        if (Content.TryGetPropertyValue(key, out node))
        {
            // Found with exact key
        }
        else
        {
            foreach (var property in Content)
            {
                if (string.Equals(property.Key, key, StringComparison.OrdinalIgnoreCase))
                {
                    node = property.Value;
                    break;
                }
            }
        }

        if (node == null)
        {
            value = default;
            return false;
        }

        try
        {
            value = node.GetValue<T>();
            return true;
        }
        catch
        {
            try
            {
                value = JsonSerializer.Deserialize<T>(node);
                return value != null;
            }
            catch
            {
                value = default;
                return false;
            }
        }
    }

    public T GetValue<T>(string key)
    {
        if (!TryGetValue<T>(key, out T? result))
        {
            throw new Exception($"No entry found for key: {key}");
        }
        return result!;
    }
}
