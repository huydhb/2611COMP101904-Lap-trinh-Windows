namespace Lab13.Models
{
    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public double Diem { get; set; }

        /// <summary>
        /// Thuộc tính chỉ đọc tính toán kết quả: "Đạt" nếu Điểm >= 5.0, ngược lại "Không đạt"
        /// </summary>
        public string KetQua => Diem >= 5.0 ? "Đạt" : "Không đạt";
    }
}
