namespace CloudVOffice.Core
{
    public interface ISoftDeletedEntity
    {
        bool Deleted { get; set; }
    }
}
