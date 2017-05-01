using System;


namespace ChatHub.Contracts.Events
{
    public class MessageReceived : IMessageReceived
    {
        public string Body { get; set; }
        public string From { get; set; }
        public DateTimeOffset SendDate { get; set; }
    }
}
