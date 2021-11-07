using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tank2021SharedContent.Decorator
{
    public class Danger : TankImageDecorator
    {
        public string dangerImage;

        public Danger()
        {
            dangerImage = "danger.png";
        }

        public Danger(UnitImage newImage) : base(newImage)
        {
            dangerImage = "danger.png";
        }

        public override Image GetImage()
        {
            Image previous_image = base.GetImage();
            return OverlayImages(previous_image, Image.FromFile(@"../../../Properties/Resources/" + dangerImage));
        }
    }
}
