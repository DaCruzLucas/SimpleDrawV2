namespace SimpleDraw
{
    partial class DrawingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingForm));
            DrawingPanel = new PictureBox();
            ToolStripContainer = new ToolStripContainer();
            DrawingToolStrip = new ToolStrip();
            PointerTool = new ToolStripButton();
            LineTool = new ToolStripButton();
            RectangleTool = new ToolStripButton();
            EllipseTool = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            FgColorButton = new ToolStripColorButton();
            toolStripSeparator2 = new ToolStripSeparator();
            Save = new ToolStripButton();
            Load = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)DrawingPanel).BeginInit();
            ToolStripContainer.ContentPanel.SuspendLayout();
            ToolStripContainer.TopToolStripPanel.SuspendLayout();
            ToolStripContainer.SuspendLayout();
            DrawingToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // DrawingPanel
            // 
            DrawingPanel.Dock = DockStyle.Fill;
            DrawingPanel.Location = new Point(0, 0);
            DrawingPanel.Name = "DrawingPanel";
            DrawingPanel.Size = new Size(800, 425);
            DrawingPanel.TabIndex = 1;
            DrawingPanel.TabStop = false;
            DrawingPanel.Paint += DrawingPanel_Paint;
            DrawingPanel.MouseDown += DrawingPanel_MouseDown;
            DrawingPanel.MouseMove += DrawingPanel_MouseMove;
            DrawingPanel.MouseUp += DrawingPanel_MouseUp;
            // 
            // ToolStripContainer
            // 
            // 
            // ToolStripContainer.ContentPanel
            // 
            ToolStripContainer.ContentPanel.AutoScroll = true;
            ToolStripContainer.ContentPanel.Controls.Add(DrawingPanel);
            ToolStripContainer.ContentPanel.Size = new Size(800, 425);
            ToolStripContainer.Dock = DockStyle.Fill;
            ToolStripContainer.Location = new Point(0, 0);
            ToolStripContainer.Name = "ToolStripContainer";
            ToolStripContainer.Size = new Size(800, 450);
            ToolStripContainer.TabIndex = 2;
            ToolStripContainer.Text = "toolStripContainer1";
            // 
            // ToolStripContainer.TopToolStripPanel
            // 
            ToolStripContainer.TopToolStripPanel.Controls.Add(DrawingToolStrip);
            // 
            // DrawingToolStrip
            // 
            DrawingToolStrip.Dock = DockStyle.None;
            DrawingToolStrip.Items.AddRange(new ToolStripItem[] { PointerTool, LineTool, RectangleTool, EllipseTool, toolStripSeparator1, FgColorButton, toolStripSeparator2, Save, Load });
            DrawingToolStrip.Location = new Point(3, 0);
            DrawingToolStrip.Name = "DrawingToolStrip";
            DrawingToolStrip.Size = new Size(185, 25);
            DrawingToolStrip.TabIndex = 0;
            DrawingToolStrip.Text = "Drawing Tools";
            DrawingToolStrip.ItemClicked += DrawingToolStrip_ItemClicked;
            // 
            // PointerTool
            // 
            PointerTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            PointerTool.Image = Properties.Resources.Pointer;
            PointerTool.ImageTransparentColor = Color.Magenta;
            PointerTool.Name = "PointerTool";
            PointerTool.Size = new Size(23, 22);
            PointerTool.Text = "Pointer";
            // 
            // LineTool
            // 
            LineTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            LineTool.Image = (Image)resources.GetObject("LineTool.Image");
            LineTool.ImageTransparentColor = Color.Magenta;
            LineTool.Name = "LineTool";
            LineTool.Size = new Size(23, 22);
            LineTool.Text = "Line";
            LineTool.ToolTipText = "Line Tool";
            // 
            // RectangleTool
            // 
            RectangleTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            RectangleTool.Image = (Image)resources.GetObject("RectangleTool.Image");
            RectangleTool.ImageTransparentColor = Color.Magenta;
            RectangleTool.Name = "RectangleTool";
            RectangleTool.Size = new Size(23, 22);
            RectangleTool.Text = "Rectangle";
            RectangleTool.ToolTipText = "Rectangle Tool";
            // 
            // EllipseTool
            // 
            EllipseTool.DisplayStyle = ToolStripItemDisplayStyle.Image;
            EllipseTool.Image = (Image)resources.GetObject("EllipseTool.Image");
            EllipseTool.ImageTransparentColor = Color.Magenta;
            EllipseTool.Name = "EllipseTool";
            EllipseTool.Size = new Size(23, 22);
            EllipseTool.Text = "Ellipse";
            EllipseTool.ToolTipText = "Ellipse Tool";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // FgColorButton
            // 
            FgColorButton.ColorRectangle = new System.Drawing.Rectangle(0, 13, 16, 3);
            FgColorButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            FgColorButton.Image = (Image)resources.GetObject("FgColorButton.Image");
            FgColorButton.ImageTransparentColor = Color.Magenta;
            FgColorButton.Name = "FgColorButton";
            FgColorButton.SelectedColor = Color.Empty;
            FgColorButton.Size = new Size(23, 22);
            FgColorButton.Text = "FgColorButton";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // Save
            // 
            Save.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Save.Image = (Image)resources.GetObject("Save.Image");
            Save.ImageTransparentColor = Color.Magenta;
            Save.Name = "Save";
            Save.Size = new Size(23, 22);
            Save.Text = "Save";
            Save.ToolTipText = "Save";
            Save.Click += Save_Click;
            // 
            // Load
            // 
            Load.DisplayStyle = ToolStripItemDisplayStyle.Image;
            Load.Image = (Image)resources.GetObject("Load.Image");
            Load.ImageTransparentColor = Color.Magenta;
            Load.Name = "Load";
            Load.Size = new Size(23, 22);
            Load.Text = "Load";
            Load.Click += Load_Click;
            // 
            // DrawingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ToolStripContainer);
            KeyPreview = true;
            Name = "DrawingForm";
            Text = "Simple Draw";
            KeyDown += DrawingForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)DrawingPanel).EndInit();
            ToolStripContainer.ContentPanel.ResumeLayout(false);
            ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            ToolStripContainer.TopToolStripPanel.PerformLayout();
            ToolStripContainer.ResumeLayout(false);
            ToolStripContainer.PerformLayout();
            DrawingToolStrip.ResumeLayout(false);
            DrawingToolStrip.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox DrawingPanel;
        private ToolStripContainer ToolStripContainer;
        private ToolStrip DrawingToolStrip;
        private ToolStripButton LineTool;
        private ToolStripButton RectangleTool;
        private ToolStripButton EllipseTool;
        private ToolStripSeparator toolStripSeparator1;
        public ToolStripButton PointerTool;
        public ToolStripColorButton FgColorButton;
        public ToolStripButton SaveBtn;
        public ToolStripButton LoadBtn;
        public new ToolStripButton Load;
        public ToolStripButton Save;
        private ToolStripSeparator toolStripSeparator2;
    }
}