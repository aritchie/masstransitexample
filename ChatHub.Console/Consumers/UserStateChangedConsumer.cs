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
            var action = context.Message.Connected ? "Connected" : "Disconnected";
            Console.WriteLine($"User '{context.Message.Name}' {action}");
            return Task.FromResult(new object());
        }
    }
}
