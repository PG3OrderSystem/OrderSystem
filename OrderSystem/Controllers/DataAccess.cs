using OrderSystem.Models;
using OrderSystem.Models;
using OrderSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OrderSystem
{
    public class DisplayItem
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Subtotal { get; set; }
    }
    public class DataAccess
    {

        public static List<Orders> GetOrdersById()
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

        public static (List<DisplayItem> Details, int TotalAmount)? GetReceipt(int orderId)
        {
            using (var context = new OrderDBContext())
            {
                var result = context.Orders
                    .Where(o => o.OrderId == orderId)
                    .Select(o => new
                    {
                        Details = o.OrderDetails.Select(d => new DisplayItem
                        {
                            ProductName = d.Product.ProductName,
                            Price = d.Product.Price,
                            Quantity = d.Quantity,
                            Subtotal = d.Subtotal
                        }).ToList(),
                        o.TotalAmount
                    })
                    .FirstOrDefault();

                if (result == null) return null;

                return (result.Details, result.TotalAmount);
            }
        }


        public static int SaveOrder(int totalAmount, List<TopPage.CartItem> items)
        {
            using (var context = new OrderDBContext())
            {
                var order = new Orders
                {
                    DateTime = DateTime.Now,
                    TotalAmount = totalAmount
                };
                context.Orders.Add(order);
                context.SaveChanges();

                foreach (var item in items)
                {
                    var detail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Subtotal = item.Subtotal
                    };
                    context.OrderDetails.Add(detail);
                }
                context.SaveChanges();

                return order.OrderId;
            }
        }

    }
}
