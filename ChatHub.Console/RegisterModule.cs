using System;
using Autofac;
using MassTransit;


namespace ChatHub
{
    public class RegisterModule : Module
    {
        public string Uri { get; set; } = "rabbitmq://192.168.99.100";
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string QueueName { get; set; } = "chathub_web";


        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterConsumers(this.ThisAssembly);

            builder
                .Register(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(this.Uri), x =>
                    {
                        x.Username(this.UserName);
                        x.Password(this.Password);
                    });
                    cfg.ReceiveEndpoint(this.QueueName, e => e.LoadFrom(context));
                }))
                .As<IBus>()
                .As<IBusControl>()
                .SingleInstance();
        }
    }
}
