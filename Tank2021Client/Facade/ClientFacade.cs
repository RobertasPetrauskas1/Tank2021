using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021SharedContent;
using Tank2021SharedContent.Enums;

namespace Tank2021Client.Facade
{
    public class ClientFacade
    {
        GameWindow gameWindow;
        ClientBulletUtils bulletUtils;
        ClientTankUtils tankUtils;
        ClientScoreUtils scoreUtils;

        public ClientFacade(GameWindow window)
        {
            gameWindow = window;
            bulletUtils = new ClientBulletUtils(window);
            tankUtils = new ClientTankUtils(window);
            scoreUtils = new ClientScoreUtils(window);
        }

        public void UpdateMap(Map map)
        {
            gameWindow.SetFigures(new List<Figure>());
            var player1Tank = map.GetPlayer(PlayerType.PLAYER1).Tank;
            var player2Tank = map.GetPlayer(PlayerType.PLAYER2).Tank;

            tankUtils.UpdateTank(player1Tank);
            tankUtils.UpdateTank(player2Tank);
            bulletUtils.UpdateBullets(player1Tank?.Gun?.Bullets);
            bulletUtils.UpdateBullets(player2Tank?.Gun?.Bullets);
            scoreUtils.UpdateScores(player1Tank, player2Tank);
            gameWindow.Invalidate();
        }
    }
}
