namespace Lab07.Forms
{
    partial class FrmProduct
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
            this.lblMaSP = new System.Windows.Forms.Label();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.lblLoaiSP = new System.Windows.Forms.Label();
            this.cboLoaiSP = new System.Windows.Forms.ComboBox();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.numDonGia = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnTatCa = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.btnLamMoi);
            this.grpInfo.Controls.Add(this.btnXoa);
            this.grpInfo.Controls.Add(this.btnSua);
            this.grpInfo.Controls.Add(this.btnThem);
            this.grpInfo.Controls.Add(this.numSoLuong);
            this.grpInfo.Controls.Add(this.lblSoLuong);
            this.grpInfo.Controls.Add(this.numDonGia);
            this.grpInfo.Controls.Add(this.lblDonGia);
            this.grpInfo.Controls.Add(this.cboLoaiSP);
            this.grpInfo.Controls.Add(this.lblLoaiSP);
            this.grpInfo.Controls.Add(this.txtTenSP);
            this.grpInfo.Controls.Add(this.lblTenSP);
            this.grpInfo.Controls.Add(this.txtMaSP);
            this.grpInfo.Controls.Add(this.lblMaSP);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpInfo.Location = new System.Drawing.Point(10, 10);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(814, 150);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Thông tin sản phẩm";
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Location = new System.Drawing.Point(20, 32);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(90, 17);
            this.lblMaSP.TabIndex = 0;
            this.lblMaSP.Text = "Mã sản phẩm:";
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(120, 29);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Size = new System.Drawing.Size(160, 24);
            this.txtMaSP.TabIndex = 1;
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Location = new System.Drawing.Point(310, 32);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(91, 17);
            this.lblTenSP.TabIndex = 2;
            this.lblTenSP.Text = "Tên sản phẩm:";
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(410, 29);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(380, 24);
            this.txtTenSP.TabIndex = 3;
            // 
            // lblLoaiSP
            // 
            this.lblLoaiSP.AutoSize = true;
            this.lblLoaiSP.Location = new System.Drawing.Point(20, 70);
            this.lblLoaiSP.Name = "lblLoaiSP";
            this.lblLoaiSP.Size = new System.Drawing.Size(95, 17);
            this.lblLoaiSP.TabIndex = 4;
            this.lblLoaiSP.Text = "Loại sản phẩm:";
            // 
            // cboLoaiSP
            // 
            this.cboLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSP.FormattingEnabled = true;
            this.cboLoaiSP.Location = new System.Drawing.Point(120, 67);
            this.cboLoaiSP.Name = "cboLoaiSP";
            this.cboLoaiSP.Size = new System.Drawing.Size(160, 24);
            this.cboLoaiSP.TabIndex = 5;
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(310, 70);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(89, 17);
            this.lblDonGia.TabIndex = 6;
            this.lblDonGia.Text = "Đơn giá (VNĐ):";
            // 
            // numDonGia
            // 
            this.numDonGia.Location = new System.Drawing.Point(410, 68);
            this.numDonGia.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numDonGia.Name = "numDonGia";
            this.numDonGia.Size = new System.Drawing.Size(140, 24);
            this.numDonGia.TabIndex = 7;
            this.numDonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDonGia.ThousandsSeparator = true;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(580, 70);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(64, 17);
            this.lblSoLuong.TabIndex = 8;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(650, 68);
            this.numSoLuong.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(140, 24);
            this.numSoLuong.TabIndex = 9;
            this.numSoLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(120, 108);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(85, 30);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(220, 108);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(85, 30);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(320, 108);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(85, 30);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(420, 108);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(85, 30);
            this.btnLamMoi.TabIndex = 13;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
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
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(20, 14);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(63, 17);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(120, 10);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(285, 24);
            this.txtTimKiem.TabIndex = 1;
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
            // grpList
            // 
            this.grpList.Controls.Add(this.dgvProduct);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.grpList.Location = new System.Drawing.Point(10, 205);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(8);
            this.grpList.Size = new System.Drawing.Size(814, 326);
            this.grpList.TabIndex = 2;
            this.grpList.TabStop = false;
            this.grpList.Text = "Danh sách sản phẩm";
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProduct.Location = new System.Drawing.Point(8, 25);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(798, 293);
            this.dgvProduct.TabIndex = 0;
            this.dgvProduct.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellClick);
            // 
            // FrmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 541);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.grpInfo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmProduct";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý sản phẩm";
            this.Load += new System.EventHandler(this.FrmProduct_Load);
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblLoaiSP;
        private System.Windows.Forms.ComboBox cboLoaiSP;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.NumericUpDown numDonGia;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown numSoLuong;
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
        private System.Windows.Forms.DataGridView dgvProduct;
    }
}
