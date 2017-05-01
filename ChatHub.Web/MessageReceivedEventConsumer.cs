using System;
using System.Threading.Tasks;
using ChatHub.Contracts;
using MassTransit;
using Microsoft.AspNet.SignalR;


namespace ChatHub.Web
{
    public class MessageReceivedEventConsumer : IConsumer<IMessageReceivedEvent>
    {
        public Task Consume(ConsumeContext<IMessageReceivedEvent> context)
        {
            Console.WriteLine("Message Received");
            GlobalHost.ConnectionManager.GetHubContext<ChatHub>()
                .Clients.All.MessageReceived(new
                {
                    context.Message.Body,
                    context.Message.From,
                    context.Message.SendDate
                });

            return Task.FromResult(new object());
        }
    }
}
