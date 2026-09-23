using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            ProductService service = new ProductService();

            // Đăng ký nhận sự kiện khi sản phẩm được thêm hoặc xóa thành công
            service.OnProductAdded += p => Console.WriteLine($"\n[Sự kiện] Thêm thành công sản phẩm: {p.TenSP} (Mã: {p.MaSP})");
            service.OnProductRemoved += p => Console.WriteLine($"\n[Sự kiện] Xóa thành công sản phẩm: {p.TenSP} (Mã: {p.MaSP})");

            int choice = -1;

            do
            {
                HienMenu();
                Console.Write("Chọn: ");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("\nLựa chọn không hợp lệ. Vui lòng nhập số từ 0 đến 7.");
                    NhanEnterDeTiepTuc();
                    continue;
                }

                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        ThemSanPham(service);
                        break;
                    case 2:
                        XuatDanhSach(service);
                        break;
                    case 3:
                        TimTheoMa(service);
                        break;
                    case 4:
                        TimTheoTen(service);
                        break;
                    case 5:
                        LocTheoKhoangGia(service);
                        break;
                    case 6:
                        XoaSanPham(service);
                        break;
                    case 7:
                        TinhTongGiaTriKho(service);
                        break;
                    case 0:
                        Console.WriteLine("Thoát chương trình. Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Chức năng không tồn tại. Vui lòng chọn lại.");
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
            Console.WriteLine("\n===== PRODUCT MANAGER =====");
            Console.WriteLine("1. Them san pham");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim theo ma");
            Console.WriteLine("4. Tim theo ten");
            Console.WriteLine("5. Loc theo khoang gia");
            Console.WriteLine("6. Xoa san pham");
            Console.WriteLine("7. Tinh tong gia tri kho");
            Console.WriteLine("0. Thoat");
        }

        static void ThemSanPham(ProductService service)
        {
            Console.WriteLine("--- THÊM SẢN PHẨM MỚI ---");
            string maSP = NhapChuoi("Nhập mã sản phẩm: ");
            string tenSP = NhapChuoi("Nhập tên sản phẩm: ");
            decimal gia = NhapSoThucDuong("Nhập đơn giá: ");
            int soLuong = NhapSoNguyenDuong("Nhập số lượng: ");

            try
            {
                Product p = new Product(maSP, tenSP, gia, soLuong);
                service.AddProduct(p);
            }
            catch (DuplicateProductException ex)
            {
                Console.WriteLine($"\n[Lỗi nghiệp vụ] {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\n[Lỗi dữ liệu] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Lỗi không xác định] {ex.Message}");
            }
        }

        static void XuatDanhSach(ProductService service)
        {
            Console.WriteLine("--- DANH SÁCH SẢN PHẨM ---");
            InDanhSach(service.GetAll());
        }

        static void TimTheoMa(ProductService service)
        {
            Console.WriteLine("--- TÌM SẢN PHẨM THEO MÃ ---");
            string maSP = NhapChuoi("Nhập mã sản phẩm cần tìm: ");

            Product? p = service.FindById(maSP);
            if (p != null)
            {
                Console.WriteLine("\nTìm thấy sản phẩm:");
                Console.WriteLine(p);
            }
            else
            {
                Console.WriteLine($"\nKhông tìm thấy sản phẩm có mã: {maSP}");
            }
        }

        static void TimTheoTen(ProductService service)
        {
            Console.WriteLine("--- TÌM SẢN PHẨM THEO TÊN ---");
            string tuKhoa = NhapChuoi("Nhập từ khóa tên sản phẩm: ");

            List<Product> ketQua = service.SearchByName(tuKhoa);
            Console.WriteLine($"\nKết quả tìm kiếm cho từ khóa \"{tuKhoa}\":");
            InDanhSach(ketQua);
        }

        static void LocTheoKhoangGia(ProductService service)
        {
            Console.WriteLine("--- LỌC SẢN PHẨM THEO KHOẢNG GIÁ ---");
            decimal min = NhapSoThucDuong("Nhập giá nhỏ nhất: ");
            decimal max;

            while (true)
            {
                max = NhapSoThucDuong("Nhập giá lớn nhất: ");
                if (max >= min)
                {
                    break;
                }
                Console.WriteLine("Giá lớn nhất phải lớn hơn hoặc bằng giá nhỏ nhất. Vui lòng nhập lại!");
            }

            List<Product> ketQua = service.FilterByPrice(min, max);
            Console.WriteLine($"\nDanh sách sản phẩm có giá từ {min:N0} đến {max:N0} VNĐ:");
            InDanhSach(ketQua);
        }

        static void XoaSanPham(ProductService service)
        {
            Console.WriteLine("--- XÓA SẢN PHẨM ---");
            string maSP = NhapChuoi("Nhập mã sản phẩm cần xóa: ");

            try
            {
                service.RemoveProduct(maSP);
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine($"\n[Lỗi nghiệp vụ] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Lỗi không xác định] {ex.Message}");
            }
        }

        static void TinhTongGiaTriKho(ProductService service)
        {
            Console.WriteLine("--- TỔNG GIÁ TRỊ KHO HÀNG ---");
            decimal tong = service.CalculateTotalValue();
            Console.WriteLine($"Tổng giá trị toàn bộ kho hàng: {tong:N0} VNĐ");
        }

        static void InDanhSach(List<Product> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống.");
                return;
            }

            Console.WriteLine(new string('-', 65));
            int stt = 1;
            foreach (Product p in danhSach)
            {
                Console.WriteLine($"{stt++,2}. {p}");
            }
            Console.WriteLine(new string('-', 65));
            Console.WriteLine($"Tổng số: {danhSach.Count} sản phẩm.");
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

        static int NhapSoNguyenDuong(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int so) && so >= 0)
                {
                    return so;
                }

                Console.WriteLine("Số lượng không hợp lệ. Phải là số nguyên >= 0. Vui lòng nhập lại!");
            }
        }

        static decimal NhapSoThucDuong(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    string formattedInput = input.Trim().Replace(',', '.');
                    if (decimal.TryParse(formattedInput, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal so) && so >= 0)
                    {
                        return so;
                    }
                }

                Console.WriteLine("Đơn giá không hợp lệ. Phải là số thực >= 0. Vui lòng nhập lại!");
            }
        }

        static void NhanEnterDeTiepTuc()
        {
            Console.WriteLine("\nNhấn Enter để tiếp tục...");
            Console.ReadLine();
        }
    }
}
