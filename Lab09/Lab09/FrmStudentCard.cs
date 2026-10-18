using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace Lab09
{
    public partial class FrmStudentCard : Form
    {
        // Ảnh đại diện lưu trong bộ nhớ
        private Image avatarImage = null;

        public FrmStudentCard()
        {
            InitializeComponent();
        }

        private void FrmStudentCard_Load(object sender, EventArgs e)
        {
            // Nạp dữ liệu mẫu cho các ComboBox
            InitSampleData();

            // Đăng ký sự kiện cập nhật preview khi dữ liệu thay đổi
            RegisterEventHandlers();

            // Vẽ lại preview ban đầu
            pnlPreview.Invalidate();
        }

        /// <summary>
        /// Khởi tạo danh sách lựa chọn mẫu cho các ComboBox
        /// </summary>
        private void InitSampleData()
        {
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.SelectedIndex = 0;

            cboLop.Items.AddRange(new object[] {
                "2211COMP101901",
                "2211COMP101902",
                "2311COMP101901",
                "2311COMP101902",
                "2411COMP101901"
            });
            cboLop.SelectedIndex = 2; // Mặc định khóa 23

            cboKhoa.Items.AddRange(new object[] {
                "Công nghệ Thông tin",
                "Toán - Tin học",
                "Vật lý",
                "Hóa học",
                "Khoa học Giáo dục"
            });
            cboKhoa.SelectedIndex = 0;

            cboKhoaHoc.Items.AddRange(new object[] {
                "K48 (2022 - 2026)",
                "K49 (2023 - 2027)",
                "K50 (2024 - 2028)",
                "K51 (2025 - 2029)"
            });
            cboKhoaHoc.SelectedIndex = 1;

            dtpNgaySinh.Value = new DateTime(2005, 5, 15);
        }

        /// <summary>
        /// Đăng ký các sự kiện để tự động cập nhật preview thẻ
        /// </summary>
        private void RegisterEventHandlers()
        {
            txtMaSV.TextChanged += (s, e) => pnlPreview.Invalidate();
            txtHoTen.TextChanged += (s, e) => pnlPreview.Invalidate();
            cboLop.TextChanged += (s, e) => pnlPreview.Invalidate();
            cboLop.SelectedIndexChanged += (s, e) => pnlPreview.Invalidate();
            cboKhoa.TextChanged += (s, e) => pnlPreview.Invalidate();
            cboKhoa.SelectedIndexChanged += (s, e) => pnlPreview.Invalidate();
            cboKhoaHoc.TextChanged += (s, e) => pnlPreview.Invalidate();
            cboKhoaHoc.SelectedIndexChanged += (s, e) => pnlPreview.Invalidate();
            cboGioiTinh.SelectedIndexChanged += (s, e) => pnlPreview.Invalidate();
            dtpNgaySinh.ValueChanged += (s, e) => pnlPreview.Invalidate();
        }

        /// <summary>
        /// F1: Chọn ảnh đại diện từ máy tính mà không khóa file
        /// </summary>
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Tệp hình ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
            openFileDialog1.Title = "Chọn ảnh thẻ sinh viên";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Đọc qua FileStream và clone Bitmap để giải phóng file lập tức, tránh lock file ảnh
                    using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                    using (Image temp = Image.FromStream(fs))
                    {
                        if (avatarImage != null)
                        {
                            avatarImage.Dispose();
                            avatarImage = null;
                        }
                        avatarImage = new Bitmap(temp);
                    }

                    picAvatar.Image = avatarImage;
                    pnlPreview.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải hình ảnh: " + ex.Message, "Lỗi đọc ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// F2: Vẽ preview thẻ sinh viên bằng GDI+ trên Panel
        /// </summary>
        private void pnlPreview_Paint(object sender, PaintEventArgs e)
        {
            Rectangle cardRect = new Rectangle(10, 10, pnlPreview.ClientSize.Width - 20, pnlPreview.ClientSize.Height - 20);
            DrawStudentCard(e.Graphics, cardRect);
        }

        /// <summary>
        /// F3: Kiểm tra tính hợp lệ của dữ liệu đầu vào bắt buộc
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sinh viên!", "Thiếu thông tin bắt buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ và tên sinh viên!", "Thiếu thông tin bắt buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (avatarImage == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh đại diện cho sinh viên!", "Thiếu thông tin bắt buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnChonAnh.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// F4: Xem trước bản in (Print Preview)
        /// </summary>
        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.WindowState = FormWindowState.Normal;
                printPreviewDialog1.StartPosition = FormStartPosition.CenterParent;
                printPreviewDialog1.Width = 850;
                printPreviewDialog1.Height = 600;
                printPreviewDialog1.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở cửa sổ xem trước in: " + ex.Message, "Lỗi in", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// F5: In thẻ qua PrintDialog và PrintDocument
        /// </summary>
        private void btnIn_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    printDocument1.Print();
                    MessageBox.Show("Lệnh in đã được gửi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi in: " + ex.Message, "Lỗi in ấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sự kiện PrintPage của PrintDocument: định nghĩa nội dung trang in
        /// </summary>
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Kích thước thẻ tiêu chuẩn khi in (khoảng 85.6mm x 54mm tỉ lệ ~ 500x315 px ở độ phân giải in tiêu chuẩn)
            Rectangle printCardRect = new Rectangle(60, 60, 520, 325);
            DrawStudentCard(e.Graphics, printCardRect);
            e.HasMorePages = false;
        }

        /// <summary>
        /// F6: Làm mới toàn bộ Form về trạng thái ban đầu
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();

            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 2;
            if (cboKhoa.Items.Count > 0) cboKhoa.SelectedIndex = 0;
            if (cboKhoaHoc.Items.Count > 0) cboKhoaHoc.SelectedIndex = 1;
            if (cboGioiTinh.Items.Count > 0) cboGioiTinh.SelectedIndex = 0;

            dtpNgaySinh.Value = new DateTime(2005, 5, 15);

            if (avatarImage != null)
            {
                avatarImage.Dispose();
                avatarImage = null;
            }
            picAvatar.Image = null;

            pnlPreview.Invalidate();
            txtMaSV.Focus();
        }

        /// <summary>
        /// Thoát ứng dụng
        /// </summary>
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Hàm dùng chung vẽ thẻ sinh viên bằng GDI+
        /// Tự động co giãn theo kích thước Rectangle bounds truyền vào
        /// </summary>
        private void DrawStudentCard(Graphics g, Rectangle bounds)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // 1. Nền thẻ
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(246, 250, 255)))
            {
                g.FillRectangle(bgBrush, bounds);
            }

            // 2. Viền ngoài thẻ
            using (Pen borderPen = new Pen(Color.FromArgb(16, 54, 110), 2))
            {
                g.DrawRectangle(borderPen, bounds);
            }

            // 3. Header thẻ (Banner xanh đậm)
            int headerHeight = (int)(bounds.Height * 0.23);
            Rectangle headerRect = new Rectangle(bounds.X, bounds.Y, bounds.Width, headerHeight);

            using (LinearGradientBrush headerBrush = new LinearGradientBrush(
                headerRect,
                Color.FromArgb(16, 54, 110),
                Color.FromArgb(25, 84, 160),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(headerBrush, headerRect);
            }

            // Tiêu đề trường
            using (Font schoolFont = new Font("Segoe UI", bounds.Height > 300 ? 11f : 9.5f, FontStyle.Bold))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            using (StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                Rectangle schoolRect = new Rectangle(headerRect.X, headerRect.Y + 3, headerRect.Width, (int)(headerRect.Height * 0.48));
                g.DrawString("TRƯỜNG ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH", schoolFont, whiteBrush, schoolRect, sfCenter);

                // Dòng THẺ SINH VIÊN
                using (Font cardTitleFont = new Font("Segoe UI", bounds.Height > 300 ? 14f : 12f, FontStyle.Bold))
                using (SolidBrush goldBrush = new SolidBrush(Color.FromArgb(255, 223, 80)))
                {
                    Rectangle titleRect = new Rectangle(headerRect.X, schoolRect.Bottom - 2, headerRect.Width, (int)(headerRect.Height * 0.52));
                    g.DrawString("THẺ SINH VIÊN", cardTitleFont, goldBrush, titleRect, sfCenter);
                }
            }

            // 4. Vùng ảnh sinh viên (bên trái thẻ)
            int photoMargin = (int)(bounds.Width * 0.04);
            int photoY = headerRect.Bottom + (int)(bounds.Height * 0.05);
            int photoWidth = (int)(bounds.Width * 0.26);
            int photoHeight = (int)(bounds.Height * 0.56);
            Rectangle photoRect = new Rectangle(bounds.X + photoMargin, photoY, photoWidth, photoHeight);

            // Nền khung ảnh
            using (SolidBrush photoBg = new SolidBrush(Color.WhiteSmoke))
            {
                g.FillRectangle(photoBg, photoRect);
            }

            if (avatarImage != null)
            {
                // Vẽ ảnh giữ nguyên tỉ lệ (không méo hình)
                DrawImageProportional(g, avatarImage, photoRect);
            }
            else
            {
                // Placeholder khi chưa có ảnh
                using (Font phFont = new Font("Segoe UI", 9f, FontStyle.Italic))
                using (SolidBrush phBrush = new SolidBrush(Color.FromArgb(120, 120, 120)))
                using (StringFormat phFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    g.DrawString("[Ảnh 3x4]", phFont, phBrush, photoRect, phFormat);
                }
            }

            // Viền khung ảnh
            using (Pen photoBorder = new Pen(Color.FromArgb(160, 180, 205), 1.5f))
            {
                g.DrawRectangle(photoBorder, photoRect);
            }

            // 5. Vùng thông tin sinh viên (bên phải thẻ)
            int infoX = photoRect.Right + (int)(bounds.Width * 0.04);
            int infoY = photoRect.Y;
            int infoWidth = bounds.Right - infoX - (int)(bounds.Width * 0.04);
            int infoHeight = photoRect.Height;

            float labelFontSize = bounds.Height > 300 ? 10f : 8.5f;
            float valFontSize = bounds.Height > 300 ? 10.5f : 9f;

            using (Font lblFont = new Font("Segoe UI", labelFontSize, FontStyle.Regular))
            using (Font valFont = new Font("Segoe UI", valFontSize, FontStyle.Bold))
            using (SolidBrush lblBrush = new SolidBrush(Color.FromArgb(60, 60, 60)))
            using (SolidBrush valBrush = new SolidBrush(Color.FromArgb(10, 30, 80)))
            using (SolidBrush nameBrush = new SolidBrush(Color.FromArgb(170, 20, 20)))
            {
                string maSV = string.IsNullOrWhiteSpace(txtMaSV.Text) ? "..." : txtMaSV.Text.Trim();
                string hoTen = string.IsNullOrWhiteSpace(txtHoTen.Text) ? "..." : txtHoTen.Text.Trim();
                string ngaySinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
                string gioiTinh = cboGioiTinh.Text;
                string lop = string.IsNullOrWhiteSpace(cboLop.Text) ? "..." : cboLop.Text;
                string khoa = string.IsNullOrWhiteSpace(cboKhoa.Text) ? "..." : cboKhoa.Text;
                string khoaHoc = string.IsNullOrWhiteSpace(cboKhoaHoc.Text) ? "..." : cboKhoaHoc.Text;

                int lineSpacing = (int)(infoHeight / 6.2);
                int currentY = infoY;

                // Họ và tên (Nổi bật)
                g.DrawString("Họ tên:", lblFont, lblBrush, infoX, currentY);
                using (Font nameFont = new Font("Segoe UI", valFontSize + 1f, FontStyle.Bold))
                {
                    g.DrawString(hoTen.ToUpper(), nameFont, nameBrush, infoX + 70, currentY);
                }
                currentY += lineSpacing;

                // Mã SV
                g.DrawString("Mã SV:", lblFont, lblBrush, infoX, currentY);
                g.DrawString(maSV, valFont, valBrush, infoX + 70, currentY);
                currentY += lineSpacing;

                // Ngày sinh & Giới tính
                g.DrawString("Ngày sinh:", lblFont, lblBrush, infoX, currentY);
                g.DrawString(ngaySinh + "  -  Giới tính: " + gioiTinh, valFont, valBrush, infoX + 70, currentY);
                currentY += lineSpacing;

                // Lớp
                g.DrawString("Lớp:", lblFont, lblBrush, infoX, currentY);
                g.DrawString(lop, valFont, valBrush, infoX + 70, currentY);
                currentY += lineSpacing;

                // Khóa học
                g.DrawString("Khóa:", lblFont, lblBrush, infoX, currentY);
                g.DrawString(khoaHoc, valFont, valBrush, infoX + 70, currentY);
                currentY += lineSpacing;

                // Khoa
                g.DrawString("Khoa:", lblFont, lblBrush, infoX, currentY);
                g.DrawString(khoa, valFont, valBrush, infoX + 70, currentY);
            }

            // 6. Thanh Footer trang trí (Dải màu nhạt + dòng chữ giá trị thẻ)
            int footerY = bounds.Bottom - (int)(bounds.Height * 0.10);
            Rectangle footerRect = new Rectangle(bounds.X, footerY, bounds.Width, bounds.Bottom - footerY);

            using (SolidBrush footerBg = new SolidBrush(Color.FromArgb(230, 240, 252)))
            {
                g.FillRectangle(footerBg, footerRect);
            }

            using (Pen footerLinePen = new Pen(Color.FromArgb(180, 205, 235), 1))
            {
                g.DrawLine(footerLinePen, bounds.X, footerY, bounds.Right, footerY);
            }

            // Text footer & Barcode giả lập
            using (Font footFont = new Font("Segoe UI", 7.5f, FontStyle.Italic))
            using (SolidBrush footBrush = new SolidBrush(Color.FromArgb(90, 100, 120)))
            {
                g.DrawString("Thẻ có giá trị trong suốt thời gian học tập", footFont, footBrush, bounds.X + photoMargin, footerY + 3);
            }

            // Vẽ giả lập barcode ở góc phải footer
            DrawMockBarcode(g, new Rectangle(bounds.Right - 130, footerY + 2, 115, footerRect.Height - 5));
        }

        /// <summary>
        /// Vẽ hình ảnh bên trong khung chữ nhật mà luôn giữ đúng tỉ lệ gốc (không méo hình)
        /// </summary>
        private void DrawImageProportional(Graphics g, Image img, Rectangle targetRect)
        {
            if (img == null || targetRect.Width <= 0 || targetRect.Height <= 0) return;

            float imgAspect = (float)img.Width / img.Height;
            float targetAspect = (float)targetRect.Width / targetRect.Height;

            int drawWidth, drawHeight;
            int drawX, drawY;

            if (imgAspect > targetAspect)
            {
                // Ảnh ngang hơn khung: căn theo chiều rộng
                drawWidth = targetRect.Width;
                drawHeight = (int)(targetRect.Width / imgAspect);
                drawX = targetRect.X;
                drawY = targetRect.Y + (targetRect.Height - drawHeight) / 2;
            }
            else
            {
                // Ảnh dọc hơn khung: căn theo chiều cao
                drawHeight = targetRect.Height;
                drawWidth = (int)(targetRect.Height * imgAspect);
                drawX = targetRect.X + (targetRect.Width - drawWidth) / 2;
                drawY = targetRect.Y;
            }

            g.DrawImage(img, new Rectangle(drawX, drawY, drawWidth, drawHeight));
        }

        /// <summary>
        /// Vẽ một dải barcode giả lập đơn giản bằng GDI+
        /// </summary>
        private void DrawMockBarcode(Graphics g, Rectangle barcodeRect)
        {
            if (barcodeRect.Width <= 0 || barcodeRect.Height <= 0) return;

            using (SolidBrush darkBrush = new SolidBrush(Color.FromArgb(40, 40, 40)))
            {
                int x = barcodeRect.X;
                int[] barWidths = { 2, 1, 3, 1, 2, 4, 1, 3, 2, 1, 3, 1, 2, 1, 4, 2, 1, 3, 1, 2, 3, 1, 2 };
                for (int i = 0; i < barWidths.Length; i++)
                {
                    int w = barWidths[i];
                    if (x + w > barcodeRect.Right) break;
                    if (i % 2 == 0)
                    {
                        g.FillRectangle(darkBrush, x, barcodeRect.Y, w, barcodeRect.Height);
                    }
                    x += w + 1;
                }
            }
        }
    }
}
