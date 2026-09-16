using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace QuanLyNhanVien
{
    // ===== Lớp cơ sở =====
    class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }

        private double luongCoBan;
        public double LuongCoBan
        {
            get => luongCoBan;
            set
            {
                if (value > 0)
                {
                    luongCoBan = value;
                }
                else
                {
                    throw new Exception("Lương cơ bản phải > 0");
                }
            }
        }

        public NhanVien(string maNV, string hoTen, double luongCoBan)
        {
            MaNV = maNV;
            HoTen = hoTen;
            LuongCoBan = luongCoBan;
        }

        public virtual double TinhLuong() => LuongCoBan;

        public virtual void HienThiThongTin()
            => Console.WriteLine($"{MaNV,-8} | {HoTen,-20} | NV thường       | Lương: {TinhLuong(),12:N0} VNĐ");
    }

    // ===== Lớp dẫn xuất: Nhân viên văn phòng =====
    class NhanVienVanPhong : NhanVien
    {
        private int soNgayLamViec;
        public int SoNgayLamViec
        {
            get => soNgayLamViec;
            set
            {
                if (value >= 0 && value <= 31)
                {
                    soNgayLamViec = value;
                }
                else
                {
                    throw new Exception("Số ngày làm việc phải từ 0 đến 31");
                }
            }
        }

        public NhanVienVanPhong(string maNV, string hoTen, double luongCoBan, int soNgayLamViec)
            : base(maNV, hoTen, luongCoBan)
        {
            SoNgayLamViec = soNgayLamViec;
        }

        public override double TinhLuong() => LuongCoBan + SoNgayLamViec * 200000;

        public override void HienThiThongTin()
            => Console.WriteLine($"{MaNV,-8} | {HoTen,-20} | Văn phòng       | Lương: {TinhLuong(),12:N0} VNĐ (Ngày làm: {SoNgayLamViec})");
    }

    // ===== Lớp dẫn xuất: Nhân viên kinh doanh =====
    class NhanVienKinhDoanh : NhanVien
    {
        private double doanhSo;
        public double DoanhSo
        {
            get => doanhSo;
            set
            {
                if (value >= 0)
                {
                    doanhSo = value;
                }
                else
                {
                    throw new Exception("Doanh số phải >= 0");
                }
            }
        }

        public NhanVienKinhDoanh(string maNV, string hoTen, double luongCoBan, double doanhSo)
            : base(maNV, hoTen, luongCoBan)
        {
            DoanhSo = doanhSo;
        }

        public override double TinhLuong() => LuongCoBan + 0.05 * DoanhSo;

        public override void HienThiThongTin()
            => Console.WriteLine($"{MaNV,-8} | {HoTen,-20} | Kinh doanh      | Lương: {TinhLuong(),12:N0} VNĐ (Doanh số: {DoanhSo:N0})");
    }

    // ===== Lớp dẫn xuất: Nhân viên thời vụ =====
    class NhanVienThoiVu : NhanVien
    {
        private double soGioLam;
        public double SoGioLam
        {
            get => soGioLam;
            set
            {
                if (value >= 0)
                {
                    soGioLam = value;
                }
                else
                {
                    throw new Exception("Số giờ làm phải >= 0");
                }
            }
        }

        private double luongTheoGio;
        public double LuongTheoGio
        {
            get => luongTheoGio;
            set
            {
                if (value > 0)
                {
                    luongTheoGio = value;
                }
                else
                {
                    throw new Exception("Lương theo giờ phải > 0");
                }
            }   
        }

        public NhanVienThoiVu(string maNV, string hoTen, double soGioLam, double luongTheoGio)
            : base(maNV, hoTen, 1)
        {
            SoGioLam = soGioLam;
            LuongTheoGio = luongTheoGio;
        }

        public override double TinhLuong() => SoGioLam * LuongTheoGio;

        public override void HienThiThongTin()
            => Console.WriteLine($"{MaNV,-8} | {HoTen,-20} | Thời vụ (Bonus) | Lương: {TinhLuong(),12:N0} VNĐ (Giờ: {SoGioLam}, Giá: {LuongTheoGio:N0}/h)");
    }

    // ===== Chương trình chính =====
    class Program
    {
        static List<NhanVien> danhSach = new List<NhanVien>();

        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            NhapDanhSach();

            int chon;
            do
            {
                Console.WriteLine("\n========== MENU ==========");
                Console.WriteLine("1. Xuất danh sách nhân viên");
                Console.WriteLine("2. Tìm nhân viên theo mã");
                Console.WriteLine("3. Tìm nhân viên có lương cao nhất");
                Console.WriteLine("4. Tính tổng lương công ty phải trả");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn: ");
                chon = int.TryParse(Console.ReadLine(), out int c) ? c : -1;

                switch (chon)
                {
                    case 1:
                        Console.WriteLine("\n--- DANH SÁCH NHÂN VIÊN ---");
                        // Đa hình: gọi HienThiThongTin()
                        foreach (var nv in danhSach)
                        {
                            nv.HienThiThongTin();
                        }
                        break;

                    case 2:
                        Console.Write("\nNhập mã nhân viên cần tìm: ");
                        string ma = Console.ReadLine()?.Trim() ?? "";
                        var kq = danhSach.Find(nv => nv.MaNV.Equals(ma, StringComparison.OrdinalIgnoreCase));
                        if (kq != null)
                        {
                            Console.WriteLine(">> Kết quả tìm kiếm:");
                            kq.HienThiThongTin();
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy!");
                        }
                        break;

                    case 3:
                        if (danhSach.Count == 0)
                        {
                            Console.WriteLine("Danh sách rỗng!");
                            break;
                        }
                        // Đa hình: gọi TinhLuong()
                        var max = danhSach[0];
                        foreach (var nv in danhSach)
                        {
                            if (nv.TinhLuong() > max.TinhLuong())
                            {
                                max = nv;
                            }
                        }
                        Console.WriteLine("\n>> Nhân viên có lương cao nhất:");
                        max.HienThiThongTin();
                        break;

                    case 4:
                        // Đa hình: tính tổng lương
                        double tong = 0;
                        foreach (var nv in danhSach)
                        {
                            tong += nv.TinhLuong();
                        }
                        Console.WriteLine($"\n>> Tổng lương: {tong:N0} VNĐ");
                        break;

                    case 0:
                        Console.WriteLine("Thoát chương trình. Tạm biệt!");
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
            } while (chon != 0);
        }

        static void NhapDanhSach()
        {
            Console.WriteLine("--- NHẬP DANH SÁCH NHÂN VIÊN ---");
            int n = 0;
            while (n < 5)
            {
                Console.Write("Nhập số lượng nhân viên (>= 5): ");
                if (!int.TryParse(Console.ReadLine(), out n) || n < 5)
                {
                    Console.WriteLine("Số lượng phải là số nguyên >= 5. Vui lòng nhập lại!");
                }
            }

            for (int i = 1; i <= n; i++)
            {
                Console.WriteLine($"\n-- Nhân viên {i}/{n} --");
                Console.Write("Loại (1-VănPhòng, 2-KinhDoanh, 3-ThờiVụ): ");
                string loai = Console.ReadLine()?.Trim() ?? "1";

                Console.Write("Mã NV: ");
                string ma = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Họ tên: ");
                string ten = Console.ReadLine()?.Trim() ?? "";

                try
                {
                    NhanVien nv;
                    if (loai == "1")
                    {
                        Console.Write("Lương cơ bản (> 0): ");
                        double lcb = double.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

                        Console.Write("Số ngày làm việc (0-31): ");
                        int ngay = int.Parse(Console.ReadLine() ?? "0");

                        nv = new NhanVienVanPhong(ma, ten, lcb, ngay);
                    }
                    else if (loai == "2")
                    {
                        Console.Write("Lương cơ bản (> 0): ");
                        double lcb = double.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

                        Console.Write("Doanh số (>= 0): ");
                        double ds = double.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

                        nv = new NhanVienKinhDoanh(ma, ten, lcb, ds);
                    }
                    else
                    {
                        Console.Write("Số giờ làm (>= 0): ");
                        double gio = double.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

                        Console.Write("Lương theo giờ (> 0): ");
                        double luongGio = double.Parse(Console.ReadLine() ?? "0", CultureInfo.InvariantCulture);

                        nv = new NhanVienThoiVu(ma, ten, gio, luongGio);
                    }

                    danhSach.Add(nv);
                    Console.WriteLine("=> Thêm thành công!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Lỗi]: {ex.Message}. Vui lòng nhập lại nhân viên này!");
                    i--;
                }
            }
        }
    }
}