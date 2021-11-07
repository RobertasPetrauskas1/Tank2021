using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tank2021SharedContent.Decorator
{
    public class Smoke : TankImageDecorator
    {
        public string smokeImage;

        public Smoke()
        {
            smokeImage = "smoke.png";
        }

        public Smoke(UnitImage newImage) : base(newImage)
        {
            smokeImage = "smoke.png";
        }

        public override Image GetImage()
        {
            Image previous_image = base.GetImage();
            return OverlayImages(previous_image, Image.FromFile(@"../../../Properties/Resources/" + smokeImage));
        }
    }
}
