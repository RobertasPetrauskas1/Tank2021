using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tank2021SharedContent.Decorator
{
    public class TankImage : UnitImage
    {
        public string baseImage;

        public TankImage()
        {

        }

        public TankImage(string imageFileName)
        {
            baseImage = imageFileName;
        }

        public override Image GetImage()
        {
            return Image.FromFile(@"../../../Properties/Resources/" + baseImage);
        }
    }
}
