using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Drawing;
using System.Windows.Forms;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.HubRezults;

namespace Tank2021Client
{
    public partial class Console : Form
    {
        private HubConnection _hubConnection;
        private PlayerType _playerType;
        public Console(HubConnection hubConnection, PlayerType playerType)
        {
            _hubConnection = hubConnection;
            _playerType = playerType;
            CreateHandlers();

            InitializeComponent();
        }

        public void CreateHandlers()
        {
            _hubConnection.On<CodeStatus>("CommandStatus", (status) =>
            {
                if(status.PlayerType == _playerType)
                {
                    if (status.Success)
                    {
                        this.Status.ForeColor = Color.DarkGreen;
                        this.Status.Text = "Cheat code worked";
                    }
                    else
                    {
                        this.Status.ForeColor = Color.Red;
                        this.Status.Text = "Cheat code failed";
                    }
                }
            });

            _hubConnection.On<CodeStatus>("CommandUndoStatus", (status) =>
            {
                if(status.PlayerType == _playerType)
                {
                    if (status.Success)
                    {
                        this.Status.ForeColor = Color.DarkGreen;
                        this.Status.Text = "Undo worked";
                    }
                    else
                    {
                        this.Status.ForeColor = Color.Red;
                        this.Status.Text = "Undo failed";
                    }
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var content = this.textBox1.Text;
            if (!string.IsNullOrWhiteSpace(content))
            {
                await _hubConnection.SendAsync("CheatCode", _playerType, content);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await _hubConnection.SendAsync("UndoCheatCode", _playerType);
        }
    }
}
