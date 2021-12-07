using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2021Client.ChanOfResponsibility;
using Tank2021Client.Facade;
using Tank2021SharedContent;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Enums;

namespace Tank2021Client
{
    public partial class GameWindow : Form
    {
        private const string ConnectionUrl = "https://localhost:5001/TankHub";
        private HubConnection _hubConnection;
        ClientFacade facade;
        PlayerType playerType;
        IList<Figure> figures;
        bool gameStarted = false;

        IKeyEventHandler keyPressHandler;

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
            facade = new ClientFacade(this);

            // Code must be after StartSignalR() because of _hubConnection dependency
            var upKeyHandler = new UpKeyHandler(_hubConnection, playerType);
            var downKeyHandler = new DownKeyHandler(_hubConnection, playerType);
            var leftKeyHandler = new LeftKeyHandler(_hubConnection, playerType);
            var rightKeyHandler = new RightKeyHandler(_hubConnection, playerType);
            var spaceKeyHandler = new SpaceKeyHandler(_hubConnection, playerType);
            var zKeyHandler = new ZKeyHandler(_hubConnection, playerType);

            upKeyHandler.SetNextHandler(downKeyHandler);
            downKeyHandler.SetNextHandler(leftKeyHandler);
            leftKeyHandler.SetNextHandler(rightKeyHandler);
            rightKeyHandler.SetNextHandler(spaceKeyHandler);
            spaceKeyHandler.SetNextHandler(zKeyHandler);
            zKeyHandler.SetNextHandler(new DefaultHandler());

            keyPressHandler = upKeyHandler;
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
            this.gameEndLabel.Text = $"{player} WON!!!";
            this.gameEndLabel.Visible = true;
            this.BackgroundImage = null;
            figures = new List<Figure>();
        }

        public void InitializeMap(Map map)
        {
            this.gameStartLabel.Visible = false;
            this.gameStarted = true;
            this.player1Score.Visible = true;
            this.player2Score.Visible = true;
            SetBackground(map.BackgroundImageLocation);
            _hubConnection.On<string>("UpdateMap", (updatedMap) =>
            {
                var map = JsonConvert.DeserializeObject<Map>(updatedMap, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
                facade.UpdateMap(map);
            });

            facade.UpdateMap(map);
        }


        private void SetBackground(string ImageLocation)
        {
            if (!string.IsNullOrWhiteSpace(ImageLocation))
                this.BackgroundImage = Image.FromFile(ImageLocation);
        }


        private async void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameStarted)
            {
                await keyPressHandler.HandleAsync(e);
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
            InitializeScores();
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
            this.gameEndLabel.AutoSize = true;
            this.gameEndLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.gameEndLabel.Location = new System.Drawing.Point(420, 390);
            this.gameEndLabel.Name = "gameStartLabel";
            this.gameEndLabel.Size = new System.Drawing.Size(250, 20);
            this.gameEndLabel.TabIndex = 0;
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
            this.HeavyForcefieldTankButton = new System.Windows.Forms.Button();

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

            this.HeavyForcefieldTankButton.Location = new System.Drawing.Point(417, 337);
            this.HeavyForcefieldTankButton.Name = "HeavyForcefieldTankButton";
            this.HeavyForcefieldTankButton.Size = new System.Drawing.Size(138, 32);
            this.HeavyForcefieldTankButton.TabIndex = 4;
            this.HeavyForcefieldTankButton.Text = "Heavy Forcefield Tank";
            this.HeavyForcefieldTankButton.UseVisualStyleBackColor = true;
            this.HeavyForcefieldTankButton.Click += TankSelectionClick;

            this.Controls.Add(this.HeavyForcefieldTankButton);
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
                    await InitializeGame(TankType.LightTank);
                    break;
                case "MediumTankButton":
                    await InitializeGame(TankType.MediumTank);
                    break;
                case "HeavyTankButton":
                    await InitializeGame(TankType.HeavyTank);
                    break;
                case "HeavyForcefieldTankButton":
                    await InitializeGame(TankType.HeavyForcefieldTank);
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
            this.HeavyForcefieldTankButton.Visible = enabled;
        }

        private async Task InitializeGame(TankType tank)
        {
            await _hubConnection.SendAsync("ConnectPlayer", playerType, tank);
            this.gameStartLabel.Text = $"{playerType} connected succesfully. Waiting for other player.";
            this.gameEndLabel.Visible = false;
            this.gameStartLabel.Visible = true;
        }

        private void InitializeScores()
        {
            this.player1Score = new System.Windows.Forms.Label();
            this.player2Score = new System.Windows.Forms.Label();
            
            this.player1Score.AutoSize = true;
            this.player1Score.BackColor = System.Drawing.Color.Transparent;
            this.player1Score.Location = new System.Drawing.Point(13, 13);
            this.player1Score.Name = "player1Score";
            this.player1Score.Size = new System.Drawing.Size(101, 20);
            this.player1Score.TabIndex = 0;
            this.player1Score.Text = "Player1 Score:";
            this.player1Score.Visible = false;
            
            this.player2Score.AutoSize = true;
            this.player2Score.BackColor = System.Drawing.Color.Transparent;
            this.player2Score.Location = new System.Drawing.Point(869, 9);
            this.player2Score.Name = "player2Score";
            this.player2Score.Size = new System.Drawing.Size(101, 20);
            this.player2Score.TabIndex = 1;
            this.player2Score.Text = "Player2 Score:";
            this.player2Score.Visible = false;
            
            this.Controls.Add(this.player2Score);
            this.Controls.Add(this.player1Score);
        }

        public void AddFigure(Figure figure)
        {
            figures.Add(figure);
        }
        public void RemoveFigure(Figure figure)
        {
            figures.Remove(figure);
        }
        public void SetFigures(IList<Figure> figures)
        {
            this.figures = figures;
        }
        public Label GetScoreLabel(PlayerType player)
        {
            switch (player)
            {
                case PlayerType.PLAYER1:
                    return this.player1Score;
                case PlayerType.PLAYER2:
                    return this.player2Score;
                default:
                    throw new ArgumentException($"No such playerType -> {player}");
            }
        }
    }
}
