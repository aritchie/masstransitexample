using System;


namespace ChatHub.Contracts.Commands
{
    public interface ISendMessage
    {
        string Body { get; set; }
        string From { get; set; }
    }
}
