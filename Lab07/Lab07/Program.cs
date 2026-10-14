using System;
using System.Windows.Forms;
using Lab07.Forms;

namespace Lab07
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                using (FrmLogin login = new FrmLogin())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }

                    using (FrmMain main = new FrmMain(login.LoggedInUser))
                    {
                        Application.Run(main);
                        if (!main.IsLoggingOut)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
