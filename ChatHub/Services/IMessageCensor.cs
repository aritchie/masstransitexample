using System;


namespace ChatHub.Services
{
    public interface IMessageCensor
    {
        string Scrub(string message);
    }
}
