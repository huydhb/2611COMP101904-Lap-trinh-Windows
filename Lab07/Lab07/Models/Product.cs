namespace Lab07.Models
{
    public class Product
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiSP { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien => DonGia * SoLuong;

        public Product() { }

        public Product(string maSP, string tenSP, string loaiSP, decimal donGia, int soLuong)
        {
            MaSP = maSP;
            TenSP = tenSP;
            LoaiSP = loaiSP;
            DonGia = donGia;
            SoLuong = soLuong;
        }
    }
}
