using Microsoft.AspNetCore.SignalR;
using MQTTnet;
using MQTTnet.Client;
using System.Text;


namespace BlazorMQTT.Data
{
    public class MQTTDataService : BackgroundService
    {
  

        public static event Action OnChange;
        private static void NotifyStateChanged() => OnChange?.Invoke();

        public static string number = "0";
        private static readonly MqttFactory mqttfactory = new();
        private static readonly IMqttClient mqttClient = mqttfactory.CreateMqttClient();

        


        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {

            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost").Build();
            mqttClient.ApplicationMessageReceivedAsync += async e  =>
            {   
                await Task.Run(() =>
                {
                    if (e.ApplicationMessage.Topic == "counter")
                    {
                        number = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                        Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
                        NotifyStateChanged();
                        
                    }
                    MQTTState.addEntry(e.ApplicationMessage.Topic, Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
                });  
            };
            var mqttSubscribeOptions = mqttfactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                f =>
                {
                f.WithTopic("#");
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