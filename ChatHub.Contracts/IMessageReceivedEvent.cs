using System;


namespace ChatHub.Contracts
{
    public interface IMessageReceivedEvent
    {
        string Body { get; set; }
        string From { get; set; }
        DateTimeOffset SendDate { get; set; }
    }
}
