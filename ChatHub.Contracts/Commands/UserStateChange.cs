using System;


namespace ChatHub.Contracts.Commands
{
    public class UserStateChange : IUserStateChange
    {
        public string Name { get; set; }
        public bool Connected { get; set; }
    }
}
