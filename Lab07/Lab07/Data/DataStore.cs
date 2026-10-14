using System.Collections.Generic;
using System.ComponentModel;
using Lab07.Models;

namespace Lab07.Data
{
    public static class DataStore
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static BindingList<Product> Products { get; set; } = new BindingList<Product>();
        public static BindingList<Customer> Customers { get; set; } = new BindingList<Customer>();

        static DataStore()
        {
            SeedData();
        }

        private static void SeedData()
        {
            // Tài khoản đăng nhập mẫu
            Users.Add(new User("admin", "123", "Quản trị viên", "Admin"));
            Users.Add(new User("user", "123", "Nhân viên bán hàng", "User"));

            // Dữ liệu sản phẩm mẫu
            Products.Add(new Product("SP01", "Chuột không dây Logitech", "Điện tử", 350000m, 25));
            Products.Add(new Product("SP02", "Bàn phím cơ DareU", "Điện tử", 650000m, 15));
            Products.Add(new Product("SP03", "Bình giữ nhiệt Lock&Lock", "Gia dụng", 280000m, 30));
            Products.Add(new Product("SP04", "Áo thun polo nam", "Thời trang", 199000m, 50));
            Products.Add(new Product("SP05", "Sổ tay da cao cấp", "Văn phòng phẩm", 85000m, 40));

            // Dữ liệu khách hàng mẫu
            Customers.Add(new Customer("KH01", "Nguyễn Văn An", "0901234567", "VIP"));
            Customers.Add(new Customer("KH02", "Trần Thị Mai", "0912345678", "Thân thiết"));
            Customers.Add(new Customer("KH03", "Lê Hoàng Nam", "0987654321", "Thường"));
            Customers.Add(new Customer("KH04", "Phạm Thị Thu", "0934567890", "Thường"));
        }
    }
}
