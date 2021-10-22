using System.Drawing;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.AbstractFactories;

namespace Tank2021SharedContent.Abstract.Tanks
{
    public class LightTank : Tank
    {
        public override Gun Gun { get; set; }
        public override Armor Armor { get; set; }
        public override int Health { get; set; }
        public override int Speed { get; set; }
        public override string ImageLocation { get; set; }

        public LightTank(Point coordinates, int speed, RotateFlipType rotation, string imageLocation, int health) : 
            base(coordinates, rotation)
        {
            Speed = speed;
            ImageLocation = imageLocation;
            Health = health;
        }

        public override AbstractFactory GetAbstractFactory()
        {
            return new LightFactory();
        }
    }
}
