namespace Mailjet.Client
{
    public enum ResourceType
    {
        NotSpecified,
        Rest,
        Data,
        Send,
        V4,
    }

    public class ResourceInfo
    {
        public ResourceType Type { get; private set; }

        public string Resource { get; private set; }

        // Mailjet resource action, if any
        public string Action { get; private set; }

        public ApiVersion ApiVersion { get; private set; }

        public ResourceInfo(string resource, string action = null, ApiVersion apiVersion = ApiVersion.V3, ResourceType type = ResourceType.Rest)
        {
            Resource = resource;
            Action = action;
            Type = type;
            ApiVersion = apiVersion;
        }

        public virtual string BuildUrl(string resourceId, string actionId)
        {
            string url = UrlHelper.CombineUrl(GetPath(), Resource);

            if (!string.IsNullOrEmpty(resourceId))
            {
                url = UrlHelper.CombineUrl(url, resourceId);
            }

            if (!string.IsNullOrEmpty(Action))
            {
                url = UrlHelper.CombineUrl(url, Action);
            }

            if (!string.IsNullOrEmpty(actionId))
            {
                url = UrlHelper.CombineUrl(url, actionId);
            }

            return url;
        }

        private string GetPath()
        {
            var path = GetApiVersionPath();

            switch (Type)
            {
                case ResourceType.Rest:
                    return path + "/REST";
                case ResourceType.Data:
                    return path + "/DATA";
                case ResourceType.Send:
                case ResourceType.V4:
                    return path;
                default:
                    return Resource != "send" ? path + "/REST" : path;
            }
        }

        private string GetApiVersionPath()
        {
            switch (ApiVersion)
            {
                case ApiVersion.V3_1:
                    return MailjetConstants.ApiVersionPathV3_1;
                case ApiVersion.V4:
                    return MailjetConstants.ApiVersionPathV4;
                default:
                    return MailjetConstants.ApiVersionPathV3;
            }
        }
    }
}
