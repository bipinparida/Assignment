namespace CloudVOffice.Data.DTO.Permission
{
    public class ApplicationDTO
    {
        public string ApplicationName { get; set; }

        public int? Parent { get; set; }
        public bool IsGroup { get; set; }
        public string? Url { get; set; }
        public string? IconImageUrl { get; set; }
        public string? IconClass { get; set; }
        public string AreaName { get; set; }
        public Int64 CreatedBy { get; set; }
    }
}
