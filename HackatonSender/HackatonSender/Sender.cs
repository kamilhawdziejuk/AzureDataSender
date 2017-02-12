using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HackatonSender
{
    using Microsoft.ServiceBus.Messaging;

    //using Newtonsoft.Json;

    public class Sender
    {
        readonly string _eventHubConnectionString;

        public Sender(string connectionString)
        {
            _eventHubConnectionString = connectionString;
        }

        public bool SendEvents(DateTime dt, string sensorName, string sensorData)
        {
            // Create EventHubClient
            EventHubClient client = EventHubClient.CreateFromConnectionString(_eventHubConnectionString);

            bool bEventSent = false;

            try
            {
                var tasks = new List<Task>();
                // Send messages to Event Hub
                Console.WriteLine("Sending messages to Event Hub {0}", client.Path);


                // Create the device/temperature metric
                var info = new MetricEvent()
                {
                    SensorName = sensorName,
                    SensorValue = sensorData,
                    EntryDateTime = dt
                };
                var serializedString = JsonConvert.SerializeObject(info);
                EventData data = new EventData(Encoding.UTF8.GetBytes(serializedString))
                {
                };

                // Set user properties if needed
                data.Properties.Add("Type", "Telemetry_" + DateTime.Now.ToLongTimeString());
                OutputMessageInfo(DateTime.Now.ToString() + " SENDING: ", data, info);

                // Send the metric to Event Hub
                tasks.Add(client.SendAsync(data));


                Task.WaitAll(tasks.ToArray());
                bEventSent = true;
            }
            catch (Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error on send: " + exp.Message);
                Console.ResetColor();
            }

            client.CloseAsync().Wait();
            return bEventSent;
        }

        static void OutputMessageInfo(string action, EventData data, MetricEvent info)
        {
            if (data == null)
            {
                return;
            }
            if (info != null)
            {
                Console.WriteLine("SensorName: {0}, SensorData: {1}", info.SensorName, info.SensorValue);
            }
        }
    }
}
