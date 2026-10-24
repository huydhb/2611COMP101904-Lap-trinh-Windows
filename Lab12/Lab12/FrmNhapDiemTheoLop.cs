using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Lab12.DAL;

namespace Lab12
{
    public partial class FrmNhapDiemTheoLop : Form
    {
        private readonly KetQuaDAL _dal = new KetQuaDAL();
        private readonly BindingSource _bsKetQua = new BindingSource();
        private DataTable _dtKetQua;

        public bool SilentMode { get; set; } = false;

        private void ShowMessage(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (!SilentMode)
            {
                MessageBox.Show(message, title, buttons, icon);
            }
        }

        public FrmNhapDiemTheoLop()
        {
            InitializeComponent();
        }

        private void FrmNhapDiemTheoLop_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachLop();
                LoadDanhSachMonHoc();
                lblTrangThai.Text = "Sẵn sàng. Vui lòng chọn Lớp và Môn học rồi bấm 'Tải danh sách'.";
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi khi nạp danh mục ban đầu: " + ex.Message, "Lỗi kết nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTrangThai.Text = "Lỗi kết nối cơ sở dữ liệu!";
            }
        }

        /// <summary>
        /// Nạp danh sách Lớp học vào ComboBox cboLop
        /// </summary>
        private void LoadDanhSachLop()
        {
            DataTable dtLop = _dal.GetDanhSachLop();
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            cboLop.DataSource = dtLop;
        }

        /// <summary>
        /// Nạp danh sách Môn học vào ComboBox cboMonHoc
        /// </summary>
        private void LoadDanhSachMonHoc()
        {
            DataTable dtMonHoc = _dal.GetDanhSachMonHoc();
            cboMonHoc.DisplayMember = "TenMon";
            cboMonHoc.ValueMember = "MaMon";
            cboMonHoc.DataSource = dtMonHoc;
        }

        /// <summary>
        /// Tải danh sách sinh viên và điểm theo Lớp và Môn học đã chọn
        /// </summary>
        private void btnTaiDanhSach_Click(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue == null || cboMonHoc.SelectedValue == null)
            {
                ShowMessage("Vui lòng chọn đầy đủ Lớp học và Môn học!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLop = cboLop.SelectedValue.ToString();
            string maMon = cboMonHoc.SelectedValue.ToString();

            try
            {
                _dtKetQua = _dal.GetKetQuaTheoLopMon(maLop, maMon);
                _bsKetQua.DataSource = _dtKetQua;
                dgvKetQua.DataSource = _bsKetQua;

                lblTrangThai.Text = $"Đã tải {_dtKetQua.Rows.Count} sinh viên cho lớp [{cboLop.Text}] - Môn [{cboMonHoc.Text}].";
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi khi tải danh sách: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTrangThai.Text = "Lỗi khi tải danh sách sinh viên.";
            }
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của dữ liệu điểm và lưu tất cả bằng SqlTransaction
        /// </summary>
        private void btnLuuTatCa_Click(object sender, EventArgs e)
        {
            // Kết thúc chỉnh sửa ô hiện tại trên DataGridView và BindingSource
            dgvKetQua.EndEdit();
            _bsKetQua.EndEdit();

            if (_dtKetQua == null || _dtKetQua.Rows.Count == 0)
            {
                ShowMessage("Chưa có danh sách sinh viên để lưu điểm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboMonHoc.SelectedValue == null)
            {
                ShowMessage("Vui lòng chọn môn học cần lưu điểm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMon = cboMonHoc.SelectedValue.ToString();

            // 1. Kiểm tra validation từng dòng trước khi gửi đến CSDL
            for (int i = 0; i < _dtKetQua.Rows.Count; i++)
            {
                DataRow row = _dtKetQua.Rows[i];
                if (row.RowState == DataRowState.Deleted)
                    continue;

                string maSV = row["MaSV"]?.ToString() ?? "";
                string hoTen = row["HoTen"]?.ToString() ?? "";
                object rawDiem = row["Diem"];

                // Điểm không được để trống / rỗng
                if (rawDiem == DBNull.Value || string.IsNullOrWhiteSpace(rawDiem?.ToString()))
                {
                    FocusCell(i, "colDiem");
                    ShowMessage(
                        $"Dòng {i + 1} (SV: {maSV} - {hoTen}): Điểm không được để trống!\nVui lòng nhập điểm hợp lệ từ 0 đến 10.",
                        "Kiểm tra dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Điểm phải là số thực
                string strDiem = rawDiem.ToString().Trim().Replace(',', '.');
                if (!double.TryParse(strDiem, NumberStyles.Any, CultureInfo.InvariantCulture, out double diem))
                {
                    FocusCell(i, "colDiem");
                    ShowMessage(
                        $"Dòng {i + 1} (SV: {maSV} - {hoTen}): Điểm '{rawDiem}' không hợp lệ!\nĐiểm phải là số thực từ 0 đến 10.",
                        "Kiểm tra dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Điểm phải nằm trong đoạn [0, 10]
                if (diem < 0.0 || diem > 10.0)
                {
                    FocusCell(i, "colDiem");
                    ShowMessage(
                        $"Dòng {i + 1} (SV: {maSV} - {hoTen}): Điểm {diem} không hợp lệ!\nĐiểm phải nằm trong khoảng từ 0 đến 10.",
                        "Kiểm tra dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật lại giá trị chuẩn số thực vào DataTable
                row["Diem"] = diem;
            }

            // 2. Thực hiện lưu bằng Transaction qua DAL
            bool thanhCong = _dal.LuuTatCaKetQua(maMon, _dtKetQua, out int soDongLuu, out string thongBaoLoi);

            if (thanhCong)
            {
                ShowMessage(
                    $"Lưu thành công {soDongLuu} dòng kết quả cho môn '{cboMonHoc.Text}'!\n(Transaction đã Commit thành công)",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                lblTrangThai.Text = $"Lưu thành công {soDongLuu} dòng điểm lúc {DateTime.Now:HH:mm:ss}.";

                // Nạp lại dữ liệu từ CSDL để kiểm chứng
                string maLop = cboLop.SelectedValue.ToString();
                _dtKetQua = _dal.GetKetQuaTheoLopMon(maLop, maMon);
                _bsKetQua.DataSource = _dtKetQua;
                dgvKetQua.DataSource = _bsKetQua;
            }
            else
            {
                ShowMessage(
                    $"Lưu dữ liệu thất bại!\nĐã Rollback toàn bộ giao dịch (không có dòng nào bị thay đổi).\nChi tiết lỗi: {thongBaoLoi}",
                    "Lỗi Transaction - Đã Rollback",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                lblTrangThai.Text = $"Lưu thất bại! Đã Rollback toàn bộ dữ liệu lúc {DateTime.Now:HH:mm:ss}.";
            }
        }

        private void FocusCell(int rowIndex, string columnName)
        {
            if (rowIndex >= 0 && rowIndex < dgvKetQua.Rows.Count && dgvKetQua.Columns.Contains(columnName))
            {
                dgvKetQua.CurrentCell = dgvKetQua.Rows[rowIndex].Cells[columnName];
                dgvKetQua.BeginEdit(true);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
