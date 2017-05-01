using System;


namespace ChatHub.Contracts.Events
{
    public interface IUserStateChanged
    {
        string Name { get; set; }
        bool Connected { get; set; }
    }
}
