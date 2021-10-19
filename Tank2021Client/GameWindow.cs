using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2021SharedContent;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Enums;

namespace Tank2021Client
{
    public partial class GameWindow : Form
    {
        private const string ConnectionUrl = "https://localhost:5001/TankHub";
        private HubConnection _hubConnection;
        PlayerType playerType;
        IList<Figure> figures;
        bool initialized = false;
        bool gameStarted = false;
        public GameWindow(PlayerType player)
        {
            playerType = player;
            
            InitializeComponent(player);
            this.SetStyle(  //Reduce flickering
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            this.UpdateStyles();

            StartSignalR();

            figures = new List<Figure>();
        }

        public void StartSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(ConnectionUrl)
                .Build();

            _hubConnection.On<string>("InitializeGame", (startingMap) => 
            {
                var map = JsonConvert.DeserializeObject<Map>(startingMap, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                InitializeMap(map);
            });

            _hubConnection.On<PlayerType>("GameOver", (playerType) =>
            {
                 GameOver(playerType);
            });

            _hubConnection.StartAsync();
        }

        public void GameOver(PlayerType player)
        {
            _hubConnection.Remove("UpdateMap");
            gameStarted = false;
            this.gameEndLabel.Text = $"{player} WON, press ENTER to play again";
            this.gameEndLabel.Visible = true;
            initialized = false;
            figures = new List<Figure>();
        }

        public void InitializeMap(Map map)
        {
            this.gameStartLabel.Visible = false;
            this.gameStarted = true;
            _hubConnection.On<string>("UpdateMap", (updatedMap) =>
            {
                var map = JsonConvert.DeserializeObject<Map>(updatedMap, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                UpdateMap(map);
            });

            UpdateMap(map);
        }

        public void UpdateMap(Map map)
        {
            figures = new List<Figure>();
            UpdateTank(map.GetPlayer(PlayerType.PLAYER1).Tank);
            UpdateTank(map.GetPlayer(PlayerType.PLAYER2).Tank);
            UpdateBullets(map.GetPlayer(PlayerType.PLAYER1).Tank?.Gun?.Bullets);
            UpdateBullets(map.GetPlayer(PlayerType.PLAYER2).Tank?.Gun?.Bullets);
            Invalidate();
        }

        private void UpdateTank(Tank tank)
        {
            if (tank != null)
            {
                var tankImage = Image.FromFile(tank.ImageLocation);
                figures.Add(new Figure(tank.Coordinates, tankImage.Width, tankImage.Height, tank.Rotation, tankImage));
            }
        }

        private void UpdateBullets(List<Bullet> bullets)
        {
            if(bullets != null && bullets.Any())
            {
                foreach(var bullet in bullets)
                {
                    var bulletImage = Image.FromFile(bullet.ImageLocation);
                    figures.Add(new Figure(bullet.Coordinates, bulletImage.Width, bulletImage.Height, bullet.Rotation, bulletImage));
                }
            }
        }

        private async void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    if (!initialized)
                    {
                        await _hubConnection.SendAsync("ConnectPlayer", playerType);
                        this.gameStartLabel.Text = $"{playerType} connected succesfully. Waiting for other player.";
                        this.gameEndLabel.Visible = false;
                        this.gameStartLabel.Visible = true;
                    }
                    break;
                case Keys.Up:
                    if (gameStarted)
                        await _hubConnection.SendAsync("MoveUp", playerType);
                    break;
                case Keys.Down:
                    if (gameStarted)
                        await _hubConnection.SendAsync("MoveDown", playerType);
                    break;
                case Keys.Left:
                    if (gameStarted)
                        await _hubConnection.SendAsync("MoveLeft", playerType);
                    break;
                case Keys.Right:
                    if (gameStarted)
                        await _hubConnection.SendAsync("MoveRight", playerType);
                    break;
                case Keys.Space:
                    if (gameStarted)
                        await _hubConnection.SendAsync("Shoot", playerType);
                    break;
            }
        }

        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            foreach (var figure in figures)
                figure.Draw(graphics);
        }

        private void InitializeComponent(PlayerType player)
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            InitializeGameWindow(player);
            InitializeGameStartLabel();
            InitializeGameEndLabel();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameWindow_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeGameWindow(PlayerType player)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(ClientSideConstants.ClientWidth, ClientSideConstants.ClientHeight);
            this.Name = "GameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = player.ToString();
        }

        private void InitializeGameStartLabel()
        {
            this.gameStartLabel = new System.Windows.Forms.Label();
            this.gameStartLabel.AutoSize = false;
            this.gameStartLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gameStartLabel.Location = new System.Drawing.Point(61, 105);
            this.gameStartLabel.Name = "gameStartLabel";
            this.gameStartLabel.Size = new System.Drawing.Size(153, 20);
            this.gameStartLabel.TabIndex = 0;
            this.gameStartLabel.Text = "press ENTER to start";
            this.gameStartLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.gameStartLabel.Dock = DockStyle.Fill;
            this.Controls.Add(this.gameStartLabel);
        }

        private void InitializeGameEndLabel()
        {
            this.gameEndLabel = new System.Windows.Forms.Label();
            this.gameEndLabel.AutoSize = false;
            this.gameEndLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gameEndLabel.Location = new System.Drawing.Point(61, 105);
            this.gameEndLabel.Name = "gameStartLabel";
            this.gameEndLabel.Size = new System.Drawing.Size(153, 20);
            this.gameEndLabel.TabIndex = 0;
            this.gameEndLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.gameEndLabel.Dock = DockStyle.Fill;
            this.gameEndLabel.Visible = false;
            this.Controls.Add(this.gameEndLabel);
        }
    }
}
