﻿using System;
using Autofac;
using MassTransit;


namespace ChatHub.Web
{
    public class RegisterModule : Module
    {
        public string Uri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }


        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterConsumers(this.ThisAssembly);

            builder
                .Register(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    //cfg.AddReceiveEndpointSpecification(new RabbitMqReceiveEndpointSpecification());
                    var host = cfg.Host(new Uri(this.Uri), x =>
                    {
                        x.Username(this.UserName);
                        x.Password(this.Password);
                    });
                    cfg.ReceiveEndpoint(host, this.QueueName, e => e.LoadFrom(context));
                }))
                .As<IBus>()
                .As<IBusControl>()
                .SingleInstance();
        }
    }
}
