using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;


namespace ChatHub.Oracle
{
    public interface IRepository
    {
        Task StartListener();
        event EventHandler<OraMessage> DbNotification;

        void Insert(string fromUser, string msg, DateTimeOffset date, bool recv);
    }
}
