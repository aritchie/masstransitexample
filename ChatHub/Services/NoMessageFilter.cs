using System;


namespace ChatHub.Services
{
    public class NoMessageFilter : IMessageFilter
    {
        public bool IsValid(string message) => true;
    }
}
