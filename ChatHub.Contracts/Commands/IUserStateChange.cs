using System;


namespace ChatHub.Contracts.Commands
{
    public interface IUserStateChange
    {
        string Name { get; set; }
        bool Connected { get; set; }
    }
}
