using System;

namespace Lab13.Models
{
    public class ThongKeLop
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public int TongSoSV { get; set; }
        public double DiemTrungBinh { get; set; }
        public int SoLuongDat { get; set; }
        public int SoLuongKhongDat { get; set; }

        /// <summary>
        /// Tỉ lệ sinh viên Đạt trên tổng số sinh viên của lớp (%)
        /// </summary>
        public double TiLeDat => TongSoSV > 0 ? Math.Round((double)SoLuongDat / TongSoSV * 100, 1) : 0;
    }
}
