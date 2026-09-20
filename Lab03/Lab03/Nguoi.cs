using System;

namespace Lab03
{
    public class Nguoi
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }

        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }

        // Cho phép class con ghi đè để bổ sung thông tin
        public virtual string LayThongTin()
        {
            return $"Họ tên: {HoTen}, Ngày sinh: {NgaySinh:dd/MM/yyyy}";
        }
    }
}
