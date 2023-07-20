using BlazorMQTT.Data;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components;
using MQTTnet;
using MQTTnet.Client;

using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BlazorMQTT.Data
{
    public class MQTTDataService : BackgroundService
    {
        [Inject]
        private IEventAggregator _eventAggregator { get; set; }

        public static string number = "0";
        private static  MqttFactory mqttfactory = new();
        private static IMqttClient mqttClient = mqttfactory.CreateMqttClient();

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {

            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();
            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {                
                number = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
                await _eventAggregator.PublishAsync(new ChangedNumberMessage());

                //return Task.CompletedTask;
            };
            var mqttSubscribeOptions = mqttfactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                f =>
                {
                f.WithTopic("counter");
                })
                .Build();
            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await mqttClient.DisconnectAsync();
            await base.StopAsync(cancellationToken);
        }
        public class ChangedNumberMessage
        {
        }

    }
}