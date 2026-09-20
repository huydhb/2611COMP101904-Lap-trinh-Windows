using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab03
{
    public class QuanLySinhVien
    {
        private List<SinhVien> _danhSach;

        public QuanLySinhVien()
        {
            _danhSach = new List<SinhVien>();
        }

        // Thêm sinh viên mới, kiểm tra trùng mã sinh viên
        public bool Them(SinhVien sv)
        {
            if (sv == null)
            {
                return false;
            }

            bool daTonTai = _danhSach.Any(s => s.MaSinhVien.Equals(sv.MaSinhVien, StringComparison.OrdinalIgnoreCase));
            if (daTonTai)
            {
                return false;
            }

            _danhSach.Add(sv);
            return true;
        }

        public List<SinhVien> LayDanhSach()
        {
            return _danhSach;
        }

        public SinhVien? TimTheoMa(string maSV)
        {
            return _danhSach.FirstOrDefault(s => s.MaSinhVien.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        // Tìm sinh viên có họ tên chứa từ khóa
        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return _danhSach
                .Where(s => s.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool Sua(string maSV, double diemMoi)
        {
            SinhVien? sv = TimTheoMa(maSV);
            if (sv == null)
            {
                return false;
            }

            try
            {
                sv.DiemTrungBinh = diemMoi;
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public bool Xoa(string maSV)
        {
            SinhVien? sv = TimTheoMa(maSV);
            if (sv == null)
            {
                return false;
            }

            _danhSach.Remove(sv);
            return true;
        }

        public List<SinhVien> SapXepTheoDiem()
        {
            return _danhSach
                .OrderByDescending(s => s.DiemTrungBinh)
                .ToList();
        }

        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSach
                .Where(s => s.DiemTrungBinh >= 5.0)
                .ToList();
        }
    }
}
