using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace snakeBohatyrov
{
    internal class HorizontalLine : Figure
    {
 

        public HorizontalLine(int xLeft, int xRight, int y, char sym)
        {
            pList = new List<Point>();

            for (int x = xLeft; x <= xRight; x++)
            {
                Point p = new Point(x, y, sym, ConsoleColor.Green);
                pList.Add(p);
            }
        }
    }
}
