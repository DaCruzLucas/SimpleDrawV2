using System.Drawing;

namespace DrawingLibrary
{
    public class DTOShape
    {
        public enum ShapeType { None, Line, Rect, Ellipse}

        public ShapeType type { get; set; }

        public int Id { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public int LineColor { get; set; } = Color.Black.ToArgb();

        public DTOShape()
        {
            
        }
    }
}
