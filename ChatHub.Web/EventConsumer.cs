using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Events;
using ChatHub.Web.Hubs;
using MassTransit;
using Microsoft.AspNet.SignalR;


namespace ChatHub.Web
{
    public class EventConsumer : IConsumer<IMessageReceived>, IConsumer<IUserStateChanged>
    {
        readonly IHubContext<ICentralHub> centralHub;


        public EventConsumer(IHubContext<ICentralHub> centralHub)
        {
            this.centralHub = centralHub;
        }


        public async Task Consume(ConsumeContext<IMessageReceived> context)
        {
            Console.WriteLine("Message Received");
            await this.centralHub.Clients.All.OnMessageReceived(new
            {
                context.Message.Body,
                context.Message.From,
                context.Message.SendDate
            });
        }


        public async Task Consume(ConsumeContext<IUserStateChanged> context)
        {
            Console.WriteLine("User State Changed");
            await this.centralHub.Clients.All.OnUserStateChanged(new
            {
                context.Message.Name,
                context.Message.Connected
            });
        }
    }
}
