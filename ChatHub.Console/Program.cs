using System;
using System.Configuration;
using Autofac;
using ChatHub.Contracts.Commands;
using MassTransit;


namespace ChatHub
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RegisterModule
            {
                Uri = ConfigurationManager.AppSettings["queue.uri"],
                UserName = ConfigurationManager.AppSettings["queue.username"],
                Password = ConfigurationManager.AppSettings["queue.password"],
                QueueName = ConfigurationManager.AppSettings["queue.name"]
            });
            var container = builder.Build();
            var bus = container.Resolve<IBusControl>();
            bus.Start();

            var quit = false;
            while (!quit)
            {
                Console.WriteLine("Type a message and press enter");
                var msg = Console.ReadLine();

                switch (msg.ToLower())
                {
                    case "quit":
                        quit = true;
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    default:

                        Console.WriteLine("Sending message");

                        bus.Publish(new SendMessage
                        {
                            Body = msg,
                            From = "Console"
                        })
                        .Wait();
                        Console.WriteLine("Message sent");
                        break;
                }
            }
        }
    }
}
