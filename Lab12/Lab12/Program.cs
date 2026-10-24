using System;
using System.Windows.Forms;

namespace Lab12
{
    internal static class Program
    {
        /// <summary>
        /// Điểm bắt đầu chính cho ứng dụng WinForms Lab 12.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args != null && args.Length > 0 && args[0].Equals("--capture", StringComparison.OrdinalIgnoreCase))
            {
                ScreenshotRunner.RunCapture();
                return;
            }

            Application.Run(new FrmNhapDiemTheoLop());
        }
    }
}
