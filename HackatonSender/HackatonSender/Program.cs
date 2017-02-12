using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace HackatonSender
{
    class Program
    {

        static string eventHubName = "eventhub2";
        private const string ConnectionString = "";
        private const string DbConnectioNString = "";

        static void Main(string[] args)
        {
            TestConnectToDb();
            SendingRandomMessages();
        }

        static bool TestConnectToDb()
        {
            var cnn = new SqlConnection(DbConnectioNString);
            try
            {
                cnn.Open();
                cnn.Close();
                return cnn.State == ConnectionState.Closed;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        static void SendingRandomMessages()
        {
            try
            {
                var builder = new ServiceBusConnectionStringBuilder(ConnectionString);
                builder.TransportType = TransportType.Amqp;
                var sender = new Sender(ConnectionString);

                while (true)
                {
                    bool bIsEventSent = sender.SendEvents(DateTime.Now, "guid", Guid.NewGuid().ToString());
                    Thread.Sleep(500);
                }

            }
            catch (Exception ex)
            {
                
                throw;
            }
          
        }
    }
}
