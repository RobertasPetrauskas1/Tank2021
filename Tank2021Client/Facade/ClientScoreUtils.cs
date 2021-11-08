using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank2021SharedContent.Abstract.Tanks;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.Facade
{
    public class ClientScoreUtils
    {
        GameWindow gameWindow;
        Label player1Score;
        Label player2Score;

        public ClientScoreUtils(GameWindow window)
        {
            gameWindow = window;
            player1Score = window.GetScoreLabel(PlayerType.PLAYER1);
            player2Score = window.GetScoreLabel(PlayerType.PLAYER2);
        }

        public void UpdateScores(Tank player1Tank, Tank player2Tank)
        {
            var player1ArmorHits = player1Tank?.Armor?.HitsLeft;
            var player1Health = player1Tank?.Health;

            var player2ArmorHits = player2Tank?.Armor?.HitsLeft;
            var player2Health = player2Tank?.Health;

            player1Score.Text = $"{PlayerType.PLAYER1}\nArmorHits: {(player1ArmorHits != null ? player1ArmorHits : "UNKNOWN")}\nTankHealth: {(player1Health != null ? player1Health : "UNKNOWN")}";
            player1Score.Size = new Size(150, 150);

            player2Score.Text = $"{PlayerType.PLAYER2}\nArmorHits: {(player2ArmorHits != null ? player2ArmorHits : "UNKNOWN")}\nTankHealth: {(player2Health != null ? player2Health : "UNKNOWN")}";
            player2Score.Size = new Size(150, 150);
        }
    }
}
