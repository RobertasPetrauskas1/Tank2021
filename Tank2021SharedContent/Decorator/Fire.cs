using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tank2021SharedContent.Decorator
{
    public class Fire : TankImageDecorator
    {
        public string fireImage;

        public Fire()
        {
            fireImage = "fire.png";
        }

        public Fire(UnitImage newImage) : base(newImage)
        {
            fireImage = "fire.png";
        }

        public override Image GetImage()
        {
            Image previous_image = base.GetImage();
            return OverlayImages(previous_image, Image.FromFile(@"../../../Properties/Resources/" + fireImage));
        }
    }
}
