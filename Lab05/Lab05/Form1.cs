using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab05
{
    public partial class Form1 : Form
    {
        // Bảng học phí các khóa học theo yêu cầu đề bài Lab 05
        private readonly Dictionary<string, decimal> dsKhoaHoc = new Dictionary<string, decimal>()
        {
            { "C# WinForms cơ bản", 800000m },
            { "SQL Server cơ bản", 700000m },
            { "Web Frontend cơ bản", 750000m },
            { "Lập trình Python cơ bản", 650000m },
            { "Lập trình C#", 800000m },
            { "Lập trình Windows", 700000m },
            { "Cơ sở dữ liệu", 750000m }
        };

        public Form1()
        {
            InitializeComponent();
        }

        private decimal LayHocPhiMon()
        {
            if (cboKhoaHoc.SelectedItem != null && dsKhoaHoc.TryGetValue(cboKhoaHoc.SelectedItem.ToString(), out decimal hocPhi))
            {
                return hocPhi;
            }
            return 0m;
        }

        private decimal LayPhuPhiHinhThuc()
        {
            // Nếu học trực tiếp, phụ thu 100.000 VND
            if (radOffline != null && radOffline.Checked)
            {
                return 100000m;
            }
            return 0m;
        }

        private void CapNhatHocPhi()
        {
            decimal hocPhiMotThang = LayHocPhiMon() + LayPhuPhiHinhThuc();
            decimal soThang = numSoThang.Value;
            decimal tongHocPhi = hocPhiMotThang * soThang;

            lblDonGia.Text = hocPhiMotThang.ToString("N0") + " VND";
            lblTongTien.Text = tongHocPhi.ToString("N0") + " VND";
        }

        private bool SoDienThoaiHopLe(string soDienThoai)
        {
            if (string.IsNullOrWhiteSpace(soDienThoai) || soDienThoai.Length != 10)
            {
                return false;
            }
            if (!soDienThoai.StartsWith("0"))
            {
                return false;
            }
            foreach (char c in soDienThoai)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool KiemTraDuLieu()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Họ tên không được để trống!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            string soDienThoai = txtSoDienThoai.Text.Trim();
            if (!SoDienThoaiHopLe(soDienThoai))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!\nVui lòng nhập đúng 10 chữ số và bắt đầu bằng số 0.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.Focus();
                return false;
            }

            if (cboKhoaHoc.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn khóa học!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoaHoc.Focus();
                return false;
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Nạp danh sách khóa học theo chuẩn bài Lab 05
            cboKhoaHoc.Items.Clear();
            cboKhoaHoc.Items.Add("C# WinForms cơ bản");
            cboKhoaHoc.Items.Add("SQL Server cơ bản");
            cboKhoaHoc.Items.Add("Web Frontend cơ bản");
            cboKhoaHoc.Items.Add("Lập trình Python cơ bản");
            cboKhoaHoc.Items.Add("Lập trình C#");
            cboKhoaHoc.Items.Add("Lập trình Windows");
            cboKhoaHoc.Items.Add("Cơ sở dữ liệu");

            cboKhoaHoc.SelectedIndex = 0;

            // Thiết lập giá trị mặc định
            radOnline.Checked = true;
            dtpNgaySinh.Value = DateTime.Today;

            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;

            chkNhanEmail.Checked = false;
            lblTrangThaiEmail.Text = "Chưa nhận email thông báo";

            CapNhatHocPhi();
            txtHoTen.Focus();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu())
            {
                return;
            }

            string hoTen = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string ngaySinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
            string khoaHoc = cboKhoaHoc.SelectedItem.ToString();
            string hinhThuc = radOnline.Checked ? "Online" : "Trực tiếp";
            int soThang = (int)numSoThang.Value;
            string tongTien = lblTongTien.Text;
            string nhanEmail = chkNhanEmail.Checked ? "Có nhận email" : "Không nhận email";

            // Định dạng thông tin rõ ràng, căn đều, không dùng ký tự kẻ dài gây rớt dòng
            string thongBao = "XÁC NHẬN ĐĂNG KÝ THÀNH CÔNG\n\n" +
                              $"• Họ và tên:  {hoTen}\n" +
                              $"• Số điện thoại:  {sdt}\n" +
                              $"• Ngày sinh:  {ngaySinh}\n" +
                              $"• Khóa học:  {khoaHoc}\n" +
                              $"• Hình thức học:  {hinhThuc}\n" +
                              $"• Số tháng đăng ký:  {soThang} tháng\n" +
                              $"• Tổng học phí:  {tongTien}\n" +
                              $"• Nhận email thông báo:  {nhanEmail}\n\n" +
                              "Cảm ơn bạn đã lựa chọn khóa học của chúng tôi!";

            MessageBox.Show(thongBao, "Thông tin phiếu đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            chkNhanEmail.Checked = false;
            lblTrangThaiEmail.Text = "Chưa nhận email thông báo";

            if (cboKhoaHoc.Items.Count > 0)
            {
                cboKhoaHoc.SelectedIndex = 0;
            }
            radOnline.Checked = true;
            numSoThang.Value = 1;

            CapNhatHocPhi();
            txtHoTen.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát khỏi chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            lblDemKyTu.Text = $"{txtHoTen.Text.Length}/50";
        }

        private void chkNhanEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNhanEmail.Checked)
            {
                lblTrangThaiEmail.Text = "Đã đăng ký nhận email thông báo";
            }
            else
            {
                lblTrangThaiEmail.Text = "Chưa nhận email thông báo";
            }
        }

        private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }

        private void radOnline_CheckedChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }

        private void radOffline_CheckedChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }

        private void numSoThang_ValueChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }
    }
}
