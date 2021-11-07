using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tank2021SharedContent.Decorator
{
    public class ForceField : TankImageDecorator
    {
        public string smokeImage;

        public ForceField()
        {
            smokeImage = "forcefield.png";
        }

        public ForceField(UnitImage newImage) : base(newImage)
        {
            smokeImage = "forcefield.png";
        }

        public override Image GetImage()
        {
            Image previous_image = base.GetImage();
            return OverlayImages(previous_image, Image.FromFile(@"../../../Properties/Resources/" + smokeImage));
        }
    }
}
