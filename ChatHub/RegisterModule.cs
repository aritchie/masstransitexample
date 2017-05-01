using System;
using Autofac;
using ChatHub.Services;
using GreenPipes;
using MassTransit;


namespace ChatHub
{
    public class RegisterModule : Module
    {
        public string Uri { get; set; } = "rabbitmq://192.168.99.100";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string QueueName { get; set; } = "chathub_main";


        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(this.Uri), x =>
                    {
                        x.Username(this.UserName);
                        x.Password(this.Password);
                    });
                    //cfg.UseRetry(x => x.Interval(3, TimeSpan.FromSeconds(1))); // retry 3 times with a 1 second break
                    cfg.ReceiveEndpoint(host, this.QueueName, e => e.LoadFrom(context));
                }))
                .As<IBus>()
                .As<IBusControl>()
                .SingleInstance();

            builder.RegisterConsumers(this.ThisAssembly);
            builder
                .RegisterType<NoMessageFilter>()
                //.RegisterType<BadWordsMessageFilter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
