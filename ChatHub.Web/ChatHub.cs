using System;
using Autofac;
using ChatHub.Contracts;
using MassTransit;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace ChatHub.Web
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            Startup.Container.Resolve<IBus>().Publish(new SendMessageCommand
            {
                Body = message,
                From = this.Context.ConnectionId
            });
        }
    }
}
