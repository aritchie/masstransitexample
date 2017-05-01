using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Commands;
using ChatHub.Contracts.Events;
using MassTransit;


namespace ChatHub.Consumers
{
    public class UserStateChangeConsumer : IConsumer<IUserStateChange>
    {
        public async Task Consume(ConsumeContext<IUserStateChange> context)
        {
            Console.WriteLine($"User state changed: {context.Message.Name} (Connected: {context.Message.Connected})");
            await context.Publish(new UserStateChanged
            {
                Name = context.Message.Name,
                Connected = context.Message.Connected
            });
        }
    }
}
