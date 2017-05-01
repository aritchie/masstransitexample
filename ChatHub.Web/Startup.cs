using System;
using System.Configuration;
using System.Reflection;
using Autofac;
using Autofac.Integration.SignalR;
using ChatHub.Web.Hubs;
using MassTransit;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Cors;
using Owin;


namespace ChatHub.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var cfg = new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = true,
                EnableJSONP = true
            };

            var builder = new ContainerBuilder();
            builder.RegisterModule(new RegisterModule
            {
                Uri = ConfigurationManager.AppSettings["queue.uri"],
                UserName = ConfigurationManager.AppSettings["queue.username"],
                Password = ConfigurationManager.AppSettings["queue.password"],
                QueueName = ConfigurationManager.AppSettings["queue.name"]
            });
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            builder
                .Register(_ => cfg.Resolver.Resolve<IConnectionManager>().GetHubContext<CentralHub, ICentralHub>())
                .As<IHubContext<ICentralHub>>()
                .SingleInstance();

            var container = builder.Build();
            cfg.Resolver = new AutofacDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseCors(CorsOptions.AllowAll);
            app.UseStaticFiles("");
            app.MapSignalR(cfg);

            container.Resolve<IBusControl>().Start();
        }
    }
}
