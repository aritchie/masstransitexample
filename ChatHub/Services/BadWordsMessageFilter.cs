using System;


namespace ChatHub.Services
{
    public class BadWordsMessageFilter : IMessageFilter
    {
        public bool IsValid(string message) => !message.Contains(" shit ");
    }
}
