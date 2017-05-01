using System;


namespace ChatHub.Contracts.Events
{
    public class UserStateChanged : IUserStateChanged
    {
        public string Name { get; set; }
        public bool Connected { get; set; }
    }
}
