using System;
using System.Threading.Tasks;
using Autofac;
using ChatHub.Contracts.Commands;
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
            Startup.Container.Resolve<IBus>().Publish(new SendMessage
            {
                Body = message,
                From = this.Context.ConnectionId
            });
            //pipe =>
            //{
            //    //pipe.Headers.Set("", "");
            //});
        }


        public override Task OnConnected()
        {
            Startup.Container
                .Resolve<IBus>()
                .Publish(new UserStateChange
                {
                    Name = this.Context.ConnectionId,
                    Connected = true
                });

            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            Startup.Container
                .Resolve<IBus>()
                .Publish(new UserStateChange
                {
                    Name = this.Context.ConnectionId,
                    Connected = false
                });

            return base.OnDisconnected(stopCalled);
        }
    }
}
