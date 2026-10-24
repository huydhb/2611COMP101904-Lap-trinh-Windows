using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lab13.Models;

namespace Lab13
{
    public partial class FrmBaoCaoSinhVien : Form
    {
        private List<SinhVien> _danhSachSinhVien;
        private List<SinhVien> _danhSachHienTai;
        private bool _isShowingThongKe = false;

        public FrmBaoCaoSinhVien()
        {
            InitializeComponent();
        }

        private void FrmBaoCaoSinhVien_Load(object sender, EventArgs e)
        {
            TaoDuLieuMau();
            LoadComboBox();
            XemBaoCao();
        }

        /// <summary>
        /// Tạo danh sách dữ liệu mẫu gồm 15 sinh viên thuộc 3 lớp khác nhau
        /// </summary>
        private void TaoDuLieuMau()
        {
            _danhSachSinhVien = new List<SinhVien>
            {
                // Lớp 23CNTT1
                new SinhVien { MaSV = "SV01", HoTen = "Nguyễn Văn An", MaLop = "23CNTT1", TenLop = "Công nghệ Thông tin 1", Diem = 8.5 },
                new SinhVien { MaSV = "SV02", HoTen = "Trần Thị Mai", MaLop = "23CNTT1", TenLop = "Công nghệ Thông tin 1", Diem = 9.5 },
                new SinhVien { MaSV = "SV03", HoTen = "Lê Hoàng Long", MaLop = "23CNTT1", TenLop = "Công nghệ Thông tin 1", Diem = 4.0 },
                new SinhVien { MaSV = "SV04", HoTen = "Phạm Minh Tuấn", MaLop = "23CNTT1", TenLop = "Công nghệ Thông tin 1", Diem = 6.5 },
                new SinhVien { MaSV = "SV05", HoTen = "Vũ Thúy Hằng", MaLop = "23CNTT1", TenLop = "Công nghệ Thông tin 1", Diem = 7.0 },

                // Lớp 23CNTT2
                new SinhVien { MaSV = "SV06", HoTen = "Đỗ Hải Yến", MaLop = "23CNTT2", TenLop = "Công nghệ Thông tin 2", Diem = 8.0 },
                new SinhVien { MaSV = "SV07", HoTen = "Bùi Quốc Anh", MaLop = "23CNTT2", TenLop = "Công nghệ Thông tin 2", Diem = 3.5 },
                new SinhVien { MaSV = "SV08", HoTen = "Đinh Gia Huy", MaLop = "23CNTT2", TenLop = "Công nghệ Thông tin 2", Diem = 7.5 },
                new SinhVien { MaSV = "SV09", HoTen = "Hoàng Thu Thảo", MaLop = "23CNTT2", TenLop = "Công nghệ Thông tin 2", Diem = 9.0 },
                new SinhVien { MaSV = "SV10", HoTen = "Ngô Quang Khải", MaLop = "23CNTT2", TenLop = "Công nghệ Thông tin 2", Diem = 5.5 },

                // Lớp 23TOAN1
                new SinhVien { MaSV = "SV11", HoTen = "Võ Quốc Huy", MaLop = "23TOAN1", TenLop = "Toán - Tin học 1", Diem = 8.8 },
                new SinhVien { MaSV = "SV12", HoTen = "Phan Kim Ngân", MaLop = "23TOAN1", TenLop = "Toán - Tin học 1", Diem = 9.2 },
                new SinhVien { MaSV = "SV13", HoTen = "Trịnh Minh Triết", MaLop = "23TOAN1", TenLop = "Toán - Tin học 1", Diem = 4.5 },
                new SinhVien { MaSV = "SV14", HoTen = "Lương Bảo Châu", MaLop = "23TOAN1", TenLop = "Toán - Tin học 1", Diem = 6.0 },
                new SinhVien { MaSV = "SV15", HoTen = "Nguyễn Tiến Dũng", MaLop = "23TOAN1", TenLop = "Toán - Tin học 1", Diem = 7.8 }
            };
        }

        /// <summary>
        /// Nạp các mục chọn vào ComboBox cboLop và cboKetQua
        /// </summary>
        private void LoadComboBox()
        {
            // 1. Nạp ComboBox Lớp học bằng LINQ
            var danhSachLop = _danhSachSinhVien
                .Select(sv => new { sv.MaLop, sv.TenLop })
                .Distinct()
                .OrderBy(l => l.MaLop)
                .ToList();

            // Thêm lựa chọn "Tất cả các lớp"
            var dsLopCombo = new List<dynamic>
            {
                new { MaLop = "", TenLop = "-- Tất cả các lớp --" }
            };
            dsLopCombo.AddRange(danhSachLop);

            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            cboLop.DataSource = dsLopCombo;
            cboLop.SelectedIndex = 0;

            // 2. Nạp ComboBox Kết quả
            cboKetQua.Items.Clear();
            cboKetQua.Items.Add("-- Tất cả kết quả --");
            cboKetQua.Items.Add("Đạt (>= 5.0)");
            cboKetQua.Items.Add("Không đạt (< 5.0)");
            cboKetQua.SelectedIndex = 0;
        }

        /// <summary>
        /// Lọc dữ liệu sinh viên bằng LINQ theo lớp, kết quả và từ khóa; sắp xếp giảm dần theo điểm
        /// </summary>
        private void XemBaoCao()
        {
            _isShowingThongKe = false;
            grpKetQua.Text = "Danh sách sinh viên hiển thị";

            var query = _danhSachSinhVien.AsEnumerable();

            // 1. Lọc theo lớp học
            if (cboLop.SelectedIndex > 0 && cboLop.SelectedValue != null)
            {
                string maLopChon = cboLop.SelectedValue.ToString();
                query = query.Where(sv => sv.MaLop == maLopChon);
            }

            // 2. Lọc theo kết quả học tập
            if (cboKetQua.SelectedIndex == 1) // Đạt
            {
                query = query.Where(sv => sv.Diem >= 5.0);
            }
            else if (cboKetQua.SelectedIndex == 2) // Không đạt
            {
                query = query.Where(sv => sv.Diem < 5.0);
            }

            // 3. Lọc theo từ khóa tìm kiếm (Mã sinh viên hoặc Họ tên)
            string tuKhoa = txtTuKhoa.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(sv => sv.MaSV.ToLower().Contains(tuKhoa) || 
                                          sv.HoTen.ToLower().Contains(tuKhoa));
            }

            // 4. Sắp xếp giảm dần theo điểm trung bình bằng LINQ
            _danhSachHienTai = query.OrderByDescending(sv => sv.Diem).ToList();

            // 5. Hiển thị lên DataGridView
            dgvBaoCao.DataSource = null;
            dgvBaoCao.AutoGenerateColumns = true;
            dgvBaoCao.DataSource = _danhSachHienTai;

            DinhDangCotSinhVien();
            CapNhatThongKe(_danhSachHienTai);

            if (_danhSachHienTai.Count == 0)
            {
                lblTrangThai.Text = "Không có sinh viên nào thỏa điều kiện lọc.";
                MessageBox.Show("Không tìm thấy sinh viên nào phù hợp với điều kiện lọc!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblTrangThai.Text = $"Đã tải {_danhSachHienTai.Count} sinh viên (sắp xếp giảm dần theo điểm).";
            }
        }

        /// <summary>
        /// Cập nhật các chỉ số thống kê trên StatusStrip bằng LINQ
        /// </summary>
        private void CapNhatThongKe(List<SinhVien> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                lblTongSV.Text = "Tổng SV: 0";
                lblSoDat.Text = "Đạt: 0";
                lblSoKhongDat.Text = "Không đạt: 0";
                lblDiemTB.Text = "ĐTB: 0.00";
                return;
            }

            int tongSV = danhSach.Count;
            int soDat = danhSach.Count(sv => sv.Diem >= 5.0);
            int soKhongDat = tongSV - soDat;
            double dtb = Math.Round(danhSach.Average(sv => sv.Diem), 2);

            lblTongSV.Text = $"Tổng SV: {tongSV}";
            lblSoDat.Text = $"Đạt: {soDat}";
            lblSoKhongDat.Text = $"Không đạt: {soKhongDat}";
            lblDiemTB.Text = $"ĐTB: {dtb:0.00}";
        }

        /// <summary>
        /// Gom nhóm và thống kê số lượng, điểm trung bình theo từng lớp học bằng LINQ
        /// </summary>
        private void ThongKeTheoLop()
        {
            _isShowingThongKe = true;
            grpKetQua.Text = "Bảng tổng hợp thống kê kết quả học tập theo lớp";

            var thongKe = _danhSachSinhVien
                .GroupBy(sv => new { sv.MaLop, sv.TenLop })
                .Select(g => new ThongKeLop
                {
                    MaLop = g.Key.MaLop,
                    TenLop = g.Key.TenLop,
                    TongSoSV = g.Count(),
                    DiemTrungBinh = Math.Round(g.Average(sv => sv.Diem), 2),
                    SoLuongDat = g.Count(sv => sv.Diem >= 5.0),
                    SoLuongKhongDat = g.Count(sv => sv.Diem < 5.0)
                })
                .OrderBy(tk => tk.MaLop)
                .ToList();

            dgvBaoCao.DataSource = null;
            dgvBaoCao.AutoGenerateColumns = true;
            dgvBaoCao.DataSource = thongKe;

            DinhDangCotThongKe();

            // Cập nhật thống kê toàn trường
            CapNhatThongKe(_danhSachSinhVien);
            lblTrangThai.Text = $"Đã thống kê {thongKe.Count} lớp học bằng LINQ GroupBy.";
        }

        /// <summary>
        /// Định dạng tiêu đề cột và căn lề cho bảng danh sách sinh viên
        /// </summary>
        private void DinhDangCotSinhVien()
        {
            if (dgvBaoCao.Columns.Count == 0) return;

            if (dgvBaoCao.Columns["MaSV"] != null)
            {
                dgvBaoCao.Columns["MaSV"].HeaderText = "Mã SV";
                dgvBaoCao.Columns["MaSV"].FillWeight = 50F;
                dgvBaoCao.Columns["MaSV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvBaoCao.Columns["HoTen"] != null)
            {
                dgvBaoCao.Columns["HoTen"].HeaderText = "Họ và tên";
                dgvBaoCao.Columns["HoTen"].FillWeight = 100F;
            }
            if (dgvBaoCao.Columns["MaLop"] != null)
            {
                dgvBaoCao.Columns["MaLop"].HeaderText = "Mã lớp";
                dgvBaoCao.Columns["MaLop"].FillWeight = 50F;
                dgvBaoCao.Columns["MaLop"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvBaoCao.Columns["TenLop"] != null)
            {
                dgvBaoCao.Columns["TenLop"].HeaderText = "Tên lớp";
                dgvBaoCao.Columns["TenLop"].FillWeight = 90F;
            }
            if (dgvBaoCao.Columns["Diem"] != null)
            {
                dgvBaoCao.Columns["Diem"].HeaderText = "Điểm TB";
                dgvBaoCao.Columns["Diem"].FillWeight = 50F;
                dgvBaoCao.Columns["Diem"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBaoCao.Columns["Diem"].DefaultCellStyle.Format = "0.0";
            }
            if (dgvBaoCao.Columns["KetQua"] != null)
            {
                dgvBaoCao.Columns["KetQua"].HeaderText = "Kết quả";
                dgvBaoCao.Columns["KetQua"].FillWeight = 50F;
                dgvBaoCao.Columns["KetQua"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        /// <summary>
        /// Định dạng tiêu đề cột và căn lề cho bảng thống kê theo lớp
        /// </summary>
        private void DinhDangCotThongKe()
        {
            if (dgvBaoCao.Columns.Count == 0) return;

            if (dgvBaoCao.Columns["MaLop"] != null)
            {
                dgvBaoCao.Columns["MaLop"].HeaderText = "Mã lớp";
                dgvBaoCao.Columns["MaLop"].FillWeight = 50F;
                dgvBaoCao.Columns["MaLop"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvBaoCao.Columns["TenLop"] != null)
            {
                dgvBaoCao.Columns["TenLop"].HeaderText = "Tên lớp";
                dgvBaoCao.Columns["TenLop"].FillWeight = 100F;
            }
            if (dgvBaoCao.Columns["TongSoSV"] != null)
            {
                dgvBaoCao.Columns["TongSoSV"].HeaderText = "Tổng số SV";
                dgvBaoCao.Columns["TongSoSV"].FillWeight = 55F;
                dgvBaoCao.Columns["TongSoSV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvBaoCao.Columns["DiemTrungBinh"] != null)
            {
                dgvBaoCao.Columns["DiemTrungBinh"].HeaderText = "Điểm TB Lớp";
                dgvBaoCao.Columns["DiemTrungBinh"].FillWeight = 60F;
                dgvBaoCao.Columns["DiemTrungBinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBaoCao.Columns["DiemTrungBinh"].DefaultCellStyle.Format = "0.00";
            }
            if (dgvBaoCao.Columns["SoLuongDat"] != null)
            {
                dgvBaoCao.Columns["SoLuongDat"].HeaderText = "Số Đạt";
                dgvBaoCao.Columns["SoLuongDat"].FillWeight = 50F;
                dgvBaoCao.Columns["SoLuongDat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvBaoCao.Columns["SoLuongKhongDat"] != null)
            {
                dgvBaoCao.Columns["SoLuongKhongDat"].HeaderText = "Không Đạt";
                dgvBaoCao.Columns["SoLuongKhongDat"].FillWeight = 50F;
                dgvBaoCao.Columns["SoLuongKhongDat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvBaoCao.Columns["TiLeDat"] != null)
            {
                dgvBaoCao.Columns["TiLeDat"].HeaderText = "Tỉ lệ Đạt (%)";
                dgvBaoCao.Columns["TiLeDat"].FillWeight = 55F;
                dgvBaoCao.Columns["TiLeDat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBaoCao.Columns["TiLeDat"].DefaultCellStyle.Format = "0.0";
            }
        }

        /// <summary>
        /// Xuất dữ liệu đang hiển thị ra file CSV hoặc TXT qua SaveFileDialog
        /// </summary>
        private void XuatFile()
        {
            if (dgvBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất file!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Chọn vị trí lưu file báo cáo";
                sfd.Filter = "File CSV (Hỗ trợ Excel) (*.csv)|*.csv|File Text (*.txt)|*.txt";
                sfd.FileName = (_isShowingThongKe ? "ThongKe_Lop_" : "BaoCao_SinhVien_") + DateTime.Now.ToString("yyyyMMdd_HHmmss");

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLower();
                        if (ext == ".csv")
                        {
                            XuatFileCSV(sfd.FileName);
                        }
                        else
                        {
                            XuatFileTXT(sfd.FileName);
                        }

                        lblTrangThai.Text = $"Xuất file thành công: {Path.GetFileName(sfd.FileName)}";
                        MessageBox.Show(
                            $"Xuất dữ liệu ra file thành công!\nĐường dẫn: {sfd.FileName}",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        lblTrangThai.Text = "Lỗi khi ghi file!";
                        MessageBox.Show(
                            "Lỗi khi xuất file: " + ex.Message,
                            "Lỗi xuất file",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void XuatFileCSV(string filePath)
        {
            // Sử dụng UTF-8 with BOM để Excel đọc tiếng Việt không bị lỗi font
            using (var sw = new StreamWriter(filePath, false, new UTF8Encoding(true)))
            {
                // Ghi dòng tiêu đề cột
                var headerCols = new List<string>();
                foreach (DataGridViewColumn col in dgvBaoCao.Columns)
                {
                    headerCols.Add($"\"{col.HeaderText}\"");
                }
                sw.WriteLine(string.Join(",", headerCols));

                // Ghi từng dòng dữ liệu
                foreach (DataGridViewRow row in dgvBaoCao.Rows)
                {
                    if (row.IsNewRow) continue;
                    var cellValues = new List<string>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string val = cell.Value?.ToString() ?? "";
                        cellValues.Add($"\"{val.Replace("\"", "\"\"")}\"");
                    }
                    sw.WriteLine(string.Join(",", cellValues));
                }
            }
        }

        private void XuatFileTXT(string filePath)
        {
            using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("=========================================================================================");
                sw.WriteLine(_isShowingThongKe
                    ? "                         BÁO CÁO THỐNG KÊ KẾT QUẢ THEO LỚP"
                    : "                         BÁO CÁO DANH SÁCH SINH VIÊN THEO LỚP");
                sw.WriteLine($" Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                sw.WriteLine("=========================================================================================");

                if (!_isShowingThongKe)
                {
                    sw.WriteLine(string.Format("{0,-6} | {1,-8} | {2,-22} | {3,-24} | {4,8} | {5,-10}",
                        "STT", "Mã SV", "Họ và tên", "Lớp học", "Điểm TB", "Kết quả"));
                    sw.WriteLine(new string('-', 89));

                    int stt = 1;
                    foreach (var sv in _danhSachHienTai)
                    {
                        sw.WriteLine(string.Format("{0,-6} | {1,-8} | {2,-22} | {3,-24} | {4,8:0.0} | {5,-10}",
                            stt++, sv.MaSV, sv.HoTen, sv.TenLop, sv.Diem, sv.KetQua));
                    }
                    sw.WriteLine(new string('-', 89));
                    sw.WriteLine($" Tổng số sinh viên: {lblTongSV.Text.Replace("Tổng SV: ", "")} | " +
                                 $"{lblSoDat.Text} | {lblSoKhongDat.Text} | {lblDiemTB.Text}");
                }
                else
                {
                    sw.WriteLine(string.Format("{0,-6} | {1,-10} | {2,-24} | {3,8} | {4,10} | {5,7} | {6,9} | {7,11}",
                        "STT", "Mã lớp", "Tên lớp", "Tổng SV", "Điểm TB", "Số Đạt", "Không Đạt", "Tỉ lệ Đạt"));
                    sw.WriteLine(new string('-', 96));

                    var dsThongKe = dgvBaoCao.DataSource as List<ThongKeLop>;
                    if (dsThongKe != null)
                    {
                        int stt = 1;
                        foreach (var tk in dsThongKe)
                        {
                            sw.WriteLine(string.Format("{0,-6} | {1,-10} | {2,-24} | {3,8} | {4,10:0.00} | {5,7} | {6,9} | {7,10:0.0}%",
                                stt++, tk.MaLop, tk.TenLop, tk.TongSoSV, tk.DiemTrungBinh, tk.SoLuongDat, tk.SoLuongKhongDat, tk.TiLeDat));
                        }
                    }
                }

                sw.WriteLine("=========================================================================================");
            }
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            XemBaoCao();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ThongKeTheoLop();
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            XuatFile();
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XemBaoCao();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
