using System;


namespace ChatHub.Oracle
{
    public class OraMessage
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool Received { get; set; }
    }
}
