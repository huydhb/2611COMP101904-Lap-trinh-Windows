using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab08.Models
{
    public class DrawItem
    {
        public ShapeMode Mode { get; set; }
        public Color Color { get; set; }
        public float Width { get; set; }
        public bool IsFilled { get; set; }

        public List<Point> Points { get; set; } = new List<Point>();
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, Width))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                switch (Mode)
                {
                    case ShapeMode.Pen:
                        if (Points != null && Points.Count > 1)
                            g.DrawLines(pen, Points.ToArray());
                        break;

                    case ShapeMode.Line:
                        g.DrawLine(pen, StartPoint, EndPoint);
                        break;

                    case ShapeMode.Rectangle:
                        Rectangle rect = GetBoundingRectangle(StartPoint, EndPoint);
                        if (IsFilled)
                        {
                            using (Brush brush = new SolidBrush(Color))
                                g.FillRectangle(brush, rect);
                        }
                        g.DrawRectangle(pen, rect);
                        break;

                    case ShapeMode.Ellipse:
                        Rectangle bounds = GetBoundingRectangle(StartPoint, EndPoint);
                        if (IsFilled)
                        {
                            using (Brush brush = new SolidBrush(Color))
                                g.FillEllipse(brush, bounds);
                        }
                        g.DrawEllipse(pen, bounds);
                        break;
                }
            }
        }

        // Chuẩn hóa tọa độ theo mọi hướng kéo chuột
        public static Rectangle GetBoundingRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y)
            );
        }
    }
}
