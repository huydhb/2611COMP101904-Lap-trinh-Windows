using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Lab11
{
    public partial class FrmLop : Form
    {
        // ── Chuỗi kết nối lấy từ App.config hoặc mặc định ──
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["EduManagerDB"]?.ConnectionString
            ?? @"Server=localhost;Database=EduManagerDB;Integrated Security=True;TrustServerCertificate=True;";

        public FrmLop()
        {
            InitializeComponent();
        }

        #region Form Load & Nạp dữ liệu

        private void FrmLop_Load(object sender, EventArgs e)
        {
            try
            {
                LoadKhoa();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối cơ sở dữ liệu:\n" + ex.Message + "\n\nVui lòng kiểm tra chuỗi kết nối trong App.config.",
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nạp danh sách Khoa vào ComboBox cboKhoa
        /// </summary>
        internal void LoadKhoa()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT MaKhoa, TenKhoa FROM KHOA ORDER BY TenKhoa";
                using (var da = new SqlDataAdapter(sql, conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    cboKhoa.DataSource = dt;
                    cboKhoa.DisplayMember = "TenKhoa";
                    cboKhoa.ValueMember = "MaKhoa";
                }
            }
        }

        /// <summary>
        /// Nạp toàn bộ danh sách lớp học lên DataGridView
        /// </summary>
        internal void LoadData()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"SELECT L.MaLop, L.TenLop, K.TenKhoa, L.MaKhoa 
                               FROM LOP L 
                               LEFT JOIN KHOA K ON L.MaKhoa = K.MaKhoa 
                               ORDER BY L.MaLop";
                using (var da = new SqlDataAdapter(sql, conn))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvLop.DataSource = dt;
                    lblTongSo.Text = $"Tổng số: {dt.Rows.Count} lớp";
                }
            }
        }

        #endregion

        #region Tương tác DataGridView

        /// <summary>
        /// Khi click chọn dòng trên DataGridView, đổ dữ liệu lên TextBox và khóa txtMaLop
        /// </summary>
        private void dgvLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLop.Rows.Count)
            {
                var row = dgvLop.Rows[e.RowIndex];
                txtMaLop.Text = row.Cells["colMaLop"].Value?.ToString() ?? "";
                txtTenLop.Text = row.Cells["colTenLop"].Value?.ToString() ?? "";

                if (row.Cells["colMaKhoa"].Value != null)
                {
                    cboKhoa.SelectedValue = row.Cells["colMaKhoa"].Value.ToString();
                }

                txtMaLop.ReadOnly = true;
            }
        }

        /// <summary>
        /// Chọn dòng theo chỉ số để tương tác hoặc kiểm thử
        /// </summary>
        internal void SelectRowByIndex(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgvLop.Rows.Count)
            {
                dgvLop.ClearSelection();
                dgvLop.Rows[rowIndex].Selected = true;
                dgvLop_CellClick(dgvLop, new DataGridViewCellEventArgs(0, rowIndex));
            }
        }

        #endregion

        #region Validation & Kiểm tra trùng mã

        /// <summary>
        /// Kiểm tra dữ liệu đầu vào trước khi thêm/sửa
        /// </summary>
        private bool ValidateInput(bool isAdding)
        {
            string maLop = txtMaLop.Text.Trim();
            string tenLop = txtTenLop.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLop))
            {
                MessageBox.Show("Vui lòng nhập mã lớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLop.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(tenLop))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLop.Focus();
                return false;
            }

            if (cboKhoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khoa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoa.Focus();
                return false;
            }

            if (isAdding && IsMaLopExists(maLop))
            {
                MessageBox.Show($"Mã lớp '{maLop}' đã tồn tại trong hệ thống!", "Cảnh báo trùng mã",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLop.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra mã lớp đã tồn tại trong database hay chưa
        /// </summary>
        private bool IsMaLopExists(string maLop)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM LOP WHERE MaLop = @MaLop";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Xóa trắng các ô nhập liệu và đưa form về trạng thái sẵn sàng nhập mới
        /// </summary>
        internal void ResetForm()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtMaLop.ReadOnly = false;
            if (cboKhoa.Items.Count > 0)
            {
                cboKhoa.SelectedIndex = 0;
            }
            txtMaLop.Focus();
        }

        #endregion

        #region Các chức năng CRUD

        /// <summary>
        /// F3: Thêm lớp mới
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput(isAdding: true)) return;

                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO LOP (MaLop, TenLop, MaKhoa) VALUES (@MaLop, @TenLop, @MaKhoa)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                        cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.SelectedValue.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thêm lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// F4: Cập nhật thông tin lớp học theo mã lớp đang chọn
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaLop.Text) || !txtMaLop.ReadOnly)
                {
                    MessageBox.Show("Vui lòng chọn một lớp học từ danh sách để sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput(isAdding: false)) return;

                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE LOP SET TenLop = @TenLop, MaKhoa = @MaKhoa WHERE MaLop = @MaLop";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaKhoa", cboKhoa.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@MaLop", txtMaLop.Text.Trim());
                        int affected = cmd.ExecuteNonQuery();
                        if (affected == 0)
                        {
                            MessageBox.Show("Không tìm thấy lớp học để cập nhật!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                MessageBox.Show("Cập nhật thông tin lớp học thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// F5: Xóa lớp học, hỏi xác nhận và bắt lỗi ràng buộc khóa ngoại (lớp có sinh viên)
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaLop.Text) || !txtMaLop.ReadOnly)
                {
                    MessageBox.Show("Vui lòng chọn một lớp học từ danh sách để xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maLop = txtMaLop.Text.Trim();
                string tenLop = txtTenLop.Text.Trim();

                var confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa lớp '{maLop}' ({tenLop}) không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = "DELETE FROM LOP WHERE MaLop = @MaLop";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Xóa lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                LoadData();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show(
                    "Không thể xóa lớp này vì đang có sinh viên trực thuộc (vi phạm ràng buộc khóa ngoại)!",
                    "Lỗi ràng buộc toàn vẹn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa lớp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// F7: Xóa nội dung nhập, mở khóa mã lớp và nạp lại toàn bộ dữ liệu
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtTimKiem.Clear();
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lại dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Tìm kiếm

        /// <summary>
        /// F6: Tìm kiếm theo mã lớp hoặc tên lớp bằng LIKE và SqlParameter
        /// </summary>
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemLop();
        }

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimKiemLop();
                e.SuppressKeyPress = true;
            }
        }

        internal void TimKiemLop()
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT L.MaLop, L.TenLop, K.TenKhoa, L.MaKhoa 
                                   FROM LOP L 
                                   LEFT JOIN KHOA K ON L.MaKhoa = K.MaKhoa 
                                   WHERE L.MaLop LIKE @kw OR L.TenLop LIKE @kw
                                   ORDER BY L.MaLop";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@kw", "%" + tuKhoa + "%");
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            var dt = new DataTable();
                            da.Fill(dt);
                            dgvLop.DataSource = dt;
                            lblTongSo.Text = $"Kết quả tìm kiếm: {dt.Rows.Count} lớp";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
