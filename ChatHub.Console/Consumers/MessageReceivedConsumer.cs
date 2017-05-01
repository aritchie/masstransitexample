using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Events;
using MassTransit;


namespace ChatHub.Consumers
{
    public class MessageReceivedConsumer : IConsumer<IMessageReceived>
    {
        public async Task Consume(ConsumeContext<IMessageReceived> context)
        {
            Console.WriteLine($"[{context.Message.From}]: {context.Message.Body}");
        }
    }
}
