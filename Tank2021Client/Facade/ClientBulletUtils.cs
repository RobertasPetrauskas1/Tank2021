using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021Client.Flyweight;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Guns;

namespace Tank2021Client.Facade
{
    public class ClientBulletUtils
    {
        GameWindow window;
        ImageFactory ImageFactory;
        public ClientBulletUtils(GameWindow window)
        {
            this.window = window;
            ImageFactory = new ImageFactory();
        }

        public void UpdateBullets(Gun gun)
        {
            if (gun != null && gun.Bullets != null && gun.Any())
            {
                foreach (var bullet in gun)
                {
                    var bulletImage = ImageFactory.GetImage(ImageType.Bullet, bullet.Rotation);
                    window.AddFigure(new Figure(bullet.Coordinates, bulletImage.Image.Width, bulletImage.Image.Height, bullet.Rotation, bulletImage.Image, false));
                }
            }
        }
    }
    
}
