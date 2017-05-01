using System;


namespace ChatHub.Services
{
    public interface IMessageFilter
    {
        bool IsValid(string message);
    }
}
