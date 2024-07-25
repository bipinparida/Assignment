namespace CloudVOffice.Core.Domain.Company
{
    public class LetterHead : IAuditEntity, ISoftDeletedEntity
    {
        public int LetterHeadId { get; set; }
        public string LetterHeadName { get; set; }
        public string LetterHeadImage { get; set; }
        public string LetterHeadImageHeight { get; set; }
        public string LetterHeadImageWidth { get; set; }
        public string LetterHeadAlign { get; set; }
        public string LetterHeadFooterImage { get; set; }
        public string LetterHeadImageFooterHeight { get; set; }
        public string LetterHeadImageFooterWidth { get; set; }
        public string LetterHeadFooterAlign { get; set; }

        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
