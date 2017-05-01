using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Commands;
using ChatHub.Contracts.Events;
using ChatHub.Services;
using MassTransit;


namespace ChatHub.Consumers
{
    public class SendMessageCommandConsumer : IConsumer<ISendMessage>
    {
        readonly IMessageFilter filter;


        public SendMessageCommandConsumer(IMessageFilter filter) => this.filter = filter;


        public async Task Consume(ConsumeContext<ISendMessage> context)
        {
            Console.WriteLine("Incoming Message");
            var attempt = context.GetRetryAttempt();
            if (attempt < 3)
                //Console.WriteLine("");
                throw new ArgumentException("Get lost, you haven't tried hard enough yet");

            if (!this.filter.IsValid(context.Message.Body))
            {
                Console.WriteLine("Message was filtered out");
                return;
            }

            Console.WriteLine("Message good - publishing back out to consumers");
            await context.Publish(new MessageReceived
            {
                Body = context.Message.Body,
                From = context.Message.From,
                SendDate = DateTimeOffset.UtcNow // this is an error - it could have been sent hours ago
            });
        }
    }
}
