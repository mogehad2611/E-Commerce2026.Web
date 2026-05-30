using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }
    }
}
