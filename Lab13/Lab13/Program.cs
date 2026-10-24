using System;
using System.Windows.Forms;

namespace Lab13
{
    internal static class Program
    {
        /// <summary>
        /// Điểm bắt đầu chính cho ứng dụng WinForms Lab 13.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmBaoCaoSinhVien());
        }
    }
}
