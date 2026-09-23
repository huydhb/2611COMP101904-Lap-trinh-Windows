using System;

namespace Lab04
{
    public class Product : IEntity
    {
        public string Id
        {
            get { return MaSP; }
        }

        public string MaSP { get; set; }
        public string TenSP { get; set; }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Đơn giá không được âm.");
                }
                _price = value;
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Số lượng không được âm.");
                }
                _quantity = value;
            }
        }

        public Product(string maSP, string tenSP, decimal price, int quantity)
        {
            MaSP = maSP;
            TenSP = tenSP;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"Mã SP: {MaSP,-8} | Tên: {TenSP,-20} | Đơn giá: {Price,10:N0} VNĐ | Số lượng: {Quantity,5}";
        }
    }
}
