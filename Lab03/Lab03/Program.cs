using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            QuanLySinhVien qlsv = new QuanLySinhVien();
            int choice = -1;

            do
            {
                HienMenu();
                Console.Write("Chọn chức năng: ");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("\nLựa chọn không hợp lệ. Vui lòng nhập số từ 0 - 8.");
                    NhanEnterDeTiepTuc();
                    continue;
                }

                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        ThemSinhVien(qlsv);
                        break;
                    case 2:
                        XuatDanhSach(qlsv);
                        break;
                    case 3:
                        TimTheoMa(qlsv);
                        break;
                    case 4:
                        TimTheoTen(qlsv);
                        break;
                    case 5:
                        SuaDiemTrungBinh(qlsv);
                        break;
                    case 6:
                        XoaSinhVien(qlsv);
                        break;
                    case 7:
                        SapXepTheoDiemGiamDan(qlsv);
                        break;
                    case 8:
                        LocSinhVienDat(qlsv);
                        break;
                    case 0:
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("Chức năng không tồn tại. Vui lòng chọn lại");
                        break;
                }

                if (choice != 0)
                {
                    NhanEnterDeTiepTuc();
                }

            } while (choice != 0);
        }

        static void HienMenu()
        {
            Console.WriteLine("\n===== QUẢN LÝ SINH VIÊN =====");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Xuất danh sách");
            Console.WriteLine("3. Tìm sinh viên theo mã");
            Console.WriteLine("4. Tìm sinh viên theo tên");
            Console.WriteLine("5. Sửa điểm trung bình");
            Console.WriteLine("6. Xóa sinh viên");
            Console.WriteLine("7. Sắp xếp theo điểm giảm dần");
            Console.WriteLine("8. Lọc sinh viên đạt");
            Console.WriteLine("0. Thoát");
        }

        static void ThemSinhVien(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- THÊM SINH VIÊN MỚI ---");

            string maSV;
            while (true)
            {
                maSV = NhapChuoi("Nhập mã sinh viên: ");
                if (qlsv.TimTheoMa(maSV) != null)
                {
                    Console.WriteLine("Mã sinh viên đã tồn tại. Vui lòng nhập mã khác!");
                }
                else
                {
                    break;
                }
            }

            string hoTen = NhapChuoi("Nhập họ và tên: ");
            DateTime ngaySinh = NhapNgaySinh("Nhập ngày sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoi("Nhập mã lớp: ");
            double diemTB = NhapDiem("Nhập điểm trung bình (0.0 - 10.0): ");

            SinhVien svMoi = new SinhVien(maSV, hoTen, ngaySinh, maLop, diemTB);
            if (qlsv.Them(svMoi))
            {
                Console.WriteLine("\nThêm sinh viên thành công!");
            }
            else
            {
                Console.WriteLine("\nThêm sinh viên thất bại (trùng mã sinh viên).");
            }
        }

        static void XuatDanhSach(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- DANH SÁCH SINH VIÊN ---");
            InDanhSachSinhVien(qlsv.LayDanhSach());
        }

        static void TimTheoMa(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- TÌM SINH VIÊN THEO MÃ ---");
            string maSV = NhapChuoi("Nhập mã sinh viên cần tìm: ");

            SinhVien? sv = qlsv.TimTheoMa(maSV);
            if (sv != null)
            {
                Console.WriteLine("\nTìm thấy sinh viên:");
                Console.WriteLine(sv.LayThongTin());
            }
            else
            {
                Console.WriteLine($"\nKhông tìm thấy sinh viên có mã: {maSV}");
            }
        }

        static void TimTheoTen(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- TÌM SINH VIÊN THEO TÊN ---");
            string tuKhoa = NhapChuoi("Nhập từ khóa họ tên cần tìm: ");

            List<SinhVien> ketQua = qlsv.TimTheoTen(tuKhoa);
            Console.WriteLine($"\nKết quả tìm kiếm với từ khóa \"{tuKhoa}\":");
            InDanhSachSinhVien(ketQua);
        }

        static void SuaDiemTrungBinh(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- SỬA ĐIỂM TRUNG BÌNH ---");
            string maSV = NhapChuoi("Nhập mã sinh viên cần sửa điểm: ");

            SinhVien? sv = qlsv.TimTheoMa(maSV);
            if (sv == null)
            {
                Console.WriteLine($"\nKhông tìm thấy sinh viên có mã: {maSV}");
                return;
            }

            Console.WriteLine("\nThông tin hiện tại của sinh viên:");
            Console.WriteLine(sv.LayThongTin());

            double diemMoi = NhapDiem("\nNhập điểm trung bình mới (0.0 - 10.0): ");
            if (qlsv.Sua(maSV, diemMoi))
            {
                Console.WriteLine("\nCập nhật điểm trung bình thành công!");
                Console.WriteLine(sv.LayThongTin());
            }
            else
            {
                Console.WriteLine("\nCập nhật điểm thất bại!");
            }
        }

        static void XoaSinhVien(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- XÓA SINH VIÊN ---");
            string maSV = NhapChuoi("Nhập mã sinh viên cần xóa: ");

            if (qlsv.Xoa(maSV))
            {
                Console.WriteLine($"\nĐã xóa thành công sinh viên có mã: {maSV}");
            }
            else
            {
                Console.WriteLine($"\nKhông tìm thấy sinh viên có mã: {maSV}");
            }
        }

        static void SapXepTheoDiemGiamDan(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- DANH SÁCH SẮP XẾP THEO ĐIỂM GIẢM DẦN ---");
            InDanhSachSinhVien(qlsv.SapXepTheoDiem());
        }

        static void LocSinhVienDat(QuanLySinhVien qlsv)
        {
            Console.WriteLine("--- DANH SÁCH SINH VIÊN ĐẠT (ĐIỂM >= 5.0) ---");
            InDanhSachSinhVien(qlsv.LocSinhVienDat());
        }

        static void InDanhSachSinhVien(List<SinhVien> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống (không có sinh viên nào).");
                return;
            }

            Console.WriteLine($"Tổng số sinh viên: {danhSach.Count}");
            Console.WriteLine(new string('-', 85));
            int stt = 1;
            foreach (SinhVien sv in danhSach)
            {
                Console.WriteLine($"{stt++}. {sv.LayThongTin()}");
            }
            Console.WriteLine(new string('-', 85));
        }

        static string NhapChuoi(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("Dữ liệu không được để trống. Vui lòng nhập lại!");
            }
        }

        static DateTime NhapNgaySinh(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();
                if (DateTime.TryParseExact(input?.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaySinh))
                {
                    return ngaySinh;
                }
                Console.WriteLine("Ngày sinh không hợp lệ (định dạng đúng: dd/MM/yyyy, ví dụ: 15/05/2004). Vui lòng nhập lại!");
            }
        }

        static double NhapDiem(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Chuyển dấu phẩy thành dấu chấm để parse số thực chuẩn xác
                    string formattedInput = input.Trim().Replace(',', '.');
                    if (double.TryParse(formattedInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double diem))
                    {
                        if (diem >= 0 && diem <= 10)
                        {
                            return diem;
                        }
                    }
                }

                Console.WriteLine("Điểm không hợp lệ. Điểm phải là số thực từ 0.0 đến 10.0. Vui lòng nhập lại!");
            }
        }

        static void NhanEnterDeTiepTuc()
        {
            Console.WriteLine("\nNhấn Enter để tiếp tục...");
            Console.ReadLine();
        }
    }
}
