using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDraw
{
    public class Line : Shape
    {

        public override void Draw(Graphics g)
        {
            g.DrawLine(linePen, X1, Y1, X2, Y2);
        }
    }
}
