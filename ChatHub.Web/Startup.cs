using System;
using System.Configuration;
using Autofac;
using MassTransit;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;


namespace ChatHub.Web
{
    public class Startup
    {
        public static IContainer Container { get; private set; }


        public void Configuration(IAppBuilder app)
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
            //app.UseCors(CorsOptions.AllowAll);
            Container = container;

            app.UseStaticFiles("");

            app.Map("/signalr", map =>
            {

                var cfg = new HubConfiguration
                {
                    EnableDetailedErrors = true,
                    EnableJavaScriptProxies = true,
                    EnableJSONP = true
                };
                map.RunSignalR(cfg);
            });
        }
    }
}
