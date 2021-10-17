using System.Drawing;
using Tank2021SharedContent.Abstract;
using Tank2021SharedContent.Constants;

namespace Tank2021SharedContent
{
    public class Tank
    {
        #region Robkes stuff
        public int Hitpoints { get; set; }
        public Gun Gun { get; set; }
        public Armor Armor { get; set; }
        #endregion

        public Point Coordinates;
        public int Speed;
        public RotateFlipType Rotation;
        public string ImageLocation;

        public Tank(Gun gun, Point coordinates, int speed, RotateFlipType rotation, string imageLocation)
        {
            Gun = gun;
            Coordinates = coordinates;
            Speed = speed;
            Rotation = rotation;
            ImageLocation = imageLocation;
        }

        public void GetHit(int damage)
        {
            int calculatedDamage = damage - Armor.DamageReduction;
            calculatedDamage = calculatedDamage < 0 ? 0 : calculatedDamage;
            
            Hitpoints = Hitpoints - calculatedDamage;
            if(Hitpoints <= 0)
            {
                //TODO: Gameover
            }

            Armor.GetHit();
        }

        public bool IsHittingBorder(int width, int height)
        {
            if (Coordinates.X < 0 || Coordinates.X + width > ClientSideConstants.ClientWidth)
                return true;
            if (Coordinates.Y < 0 || Coordinates.Y + height > ClientSideConstants.ClientHeight)
                return true;

            return false;
        }

        public void MoveDown()
        {
            if(Coordinates.Y + Speed + ClientSideConstants.TankHeight <= ClientSideConstants.ClientHeight)
            {
                Coordinates.Y += Speed;
                Rotation = RotateFlipType.RotateNoneFlipNone;
            }
        }

        public void MoveUp()
        {
            if(Coordinates.Y - Speed >= 0)
            {
                Coordinates.Y -= Speed;
                Rotation = RotateFlipType.RotateNoneFlipY;
            }
        }

        public void MoveRight()
        {
            if(Coordinates.X + Speed + ClientSideConstants.TankHeight <= ClientSideConstants.ClientWidth)
            {
                Coordinates.X += Speed;
                Rotation = RotateFlipType.Rotate90FlipX;
            }
        }

        public void MoveLeft()
        {
            if(Coordinates.X - Speed >= 0)
            {
                Coordinates.X -= Speed;
                Rotation = RotateFlipType.Rotate270FlipX;
            }
        }

        public Bullet Shoot()
        {
            return Gun.Shoot(Coordinates, Rotation);
        }
    }
}
