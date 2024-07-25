namespace CloudVOffice.Core.Infrastructure.Applications
{
    public class PluginConfig
    {
        public string Group { get; set; }
        public string FriendlyName { get; set; }
        public string SystemName { get; set; }
        public double Version { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string InstallationUrl { get; set; }
        public string UnInstallationUrl { get; set; }
        public List<string> Dependency { get; set; }
        public bool IsInstalled { get; set; }
        public bool IsNewVersion { get; set; }
    }
}
