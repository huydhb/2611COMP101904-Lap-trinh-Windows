using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Lab07.Data;
using Lab07.Models;

namespace Lab07.Forms
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            // Nạp danh mục loại khách hàng
            cboLoaiKH.Items.AddRange(new string[] { "Thường", "Thân thiết", "VIP" });
            cboLoaiKH.SelectedIndex = 0;

            // Liên kết dữ liệu lên DataGridView
            dgvCustomer.DataSource = DataStore.Customers;
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            if (dgvCustomer.Columns.Count == 0) return;

            dgvCustomer.Columns["MaKH"].HeaderText = "Mã KH";
            dgvCustomer.Columns["HoTen"].HeaderText = "Họ và tên";
            dgvCustomer.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dgvCustomer.Columns["LoaiKH"].HeaderText = "Loại khách hàng";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();

            // Kiểm tra rỗng
            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã KH, Họ tên và Số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra trùng mã khách hàng
            if (DataStore.Customers.Any(c => c.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Mã khách hàng đã tồn tại trong hệ thống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKH.Focus();
                return;
            }

            Customer customer = new Customer(maKH, hoTen, sdt, cboLoaiKH.SelectedItem.ToString());
            DataStore.Customers.Add(customer);
            MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInput();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            Customer customer = DataStore.Customers.FirstOrDefault(c => c.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase));

            if (customer == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Họ tên và số điện thoại không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            customer.HoTen = hoTen;
            customer.SoDienThoai = sdt;
            customer.LoaiKH = cboLoaiKH.SelectedItem.ToString();

            dgvCustomer.Refresh();
            MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearInput();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text.Trim();
            Customer customer = DataStore.Customers.FirstOrDefault(c => c.MaKH.Equals(maKH, StringComparison.OrdinalIgnoreCase));

            if (customer == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khách hàng '{customer.HoTen}' (Mã: {customer.MaKH})?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                DataStore.Customers.Remove(customer);
                MessageBox.Show("Đã xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInput();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInput();
            dgvCustomer.DataSource = DataStore.Customers;
        }

        private void ClearInput()
        {
            txtMaKH.Clear();
            txtMaKH.ReadOnly = false;
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            cboLoaiKH.SelectedIndex = 0;
            txtTimKiem.Clear();
            txtMaKH.Focus();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvCustomer.Rows.Count)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString();
                txtMaKH.ReadOnly = true;
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString();
                cboLoaiKH.SelectedItem = row.Cells["LoaiKH"].Value?.ToString();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                dgvCustomer.DataSource = DataStore.Customers;
                return;
            }

            // Lọc theo mã, họ tên hoặc số điện thoại
            var result = DataStore.Customers.Where(c =>
                c.MaKH.ToLower().Contains(keyword) ||
                c.HoTen.ToLower().Contains(keyword) ||
                c.SoDienThoai.Contains(keyword)).ToList();

            dgvCustomer.DataSource = new BindingList<Customer>(result);
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            dgvCustomer.DataSource = DataStore.Customers;
        }
    }
}
