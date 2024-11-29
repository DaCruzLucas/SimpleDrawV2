using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDraw
{
    public class Rectangle : Shape
    {

        public override void Draw(Graphics g)
        {
            g.DrawRectangle(linePen, Math.Min(X1, X2), Math.Min(Y1, Y2), Math.Abs(X2 - X1), Math.Abs(Y2 - Y1));
        }
    }
}
