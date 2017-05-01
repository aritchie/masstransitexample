using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Commands;
using MassTransit;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace ChatHub.Web.Hubs
{
    public interface ICentralHub
    {
        Task OnMessageReceived(object obj);
        Task OnUserStateChanged(object obj);
    }


    [HubName("chatHub")]
    public class CentralHub : Hub<ICentralHub>
    {
        readonly IBus bus;


        public CentralHub(IBus bus)
        {
            this.bus = bus;
        }

        //var endpoint = await _bus.GetSendEndpoint(new Uri(ConfigurationManager.AppSettings["MyCommandQueueFullUri"]));
        //await sendEndpoint.Send<MyCommand>(...);

        public Task SendMessage(string message) => this.bus.Publish(new SendMessage
        {
            Body = message,
            From = this.Context.ConnectionId
        });


        public override Task OnConnected()
        {
            this.bus.Publish(new UserStateChange
            {
                Name = this.Context.ConnectionId,
                Connected = true
            });

            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            this.bus.Publish(new UserStateChange
            {
                Name = this.Context.ConnectionId,
                Connected = false
            });

            return base.OnDisconnected(stopCalled);
        }
    }
}
