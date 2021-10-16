using System;
using System.Drawing;

namespace Tank2021SharedContent
{
    public class Figure
    {
        public Point Coordinates { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public RotateFlipType Rotation { get; set; }
        public Image Sprite { get; set; }

        public Figure(Point coordinates, int width, int height, RotateFlipType rotation, Image sprite)
        {
            Coordinates = coordinates;
            Width = width;
            Height = height;
            Rotation = rotation;
            Sprite = sprite;
        }

        public void Draw(Graphics graphics)
        {
            //Rotate
            Sprite.RotateFlip(Rotation);

            var width = Sprite.Width;
            var height = Sprite.Height;
            var bitMap = new Bitmap(width, height);

            using (var bitMapGraphics = Graphics.FromImage(bitMap))
            {
                bitMapGraphics.DrawImage(Sprite, 0, 0, width, height);
            }

            graphics.DrawImage(bitMap, Coordinates);
        }
    }
}
