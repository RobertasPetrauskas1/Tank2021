using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021Client.Flyweight;
using Tank2021Client.Mediator.Enum;
using Tank2021Client.Mediator.Mediators;
using Tank2021SharedContent;
using Tank2021SharedContent.Abstract.Guns;

namespace Tank2021Client.Mediator.Colleagues
{
    public class ClientBulletColleague : BaseColleague
    {
        private ImageFactory _imageFactory;
        public ClientBulletColleague(IMediator mediator) : base(mediator)
        {
            _imageFactory = new ImageFactory();
        }

        public override ColleagueType getType() => ColleagueType.Bullet;

        public override void ReceiveData(object data, string ev)
        {
            if(ev == "UpdateBullet" && data is Gun gun && gun != null && gun.Any())
            {
                UpdateBullets(gun);
            }
        }

        public override void SendData(object data, string ev)
        {
            _mediator.Notify(ColleagueType.Bullet, data, ev);
        }

        public void UpdateBullets(Gun gun)
        {
            var figures = new List<Figure>();
            if (gun != null && gun.Any())
            {
                foreach (var bullet in gun)
                {
                    var bulletImage = _imageFactory.GetImage(ImageType.Bullet, bullet.Rotation);
                    figures.Add(new Figure(bullet.Coordinates, bulletImage.Image.Width, bulletImage.Image.Height, bullet.Rotation, bulletImage.Image, false));
                }
            }

            SendData(figures, "ChangedBullet");
        }
    }
}
