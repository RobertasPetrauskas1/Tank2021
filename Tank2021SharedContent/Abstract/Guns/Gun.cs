using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tank2021SharedContent.Constants;
using Tank2021SharedContent.Enums;
using Tank2021SharedContent.Template_Method;

namespace Tank2021SharedContent.Abstract.Guns
{
    public abstract class Gun : IUnit, ICollection<Bullet>
    {
        public DamageAlgorithmTemplate DamageAlgorithm { get; set; }
        public abstract TimeSpan Cooldown { get; set; }
        public abstract int Damage { get; set; }
        public abstract int Speed { get; set; }

        public int Count => Bullets.Count;
        public bool IsReadOnly => false;

        public List<Bullet> Bullets = new List<Bullet>();
        public DateTime NextShootTime = DateTime.Now;

        public virtual void Shoot(Point currentCoordinates, RotateFlipType flipType)
        {
            if(DateTime.Now >= NextShootTime)
            {
                Direction direction;
                switch (flipType)
                {
                    case RotateFlipType.RotateNoneFlipY:
                        direction = Direction.Up;
                        break;
                    case RotateFlipType.RotateNoneFlipNone:
                        direction = Direction.Down;
                        break;
                    case RotateFlipType.Rotate90FlipX:
                        direction = Direction.Right;
                        break;
                    case RotateFlipType.Rotate270FlipX:
                        direction = Direction.Left;
                        break;
                    default:
                        throw new Exception("Unknown RotateFlipType");
                }

                Bullets.Add(new Bullet(DamageAlgorithm.CalculateDamage(Damage), direction, currentCoordinates, Speed, flipType));
                NextShootTime = DateTime.Now.Add(Cooldown);
            }
        }

        public Gun Copy()
        {
            return (Gun)this.MemberwiseClone();
        }

        public IEnumerator<Bullet> GetEnumerator()
        {
            return Bullets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Bullet item)
        {
            Bullets.Add(item);
        }

        public void Clear()
        {
            Bullets.Clear();
        }

        public bool Contains(Bullet item)
        {
            return Bullets.Contains(item);
        }

        public void CopyTo(Bullet[] array, int arrayIndex)
        {
            Bullets.CopyTo(array, arrayIndex);
        }

        public bool Remove(Bullet item)
        {
            return Bullets.Remove(item);
        }
    }
}
