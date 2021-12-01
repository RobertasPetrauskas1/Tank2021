using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank2021SharedContent;

namespace Tank2021Client.Flyweight
{
    public class ImageFactory
    {
        public struct Tuple<T1, T2>
        {
            public readonly T1 Item1;
            public readonly T2 Item2;
            public Tuple(T1 item1, T2 item2) { Item1 = item1; Item2 = item2; }
        }

        public static class Tuple
        { // for type-inference goodness.
            public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
            {
                return new Tuple<T1, T2>(item1, item2);
            }
        }

        private IDictionary<Tuple<ImageType, RotateFlipType>, RotatedImage> images = new Dictionary<Tuple<ImageType, RotateFlipType>, RotatedImage>();

        public RotatedImage GetImage(ImageType imageType, RotateFlipType rotateFlipType)
        {
            var key = new Tuple<ImageType, RotateFlipType>(imageType, rotateFlipType);
            RotatedImage rotatedImage;
            if(!images.TryGetValue(key, out rotatedImage))
            { 
                Image img;
                switch (imageType)
                {
                    case ImageType.Bullet:
                        img = Image.FromFile(@"../../../Properties/Resources/small_bullet.png");
                        break;
                    default:
                        throw new Exception("Missing implementation");
                }
                img.RotateFlip(rotateFlipType);
                rotatedImage = new RotatedImage(img, rotateFlipType);

                images.Add(key, rotatedImage);
            }

            return rotatedImage;
        }
    }
}
