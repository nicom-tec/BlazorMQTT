using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Server;


namespace BlazorMQTT.Data
{
    public class MQTTBrokerService : BackgroundService
    {

        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            var mqttFactory = new MqttFactory();
            var mqttServerOptions = new MqttServerOptionsBuilder()
             .WithDefaultEndpoint()
             .WithDefaultEndpointPort(1883)
             .Build();
            var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
            await mqttServer.StartAsync();
        }
    }



}
