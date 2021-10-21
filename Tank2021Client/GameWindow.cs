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
            EnableTankSelection(true);
            this.gameEndLabel.Text = $"{player} WON";
            this.gameEndLabel.Visible = true;
            initialized = false;
            figures = new List<Figure>();
        }

        public void InitializeMap(Map map)
        {
            this.gameStartLabel.Visible = false;
            this.gameStarted = true;
            SetBackground(map.BackgroundImageLocation);
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

        private void SetBackground(string ImageLocation)
        {
            if (!string.IsNullOrWhiteSpace(ImageLocation))
                this.BackgroundImage = Image.FromFile(ImageLocation);
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
                //case Keys.Enter:
                //    if (!initialized)
                //    {
                //        await _hubConnection.SendAsync("ConnectPlayer", playerType);
                //        this.gameStartLabel.Text = $"{playerType} connected succesfully. Waiting for other player.";
                //        this.gameEndLabel.Visible = false;
                //        this.gameStartLabel.Visible = true;
                //    }
                //    break;
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
            InitializeTankSelection();
            InitializeGameStartLabel();
            InitializeGameEndLabel();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameWindow_Paint);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameWindow_FormClosed);
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
            this.gameStartLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.gameStartLabel.Dock = DockStyle.Fill;
            this.gameStartLabel.Visible = false;
            this.Controls.Add(this.gameStartLabel);
        }

        private void InitializeGameEndLabel()
        {
            this.gameEndLabel = new System.Windows.Forms.Label();
            this.gameEndLabel.AutoSize = false;
            this.gameEndLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gameEndLabel.Location = new System.Drawing.Point(417, 350);
            this.gameEndLabel.Name = "gameStartLabel";
            this.gameEndLabel.Size = new System.Drawing.Size(250, 20);
            this.gameEndLabel.TabIndex = 0;
            this.gameEndLabel.TextAlign = ContentAlignment.MiddleCenter;
            //this.gameEndLabel.Dock = DockStyle.Fill;
            this.gameEndLabel.Visible = false;
            this.Controls.Add(this.gameEndLabel);
        }

        private async void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            await _hubConnection.SendAsync("DisconnectPlayer", playerType);
            this.Close();
        }

        private void InitializeTankSelection()
        {
            this.ChooseTankLabel = new System.Windows.Forms.Label();
            this.LightTankButton = new System.Windows.Forms.Button();
            this.MediumTankButton = new System.Windows.Forms.Button();
            this.HeavyTankButton = new System.Windows.Forms.Button();
            
            this.ChooseTankLabel.AutoSize = true;
            this.ChooseTankLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ChooseTankLabel.Location = new System.Drawing.Point(417, 45);
            this.ChooseTankLabel.Name = "ChooseTankLabel";
            this.ChooseTankLabel.Size = new System.Drawing.Size(138, 20);
            this.ChooseTankLabel.TabIndex = 0;
            this.ChooseTankLabel.Text = "Choose Tank Type:";
            
            this.LightTankButton.Location = new System.Drawing.Point(417, 109);
            this.LightTankButton.Name = "LightTankButton";
            this.LightTankButton.Size = new System.Drawing.Size(138, 32);
            this.LightTankButton.TabIndex = 1;
            this.LightTankButton.Text = "Light Tank";
            this.LightTankButton.UseVisualStyleBackColor = true;
            this.LightTankButton.Click += TankSelectionClick;

            this.MediumTankButton.Location = new System.Drawing.Point(417, 186);
            this.MediumTankButton.Name = "MediumTankButton";
            this.MediumTankButton.Size = new System.Drawing.Size(138, 32);
            this.MediumTankButton.TabIndex = 2;
            this.MediumTankButton.Text = "Medium Tank";
            this.MediumTankButton.UseVisualStyleBackColor = true;
            this.MediumTankButton.Click += TankSelectionClick;

            this.HeavyTankButton.Location = new System.Drawing.Point(417, 261);
            this.HeavyTankButton.Name = "HeavyTankButton";
            this.HeavyTankButton.Size = new System.Drawing.Size(138, 32);
            this.HeavyTankButton.TabIndex = 3;
            this.HeavyTankButton.Text = "Heavy Tank";
            this.HeavyTankButton.UseVisualStyleBackColor = true;
            this.HeavyTankButton.Click += TankSelectionClick;

            this.Controls.Add(this.HeavyTankButton);
            this.Controls.Add(this.MediumTankButton);
            this.Controls.Add(this.LightTankButton);
            this.Controls.Add(this.ChooseTankLabel);
        }

        private async void TankSelectionClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            switch (button.Name)
            {
                case "LightTankButton":
                    await InitializeGame();
                    break;
                case "MediumTankButton":
                    await InitializeGame();
                    break;
                case "HeavyTankButton":
                    await InitializeGame();
                    break;
                default:
                    throw new ArgumentException($"No such button exists -> {button.Name}");
            }

            EnableTankSelection(false);
        }

        private void EnableTankSelection(bool enabled)
        {
            this.ChooseTankLabel.Visible = enabled;
            this.LightTankButton.Visible = enabled;
            this.MediumTankButton.Visible = enabled;
            this.HeavyTankButton.Visible = enabled;
        }

        private async Task InitializeGame()
        {
            await _hubConnection.SendAsync("ConnectPlayer", playerType);
            this.gameStartLabel.Text = $"{playerType} connected succesfully. Waiting for other player.";
            this.gameEndLabel.Visible = false;
            this.gameStartLabel.Visible = true;
        }
    }
}
