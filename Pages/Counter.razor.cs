using Microsoft.AspNetCore.Components;

using static BlazorMQTT.Data.MQTTDataService;
using BlazorMQTT.Data;

namespace BlazorMQTT.Pages
{
    public class CounterComponent : ComponentBase, IDisposable
    {


        public int currentCount = 0;
        public static String res
        {
            get
            {
                return Data.MQTTDataService.number;
            }
            set
            {
                res = value;
            }
        }

        public async void IncrementCount()
        {
            
            //Data.MQTTDataService.number = currentCount.ToString();
            await Data.MQTTClient.Publish("counter", (Int32.Parse(Data.MQTTDataService.number)+1).ToString());
        }

        protected override void OnInitialized()
        {
            MQTTDataService.OnChange += OnChangeHandler;
            currentCount = Int32.Parse(res);
        }

        private async void OnChangeHandler()
        {
            await InvokeAsync(StateHasChanged);
        }
        public void Dispose() {
            MQTTDataService.OnChange -= OnChangeHandler;
        }
    }
}