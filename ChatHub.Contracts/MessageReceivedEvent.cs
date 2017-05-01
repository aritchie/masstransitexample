using System;


namespace ChatHub.Contracts
{
    public class MessageReceivedEvent : IMessageReceivedEvent
    {
        public string Body { get; set; }
        public string From { get; set; }
        public DateTimeOffset SendDate { get; set; }
    }
}
