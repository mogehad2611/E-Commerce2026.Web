using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(int quantity, decimal price, ProductItemOrdered product)
        {
            Quantity = quantity;
            Price = price;
            Product = product;
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ProductItemOrdered Product { get; set; } = default!;

    }
}
