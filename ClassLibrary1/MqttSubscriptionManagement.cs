using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public class MqttSubscriptionManagement
    {
        public string Topic { get; set; }
        public MqttSubscriptionManagementType Type { get; set; }
        public MqttQualityOfServiceLevel QualityOfServiceLevel { get; set; }
        public MqttSubscriptionManagement(string topic, MqttSubscriptionManagementType type, MqttQualityOfServiceLevel qualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce)
        {
            Topic = topic;
            Type = type;
            QualityOfServiceLevel = qualityOfServiceLevel;
        }
    }
}
