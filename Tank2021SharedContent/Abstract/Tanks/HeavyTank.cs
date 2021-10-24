using System.Drawing;
using Tank2021SharedContent.Abstract.Armors;
using Tank2021SharedContent.Abstract.Guns;
using Tank2021SharedContent.AbstractFactories;
using Tank2021SharedContent.Strategy;

namespace Tank2021SharedContent.Abstract.Tanks
{
    public class HeavyTank : Tank
    {
        public override Gun Gun { get; set; }
        public override Armor Armor { get; set; }
        public override int Health { get; set; }
        public override int Speed { get; set; }
        public override string ImageLocation { get; set; }

        public HeavyTank(Point coordinates, RotateFlipType rotation, MoveAlgorithm moveAlgorithm) :
            base(coordinates, rotation, moveAlgorithm)
        {
        }

        public override AbstractFactory GetAbstractFactory()
        {
            return new HeavyFactory();
        }
    }
}
