using System.Drawing;
using Tank2021SharedContent.Abstract.Tanks;

namespace Tank2021SharedContent.Strategy
{
    public class StopMovement : MoveAlgorithm
    {
        public override void MoveDown(Tank tank)
        {
            tank.Rotation = RotateFlipType.RotateNoneFlipNone;
        }

        public override void MoveLeft(Tank tank)
        {
            tank.Rotation = RotateFlipType.Rotate270FlipX;
        }

        public override void MoveRight(Tank tank)
        {
            tank.Rotation = RotateFlipType.Rotate90FlipX;
        }

        public override void MoveUp(Tank tank)
        {
            tank.Rotation = RotateFlipType.RotateNoneFlipY;
        }
    }
}
