using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tank2021SharedContent.Decorator
{
    public abstract class TankImageDecorator : UnitImage
    {
        public UnitImage tempTank;

        public TankImageDecorator()
        {

        }
        public TankImageDecorator(UnitImage newImage)
        {
            tempTank = newImage;
        }

        public override Image GetImage()
        {
            return tempTank.GetImage();
        }

        protected Image OverlayImages(Image img1, Image img2)
        {
            Image clone = (Image)img1.Clone();
            using (Graphics g = Graphics.FromImage(clone))
            {
                g.DrawImage(img2, 0, 0, clone.Width, clone.Height);
            }

            return clone;
        }
    }
}
