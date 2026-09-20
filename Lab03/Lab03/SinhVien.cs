using System;

namespace Lab03
{
    public class SinhVien : Nguoi
    {
        public string MaSinhVien { get; set; }
        public string MaLop { get; set; }

        private double _diemTrungBinh;

        // Điểm trung bình chỉ nhận giá trị hợp lệ từ 0 đến 10
        public double DiemTrungBinh
        {
            get { return _diemTrungBinh; }
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentException("Điểm trung bình phải nằm trong khoảng từ 0 đến 10.");
                }
                _diemTrungBinh = value;
            }
        }

        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 9.0)
            {
                return "Xuất sắc";
            }
            else if (DiemTrungBinh >= 8.0)
            {
                return "Giỏi";
            }
            else if (DiemTrungBinh >= 6.5)
            {
                return "Khá";
            }
            else if (DiemTrungBinh >= 5.0)
            {
                return "Trung bình";
            }
            else
            {
                return "Yếu";
            }
        }

        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien} | Họ tên: {HoTen} | Ngày sinh: {NgaySinh:dd/MM/yyyy} | Lớp: {MaLop} | ĐTB: {DiemTrungBinh:F2} | Xếp loại: {XepLoai()}";
        }
    }
}
