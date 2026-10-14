using System;
using System.Windows.Forms;
using Lab07.Models;

namespace Lab07.Forms
{
    public partial class FrmMain : Form
    {
        private User currentUser;
        public bool IsLoggingOut { get; private set; } = false;

        public FrmMain(User user)
        {
            InitializeComponent();
            currentUser = user;
            DisplayUserInfo();
        }

        private void DisplayUserInfo()
        {
            if (currentUser != null)
            {
                lblUser.Text = $"Người dùng: {currentUser.FullName} ({currentUser.Role})";
            }
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // Kích hoạt Form con nếu đã mở, trả về true nếu đã tồn tại
        private bool CheckFormExists(Type formType)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.GetType() == formType)
                {
                    child.Activate();
                    if (child.WindowState == FormWindowState.Minimized)
                        child.WindowState = FormWindowState.Normal;
                    return true;
                }
            }
            return false;
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            if (!CheckFormExists(typeof(FrmProduct)))
            {
                FrmProduct f = new FrmProduct { MdiParent = this };
                f.Show();
            }
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            if (!CheckFormExists(typeof(FrmCustomer)))
            {
                FrmCustomer f = new FrmCustomer { MdiParent = this };
                f.Show();
            }
        }

        private void mnuGioiThieu_Click(object sender, EventArgs e)
        {
            if (!CheckFormExists(typeof(FrmAbout)))
            {
                FrmAbout f = new FrmAbout { MdiParent = this };
                f.Show();
            }
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            IsLoggingOut = true;
            this.Close();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsLoggingOut)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn thoát chương trình?",
                    "Xác nhận thoát",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void tmrClock_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
