using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using var db = new MyStoreContext();
                listProducts = db.Products.Include(f => f.Category).ToList();
            }
            catch (Exception) { }
            return listProducts;
        }

        public static void SaveProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();

                // Kiểm tra xem CategoryId đã tồn tại trong database chưa
                var existingCategory = context.Categories.Find(p.CategoryId);

                if (existingCategory == null)
                {
                    // Nếu chưa tồn tại, thêm mới Category
                    if (p.Category != null)
                    {
                        p.Category.CategoryId = p.CategoryId; // Đảm bảo ID đồng bộ
                        context.Categories.Add(p.Category);
                    }
                    else
                    {
                        // Nếu user không truyền chi tiết Category, tạo với tên mặc định
                        var newCat = new Category 
                        { 
                            CategoryId = p.CategoryId, 
                            CategoryName = "Category " + p.CategoryId 
                        };
                        context.Categories.Add(newCat);
                        p.Category = newCat;
                    }
                }
                else
                {
                    // Nếu đã tồn tại, ta set null navigation property để tránh EF cố insert lần nữa hoặc bị đụng độ tracking
                    p.Category = null;
                }

                context.Products.Add(p);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Entry<Product>(p).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                var p1 = context.Products.SingleOrDefault(c => c.ProductId == p.ProductId);
                if (p1 != null)
                {
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Product GetProductById(int id)
        {
            using var db = new MyStoreContext();
            return db.Products.FirstOrDefault(c => c.ProductId.Equals(id));
        }
    }
}