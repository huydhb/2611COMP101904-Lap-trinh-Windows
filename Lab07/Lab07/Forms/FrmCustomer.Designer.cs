namespace Lab07.Forms
{
    partial class FrmCustomer
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
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.cboLoaiKH = new System.Windows.Forms.ComboBox();
            this.lblLoaiKH = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dgvCustomer = new System.Windows.Forms.DataGridView();
            this.grpInfo.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.btnLamMoi);
            this.grpInfo.Controls.Add(this.btnXoa);
            this.grpInfo.Controls.Add(this.btnSua);
            this.grpInfo.Controls.Add(this.btnThem);
            this.grpInfo.Controls.Add(this.cboLoaiKH);
            this.grpInfo.Controls.Add(this.lblLoaiKH);
            this.grpInfo.Controls.Add(this.txtSoDienThoai);
            this.grpInfo.Controls.Add(this.lblSoDienThoai);
            this.grpInfo.Controls.Add(this.txtHoTen);
            this.grpInfo.Controls.Add(this.lblHoTen);
            this.grpInfo.Controls.Add(this.txtMaKH);
            this.grpInfo.Controls.Add(this.lblMaKH);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpInfo.Location = new System.Drawing.Point(10, 10);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(814, 150);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Thông tin khách hàng";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(420, 108);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(85, 30);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(320, 108);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(85, 30);
            this.btnXoa.TabIndex = 10;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(220, 108);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(85, 30);
            this.btnSua.TabIndex = 9;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(120, 108);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(85, 30);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // cboLoaiKH
            // 
            this.cboLoaiKH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiKH.FormattingEnabled = true;
            this.cboLoaiKH.Location = new System.Drawing.Point(520, 67);
            this.cboLoaiKH.Name = "cboLoaiKH";
            this.cboLoaiKH.Size = new System.Drawing.Size(200, 24);
            this.cboLoaiKH.TabIndex = 7;
            // 
            // lblLoaiKH
            // 
            this.lblLoaiKH.AutoSize = true;
            this.lblLoaiKH.Location = new System.Drawing.Point(410, 70);
            this.lblLoaiKH.Name = "lblLoaiKH";
            this.lblLoaiKH.Size = new System.Drawing.Size(105, 17);
            this.lblLoaiKH.TabIndex = 6;
            this.lblLoaiKH.Text = "Loại khách hàng:";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.Location = new System.Drawing.Point(120, 67);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new System.Drawing.Size(200, 24);
            this.txtSoDienThoai.TabIndex = 5;
            // 
            // lblSoDienThoai
            // 
            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Location = new System.Drawing.Point(20, 70);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Size = new System.Drawing.Size(88, 17);
            this.lblSoDienThoai.TabIndex = 4;
            this.lblSoDienThoai.Text = "Số điện thoại:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(520, 29);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(270, 24);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(410, 32);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(67, 17);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ và tên:";
            // 
            // txtMaKH
            // 
            this.txtMaKH.Location = new System.Drawing.Point(120, 29);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.Size = new System.Drawing.Size(200, 24);
            this.txtMaKH.TabIndex = 1;
            // 
            // lblMaKH
            // 
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Location = new System.Drawing.Point(20, 32);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(100, 17);
            this.lblMaKH.TabIndex = 0;
            this.lblMaKH.Text = "Mã khách hàng:";
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.btnTatCa);
            this.pnlSearch.Controls.Add(this.btnTimKiem);
            this.pnlSearch.Controls.Add(this.txtTimKiem);
            this.pnlSearch.Controls.Add(this.lblTimKiem);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.pnlSearch.Location = new System.Drawing.Point(10, 160);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(814, 45);
            this.pnlSearch.TabIndex = 1;
            // 
            // btnTatCa
            // 
            this.btnTatCa.Location = new System.Drawing.Point(515, 8);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(85, 28);
            this.btnTatCa.TabIndex = 3;
            this.btnTatCa.Text = "Tất cả";
            this.btnTatCa.UseVisualStyleBackColor = true;
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(420, 8);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(85, 28);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(120, 10);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(285, 24);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(20, 14);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(63, 17);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dgvCustomer);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpList.Location = new System.Drawing.Point(10, 205);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(8);
            this.grpList.Size = new System.Drawing.Size(814, 326);
            this.grpList.TabIndex = 2;
            this.grpList.TabStop = false;
            this.grpList.Text = "Danh sách khách hàng";
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.AllowUserToAddRows = false;
            this.dgvCustomer.AllowUserToDeleteRows = false;
            this.dgvCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomer.Location = new System.Drawing.Point(8, 25);
            this.dgvCustomer.MultiSelect = false;
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.ReadOnly = true;
            this.dgvCustomer.RowHeadersVisible = false;
            this.dgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomer.Size = new System.Drawing.Size(798, 293);
            this.dgvCustomer.TabIndex = 0;
            this.dgvCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomer_CellClick);
            // 
            // FrmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 541);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.grpInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmCustomer";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý khách hàng";
            this.Load += new System.EventHandler(this.FrmCustomer_Load);
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblSoDienThoai;
        private System.Windows.Forms.TextBox txtSoDienThoai;
        private System.Windows.Forms.Label lblLoaiKH;
        private System.Windows.Forms.ComboBox cboLoaiKH;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTatCa;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dgvCustomer;
    }
}
