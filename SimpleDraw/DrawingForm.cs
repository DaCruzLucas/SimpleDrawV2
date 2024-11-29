using DrawingLibrary;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace SimpleDraw
{
    public partial class DrawingForm : Form
    {
        HubConnection connection;

        List<ToolStripButton> Tools = new List<ToolStripButton>();
        ToolStripButton? SelectedTool;

        List<Shape> Shapes = new List<Shape>();
        Shape? SelectedShape;

        DTOShape? OldDTO;
        DTOShape? CopyDTO;

        bool firstClick = false;

        private int PrevMouseX;
        private int PrevMouseY;

        int selectedPoint = 0;
        int id;

        public Dictionary<string, Point> Mouses = new Dictionary<string, Point>();

        public DrawingForm()
        {
            InitializeComponent();

            FgColorButton.SelectedColor = Color.Black;

            Tools.Add(LineTool);
            Tools.Add(RectangleTool);
            Tools.Add(EllipseTool);
            Tools.Add(PointerTool);

            SelectTool(LineTool);

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/draw")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<List<DTOShape>>("ReceiveLoadShapes", (shapes) =>
            {
                Shapes.Clear();

                SelectedShape = null;

                foreach (var dto in shapes)
                {
                    Shape? shape = null;

                    switch (dto.type)
                    {
                        case DTOShape.ShapeType.Line:
                            shape = new Line();
                            shape.dto = dto;
                            break;

                        case DTOShape.ShapeType.Rect:
                            shape = new Rectangle();
                            shape.dto = dto;
                            break;

                        case DTOShape.ShapeType.Ellipse:
                            shape = new Ellipse();
                            shape.dto = dto;
                            break;
                    }

                    if (shape != null)
                    {
                        Shapes.Add(shape);
                    }
                }

                DrawingPanel.Invalidate();
            });

            connection.On<string>("ReceiveDisconnect", (connectionId) =>
            {
                Mouses.Remove(connectionId);

                DrawingPanel.Invalidate();
            });

            connection.On<DTOShape>("ReceiveAddShape", (dto) =>
            {
                Shape? shape = null;

                switch (dto.type)
                {
                    case DTOShape.ShapeType.Line:
                        shape = new Line();
                        shape.dto = dto;
                        break;

                    case DTOShape.ShapeType.Rect:
                        shape = new Rectangle();
                        shape.dto = dto;
                        break;

                    case DTOShape.ShapeType.Ellipse:
                        shape = new Ellipse();
                        shape.dto = dto;
                        break;
                }

                if (shape != null)
                {
                    Shapes.Add(shape);
                }

                DrawingPanel.Invalidate();
            });

            connection.On<int>("ReceiveRemoveShape", (id) =>
            {
                if (SelectedShape != null && SelectedShape.Id == id)
                {
                    SelectedShape = null;
                }

                Shapes.RemoveAll(s => s.Id == id);

                DrawingPanel.Invalidate();
            });

            connection.On<DTOShape>("ReceiveModifyShape", (dto) =>
            {
                Shape? shape = Shapes.Find(s => s.Id == dto.Id);

                if (shape != null)
                {
                    if (shape == SelectedShape)
                    {
                        FgColorButton.SelectedColor = Color.FromArgb(dto.LineColor);
                    }

                    shape.dto = dto;
                }

                DrawingPanel.Invalidate();
            });

            connection.On<string, int, int>("ReceiveUpdateMousePos", (connectionId, x, y) =>
            {
                if (Mouses.ContainsKey(connectionId))
                {
                    Mouses[connectionId] = new Point(x, y);
                }
                else
                {
                    Mouses.Add(connectionId, new Point(x, y));
                }

                DrawingPanel.Invalidate();
            });

            connection.StartAsync();
        }

        void SelectTool(ToolStripItem? tool)
        {
            foreach (var t in Tools)
            {
                if (t.Checked = t == tool)
                {
                    SelectedTool = t;
                }
            }

            if (SelectedTool != null)
            {
                SelectedTool.Checked = true;
            }
        }

        private Shape? SelectShape(int x, int y)
        {
            for (int i = Shapes.Count - 1; i >= 0; i--)
            {
                if (Shapes[i].IsHit(x, y))
                {
                    return Shapes[i];
                }
            }

            return null;
        }

        private async void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (connection.State == HubConnectionState.Connected && e.Button == MouseButtons.Left)
                {
                    if (SelectedTool == PointerTool)
                    {
                        OldDTO = SelectedShape?.dto;

                        if (SelectedShape != null && SelectedShape.IsHandleHit(e.X, e.Y, SelectedShape.X1, SelectedShape.Y1))
                        {
                            selectedPoint = 1;
                        }
                        else if (SelectedShape != null && SelectedShape.IsHandleHit(e.X, e.Y, SelectedShape.X2, SelectedShape.Y2))
                        {
                            selectedPoint = 2;
                        }
                        else if (SelectedShape != null && SelectedShape.IsHandleHit(e.X, e.Y, SelectedShape.X2, SelectedShape.Y1))
                        {
                            selectedPoint = 3;
                        }
                        else if (SelectedShape != null && SelectedShape.IsHandleHit(e.X, e.Y, SelectedShape.X1, SelectedShape.Y2))
                        {
                            selectedPoint = 4;
                        }
                        else
                        {
                            SelectedShape = SelectShape(e.X, e.Y);

                            if (SelectedShape != null)
                            {
                                selectedPoint = 0;
                                FgColorButton.SelectedColor = SelectedShape.LineColor;
                            }
                            else
                            {
                                selectedPoint = -1;
                            }

                            PrevMouseX = e.X;
                            PrevMouseY = e.Y;
                        }
                    }
                    else
                    {
                        id = await connection.InvokeAsync<int>("GetNextId");

                        if (SelectedTool == LineTool)
                        {
                            SelectedShape = new Line();
                            SelectedShape.Type = DTOShape.ShapeType.Line;
                        }
                        else if (SelectedTool == RectangleTool)
                        {
                            SelectedShape = new Rectangle();
                            SelectedShape.Type = DTOShape.ShapeType.Rect;
                        }
                        else if (SelectedTool == EllipseTool)
                        {
                            SelectedShape = new Ellipse();
                            SelectedShape.Type = DTOShape.ShapeType.Ellipse;
                        }

                        if (SelectedShape != null)
                        {
                            SelectedShape.Id = id;

                            SelectedShape.X1 = SelectedShape.X2 = e.X;
                            SelectedShape.Y1 = SelectedShape.Y2 = e.Y;
                            SelectedShape.LineColor = FgColorButton.SelectedColor;

                            await connection.SendAsync("AddShape", SelectedShape.dto);
                        }
                    }

                    DrawingPanel.Invalidate();
                }
            }
            catch
            {

            }
        }

        private async void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (connection.State == HubConnectionState.Connected && e.Button == MouseButtons.Left)
            {
                if (SelectedTool == PointerTool)
                {
                    if (SelectedShape != null)
                    {
                        if (selectedPoint != -1)
                        {
                            await connection.SendAsync("ModifyShapeOnRelease", SelectedShape.dto, OldDTO);
                        }
                    }
                }
                else
                {
                    if (SelectedShape != null)
                    {
                        if (SelectedShape.X1 == SelectedShape.X2 && SelectedShape.Y1 == SelectedShape.Y2)
                        {
                            await connection.SendAsync("RemoveShapeOnRelease", SelectedShape.dto);
                            SelectedShape = null;
                        }
                        else
                        {
                            await connection.SendAsync("AddShapeOnRelease", SelectedShape.dto);
                        }
                    }
                }
            }
        }

        private async void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (connection.State == HubConnectionState.Connected)
            {
                await connection.SendAsync("UpdateMousePos", e.X, e.Y);
            }

            if (connection.State == HubConnectionState.Connected && e.Button == MouseButtons.Left)
            {
                if (SelectedTool == PointerTool)
                {
                    if (SelectedShape != null)
                    {
                        if (selectedPoint == 1)
                        {
                            SelectedShape.X1 = e.X;
                            SelectedShape.Y1 = e.Y;
                        }
                        else if (selectedPoint == 2)
                        {
                            SelectedShape.X2 = e.X;
                            SelectedShape.Y2 = e.Y;
                        }
                        else if (selectedPoint == 3)
                        {
                            SelectedShape.X2 = e.X;
                            SelectedShape.Y1 = e.Y;
                        }
                        else if (selectedPoint == 4)
                        {
                            SelectedShape.X1 = e.X;
                            SelectedShape.Y2 = e.Y;
                        }
                        else
                        {
                            int dx = e.X - PrevMouseX;
                            int dy = e.Y - PrevMouseY;

                            SelectedShape.X1 += dx;
                            SelectedShape.Y1 += dy;
                            SelectedShape.X2 += dx;
                            SelectedShape.Y2 += dy;

                            PrevMouseX = e.X;
                            PrevMouseY = e.Y;
                        }
                    }
                }
                else if (SelectedShape != null)
                {
                    SelectedShape.X2 = e.X;
                    SelectedShape.Y2 = e.Y;
                }

                if (SelectedShape != null)
                {
                    await connection.SendAsync("ModifyShape", SelectedShape.dto);
                }

                DrawingPanel.Invalidate();
            }
        }

        private async void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (SelectedShape != null && SelectedShape.LineColor != FgColorButton.SelectedColor)
            {
                SelectedShape.LineColor = FgColorButton.SelectedColor;
                await connection.SendAsync("ModifyShape", SelectedShape.dto);
            }

            for (int i = 0; i < Shapes.Count; i++)
            {
                Shapes[i].Draw(e.Graphics);
            }

            SelectedShape?.DrawHandles(e.Graphics);

            var MousesCopy = Mouses;

            foreach (var mouse in MousesCopy)
            {
                if (mouse.Key != connection.ConnectionId)
                {
                    Image resizedImage = new Bitmap(Properties.Resources.cursor, new Size(16, 16));
                    e.Graphics.DrawImage(resizedImage, mouse.Value.X - 4, mouse.Value.Y - 2);
                    e.Graphics.DrawString(Environment.UserName, Font, Brushes.Black, mouse.Value.X + 10, mouse.Value.Y - 5);
                }
            }
        }

        private void DrawingToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SelectTool(e.ClickedItem);
        }

        private async void DrawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SelectedShape = null;
                DrawingPanel.Invalidate();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (SelectedShape != null)
                {
                    await connection.SendAsync("RemoveShape", SelectedShape.dto);
                    SelectedShape = null;
                    DrawingPanel.Invalidate();
                }
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.Z)
                {
                    await connection.SendAsync("UndoAction");
                }
                else if (e.KeyCode == Keys.Y)
                {
                    await connection.SendAsync("RedoAction");
                }
                else if (e.KeyCode == Keys.C)
                {
                    if (SelectedShape != null)
                    {
                        CopyDTO = SelectedShape.dto;
                    }                    
                }
                else if (e.KeyCode == Keys.V)
                {
                    if (CopyDTO != null)
                    {
                        CopyDTO.Id = await connection.InvokeAsync<int>("GetNextId");
                        CopyDTO.X1 += 10;
                        CopyDTO.Y1 += 10;
                        CopyDTO.X2 += 10;
                        CopyDTO.Y2 += 10;
                        await connection.SendAsync("PasteShape", CopyDTO);
                    }
                }
            }
        }

        private async void Save_Click(object sender, EventArgs e)
        {
            await connection.SendAsync("SaveData");
        }

        private async void Load_Click(object sender, EventArgs e)
        {
            await connection.SendAsync("LoadData");
        }
    }
}