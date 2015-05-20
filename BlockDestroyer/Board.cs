using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockDestroyer
{
    class Board
    {
        public Board(sbyte xpos, byte width)
        {
            Xpos = xpos;
            Width = width;
        }

        public sbyte Xpos { get; set; }
        public byte Width { get; set; }
    }
}
