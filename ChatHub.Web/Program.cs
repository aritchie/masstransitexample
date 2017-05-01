using System;
using System.Configuration;
using Microsoft.Owin.Hosting;


namespace ChatHub.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            var ep = ConfigurationManager.AppSettings["http.endpoint"];
            using (WebApp.Start<Startup>(ep))
            {
                Console.WriteLine("ChatHub Web is running on " + ep);
                Console.WriteLine("Press <ENTER> to quit");
                Console.ReadLine();
            }
        }
    }
}
