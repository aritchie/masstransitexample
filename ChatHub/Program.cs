using System;
using System.Configuration;
using Autofac;
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
            container.Resolve<IBusControl>().Start();

            Console.WriteLine("ChatHub Central is running.  Press <ENTER> to quit");
            Console.ReadLine();
        }
    }
}