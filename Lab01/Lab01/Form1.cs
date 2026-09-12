using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra Họ tên không được rỗng
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được rỗng.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // 2. Kiểm tra Năm sinh không được rỗng và phải là số nguyên
            if (string.IsNullOrWhiteSpace(txtNamSinh.Text))
            {
                MessageBox.Show("Năm sinh không được rỗng.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            int namSinh;
            try
            {
                // Nếu txtNamSinh là chữ thì lỗi
                namSinh = int.Parse(txtNamSinh.Text);
            }
            catch
            {
                // Bắt lỗi khi người dùng không nhập số
                MessageBox.Show("Năm sinh phải là số nguyên.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // 3. Năm sinh phải nằm trong khoảng từ 1900 đến năm hiện tại
            int namHienTai = DateTime.Now.Year;
            if (namSinh < 1900 || namSinh > namHienTai)
            {
                MessageBox.Show($"Năm sinh phải nằm trong khoảng từ 1900 đến {namHienTai}.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamSinh.Focus();
                return;
            }

            // 4. Email không được rỗng
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email không được rỗng.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // 5. Phải chọn giới tính
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Phải chọn giới tính.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 6. Phải chọn khoa hoặc lớp
            if (cboKhoaLop.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cboKhoaLop.Text))
            {
                MessageBox.Show("Phải chọn khoa hoặc lớp.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaLop.Focus();
                return;
            }

            // Hiển thị thông tin
            int tuoi = namHienTai - namSinh;
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            string result = $"Họ tên: {txtHoTen.Text.Trim()}\n" +
                            $"Tuổi: {tuoi}\n" +
                            $"Email: {txtEmail.Text.Trim()}\n" +
                            $"Giới tính: {gioiTinh}\n" +
                            $"Khoa/Lớp: {cboKhoaLop.Text.Trim()}";

            MessageBox.Show(result, "Thông tin sinh viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            //Nút Thoát hỏi xác nhận trước khi đóng chương trình
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Nút Xóa đưa các TextBox về rỗng, bỏ chọn giới tính, đưa ComboBox về không chọn
            txtHoTen.Clear();
            txtNamSinh.Clear();
            txtEmail.Clear();
            radNam.Checked = false;
            radNu.Checked = false;
            cboKhoaLop.SelectedIndex = -1;
            txtHoTen.Focus();
        }
    }
}   
