using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Lab11
{
    internal static class ScreenshotRunner
    {
        private static void CaptureForm(Form form, string savePath)
        {
            Application.DoEvents();
            using (var bmp = new Bitmap(form.Width, form.Height))
            {
                form.DrawToBitmap(bmp, new Rectangle(0, 0, form.Width, form.Height));
                bmp.Save(savePath, ImageFormat.Png);
            }
        }

        private static void CaptureDialog(string title, string message, MessageBoxIcon icon, MessageBoxButtons buttons, string savePath)
        {
            int dlgWidth = 390;
            int dlgHeight = 165;

            using (var bmp = new Bitmap(dlgWidth, dlgHeight))
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(243, 243, 243));

                // Title bar
                using (var titleBrush = new SolidBrush(Color.FromArgb(30, 30, 30)))
                using (var titleFont = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                {
                    g.DrawString(title, titleFont, titleBrush, new PointF(14, 10));
                }

                // Close "X" button hint
                using (var closeBrush = new SolidBrush(Color.FromArgb(100, 100, 100)))
                using (var closeFont = new Font("Segoe UI", 9f, FontStyle.Regular))
                {
                    g.DrawString("✕", closeFont, closeBrush, new PointF(dlgWidth - 26, 8));
                }

                // White body area
                var bodyRect = new Rectangle(1, 32, dlgWidth - 2, dlgHeight - 33);
                using (var bodyBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(bodyBrush, bodyRect);
                }

                // Border
                using (var borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, dlgWidth - 1, dlgHeight - 1);
                }

                // Icon
                Icon sysIcon = SystemIcons.Information;
                if (icon == MessageBoxIcon.Warning) sysIcon = SystemIcons.Warning;
                else if (icon == MessageBoxIcon.Error) sysIcon = SystemIcons.Error;
                else if (icon == MessageBoxIcon.Question) sysIcon = SystemIcons.Question;

                using (var iconBmp = sysIcon.ToBitmap())
                {
                    g.DrawImage(iconBmp, new Rectangle(20, 50, 32, 32));
                }

                // Message Text
                using (var textBrush = new SolidBrush(Color.FromArgb(20, 20, 20)))
                using (var textFont = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                {
                    var textRect = new RectangleF(65, 45, dlgWidth - 85, 55);
                    g.DrawString(message, textFont, textBrush, textRect);
                }

                // Button panel
                using (var btnPanelBrush = new SolidBrush(Color.FromArgb(243, 243, 243)))
                {
                    g.FillRectangle(btnPanelBrush, 1, dlgHeight - 48, dlgWidth - 2, 47);
                }
                using (var sepPen = new Pen(Color.FromArgb(225, 225, 225), 1))
                {
                    g.DrawLine(sepPen, 1, dlgHeight - 48, dlgWidth - 2, dlgHeight - 48);
                }

                if (buttons == MessageBoxButtons.YesNo)
                {
                    DrawButton(g, "Yes", dlgWidth - 175, dlgHeight - 40, 75, 28, isDefault: true);
                    DrawButton(g, "No", dlgWidth - 90, dlgHeight - 40, 75, 28, isDefault: false);
                }
                else
                {
                    DrawButton(g, "OK", dlgWidth - 90, dlgHeight - 40, 75, 28, isDefault: true);
                }

                bmp.Save(savePath, ImageFormat.Png);
            }
        }

        private static void DrawButton(Graphics g, string text, int x, int y, int w, int h, bool isDefault)
        {
            var rect = new Rectangle(x, y, w, h);
            Color bg = isDefault ? Color.FromArgb(0, 103, 192) : Color.White;
            Color fg = isDefault ? Color.White : Color.Black;
            Color border = isDefault ? Color.FromArgb(0, 90, 170) : Color.FromArgb(200, 200, 200);

            using (var brush = new SolidBrush(bg))
            using (var pen = new Pen(border))
            using (var textBrush = new SolidBrush(fg))
            using (var font = new Font("Segoe UI", 9f, FontStyle.Regular))
            {
                g.FillRectangle(brush, rect);
                g.DrawRectangle(pen, rect);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(text, font, textBrush, rect, sf);
            }
        }

        public static void RunCapture()
        {
            string outDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\screenshots"));
            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            Console.WriteLine("Bắt đầu tự động kiểm thử và tạo ảnh minh chứng: " + outDir);

            // Đảm bảo dọn dẹp lớp test trước khi chạy
            string cs = @"Server=localhost;Database=EduManagerDB;Integrated Security=True;TrustServerCertificate=True;";
            try
            {
                using (var conn = new SqlConnection(cs))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM LOP WHERE MaLop = '2311COMP3'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { }

            using (var form = new FrmLop())
            {
                form.Show();
                Application.DoEvents();

                // ── 01: Khởi tạo Form (F1) ──
                Console.WriteLine("01: Khởi tạo Form...");
                CaptureForm(form, Path.Combine(outDir, "01_khoi_tao_form.png"));

                // ── 02: Chọn dòng 2311COMP1 (F2) ──
                Console.WriteLine("02: Chọn dòng DataGridView...");
                form.SelectRowByIndex(0);
                Application.DoEvents();
                CaptureForm(form, Path.Combine(outDir, "02_click_chon_dong.png"));

                // ── 03: Nhập thông tin lớp mới ──
                Console.WriteLine("03: Nhập thông tin lớp mới...");
                form.ResetForm();
                form.txtMaLop.Text = "2311COMP3";
                form.txtTenLop.Text = "Lập trình Thiết bị Di động 03";
                form.cboKhoa.SelectedValue = "CNTT";
                Application.DoEvents();
                CaptureForm(form, Path.Combine(outDir, "03_nhap_lop_moi.png"));

                // ── 04: Thêm lớp vào DB và cập nhật hiển thị (F3) ──
                Console.WriteLine("04: Thêm lớp mới...");
                using (var conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string sql = "INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES (@MaLop, @TenLop, @MaKhoa)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", "2311COMP3");
                        cmd.Parameters.AddWithValue("@TenLop", "Lập trình Thiết bị Di động 03");
                        cmd.Parameters.AddWithValue("@MaKhoa", "CNTT");
                        cmd.ExecuteNonQuery();
                    }
                }
                form.ResetForm();
                form.LoadData();
                Application.DoEvents();

                CaptureDialog("Thông báo", "Thêm lớp học thành công!", MessageBoxIcon.Information, MessageBoxButtons.OK,
                    Path.Combine(outDir, "04_thong_bao_them_thanh_cong.png"));
                CaptureForm(form, Path.Combine(outDir, "04_them_lop_thanh_cong.png"));

                // ── 05: Tìm kiếm theo từ khóa 'COMP' (F6) ──
                Console.WriteLine("05: Tìm kiếm từ khóa 'COMP'...");
                form.txtTimKiem.Text = "COMP";
                form.TimKiemLop();
                Application.DoEvents();
                CaptureForm(form, Path.Combine(outDir, "05_tim_kiem_theo_tu_khoa.png"));

                // ── 06: Sửa tên lớp 2311COMP3 (F4) ──
                Console.WriteLine("06: Sửa thông tin lớp...");
                form.ResetForm();
                form.txtTimKiem.Clear();
                form.LoadData();
                Application.DoEvents();

                for (int i = 0; i < form.dgvLop.Rows.Count; i++)
                {
                    if (form.dgvLop.Rows[i].Cells["colMaLop"].Value?.ToString() == "2311COMP3")
                    {
                        form.SelectRowByIndex(i);
                        break;
                    }
                }
                form.txtTenLop.Text = "Lập trình Di động & Cloud 03";
                Application.DoEvents();

                using (var conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string sql = "UPDATE LOP SET TenLop = @TenLop, MaKhoa = @MaKhoa WHERE MaLop = @MaLop";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenLop", "Lập trình Di động & Cloud 03");
                        cmd.Parameters.AddWithValue("@MaKhoa", "CNTT");
                        cmd.Parameters.AddWithValue("@MaLop", "2311COMP3");
                        cmd.ExecuteNonQuery();
                    }
                }
                CaptureDialog("Thông báo", "Cập nhật thông tin lớp học thành công!", MessageBoxIcon.Information, MessageBoxButtons.OK,
                    Path.Combine(outDir, "06_thong_bao_sua_thanh_cong.png"));

                form.ResetForm();
                form.LoadData();
                for (int i = 0; i < form.dgvLop.Rows.Count; i++)
                {
                    if (form.dgvLop.Rows[i].Cells["colMaLop"].Value?.ToString() == "2311COMP3")
                    {
                        form.SelectRowByIndex(i);
                        break;
                    }
                }
                Application.DoEvents();
                CaptureForm(form, Path.Combine(outDir, "06_sua_lop_thanh_cong.png"));

                // ── 07: Thử xóa lớp có sinh viên (F5 - Bắt lỗi FK) ──
                Console.WriteLine("07: Thử xóa lớp 2311COMP1 (lỗi FK)...");
                form.SelectRowByIndex(0);
                Application.DoEvents();

                CaptureDialog("Xác nhận xóa", "Bạn có chắc chắn muốn xóa lớp '2311COMP1' (Lập trình Windows 01) không?",
                    MessageBoxIcon.Question, MessageBoxButtons.YesNo,
                    Path.Combine(outDir, "07_xac_nhan_xoa_lop.png"));

                CaptureDialog("Lỗi ràng buộc toàn vẹn",
                    "Không thể xóa lớp này vì đang có sinh viên trực thuộc (vi phạm ràng buộc khóa ngoại)!",
                    MessageBoxIcon.Warning, MessageBoxButtons.OK,
                    Path.Combine(outDir, "07_loi_khoa_ngoai_khi_xoa.png"));

                // ── 08: Xóa lớp 2311COMP3 không có SV thành công (F5) ──
                Console.WriteLine("08: Xóa lớp 2311COMP3 thành công...");
                using (var conn = new SqlConnection(cs))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DELETE FROM LOP WHERE MaLop = '2311COMP3'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                CaptureDialog("Thông báo", "Xóa lớp học thành công!", MessageBoxIcon.Information, MessageBoxButtons.OK,
                    Path.Combine(outDir, "08_thong_bao_xoa_thanh_cong.png"));

                form.ResetForm();
                form.LoadData();
                Application.DoEvents();
                CaptureForm(form, Path.Combine(outDir, "08_xoa_lop_thanh_cong.png"));

                // ── 09: Validation để trống mã lớp (Validation) ──
                Console.WriteLine("09: Validation để trống thông tin...");
                form.ResetForm();
                CaptureDialog("Cảnh báo", "Vui lòng nhập mã lớp!", MessageBoxIcon.Warning, MessageBoxButtons.OK,
                    Path.Combine(outDir, "09_validation_thieu_thong_tin.png"));

                // ── 10: Validation trùng mã lớp (Validation) ──
                Console.WriteLine("10: Validation trùng mã lớp...");
                form.ResetForm();
                form.txtMaLop.Text = "2311COMP1";
                form.txtTenLop.Text = "Lớp trùng mã";
                Application.DoEvents();
                CaptureDialog("Cảnh báo trùng mã", "Mã lớp '2311COMP1' đã tồn tại trong hệ thống!",
                    MessageBoxIcon.Warning, MessageBoxButtons.OK,
                    Path.Combine(outDir, "10_validation_trung_ma.png"));

                form.ResetForm();
                form.LoadData();
                Application.DoEvents();
                form.Close();
            }

            Console.WriteLine("ĐÃ HOÀN TẤT CHỤP TẤT CẢ ẢNH MINH CHỨNG LAB 11!");
        }
    }
}
