using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Guns;

namespace Tank2021Client.Facade
{
    public class ClientBulletUtils
    {
        GameWindow window;
        public ClientBulletUtils(GameWindow window)
        {
            this.window = window;
        }

        public void UpdateBullets(List<Bullet> bullets)
        {
            if (bullets != null && bullets.Any())
            {
                foreach (var bullet in bullets)
                {
                    var bulletImage = Image.FromFile(bullet.ImageLocation);
                    window.AddFigure(new Figure(bullet.Coordinates, bulletImage.Width, bulletImage.Height, bullet.Rotation, bulletImage));
                }
            }
        }
    }
    
}
