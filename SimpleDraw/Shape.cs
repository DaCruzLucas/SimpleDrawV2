using DrawingLibrary;

namespace SimpleDraw
{
    public abstract class Shape
    {
        protected Pen linePen = new Pen(Color.Black);
        private DTOShape _dto = new DTOShape();
        private const int sizeHandle = 5;
        private const int margeHandle = (sizeHandle - 1) / 2;

        public DTOShape dto
        {
            get
            {
                return _dto;
            }
            set
            {
                linePen = new Pen(Color.FromArgb(value.LineColor));
                _dto = value;
            }
        }

        public DTOShape.ShapeType Type
        {
            get
            {
                return dto.type;
            }
            set
            {
                dto.type = value;
            }
        }

        public int Id
        {
            get
            {
                return dto.Id;
            }
            set
            {
                dto.Id = value;
            }
        }

        public int X1
        {
            get
            {
                return dto.X1;
            }
            set
            {
                dto.X1 = value;
            }
        }

        public int Y1
        {
            get
            {
                return dto.Y1;
            }
            set
            {
                dto.Y1 = value;
            }
        }

        public int X2
        {
            get
            {
                return dto.X2;
            }
            set
            {
                dto.X2 = value;
            }
        }

        public int Y2
        {
            get
            {
                return dto.Y2;
            }
            set
            {
                dto.Y2 = value;
            }
        }

        public Color LineColor 
        { 
            get
            {
                return Color.FromArgb(dto.LineColor);
            }
            set
            {
                linePen = new Pen(value);
                dto.LineColor = value.ToArgb();
            }
        }

        public void SetDTO(DTOShape dtoshape)
        {
            linePen = new Pen(Color.FromArgb(dtoshape.LineColor));
            dto = dtoshape;
        }

        public DTOShape GetDTO()
        {
            return dto;
        }

        public void SetID(int id)
        {
            dto.Id = id;
        }

        public int GetId()
        {
            if (dto == null)
            {
                return -1;
            }
            else
            {
                return dto.Id;
            }
        }

        public virtual void Draw(Graphics g) { }

        private void DrawHandle(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.White, x - sizeHandle / 2, y - sizeHandle / 2, sizeHandle, sizeHandle);
            g.DrawRectangle(Pens.Black, x - sizeHandle / 2, y - sizeHandle / 2, sizeHandle, sizeHandle);
        }

        public void DrawHandles(Graphics g)
        {
            DrawHandle(g, X1, Y1);
            DrawHandle(g, X2, Y2);

            if (GetType() != typeof(Line))
            {
                DrawHandle(g, X2, Y1);
                DrawHandle(g, X1, Y2);
            }
        }

        public bool IsHit(int x, int y)
        {
            return x >= Math.Min(X1, X2) && x <= Math.Max(X1, X2) && y >= Math.Min(Y1, Y2) && y <= Math.Max(Y1, Y2);
        }
        
        public bool IsHandleHit(int mouseX, int mouseY, int x, int y)
        {
            if (mouseX >= x - sizeHandle && mouseX <= x + sizeHandle && mouseY >= y - sizeHandle && mouseY <= y + sizeHandle)
            {
                return true;
            }

            return false;
        }
    }
}
