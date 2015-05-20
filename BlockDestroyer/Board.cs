using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockDestroyer
{
    internal class Board
    {
        public Board(sbyte xPos, byte width, bool direction)
        {
            XPos = xPos;
            Width = width;
            Direction = direction;
        }

        public sbyte XPos { get; set; }
        public byte Width { get; set; }
        public bool Direction { get; set; }
    }
}