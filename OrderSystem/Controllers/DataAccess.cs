using OrderSystem.Models;
using OrderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrderSystem
{
    public class DataAccess
    {


        public static List<Order> GetOrdersById()
        {
            using (var context = new OrderDBContext())
            {
                return context.Orders.OrderBy(x => x.OrderId).ToList();
            }
        }

        public static List<Products> GetProductsByCategory(string category)
        {
            using (var context = new OrderDBContext())
            {
                return context.Products
                    .Where(p => p.Category == category)
                    .ToList();
            }
        }



        public static bool CheckLogin(string id, string password)
        {
            using (var context = new Models.OrderDBContext())
            {
                var admin = context.Admins
                    .FirstOrDefault(a => a.AdminId == id && a.Password == password);

                return admin != null;
            }
        }


        // Products
        public static List<Products> GetAllProducts()
        {
            using (var context = new OrderDBContext())
            {
                return context.Products.OrderBy(x => x.ProductId).ToList();
            }
        }





        public static void AddProduct(Products product)
        {
            using (var context = new OrderDBContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public static void UpdateProduct(Products updated)
        {
            using (var context = new OrderDBContext())
            {
                var product = context.Products.Find(updated.ProductId);
                if (product != null)
                {
                    product.ProductName = updated.ProductName;
                    product.Price = updated.Price;
                    product.Category = updated.Category;
                    context.SaveChanges();
                }
            }
        }

        public static void DeleteProduct(string productId)
        {
            using (var context = new OrderDBContext())
            {
                var product = context.Products.Find(productId);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
            }
        }

        public static List<Products> SearchProducts(string id, string name, string category, string priceText)
        {
            using (var context = new OrderDBContext())
            {
                var query = context.Products.AsQueryable();
                bool anyInput = !string.IsNullOrWhiteSpace(id)
                    || !string.IsNullOrWhiteSpace(name)
                    || !string.IsNullOrWhiteSpace(category)
                    || !string.IsNullOrWhiteSpace(priceText);

                if (anyInput)
                {
                    query = query.Where(p =>
                        (!string.IsNullOrWhiteSpace(id) && p.ProductId.Contains(id)) ||
                        (!string.IsNullOrWhiteSpace(name) && p.ProductName.Contains(name)) ||
                        (!string.IsNullOrWhiteSpace(category) && p.Category == category) ||
                        (!string.IsNullOrWhiteSpace(priceText) && p.Price.ToString().Contains(priceText))
                    );
                }
                return query.ToList();
            }
        }





    }
}
