using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Lab08.Models;

namespace Lab08
{
    public partial class FrmPaint : Form
    {
        private List<DrawItem> items = new List<DrawItem>();
        private DrawItem currentItem = null;
        private bool isDrawing = false;
        private Color currentColor = Color.Black;
        private Image backgroundImage = null;

        public FrmPaint()
        {
            InitializeComponent();
            EnableDoubleBuffered(canvas);
        }

        // Bật bộ đệm đôi chống nhấp nháy triệt để
        private void EnableDoubleBuffered(Control control)
        {
            typeof(Control).GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(control, new object[] {
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.OptimizedDoubleBuffer,
                    true
                });

            typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(control, true, null);
        }

        private void FrmPaint_Load(object sender, EventArgs e)
        {
            cboMode.Items.AddRange(new object[] { ShapeMode.Pen, ShapeMode.Line, ShapeMode.Rectangle, ShapeMode.Ellipse });
            cboMode.SelectedIndex = 0;
            pnlColorPreview.BackColor = currentColor;
            UpdateStatus(0, 0);
        }

        private void UpdateStatus(int x, int y)
        {
            string modeName = cboMode.SelectedItem != null ? cboMode.SelectedItem.ToString() : "Pen";
            lblStatus.Text = $"Tọa độ: X = {x}, Y = {y} | Chế độ: {modeName} | Nét: {nudWidth.Value}px";
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            isDrawing = true;
            ShapeMode mode = (ShapeMode)cboMode.SelectedItem;

            currentItem = new DrawItem
            {
                Mode = mode,
                Color = currentColor,
                Width = (float)nudWidth.Value,
                IsFilled = chkFill.Checked
            };

            if (mode == ShapeMode.Pen)
            {
                currentItem.Points.Add(e.Location);
            }
            else
            {
                currentItem.StartPoint = e.Location;
                currentItem.EndPoint = e.Location;
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateStatus(e.X, e.Y);

            if (isDrawing && currentItem != null)
            {
                if (currentItem.Mode == ShapeMode.Pen)
                {
                    currentItem.Points.Add(e.Location);
                }
                else
                {
                    currentItem.EndPoint = e.Location;
                }
                canvas.Invalidate();
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing && currentItem != null)
            {
                if (currentItem.Mode == ShapeMode.Pen && currentItem.Points.Count > 1 || currentItem.Mode != ShapeMode.Pen)
                {
                    items.Add(currentItem);
                }

                currentItem = null;
                isDrawing = false;
                canvas.Invalidate();
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Xóa nền bằng màu canvas hoặc vẽ ảnh nền
            if (backgroundImage != null)
            {
                e.Graphics.DrawImage(backgroundImage, 0, 0, canvas.Width, canvas.Height);
            }
            else
            {
                e.Graphics.Clear(canvas.BackColor);
            }

            // Vẽ các hình đã lưu trong danh sách
            foreach (DrawItem item in items)
            {
                item.Draw(e.Graphics);
            }

            // Vẽ hình xem trước khi đang kéo chuột
            if (isDrawing && currentItem != null)
            {
                currentItem.Draw(e.Graphics);
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = currentColor;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    currentColor = cd.Color;
                    pnlColorPreview.BackColor = currentColor;
                }
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            PerformUndo();
        }

        private void PerformUndo()
        {
            if (items.Count > 0)
            {
                items.RemoveAt(items.Count - 1);
                canvas.Invalidate();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (items.Count > 0 || backgroundImage != null)
            {
                DialogResult r = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa toàn bộ vùng vẽ?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (r == DialogResult.Yes)
                {
                    items.Clear();
                    backgroundImage?.Dispose();
                    backgroundImage = null;
                    canvas.Invalidate();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image (*.png)|*.png";
                sfd.FileName = "Drawing.png";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bmp = new Bitmap(canvas.Width, canvas.Height))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.Clear(canvas.BackColor);
                            if (backgroundImage != null)
                            {
                                g.DrawImage(backgroundImage, 0, 0, canvas.Width, canvas.Height);
                            }
                            foreach (DrawItem item in items)
                            {
                                item.Draw(g);
                            }
                        }
                        bmp.Save(sfd.FileName, ImageFormat.Png);
                    }
                    MessageBox.Show("Đã lưu hình ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (Image temp = Image.FromFile(ofd.FileName))
                    {
                        backgroundImage?.Dispose();
                        backgroundImage = new Bitmap(temp);
                    }
                    canvas.Invalidate();
                }
            }
        }

        private void btnBgColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = canvas.BackColor;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    canvas.BackColor = cd.Color;
                    canvas.Invalidate();
                }
            }
        }

        private void FrmPaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                PerformUndo();
                e.Handled = true;
            }
        }

        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatus(0, 0);
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateStatus(0, 0);
        }
    }
}
