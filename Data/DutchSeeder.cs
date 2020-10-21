using System.Collections.Generic;
using System.IO;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace DutchTreat.Data {
    public class DutchSeeder {
        private readonly DutchContext _context;
        private readonly IHostingEnvironment _hosting;
        public DutchSeeder (DutchContext context, IHostingEnvironment hosting) {
            this._hosting = hosting;
            this._context = context;
        }

        public void Seed () {
            _context.Database.EnsureCreated ();

            if (!_context.Products.Any ()) {
                // Create sample data if no products
                var filepath = Path.Combine (_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText (filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>> (json);
                _context.Products.AddRange (products);

                var order = _context.Orders.Where (o => o.Id == 1).FirstOrDefault ();

                if (order != null) {
                    order.Items = new List<OrderItem> () {
                    new OrderItem () {
                    Product = products.First (),
                    Quantity = 7,
                    UnitPrice = products.First ().Price
                    }
                    };
                }

                _context.SaveChanges ();
            }
        }
    }
}