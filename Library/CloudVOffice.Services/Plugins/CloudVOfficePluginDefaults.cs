namespace CloudVOffice.Services.Plugins
{
    public static partial class CloudVOfficePluginDefaults
    {
        public static string Path => "~/Plugins";

        /// <summary>
        /// Gets the path to plugins folder
        /// </summary>
        public static string UploadedPath => "~/Plugins/Uploaded";

        /// <summary>
        /// Gets the plugins folder name
        /// </summary>
        public static string PathName => "./Plugins";

        /// <summary>
        /// Gets the path to plugins refs folder
        /// </summary>
        public static string RefsPathName => "refs";

        /// <summary>
        /// Gets the name of the plugin description file
        /// </summary>
        public static string DescriptionFileName => "plugin.json";

        /// <summary>
        /// Gets the plugins logo filename
        /// </summary>
        public static string LogoFileName => "logo";

        /// <summary>
        /// Gets supported extensions of logo file
        /// </summary>
        public static List<string> SupportedLogoImageExtensions => new() { "jpg", "png", "gif" };

    }
}
