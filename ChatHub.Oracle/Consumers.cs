using System;
using System.Threading.Tasks;
using ChatHub.Contracts.Events;
using MassTransit;


namespace ChatHub.Oracle
{
    public class Consumers : IConsumer<IMessageReceived>, IConsumer<IUserStateChanged>
    {
        readonly IRepository repository;

        public Consumers(IRepository repository)
        {
            this.repository = repository;
        }


        public Task Consume(ConsumeContext<IMessageReceived> context)
        {
            if (context.Message.From != "THE GREAT ORACLE")
            {
                this.Insert(
                    context.Message.From,
                    context.Message.Body,
                    context.Message.SendDate
                );
            }
            return Task.FromResult(new object());
        }


        public Task Consume(ConsumeContext<IUserStateChanged> context)
        {
            this.Insert(
                context.Message.Name,
                context.Message.Connected ? "CONNECTED" : "DISCONNECTED",
                DateTimeOffset.Now
            );
            return Task.FromResult(new object());
        }


        void Insert(string from, string msg, DateTimeOffset date) => this.repository.Insert(from, msg, date, false);
    }
}
