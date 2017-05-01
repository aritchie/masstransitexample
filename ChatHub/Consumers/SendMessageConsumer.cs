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
        readonly IMessageCensor filter;


        public SendMessageCommandConsumer(IMessageCensor filter) => this.filter = filter;


        public async Task Consume(ConsumeContext<ISendMessage> context)
        {
            Console.WriteLine("Incoming Message");
            //var attempt = context.GetRetryAttempt();
            //if (attempt < 3)
            //    //Console.WriteLine("");
            //    throw new ArgumentException("Get lost, you haven't tried hard enough yet");

            var body = this.filter.Scrub(context.Message.Body);
            await context.Publish(new MessageReceived
            {
                Body = body,
                From = context.Message.From,
                SendDate = DateTimeOffset.UtcNow // this is an error - it could have been sent hours ago
            });
        }
    }
}
