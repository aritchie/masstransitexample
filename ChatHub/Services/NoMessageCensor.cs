using System;


namespace ChatHub.Services
{
    public class NoMessageCensor : IMessageCensor
    {
        public string Scrub(string message) => message;
    }
}
