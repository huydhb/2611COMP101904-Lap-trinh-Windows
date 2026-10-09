using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab06
{
    public partial class FrmProduct : Form
    {
        private readonly BindingList<Product> _productList = new BindingList<Product>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private bool _isClearingInput = false;

        private readonly string[] _danhMucLoai = new string[]
        {
            "Điện tử",
            "Văn phòng",
            "Gia dụng",
            "Thời trang"
        };

        public FrmProduct()
        {
            InitializeComponent();
        }

        #region Form Lifecycle & Khởi tạo

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            cboLoaiSP.Items.Clear();
            cboLoaiSP.Items.AddRange(_danhMucLoai);
            if (cboLoaiSP.Items.Count > 0)
            {
                cboLoaiSP.SelectedIndex = 0;
            }

            SeedSampleData();

            _bindingSource.DataSource = _productList;
            dgvProducts.DataSource = _bindingSource;

            ClearInput();
            UpdateStatus("Khởi tạo hệ thống thành công. Đã sẵn sàng thao tác.");
        }

        private void SeedSampleData()
        {
            _productList.Add(new Product("SP01", "Laptop ASUS Vivobook 15", "Điện tử", 15490000m, 12, DateTime.Today.AddDays(-20), true));
            _productList.Add(new Product("SP02", "Chuột không dây Logitech M331", "Điện tử", 350000m, 45, DateTime.Today.AddDays(-15), true));
            _productList.Add(new Product("SP03", "Hộp bút bi Thiên Long 20 cây", "Văn phòng", 95000m, 120, DateTime.Today.AddDays(-10), true));
            _productList.Add(new Product("SP04", "Nồi chiên không dầu Philips HD9252", "Gia dụng", 2190000m, 8, DateTime.Today.AddDays(-5), true));
            _productList.Add(new Product("SP05", "Áo thun nam Cotton Compact", "Thời trang", 189000m, 60, DateTime.Today.AddDays(-2), false));
        }

        #endregion

        #region Helper Methods

        private bool ValidateInput(bool isEdit)
        {
            errorProvider1.Clear();
            bool isValid = true;

            string maSP = txtMaSP.Text.Trim();
            if (string.IsNullOrWhiteSpace(maSP))
            {
                errorProvider1.SetError(txtMaSP, "Mã sản phẩm không được để trống!");
                isValid = false;
            }
            else if (!isEdit)
            {
                bool exists = _productList.Any(p => p.MaSP.Equals(maSP, StringComparison.OrdinalIgnoreCase));
                if (exists)
                {
                    errorProvider1.SetError(txtMaSP, $"Mã sản phẩm '{maSP}' đã tồn tại! Vui lòng chọn mã khác.");
                    isValid = false;
                }
            }

            string tenSP = txtTenSP.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenSP))
            {
                errorProvider1.SetError(txtTenSP, "Tên sản phẩm không được để trống!");
                isValid = false;
            }

            if (cboLoaiSP.SelectedIndex < 0 || string.IsNullOrWhiteSpace(cboLoaiSP.Text))
            {
                errorProvider1.SetError(cboLoaiSP, "Vui lòng chọn loại sản phẩm!");
                isValid = false;
            }

            if (numDonGia.Value <= 0)
            {
                errorProvider1.SetError(numDonGia, "Đơn giá phải lớn hơn 0!");
                isValid = false;
            }

            if (numSoLuong.Value < 0)
            {
                errorProvider1.SetError(numSoLuong, "Số lượng phải lớn hơn hoặc bằng 0!");
                isValid = false;
            }

            if (dtpNgayNhap.Value.Date > DateTime.Today)
            {
                errorProvider1.SetError(dtpNgayNhap, "Ngày nhập không được lớn hơn ngày hiện tại!");
                isValid = false;
            }

            return isValid;
        }

        private Product GetSelectedProduct()
        {
            if (dgvProducts.SelectedRows.Count == 0 || dgvProducts.SelectedRows[0].Index < 0)
            {
                return null;
            }

            return dgvProducts.SelectedRows[0].DataBoundItem as Product;
        }

        private void ClearInput()
        {
            _isClearingInput = true; // Tránh kích hoạt SelectionChanged khi reset
            try
            {
                txtMaSP.Clear();
                txtMaSP.ReadOnly = false;
                txtTenSP.Clear();
                if (cboLoaiSP.Items.Count > 0)
                {
                    cboLoaiSP.SelectedIndex = 0;
                }
                numDonGia.Value = 0;
                numSoLuong.Value = 0;
                dtpNgayNhap.Value = DateTime.Today;
                chkConKinhDoanh.Checked = true;

                txtDuongDanAnh.Clear();
                ClearProductImage();

                errorProvider1.Clear();
                dgvProducts.ClearSelection();
                txtMaSP.Focus();
            }
            finally
            {
                _isClearingInput = false;
            }
        }

        private void LoadProductToForm()
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                return;
            }

            errorProvider1.Clear();
            txtMaSP.Text = product.MaSP;
            txtMaSP.ReadOnly = true; // Khóa mã khi chỉnh sửa sản phẩm đã có
            txtTenSP.Text = product.TenSP;

            int loaiIdx = cboLoaiSP.FindStringExact(product.LoaiSP);
            if (loaiIdx >= 0)
            {
                cboLoaiSP.SelectedIndex = loaiIdx;
            }
            else
            {
                cboLoaiSP.Text = product.LoaiSP;
            }

            numDonGia.Value = Math.Min(Math.Max(product.DonGia, numDonGia.Minimum), numDonGia.Maximum);
            numSoLuong.Value = Math.Min(Math.Max(product.SoLuong, numSoLuong.Minimum), numSoLuong.Maximum);
            dtpNgayNhap.Value = product.NgayNhap;
            chkConKinhDoanh.Checked = product.ConKinhDoanh;
            txtDuongDanAnh.Text = product.DuongDanAnh;

            LoadProductImage(product.DuongDanAnh);
        }

        private void LoadProductImage(string path)
        {
            ClearProductImage();

            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                try
                {
                    // Dùng FileStream đọc byte để tránh khóa file trên ổ đĩa
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        picAnh.Image = Image.FromStream(fs);
                    }
                }
                catch
                {
                    picAnh.Image = null;
                }
            }
        }

        private void ClearProductImage()
        {
            if (picAnh.Image != null)
            {
                picAnh.Image.Dispose();
                picAnh.Image = null;
            }
        }

        private void UpdateStatus(string message = "")
        {
            lblStatus.Text = string.IsNullOrWhiteSpace(message) ? "Sẵn sàng" : message;
            lblCount.Text = $"Tổng số sản phẩm: {_productList.Count}";

            decimal tongTienTonKho = _productList.Sum(p => p.ThanhTien);
            lblTongGiaTri.Text = $"Tổng tồn kho: {tongTienTonKho:N0} VNĐ";
        }

        #endregion

        #region Thao tác CRUD

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(isEdit: false))
            {
                UpdateStatus("Dữ liệu nhập chưa hợp lệ! Vui lòng kiểm tra các ô báo lỗi.");

                string errMa = errorProvider1.GetError(txtMaSP);
                if (!string.IsNullOrEmpty(errMa) && errMa.Contains("đã tồn tại"))
                {
                    MessageBox.Show(
                        $"{errMa}\n\n💡 Hướng dẫn:\n• Nhấn [✏️ Sửa] nếu muốn cập nhật thông tin sản phẩm này.\n• Nhấn [🔄 Làm mới] để nhập mã sản phẩm mới.",
                        "Mã sản phẩm đã tồn tại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Dữ liệu nhập chưa hợp lệ! Vui lòng kiểm tra các ô có dấu chấm than đỏ ❗.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            var newProduct = new Product
            {
                MaSP = txtMaSP.Text.Trim(),
                TenSP = txtTenSP.Text.Trim(),
                LoaiSP = cboLoaiSP.Text,
                DonGia = numDonGia.Value,
                SoLuong = (int)numSoLuong.Value,
                NgayNhap = dtpNgayNhap.Value.Date,
                ConKinhDoanh = chkConKinhDoanh.Checked,
                DuongDanAnh = txtDuongDanAnh.Text.Trim()
            };

            _productList.Add(newProduct);
            LamMoiHienThi(newProduct);

            UpdateStatus($"Đã thêm mới thành công sản phẩm: [{newProduct.MaSP}] {newProduct.TenSP}");
            MessageBox.Show($"Đã thêm sản phẩm '{newProduct.TenSP}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearInput();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var current = GetSelectedProduct();
            if (current == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách trước khi nhấn Sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput(isEdit: true))
            {
                UpdateStatus("Dữ liệu chỉnh sửa chưa hợp lệ! Vui lòng kiểm tra lại.");
                MessageBox.Show("Dữ liệu chỉnh sửa chưa hợp lệ! Vui lòng kiểm tra các ô có dấu chấm than đỏ ❗.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            current.TenSP = txtTenSP.Text.Trim();
            current.LoaiSP = cboLoaiSP.Text;
            current.DonGia = numDonGia.Value;
            current.SoLuong = (int)numSoLuong.Value;
            current.NgayNhap = dtpNgayNhap.Value.Date;
            current.ConKinhDoanh = chkConKinhDoanh.Checked;
            current.DuongDanAnh = txtDuongDanAnh.Text.Trim();

            LamMoiHienThi(current);

            UpdateStatus($"Đã cập nhật sản phẩm: [{current.MaSP}] {current.TenSP}");
            MessageBox.Show($"Cập nhật thông tin sản phẩm [{current.MaSP}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var current = GetSelectedProduct();
            if (current == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách trước khi nhấn Xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm:\n\n- Mã: {current.MaSP}\n- Tên: {current.TenSP}\n\nThao tác này không thể hoàn tác!",
                "Xác nhận xóa sản phẩm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirm == DialogResult.Yes)
            {
                string maSP = current.MaSP;
                string tenSP = current.TenSP;

                _productList.Remove(current);
                LamMoiHienThi();

                ClearInput();
                UpdateStatus($"Đã xóa sản phẩm: [{maSP}] {tenSP}");
                MessageBox.Show($"Đã xóa sản phẩm [{maSP}] thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            _bindingSource.DataSource = _productList;
            _bindingSource.ResetBindings(false);
            ClearInput();
            UpdateStatus("Đã làm mới dữ liệu và xóa trắng vùng nhập liệu.");
        }

        #endregion

        #region Tìm kiếm & Bộ lọc

        private void btnToolTimKiem_Click(object sender, EventArgs e)
        {
            ThucHienTimKiem();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThucHienTimKiem();
            }
        }

        // Đồng bộ dữ liệu DataGridView theo từ khóa tìm kiếm (nếu có)
        private void LamMoiHienThi(Product focusItem = null)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                _bindingSource.DataSource = _productList;
                _bindingSource.ResetBindings(false);
                UpdateStatus($"Đang hiển thị toàn bộ {_productList.Count} sản phẩm.");
            }
            else
            {
                var ketQua = _productList.Where(p =>
                    (!string.IsNullOrEmpty(p.MaSP) && p.MaSP.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(p.TenSP) && p.TenSP.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(p.LoaiSP) && p.LoaiSP.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();

                _bindingSource.DataSource = new BindingList<Product>(ketQua);
                UpdateStatus($"Tìm kiếm với từ khóa '{keyword}': Tìm thấy {ketQua.Count} sản phẩm.");
            }

            if (focusItem != null && _bindingSource.Contains(focusItem))
            {
                _bindingSource.Position = _bindingSource.IndexOf(focusItem);
            }
        }

        private void ThucHienTimKiem()
        {
            LamMoiHienThi();
        }

        private void btnToolTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LamMoiHienThi();
        }

        #endregion

        #region Xử lý Hình ảnh

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                txtDuongDanAnh.Text = filePath;
                LoadProductImage(filePath);
                UpdateStatus($"Đã chọn ảnh: {Path.GetFileName(filePath)}");
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            txtDuongDanAnh.Clear();
            ClearProductImage();
            UpdateStatus("Đã xóa ảnh sản phẩm.");
        }

        #endregion

        #region Tương tác DataGridView

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProducts.SelectedRows.Count > 0)
            {
                LoadProductToForm();
            }
        }

        // Đồng bộ dòng chọn khi click chuột phải để thao tác đúng trên ContextMenu
        private void dgvProducts_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvProducts.ClearSelection();
                dgvProducts.Rows[e.RowIndex].Selected = true;
                if (e.ColumnIndex >= 0)
                {
                    dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
                else
                {
                    dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[0];
                }
                LoadProductToForm();
            }
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (_isClearingInput)
            {
                return;
            }

            if (dgvProducts.SelectedRows.Count > 0 && dgvProducts.SelectedRows[0].Index >= 0)
            {
                LoadProductToForm();
            }
        }

        #endregion

        #region ContextMenuStrip

        private void menuCtxChiTiet_Click(object sender, EventArgs e)
        {
            var p = GetSelectedProduct();
            if (p == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string chiTiet = new StringBuilder()
                .AppendLine($"MÃ SẢN PHẨM:    {p.MaSP}")
                .AppendLine($"TÊN SẢN PHẨM:   {p.TenSP}")
                .AppendLine($"LOẠI SẢN PHẨM:  {p.LoaiSP}")
                .AppendLine($"ĐƠN GIÁ:        {p.DonGia:N0} VNĐ")
                .AppendLine($"SỐ LƯỢNG:       {p.SoLuong:N0}")
                .AppendLine($"TỔNG GIÁ TRỊ:   {p.ThanhTien:N0} VNĐ")
                .AppendLine($"NGÀY NHẬP:      {p.NgayNhap:dd/MM/yyyy}")
                .AppendLine($"TRẠNG THÁI:     {(p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh")}")
                .AppendLine($"ĐƯỜNG DẪN ẢNH:  {(string.IsNullOrEmpty(p.DuongDanAnh) ? "(Không có)" : p.DuongDanAnh)}")
                .ToString();

            MessageBox.Show(chiTiet, $"Chi tiết sản phẩm [{p.MaSP}]", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Xuất file, Giới thiệu & Phím tắt

        private void menuItemXuatText_Click(object sender, EventArgs e)
        {
            if (_productList.Count == 0)
            {
                MessageBox.Show("Danh sách sản phẩm đang trống! Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.FileName = $"DanhSachSanPham_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8))
                    {
                        writer.WriteLine("==========================================================================================");
                        writer.WriteLine("                         DANH SÁCH SẢN PHẨM - PRODUCT MANAGER                             ");
                        writer.WriteLine($"Ngày xuất báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                        writer.WriteLine($"Tổng số lượng sản phẩm: {_productList.Count}");
                        writer.WriteLine($"Tổng giá trị tồn kho: {_productList.Sum(p => p.ThanhTien):N0} VNĐ");
                        writer.WriteLine("==========================================================================================");
                        writer.WriteLine(string.Format("{0,-8} | {1,-32} | {2,-12} | {3,14} | {4,8} | {5,12} | {6,-10}",
                            "Mã SP", "Tên sản phẩm", "Loại SP", "Đơn giá (VNĐ)", "Số lượng", "Ngày nhập", "Trạng thái"));
                        writer.WriteLine("------------------------------------------------------------------------------------------");

                        foreach (var item in _productList)
                        {
                            writer.WriteLine(string.Format("{0,-8} | {1,-32} | {2,-12} | {3,14:N0} | {4,8:N0} | {5,12:dd/MM/yyyy} | {6,-10}",
                                item.MaSP,
                                item.TenSP.Length > 30 ? item.TenSP.Substring(0, 27) + "..." : item.TenSP,
                                item.LoaiSP,
                                item.DonGia,
                                item.SoLuong,
                                item.NgayNhap,
                                item.ConKinhDoanh ? "Kinh doanh" : "Ngừng KD"));
                        }

                        writer.WriteLine("==========================================================================================");
                    }

                    UpdateStatus($"Đã xuất danh sách thành công ra file: {Path.GetFileName(saveFileDialog1.FileName)}");
                    MessageBox.Show("Xuất danh sách sản phẩm ra file .txt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void menuItemGioiThieu_Click(object sender, EventArgs e)
        {
            string intro = new StringBuilder()
                .AppendLine("HỆ THỐNG QUẢN LÝ SẢN PHẨM - PRODUCT MANAGER")
                .AppendLine("Môn học: COMP1019 - Lập trình trên Windows")
                .AppendLine("Bài thực hành: LAB 06 - Windows Forms Nâng cao và DataGridView")
                .AppendLine("--------------------------------------------------")
                .AppendLine("Các tính năng hỗ trợ:")
                .AppendLine("• MenuStrip, ToolStrip, StatusStrip, ContextMenuStrip")
                .AppendLine("• DataGridView liên kết BindingList & BindingSource")
                .AppendLine("• Thao tác CRUD (Thêm, Sửa, Xóa, Làm mới)")
                .AppendLine("• ErrorProvider kiểm tra dữ liệu nghiêm ngặt")
                .AppendLine("• Chọn ảnh bằng OpenFileDialog")
                .AppendLine("• Tìm kiếm tức thì & Xuất báo cáo file .txt")
                .ToString();

            MessageBox.Show(intro, "Giới thiệu ứng dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmProduct_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl + F: Tiêu điểm nhanh vào ô tìm kiếm
            if (e.Control && e.KeyCode == Keys.F)
            {
                txtTimKiem.Focus();
                e.Handled = true;
            }
        }

        #endregion
    }
}
