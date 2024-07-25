namespace CloudVOffice.Data.DTO.Comunication
{
    public class EmailDomainDTO
    {
        public int? EmailDomainId { get; set; }
        public string DomainName { get; set; }
        public string IncomingServer { get; set; }
        public int IncomingPort { get; set; }
        public bool IncomingIsIMAP { get; set; }
        public bool IncomingIsSsl { get; set; }
        public bool IncomingIsStartTLs { get; set; }
        public string OutingServer { get; set; }
        public int OutgoingPort { get; set; }
        public bool OutgoingIsTLs { get; set; }
        public bool OutgoingIsSsl { get; set; }

        public Int64 CreatedBy { get; set; }
    }
}
