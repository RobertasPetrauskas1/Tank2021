using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank2021Client
{
    public partial class GameWindow : Form
    {
        private const string ConnectionUrl = "https://localhost:5001/TankHub";
        private HubConnection _hubConnection;
        private Bitmap BattleField;
        public GameWindow()
        {
            InitializeComponent();
            StartSignalR();
        }

        public void StartSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(ConnectionUrl)
                .Build();

            //On smth insert here

            _hubConnection.StartAsync();
        }

        public void InitializeGameModel()
        {
            BattleField = new Bitmap(1000, 1000, PixelFormat.Format24bppRgb);
            Graphics.FromImage(BattleField);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "GameWindow";
        }
    }
}
