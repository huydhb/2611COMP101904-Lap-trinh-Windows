using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Lab07.Data;
using Lab07.Models;

namespace Lab07.Forms
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            // Nạp danh mục loại sản phẩm
            cboLoaiSP.Items.AddRange(new string[] { "Điện tử", "Gia dụng", "Thời trang", "Thực phẩm", "Văn phòng phẩm" });
            cboLoaiSP.SelectedIndex = 0;

            // Liên kết dữ liệu lên DataGridView
            dgvProduct.DataSource = DataStore.Products;
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            if (dgvProduct.Columns.Count == 0) return;

            dgvProduct.Columns["MaSP"].HeaderText = "Mã SP";
            dgvProduct.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvProduct.Columns["LoaiSP"].HeaderText = "Loại sản phẩm";
            dgvProduct.Columns["DonGia"].HeaderText = "Đơn giá (VNĐ)";
            dgvProduct.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvProduct.Columns["ThanhTien"].HeaderText = "Thành tiền (VNĐ)";

            dgvProduct.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
            dgvProduct.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProduct.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProduct.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
            dgvProduct.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            string tenSP = txtTenSP.Text.Trim();

            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã sản phẩm và Tên sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng mã sản phẩm
            if (DataStore.Products.Any(p => p.MaSP.Equals(maSP, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSP.Focus();
                return;
            }

            if (numDonGia.Value <= 0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numDonGia.Focus();
                return;
            }

            Product product = new Product(maSP, tenSP, cboLoaiSP.SelectedItem.ToString(), numDonGia.Value, (int)numSoLuong.Value);
            DataStore.Products.Add(product);
            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInput();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            Product product = DataStore.Products.FirstOrDefault(p => p.MaSP.Equals(maSP, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenSP = txtTenSP.Text.Trim();
            if (string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("Tên sản phẩm không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numDonGia.Value <= 0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cập nhật thông tin
            product.TenSP = tenSP;
            product.LoaiSP = cboLoaiSP.SelectedItem.ToString();
            product.DonGia = numDonGia.Value;
            product.SoLuong = (int)numSoLuong.Value;

            dgvProduct.Refresh();
            MessageBox.Show("Cập nhật thông tin sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInput();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSP = txtMaSP.Text.Trim();
            Product product = DataStore.Products.FirstOrDefault(p => p.MaSP.Equals(maSP, StringComparison.OrdinalIgnoreCase));

            if (product == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm '{product.TenSP}' (Mã: {product.MaSP})?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                DataStore.Products.Remove(product);
                MessageBox.Show("Đã xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            dgvProduct.DataSource = DataStore.Products;
        }

        private void ClearInput()
        {
            txtMaSP.Clear();
            txtMaSP.ReadOnly = false;
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = 0;
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
            txtTimKiem.Clear();
            txtMaSP.Focus();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProduct.Rows.Count)
            {
                DataGridViewRow row = dgvProduct.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["MaSP"].Value?.ToString();
                txtMaSP.ReadOnly = true; // Không cho sửa mã sản phẩm khi đang xem/sửa
                txtTenSP.Text = row.Cells["TenSP"].Value?.ToString();
                cboLoaiSP.SelectedItem = row.Cells["LoaiSP"].Value?.ToString();
                numDonGia.Value = Convert.ToDecimal(row.Cells["DonGia"].Value);
                numSoLuong.Value = Convert.ToInt32(row.Cells["SoLuong"].Value);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dgvProduct.DataSource = DataStore.Products;
                return;
            }

            // Lọc theo mã hoặc tên sản phẩm
            var result = DataStore.Products.Where(p =>
                p.MaSP.ToLower().Contains(keyword) ||
                p.TenSP.ToLower().Contains(keyword)).ToList();

            dgvProduct.DataSource = new BindingList<Product>(result);
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            dgvProduct.DataSource = DataStore.Products;
        }
    }
}
