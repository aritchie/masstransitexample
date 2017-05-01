using System;


namespace ChatHub.Contracts
{
    public interface ISendMessageCommand
    {
        string Body { get; set; }
        string From { get; set; }
    }
}
