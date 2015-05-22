using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockDestroyer;

namespace BlockDestroyer
{
    class Ball : GameObject
    {
        public Ball(int xPosition, int yPosition, bool exists, ConsoleColor color = ConsoleColor.White) : base(xPosition, yPosition, exists, color)
        {

        }
    }
}
