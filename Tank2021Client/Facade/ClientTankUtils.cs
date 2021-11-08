using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021Client.Facade
{
    public class ClientTankUtils
    {
        GameWindow window;
        public ClientTankUtils(GameWindow window)
        {
            this.window = window;
        }
        public void UpdateTank(Tank tank)
        {
            if (tank != null)
            {
                var tankImage = tank.TankImage.GetImage();
                window.AddFigure(new Figure(tank.Coordinates, tankImage.Width, tankImage.Height, tank.Rotation, tankImage));
            }
        }
    }
}
