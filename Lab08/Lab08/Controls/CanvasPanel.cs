using System.Windows.Forms;

namespace Lab08.Controls
{
    public class CanvasPanel : Panel
    {
        public CanvasPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }
    }
}
