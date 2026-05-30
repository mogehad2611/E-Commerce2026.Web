using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.BasketModule
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
