using System;
using System.Text;

namespace Lab02
{
    public class Program
    {
        // Hàm nhập số nguyên dương (Dùng chung cho nhập n, nhập choice)
        public static int NhapSoNguyenDuong(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                string? line = Console.ReadLine();
                if (int.TryParse(line, out result) && result > 0)
                {
                    return result;
                }
                Console.WriteLine("Du lieu khong hop le. Vui long nhap lai!");
            }
        }

        // Hàm nhập số nguyên bất kỳ (Dùng cho nhập phần tử mảng, nhập x)
        public static int NhapSoNguyen(string message)
        {
            int result;
            while (true)
            {
                Console.Write(message);
                string? line = Console.ReadLine();
                if (int.TryParse(line, out result))
                {
                    return result;
                }
                Console.WriteLine("Du lieu khong hop le. Vui long nhap lai!");
            }
        }

        public static int[] NhapMang()
        {
            int n = NhapSoNguyenDuong("Nhap so luong phan tu (n > 0): ");
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = NhapSoNguyen($"Nhap phan tu thu {i + 1}: ");
            }
            return a;
        }

        public static void XuatMang(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }

        public static int TinhTong(int[] a)
        {
            int sum = 0;
            foreach (int x in a) sum += x;
            return sum;
        }

        public static int TimMax(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; i++)
                if (a[i] > max) max = a[i];
            return max;
        }

        public static int TimMin(int[] a)
        {
            int min = a[0];
            for (int i = 1; i < a.Length; i++)
                if (a[i] < min) min = a[i];
            return min;
        }

        public static int DemChan(int[] a)
        {
            int count = 0;
            foreach (int x in a) if (x % 2 == 0) count++;
            return count;
        }

        public static int DemLe(int[] a)
        {
            int count = 0;
            foreach (int x in a) if (x % 2 != 0) count++;
            return count;
        }

        public static void SapXepTangDan(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[i] > a[j])
                    {
                        int temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                }
            }
        }

        public static int TimKiem(int[] a, int x)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == x) return i; // Trả về vị trí tính từ 0
            }
            return -1;
        }

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            int[]? a = null; // Khởi tạo mảng rỗng
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Nhap mang");
                Console.WriteLine("2. Xuat mang");
                Console.WriteLine("3. Tinh tong");
                Console.WriteLine("4. Tim max/min");
                Console.WriteLine("5. Dem chan/le");
                Console.WriteLine("6. Sap xep tang dan");
                Console.WriteLine("7. Tim kiem");
                Console.WriteLine("0. Thoat");
                while (true)
                {
                    Console.Write("Chon chuc nang (0-7): ");

                    // Kiểm tra lựa chọn hợp lệ
                    if (int.TryParse(Console.ReadLine(), out choice) && choice >= 0 && choice <= 7)
                        break;
                    Console.WriteLine("Lua chon khong hop le. Vui long nhap tu 0 den 7!");
                }

                // Kiểm tra mảng đã được nhập chưa
                if (choice != 1 && choice != 0 && a == null)
                {
                    Console.WriteLine(">> Ban chua nhap mang! Vui long chon chuc nang 1 de nhap mang.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        a = NhapMang();
                        Console.WriteLine(">> Nhap mang thanh cong!");
                        break;
                    case 2:
                        Console.Write("Mang hien tai: ");
                        XuatMang(a!);
                        break;
                    case 3:
                        Console.WriteLine("Tong cac phan tu: " + TinhTong(a!));
                        break;
                    case 4:
                        Console.WriteLine("Max: " + TimMax(a!));
                        Console.WriteLine("Min: " + TimMin(a!));
                        break;
                    case 5:
                        Console.WriteLine("So chan: " + DemChan(a!));
                        Console.WriteLine("So le: " + DemLe(a!));
                        break;
                    case 6:
                        SapXepTangDan(a!);
                        Console.Write("Mang sau khi sap xep: ");
                        XuatMang(a!);
                        break;
                    case 7:
                        int x = NhapSoNguyen("Nhap phan tu can tim (x): ");
                        int index = TimKiem(a!, x);
                        if (index != -1)
                            Console.WriteLine($"Tim thay x = {x} tai vi tri {index} (tinh tu 0).");
                        else
                            Console.WriteLine($"Khong tim thay x = {x} trong mang.");
                        break;
                    case 0:
                        Console.WriteLine(">> Chuong trinh ket thuc. Tam biet!");
                        break;
                }
            }
        }
    }
}