using System;
using System.Collections.Generic;

namespace Lab04
{
    public class ProductService
    {
        private Repository<Product> _repository;

        public event Action<Product>? OnProductAdded;
        public event Action<Product>? OnProductRemoved;

        public ProductService()
        {
            _repository = new Repository<Product>();
        }

        public void AddProduct(Product product)
        {
            Product? existing = _repository.FindById(product.MaSP);
            if (existing != null)
            {
                throw new DuplicateProductException($"Mã sản phẩm '{product.MaSP}' đã tồn tại.");
            }

            _repository.Add(product);
            OnProductAdded?.Invoke(product);
        }

        public void RemoveProduct(string maSP)
        {
            Product? product = _repository.FindById(maSP);
            if (product == null)
            {
                throw new ProductNotFoundException($"Không tìm thấy sản phẩm có mã '{maSP}'.");
            }

            _repository.Remove(product);
            OnProductRemoved?.Invoke(product);
        }

        public Product? FindById(string maSP)
        {
            return _repository.FindById(maSP);
        }

        public List<Product> SearchByName(string keyword)
        {
            return _repository.Find(p => p.TenSP.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        public List<Product> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            return _repository.Find(p => p.Price >= minPrice && p.Price <= maxPrice);
        }

        public List<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public decimal CalculateTotalValue()
        {
            decimal total = 0;
            foreach (Product p in _repository.GetAll())
            {
                total += p.Price * p.Quantity;
            }
            return total;
        }
    }
}
