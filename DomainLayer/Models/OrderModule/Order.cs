using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
        }

        public Order(string userEmail, OrderAddress orderAddress, DeliveryMethod deliveryMethod, decimal subTotal, ICollection<OrderItem> items)
        {
            UserEmail = userEmail;
            OrderAddress = orderAddress;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            Items = items;
        }

        public string UserEmail { get; set; } = default!;
        public OrderAddress OrderAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];


        public int DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        /*
        [NotMapped]
        public decimal Total { get => SubTotal + DeliveryMethod.Price; }
        */

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
    }
}
