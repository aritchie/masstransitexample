using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace ChatHub.Oracle
{
    public class Repository : IRepository, IStartable
    {
        public event EventHandler<OraMessage> DbNotification;


        public Task StartListener() => Task.Run(() =>
        {
            var dependency = new OracleDependency
            {
                QueryBasedNotification = false
            };
            dependency.OnChange += this.OnNotification;

            var conn = this.CreateConnection();
            var cmd = conn.CreateCommand();
            dependency.AddCommandDependency(cmd);

            cmd.CommandText = "SELECT * FROM CHATS";
            cmd.AddRowid = true;
            cmd.Notification.IsNotifiedOnce = false;
            cmd.ExecuteNonQuery();
        });


        public void Insert(string fromUser, string msg, DateTimeOffset date, bool recv)
        {
            using (var conn = this.CreateConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO CHATS(FROM_USER, MESSAGE, DATE_CREATED, RECEIVED) VALUES (:from_user, :msg_body, :created_on, 1)";
                    cmd.Parameters.Add(":from_user", fromUser);
                    cmd.Parameters.Add(":msg_body", msg);

                    cmd.Parameters.Add(new OracleParameter("created_on", OracleDbType.TimeStampTZ) { Value = date });
                    cmd.ExecuteNonQuery();
                }
            }
        }


        OracleConnection CreateConnection()
        {
            var conn = new OracleConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            conn.Open();
            return conn;
        }


        void OnNotification(object sender, OracleNotificationEventArgs args)
        {
            if (args.Type != OracleNotificationType.Change || args.Info != OracleNotificationInfo.Insert)
                return;

            var rowIds = new List<string>();
            foreach (DataRow row in args.Details.Rows)
                rowIds.Add((string)row["rowId"]);

            using (var conn = this.CreateConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT MESSAGE, FROM_USER, DATE_CREATED, RECEIVED FROM CHATS WHERE ROWID IN (";
                    for (var i = 0; i < rowIds.Count; i++)
                    {
                        if (i > 0)
                            cmd.CommandText += ",";

                        cmd.CommandText += $"'{rowIds[i]}'";
                    }
                    cmd.CommandText += ")";

                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            this.OnDbNotification(new OraMessage
                            {
                                Message = reader.GetString(0),
                                From = reader.GetString(1),
                                DateCreated = reader.GetDateTime(2),
                                Received = reader.GetInt32(3) == 1
                            });
                        }
                    }
                }
            }
        }


        void OnDbNotification(OraMessage message) => this.DbNotification?.Invoke(this, message);


        public void Start()
        {
            try
            {
                using (var conn = this.CreateConnection())
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = File.ReadAllText("chats.sql");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Table already exist? - " + ex);
            }
        }
    }
}
