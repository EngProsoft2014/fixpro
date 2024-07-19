using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace FixPro.Services.Data
{
    public interface IHubConnections
    {
        Task Connect();
    }


    public class HubConnectionService 
    {

        private readonly Microsoft.AspNet.SignalR.Client.HubConnection connection;
        private readonly IHubProxy hubProxy;
        public event Action<string, string> MessageReceived;

        public string Massage { get; set; }

        public HubConnectionService()
        {
            //connection = new Microsoft.AspNet.SignalR.Client.HubConnection("https://projectservicesapi.engprosoft.com/");
            connection = new Microsoft.AspNet.SignalR.Client.HubConnection("https://fixproapi.engprosoft.net/");
            hubProxy = connection.CreateHubProxy("ChatHub");

            hubProxy.On<string, string, string, string>("addMessage", (name, message, fromUserId, toUserId) =>
            {
                MessageReceived?.Invoke(name, message);
            });

            if (hubProxy != null)
            {

                MessageReceived += (name, message) =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Massage = ($"{name}: {message}");
                    });
                };


            }
            else
            {
                return;
            }
        }

        public async Task Connect()
        {
            await connection.Start();
        }

        public async Task Disconnect()
        {
            connection.Stop();
        }

    }
}
