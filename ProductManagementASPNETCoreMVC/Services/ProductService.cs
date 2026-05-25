using System.Collections.Generic;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository iProductRepository;

        public ProductService()
        {
            iProductRepository = new ProductRepository();
        }

        public void DeleteProduct(Product p) => iProductRepository.DeleteProduct(p);
        public Product GetProductById(int id) => iProductRepository.GetProductById(id);
        public List<Product> GetProducts() => iProductRepository.GetProducts();
        public void SaveProduct(Product p) => iProductRepository.SaveProduct(p);
        public void UpdateProduct(Product p) => iProductRepository.UpdateProduct(p);
    }
}