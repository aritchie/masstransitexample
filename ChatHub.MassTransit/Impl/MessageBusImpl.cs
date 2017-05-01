using System;
using System.Configuration;
using System.Threading.Tasks;
using MassTransit;


namespace ChatHub.MassTransit.Impl
{
    public class MessageBusImpl : IMessageBus
    {
        readonly IBus bus;


        public MessageBusImpl(IBus bus)
        {
            this.bus = bus;
        }


        public async Task Send<T>(T obj) where T : class
        {
            var ep = typeof(T).Assembly.GetName().Name + ".Endpoint";
            var uri = ConfigurationManager.AppSettings[ep];
            var endpoint = await this.bus.GetSendEndpoint(new Uri(uri));
            await endpoint.Send(obj);
        }
    }
}
