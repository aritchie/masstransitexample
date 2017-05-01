using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Events;
using MassTransit;
using Microsoft.AspNet.SignalR;


namespace ChatHub.Web
{
    public class EventConsumer : IConsumer<IMessageReceived>, IConsumer<IUserStateChanged>
    {
        public Task Consume(ConsumeContext<IMessageReceived> context)
        {
            Console.WriteLine("Message Received");
            GlobalHost
                .ConnectionManager
                .GetHubContext<ChatHub>()
                .Clients.All.MessageReceived(new
                {
                    context.Message.Body,
                    context.Message.From,
                    context.Message.SendDate
                });

            return Task.FromResult(new object());
        }


        public Task Consume(ConsumeContext<IUserStateChanged> context)
        {
            Console.WriteLine("User State Changed");
            GlobalHost
                .ConnectionManager
                .GetHubContext<ChatHub>()
                .Clients.All.UserStateChanged(new
                {
                    context.Message.Name,
                    context.Message.Connected
                });

            return Task.FromResult(new object());
        }
    }
}
