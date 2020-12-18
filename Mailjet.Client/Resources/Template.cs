namespace Mailjet.Client.Resources
{
    public static class Template
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("template");

        public const string Author = "Author";
        public const string Categories = "Categories";
        public const string Copyright = "Copyright";
        public const string Description = "Description";
        public const string EditMode = "EditMode";
        public const string ID = "ID";
        public const string IsStarred = "IsStarred";
        public const string Locale = "Locale";
        public const string Name = "Name";
        public const string OwnerId = "OwnerId";
        public const string OwnerType = "OwnerType";
        public const string Presets = "Presets";
        public const string Previews = "Previews";
        public const string Purposes = "Purposes";
        public const string APIKey = "APIKey";
        public const string CategoriesSelectionMethod = "CategoriesSelectionMethod";
        public const string PurposesSelectionMethod = "PurposesSelectionMethod";
        public const string IsTextPartGenerationEnabled = "IsTextPartGenerationEnabled";
        public const string User = "User";
        public const string Limit = "Limit";
        public const string Offset = "Offset";
        public const string Sort = "Sort";
        public const string CountOnly = "CountOnly";

        public const long EditModeValue_DNDBuilder = 1;
        public const long EditModeValue_HTMLBuilder = 2;
        public const long EditModeValue_SavedSectionBuilder = 3;
        public const long EditModeValue_MJMLBuilder = 4;

        public const string OwnerTypeValue_Apikey = "apikey";
        public const string OwnerTypeValue_User = "user";
        public const string OwnerTypeValue_Global = "global";
    }
}


