namespace Mailjet.Client;

public enum ResourceType
{
    NotSpecified,
    Rest,
    Data,
    Send,
    V4,
}

public class ResourceInfo(
    string resource,
    string? action = null,
    ApiVersion apiVersion = ApiVersion.V3,
    ResourceType type = ResourceType.Rest)
{
    public ResourceType Type { get; private set; } = type;
    public string Resource { get; private set; } = resource;
    public string? Action { get; private set; } = action;
    public ApiVersion ApiVersion { get; private set; } = apiVersion;

    public virtual string BuildUrl(string? resourceId, string? actionId)
    {
        string url = UrlHelper.CombineUrl(GetPath(), Resource);

        if (!string.IsNullOrEmpty(resourceId))
            url = UrlHelper.CombineUrl(url, resourceId);

        if (!string.IsNullOrEmpty(Action))
            url = UrlHelper.CombineUrl(url, Action);

        if (!string.IsNullOrEmpty(actionId))
            url = UrlHelper.CombineUrl(url, actionId);

        return url;
    }

    private string GetPath()
    {
        var path = GetApiVersionPath();
        return Type switch
        {
            ResourceType.Rest => path + "/REST",
            ResourceType.Data => path + "/DATA",
            ResourceType.Send or ResourceType.V4 => path,
            _ => Resource != "send" ? path + "/REST" : path,
        };
    }

    private string GetApiVersionPath()
    {
        return ApiVersion switch
        {
            ApiVersion.V3_1 => MailjetConstants.ApiVersionPathV3_1,
            ApiVersion.V4 => MailjetConstants.ApiVersionPathV4,
            ApiVersion.V3 => MailjetConstants.ApiVersionPathV3,
            _ => throw new NotImplementedException("Wrong API version"),
        };
    }
}
