using System;
using System.Threading.Tasks;


namespace ChatHub.MassTransit
{
    public interface IMessageBus
    {
        Task Send<T>(T obj) where T : class;
    }
}
