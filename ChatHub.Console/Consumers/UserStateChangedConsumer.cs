using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Events;
using MassTransit;


namespace ChatHub.Consumers
{
    public class UserStateChangedConsumer : IConsumer<IUserStateChanged>
    {
        public Task Consume(ConsumeContext<IUserStateChanged> context)
        {

            return Task.FromResult(new object());
        }
    }
}
