using System;
using System.Threading.Tasks;
using ChatHub.Contracts;
using MassTransit;


namespace ChatHub
{
    public class MessageReceivedEventConsumer : IConsumer<IMessageReceivedEvent>
    {
        public async Task Consume(ConsumeContext<IMessageReceivedEvent> context)
        {
            Console.WriteLine($"[{context.Message.From}]: {context.Message.Body}");
        }
    }
}
