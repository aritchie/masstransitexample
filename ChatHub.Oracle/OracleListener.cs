using System;
using Autofac;
using ChatHub.Contracts.Commands;
using MassTransit;


namespace ChatHub.Oracle
{
    public class OracleListener : IStartable
    {
        readonly IBus bus;
        readonly IRepository repository;


        public OracleListener(IBus bus, IRepository repository)
        {
            this.bus = bus;
            this.repository = repository;
        }


        public void Start()
        {
            this.repository.StartListener();
            this.repository.DbNotification += async (sender, message) =>
            {
                if (message.Received)
                {
                    Console.WriteLine("MESSAGE RECEIVED FROM: " + message.From);
                    Console.WriteLine(message.Message);
                }
                else
                {
                    await this.bus.Publish(new SendMessage
                    {
                        From = "THE GREAT ORACLE",
                        Body = message.Message
                    });
                    Console.WriteLine("Message Sent - " + message.Message);
                }
            };
        }
    }
}
