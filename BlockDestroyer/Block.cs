using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockDestroyer
{
    class Block
    {
        public int Xposition { get; set; }
        public int Yposition { get; set; }
        public List<ConsolePoint> AbsolutXyPoints { get; }

        public Block(int xposition, int yposition)
        {
            Xposition = xposition;
            Yposition = yposition;
            AbsolutXyPoints = new List<ConsolePoint>();

            int row = 10 + (yposition * 2);
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    AbsolutXyPoints.Add(new ConsolePoint(xposition * 5, row));
                    continue;
                }
                AbsolutXyPoints.Add(new ConsolePoint(AbsolutXyPoints[0].x + i, row));
            }
        }
    }


    class ConsolePoint
    {
        public int x;
        public int y;

        public ConsolePoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}