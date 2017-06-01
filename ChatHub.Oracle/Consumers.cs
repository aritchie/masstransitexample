using System;
using System.Configuration;
using System.Threading.Tasks;
using ChatHub.Contracts.Events;
using MassTransit;
using Oracle.ManagedDataAccess.Client;


namespace ChatHub.Oracle
{
    public class Consumers : IConsumer<IMessageReceived>, IConsumer<IUserStateChanged>
    {
        public Task Consume(ConsumeContext<IMessageReceived> context)
        {
            this.Insert(
                context.Message.From,
                context.Message.Body,
                context.Message.SendDate
            );
            return Task.FromResult(new object());
        }


        public Task Consume(ConsumeContext<IUserStateChanged> context)
        {
            this.Insert(
                context.Message.Name,
                context.Message.Connected ? "CONNECTED" : "DISCONNECTED",
                DateTimeOffset.Now
            );
            return Task.FromResult(new object());
        }


        void Insert(string from, string body, DateTimeOffset date)
        {
            using (var conn = new OracleConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO CHAT(FROM, BODY, DATE, RECEIVED) VALUES (:from, :body, :date, 1)";
                    cmd.Parameters.Add(new OracleParameter("from", from));
                    cmd.Parameters.Add(new OracleParameter("body", body));
                    cmd.Parameters.Add(new OracleParameter("date", date));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
