namespace Lab07.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGioiThieu = new System.Windows.Forms.ToolStripMenuItem();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.msMain.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuDanhMuc,
            this.mnuTroGiup});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(984, 25);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDangXuat,
            this.toolStripSeparator1,
            this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(74, 21);
            this.mnuHeThong.Text = "&Hệ thống";
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(136, 22);
            this.mnuDangXuat.Text = "Đăng &xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(136, 22);
            this.mnuThoat.Text = "&Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSanPham,
            this.mnuKhachHang});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(78, 21);
            this.mnuDanhMuc.Text = "&Danh mục";
            // 
            // mnuSanPham
            // 
            this.mnuSanPham.Name = "mnuSanPham";
            this.mnuSanPham.Size = new System.Drawing.Size(193, 22);
            this.mnuSanPham.Text = "Quản lý &sản phẩm";
            this.mnuSanPham.Click += new System.EventHandler(this.mnuSanPham_Click);
            // 
            // mnuKhachHang
            // 
            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Size = new System.Drawing.Size(193, 22);
            this.mnuKhachHang.Text = "Quản lý &khách hàng";
            this.mnuKhachHang.Click += new System.EventHandler(this.mnuKhachHang_Click);
            // 
            // mnuTroGiup
            // 
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGioiThieu});
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(71, 21);
            this.mnuTroGiup.Text = "Trợ &giúp";
            // 
            // mnuGioiThieu
            // 
            this.mnuGioiThieu.Name = "mnuGioiThieu";
            this.mnuGioiThieu.Size = new System.Drawing.Size(132, 22);
            this.mnuGioiThieu.Text = "&Giới thiệu";
            this.mnuGioiThieu.Click += new System.EventHandler(this.mnuGioiThieu_Click);
            // 
            // ssMain
            // 
            this.ssMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUser,
            this.lblSpacer,
            this.lblTime});
            this.ssMain.Location = new System.Drawing.Point(0, 589);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(984, 22);
            this.ssMain.TabIndex = 2;
            this.ssMain.Text = "statusStrip1";
            // 
            // lblUser
            // 
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(130, 17);
            this.lblUser.Text = "Người dùng: Chưa rõ";
            // 
            // lblSpacer
            // 
            this.lblSpacer.Name = "lblSpacer";
            this.lblSpacer.Size = new System.Drawing.Size(713, 17);
            this.lblSpacer.Spring = true;
            // 
            // lblTime
            // 
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(126, 17);
            this.lblTime.Text = "00/00/0000 00:00:00";
            // 
            // tmrClock
            // 
            this.tmrClock.Enabled = true;
            this.tmrClock.Interval = 1000;
            this.tmrClock.Tick += new System.EventHandler(this.tmrClock_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShopManagerMini - Hệ thống quản lý cửa hàng mini";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuGioiThieu;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblSpacer;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.Timer tmrClock;
    }
}
