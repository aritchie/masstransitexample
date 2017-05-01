using System;


namespace ChatHub.Contracts
{
    public class SendMessageCommand : ISendMessageCommand
    {
        public string Body { get; set; }
        public string From { get; set; }
    }
}
