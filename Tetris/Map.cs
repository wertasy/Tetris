using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    public class Map
    {
        public struct cell
        {
            public int p;
            public Color c;
        };
        public cell[,] Mod;
        public int Height;
        public int Width;
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Mod = new cell[height + 8, width + 8];
            for (int i = 0; i < height + 8; i++)
            {
                for (int j = 0; j < width + 8; j++)
                {
                    if (j < 4 || j > width + 3 || i > height + 3)
                    {
                        Mod[i, j].p = 1;
                    }
                }
            }
        }
    }
}
