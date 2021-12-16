using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2021Client.Mediator.Enum;
using Tank2021Client.Mediator.Mediators;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.Mediator.Colleagues
{
    public class GameWindowColleague : BaseColleague
    {
        private GameWindow _gameWindow;
        private Label player1Score;
        private Label player2Score;
        public GameWindowColleague(GameWindow gameWindow, IMediator mediator) : base(mediator)
        {
            _gameWindow = gameWindow;
            player1Score = _gameWindow.GetScoreLabel(PlayerType.PLAYER1);
            player2Score = _gameWindow.GetScoreLabel(PlayerType.PLAYER2);
        }

        public override ColleagueType getType() => ColleagueType.GameWindow;

        public override void ReceiveData(object data, string ev)
        {
            if(ev == "ChangedBullet" && data is List<Figure> bullets && bullets.Any())
            {
                _gameWindow.AddFigureRange(bullets);
            }
            
            if(ev == "ChangedTank" && data is Figure tank && tank != null)
            {
                _gameWindow.AddFigure(tank);
            }

            if(ev == "ChangedScore" && data is TankStatus status && status != null)
            {
                if(status.PlayerType == PlayerType.PLAYER1)
                {
                    player1Score.Text = $"{PlayerType.PLAYER1}\nArmorHits: {(status.ArmorHits != null ? status.ArmorHits : "UNKNOWN")}\nTankHealth: {(status.Health != null ? status.Health : "UNKNOWN")}";
                    player1Score.Size = new Size(150, 150);
                }
                else
                {
                    player2Score.Text = $"{PlayerType.PLAYER1}\nArmorHits: {(status.ArmorHits != null ? status.ArmorHits : "UNKNOWN")}\nTankHealth: {(status.Health != null ? status.Health : "UNKNOWN")}";
                    player2Score.Size = new Size(150, 150);
                }
            }

        }

        public override void SendData(object data, string ev)
        {
            _mediator.Notify(ColleagueType.GameWindow, data, ev);
        }

        public void UpdateGame(Map map)
        {
            _gameWindow.SetFigures(new List<Figure>());
            var player1Tank = map.GetPlayer(PlayerType.PLAYER1).Tank;
            var player2Tank = map.GetPlayer(PlayerType.PLAYER2).Tank;

            SendData(player1Tank?.Gun, "UpdateBullet");
            SendData(player2Tank?.Gun, "UpdateBullet");

            SendData(player1Tank, "UpdateTank");
            SendData(player2Tank, "UpdateTank");

            SendData(player1Tank, "UpdateScoreTank1");
            SendData(player2Tank, "UpdateScoreTank2");

            _gameWindow.Invalidate();
        }
    }
}
