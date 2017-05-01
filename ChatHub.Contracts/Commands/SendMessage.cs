using System;


namespace ChatHub.Contracts.Commands
{
    public class SendMessage : ISendMessage
    {
        public string Body { get; set; }
        public string From { get; set; }
    }
}
