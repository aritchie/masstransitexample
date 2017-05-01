using System;


namespace ChatHub.Contracts.Events
{
    public interface IMessageReceived
    {
        string Body { get; set; }
        string From { get; set; }
        DateTimeOffset SendDate { get; set; }
    }
}
