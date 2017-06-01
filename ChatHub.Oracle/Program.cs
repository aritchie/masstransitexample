using System;
using System.Configuration;
using Autofac;
using MassTransit;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;


namespace ChatHub.Oracle
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

            Console.WriteLine("Press <ENTER> to quit");
            Console.ReadLine();
        }
    }
}
