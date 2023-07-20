using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using BlazorMQTT;
using BlazorMQTT.Shared;
using EventAggregator.Blazor;
using static BlazorMQTT.Data.MQTTDataService;

namespace BlazorMQTT.Pages
{
    public partial class Counter : IHandle<ChangedNumberMessage>, IDisposable
    {
        [Inject]
        private IEventAggregator _eventAggregator { get; set; }

        private int currentCount = Int32.Parse(Data.MQTTDataService.number);
        private static String res
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

        private async void IncrementCount()
        {
            currentCount++;
            //Data.MQTTDataService.number = currentCount.ToString();
            await Data.MQTTClient.Publish("counter", currentCount.ToString());
        }

        protected override void OnInitialized()
        {
            _eventAggregator.Subscribe(this);
        }

        public Task HandleAsync(ChangedNumberMessage message)
        {
            StateHasChanged();
            return Task.CompletedTask;
        }
        public void Dispose() {
            _eventAggregator.Unsubscribe(this);
        }
    }
}