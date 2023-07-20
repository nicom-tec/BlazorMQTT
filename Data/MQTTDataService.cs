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
            mqttClient.ApplicationMessageReceivedAsync += async e =>
            {                
                number = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment));
                NotifyStateChanged();
                //Hier muss man nochmal schauen, Object reference not set to an instance of an Object
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