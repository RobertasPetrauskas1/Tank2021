using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank2021Client
{
    public partial class Form1 : Form
    {
        private const string ConnectionUrl = "https://localhost:5001/TankHub";
        private readonly HubConnection _hubConnection;
        public Form1()
        {
            InitializeComponent();

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(ConnectionUrl)
                .Build();

            _hubConnection.Closed += async (error) =>
            {
                Console.WriteLine(error.Message);
                Console.WriteLine(error.InnerException);
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _hubConnection.InvokeAsync("SendMovement", "yamumgay");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            _hubConnection.On<string>("ReceivedMovement", (direction) =>
            {
                label1.Text = direction;
            });

            await _hubConnection.StartAsync();
        }
    }
}
