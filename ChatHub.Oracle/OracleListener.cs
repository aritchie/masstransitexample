using System;
using System.Configuration;
using System.Data;
using Autofac;
using ChatHub.Contracts.Commands;
using MassTransit;
using Oracle.ManagedDataAccess.Client;


namespace ChatHub.Oracle
{
    public class OracleListener : IStartable
    {
        readonly IBus bus;


        public OracleListener(IBus bus)
        {
            this.bus = bus;
        }


        public void Start()
        {
            var conn = new OracleConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM CHAT";
            cmd.AddRowid = true;
            cmd.Notification.IsNotifiedOnce = false;

            var dependency = new OracleDependency(cmd);
            dependency.OnChange += async (sender, e) =>
            {
                if (e.Type != OracleNotificationType.Change || e.Info != OracleNotificationInfo.Insert)
                    return;

                foreach (DataRow row in e.Details.Rows)
                {
                    var recv = (int)row["RECV"] == 1;

                    if (recv)
                    {
                        Console.WriteLine("MESSAGE RECEIVED FROM: " + (string)row["FROM"]);
                        Console.WriteLine((string)row["BODY"]);
                    }
                    else
                    {
                        await this.bus.Publish(new SendMessage
                        {
                            From = "THE GREAT ORACLE",
                            Body = (string)row["BODY"]
                        });
                        Console.WriteLine("Message Sent - " + (string)row["BODY"]);
                    }
                }
            };
            Console.WriteLine("The all-powerful ORACLE LISTENER is active....Larry is watching too... because he's a pervert");
        }
    }
}
