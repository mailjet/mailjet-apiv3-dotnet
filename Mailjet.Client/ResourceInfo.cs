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

        public ResourceInfo(string resource, string action = null, ResourceType type = ResourceType.Rest)
        {
            Resource = resource;
            Action = action;
            Type = type;
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
            switch (Type)
            {
                case ResourceType.Rest:
                    return "REST";
                case ResourceType.Data:
                    return "DATA";
                case ResourceType.Send:
                case ResourceType.V4:
                    return string.Empty;
                default:
                    return Resource != "send" ? "REST" : string.Empty;
            }
        }
    }
}
