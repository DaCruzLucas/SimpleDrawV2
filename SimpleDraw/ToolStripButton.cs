namespace SimpleDraw
{
    public class ToolStripColorButton : ToolStripButton
    {
        private Color selectedColor;

        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }

            set
            {
                selectedColor = value;
                UpdateImage();
            }
        }

        public System.Drawing.Rectangle ColorRectangle { get; set; } = new (0, 13, 16, 3);

        private void UpdateImage()
        {
            try
            {
                if (Image != null)
                {
                    Graphics g = Graphics.FromImage(Image);
                    g.FillRectangle(new SolidBrush(selectedColor), ColorRectangle);
                    Invalidate();
                }
            }
            catch
            {

            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            ColorDialog dlg = new ColorDialog();
            
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = dlg.Color;
                Form.ActiveForm?.Refresh();
            }
        }
    }
}
