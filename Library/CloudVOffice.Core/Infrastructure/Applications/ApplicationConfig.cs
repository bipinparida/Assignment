namespace CloudVOffice.Core.Infrastructure.Applications
{
    public class ApplicationConfig
    {
        public string ApplicationName { get; set; }
        public string Url { get; set; }
        public string IconImageUrl { get; set; }
        public string IconClass { get; set; }
        public string AreaName { get; set; }
        public List<string> Roles { get; set; }
        public bool IsGroup { get; set; }
        public int? ServerApplicationId { get; set; }
        public List<ApplicationConfig> Children { get; set; }
    }
}
