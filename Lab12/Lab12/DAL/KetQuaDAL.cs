using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Lab12.DAL
{
    public class KetQuaDAL
    {
        /// <summary>
        /// Lấy toàn bộ danh sách lớp học thông qua stored procedure sp_Lop_GetAll
        /// </summary>
        public DataTable GetDanhSachLop()
        {
            var dt = new DataTable();
            using (var conn = Database.GetConnection())
            using (var cmd = new SqlCommand("sp_Lop_GetAll", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách môn học thông qua stored procedure sp_MonHoc_GetAll
        /// </summary>
        public DataTable GetDanhSachMonHoc()
        {
            var dt = new DataTable();
            using (var conn = Database.GetConnection())
            using (var cmd = new SqlCommand("sp_MonHoc_GetAll", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách kết quả học tập của sinh viên theo lớp và môn học
        /// </summary>
        public DataTable GetKetQuaTheoLopMon(string maLop, string maMon)
        {
            var dt = new DataTable();
            using (var conn = Database.GetConnection())
            using (var cmd = new SqlCommand("sp_KetQua_GetByLopMon", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaLop", SqlDbType.VarChar, 10).Value = maLop ?? (object)DBNull.Value;
                cmd.Parameters.Add("@MaMon", SqlDbType.VarChar, 10).Value = maMon ?? (object)DBNull.Value;

                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lưu toàn bộ điểm của danh sách bằng SqlTransaction.
        /// Tự động Rollback nếu có lỗi xảy ra ở bất kỳ dòng nào.
        /// </summary>
        public bool LuuTatCaKetQua(string maMon, DataTable dtKetQua, out int soDongLuu, out string thongBaoLoi)
        {
            soDongLuu = 0;
            thongBaoLoi = string.Empty;

            if (dtKetQua == null || dtKetQua.Rows.Count == 0)
            {
                thongBaoLoi = "Không có dữ liệu sinh viên để lưu.";
                return false;
            }

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dtKetQua.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted)
                                continue;

                            string maSV = row["MaSV"]?.ToString();
                            if (string.IsNullOrWhiteSpace(maSV))
                                continue;

                            double diem = Convert.ToDouble(row["Diem"]);
                            string ghiChu = row["GhiChu"] != DBNull.Value ? row["GhiChu"].ToString() : string.Empty;

                            using (var cmd = new SqlCommand("sp_KetQua_Save", conn, trans))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@MaSV", SqlDbType.VarChar, 10).Value = maSV;
                                cmd.Parameters.Add("@MaMon", SqlDbType.VarChar, 10).Value = maMon;
                                cmd.Parameters.Add("@Diem", SqlDbType.Float).Value = diem;
                                cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 255).Value = 
                                    string.IsNullOrEmpty(ghiChu) ? (object)DBNull.Value : ghiChu;

                                cmd.ExecuteNonQuery();
                            }

                            soDongLuu++;
                        }

                        // Commit toàn bộ giao dịch khi mọi dòng đều thực thi thành công
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Rollback hủy toàn bộ nếu có bất kỳ lỗi nào xảy ra
                        try
                        {
                            trans.Rollback();
                        }
                        catch
                        {
                        }

                        soDongLuu = 0;
                        thongBaoLoi = ex.Message;
                        return false;
                    }
                }
            }
        }
    }
}
