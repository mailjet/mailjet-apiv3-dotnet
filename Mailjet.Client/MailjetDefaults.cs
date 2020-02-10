namespace Mailjet.Client
{
    public static class MailjetDefaults
    {
        public static readonly string DefaultBaseAdress = "https://api.mailjet.com";
        public static readonly string UserAgent = "mailjet-api-v3-net/1.2.2";
        public static readonly string JsonMediaType = "application/json";
        public static readonly string ApiVersionPathV3 = "v3";
        public static readonly string ApiVersionPathV3_1 = "v3.1";
        public static readonly string ApiVersionPathV4 = "v4";
        public static readonly string ErrorInfo = "ErrorInfo";
        public static readonly string TooManyRequestsMessage = "Too many requests";
        public static readonly string InternalServerErrorGeneralMessage = "Internal Server Error";
    }
}
