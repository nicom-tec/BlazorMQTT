using Microsoft.AspNetCore.SignalR;
using static BlazorMQTT.Data.MQTTState;

namespace BlazorMQTT.Data
{
    public class MQTTDataHub : Hub   
    {
        public void SendMQTTEntryHasChanged(Entry entry)
        {
            Clients.All.SendAsync("EntryReceive", entry);
        }
        public  void SendMQTTTreeHasChanged()
        {
            Clients.All.SendAsync("newTreeReceive");
        }
    }
}
