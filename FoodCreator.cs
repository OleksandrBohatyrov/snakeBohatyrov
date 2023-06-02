using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snakeBohatyrov
{
    internal class FoodCreator
    {
        
            int mapWidht;
            int mapHeight;
            char sym;
            private ConsoleColor color;

            Random random = new Random();

            public FoodCreator(int mapWidth, int mapHeight, char sym, ConsoleColor color)
            {
                this.mapWidht = mapWidth;
                this.mapHeight = mapHeight;
                this.sym = sym;
                this.color = color;
            }



            public Point CreateFood()
            {
                
                int x = random.Next(2, mapWidht - 2);
                int y = random.Next(2, mapHeight - 2);
                return new Point(x, y, sym, color);
            }

            public Point CreateFood2()
            {

                int x = random.Next(2, mapWidht - 2);
                int y = random.Next(2, mapHeight - 2);
                return new Point(x, y, sym, color);
            }

            public Point CreateFood3()
            {

                int x = random.Next(2, mapWidht - 2);
                int y = random.Next(2, mapHeight - 2);
                return new Point(x, y, sym, color);
            }


    }
}
