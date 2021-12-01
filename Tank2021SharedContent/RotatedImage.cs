using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tank2021SharedContent
{
    public class RotatedImage
    {
        public Image Image;
        public RotateFlipType RotateFlipType;

        public RotatedImage(Image Image, RotateFlipType RotateFlipType)
        {
            this.Image = Image;
            this.RotateFlipType = RotateFlipType;
        }
    }
}
