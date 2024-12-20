using ClassLibrary1;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Formatter;
using System;
using System.Collections.Concurrent;

namespace ClassLibrary1 
{
    public class MqttThread : InfiniteLoopThread
    {
        private IManagedMqttClient mqttClient;
        private MqttPublicationMessageLite auxMqttPublicationMessageLite;
        private MqttSubscriptionManagement auxSubscriptionManagement;

        public ConcurrentQueue<MqttSubscriptionManagement> SubscriptionManagement { get; private set; }
        public ConcurrentQueue<MqttApplicationMessageReceivedEventArgs> MessagesFromMqtt { get; private set; }
        public ConcurrentQueue<MqttPublicationMessageLite> MessagesForMqtt { get; private set; }
        private string ServerIp;

        public delegate void OnStoppingHandler();
        public event OnStoppingHandler OnStoppingEvent;

        public MqttThread(string serverIp) : base($"MqttThread", false)
        {
            SubscriptionManagement = new ConcurrentQueue<MqttSubscriptionManagement>();
            MessagesFromMqtt = new ConcurrentQueue<MqttApplicationMessageReceivedEventArgs>();
            MessagesForMqtt = new ConcurrentQueue<MqttPublicationMessageLite>();
            this.ServerIp = serverIp;
            Start();
        }
        protected override void Finally() { }

        protected override async void MainLoop()
        {
            while (SubscriptionManagement.TryDequeue(out auxSubscriptionManagement))
            {
                switch (auxSubscriptionManagement.Type)
                {
                    case MqttSubscriptionManagementType.Subscribe:
                        await mqttClient.SubscribeAsync(auxSubscriptionManagement.Topic);
                        break;
                    case MqttSubscriptionManagementType.Unsuscribe:
                        await mqttClient.UnsubscribeAsync(auxSubscriptionManagement.Topic);
                        break;
                    default:
                        break;
                }
            }
            while (MessagesForMqtt.TryDequeue(out auxMqttPublicationMessageLite))
               await mqttClient.PublishAsync(auxMqttPublicationMessageLite.Topic, auxMqttPublicationMessageLite.Payload, auxMqttPublicationMessageLite.QualityOfServiceLevel, auxMqttPublicationMessageLite.Retain);
               
        }

        protected override async void Setup()
        {
            // Setup and start a managed MQTT client.
            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithTcpServer(this.ServerIp)
                    .WithProtocolVersion(MqttProtocolVersion.V500)
                    .Build())
                .Build();

            mqttClient = new MqttFactory().CreateManagedMqttClient();
            mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnClientConnected);
            mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnClientDisconnected);
            mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(OnClientMessageReceived);
            await mqttClient.StartAsync(options);
        }

        //protected override void StopRequested() { }

        private void OnClientMessageReceived(MqttApplicationMessageReceivedEventArgs x) { MessagesFromMqtt.Enqueue(x); }

        private static void OnClientConnected(MqttClientConnectedEventArgs x) { }

        private static void OnClientDisconnected(MqttClientDisconnectedEventArgs x) { }
    }
}