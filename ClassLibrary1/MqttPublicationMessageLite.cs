using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class MqttPublicationMessageLite
    {
        public string Topic { get; private set; }
        public string Payload { get; private set; }
        public MqttQualityOfServiceLevel QualityOfServiceLevel { get; private set; }
        public bool Retain { get; private set; }

        public MqttPublicationMessageLite(string topic, string payload)
        {
            Topic = topic;
            Payload = payload;
            QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            Retain = false;
        }

        public MqttPublicationMessageLite(string topic, string payload, MqttQualityOfServiceLevel qualityOfServiceLevel)
        {
            Topic = topic;
            Payload = payload;
            QualityOfServiceLevel = qualityOfServiceLevel;
            Retain = false;
        }

        public MqttPublicationMessageLite(string topic, string payload, bool retain)
        {
            Topic = topic;
            Payload = payload;
            QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            Retain = retain;
        }
    }
}
