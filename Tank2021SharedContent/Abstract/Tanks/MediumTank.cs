using System.Drawing;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.AbstractFactories;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Abstract.Tanks
{
    public class MediumTank : Tank
    {
        public override Gun Gun { get; set; }
        public override Armor Armor { get; set; }
        public override int Health { get; set; }
        public override int Speed { get; set; }
        public override string ImageLocation { get; set; }

        public MediumTank(Point coordinates, int speed, RotateFlipType rotation, string imageLocation, int health, MoveAlgorithm moveAlgorithm) :
            base(coordinates, rotation, moveAlgorithm)
        {
            Speed = speed;
            ImageLocation = imageLocation;
            Health = health;
        }

        public override AbstractFactory GetAbstractFactory()
        {
            return new MediumFactory();
        }
    }
}
