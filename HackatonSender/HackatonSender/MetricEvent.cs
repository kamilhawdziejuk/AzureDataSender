using System;
using System.Runtime.Serialization;

namespace HackatonSender
{
    [DataContract]
    public class MetricEvent
    {
        [DataMember]
        public string SensorName { get; set; }


        [DataMember]
        public string SensorValue { get; set; }
        
        [DataMember] 
        public DateTime EntryDateTime { get; set; }
    }

}
