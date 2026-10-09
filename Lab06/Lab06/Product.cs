using System;

namespace Lab06
{
    /// <summary>
    /// Lớp biểu diễn thông tin sản phẩm (Product Model)
    /// </summary>
    public class Product
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgayNhap { get; set; }
        public bool ConKinhDoanh { get; set; }
        public string DuongDanAnh { get; set; }

        /// <summary>
        /// Thuộc tính tính toán: Tổng giá trị tồn kho của sản phẩm
        /// </summary>
        public decimal ThanhTien => DonGia * SoLuong;

        public Product()
        {
            MaSP = string.Empty;
            TenSP = string.Empty;
            LoaiSP = string.Empty;
            DonGia = 0;
            SoLuong = 0;
            NgayNhap = DateTime.Today;
            ConKinhDoanh = true;
            DuongDanAnh = string.Empty;
        }

        public Product(string maSP, string tenSP, string loaiSP, decimal donGia, int soLuong, DateTime ngayNhap, bool conKinhDoanh, string duongDanAnh = "")
        {
            MaSP = maSP;
            TenSP = tenSP;
            LoaiSP = loaiSP;
            DonGia = donGia;
            SoLuong = soLuong;
            NgayNhap = ngayNhap;
            ConKinhDoanh = conKinhDoanh;
            DuongDanAnh = duongDanAnh ?? string.Empty;
        }
    }
}
