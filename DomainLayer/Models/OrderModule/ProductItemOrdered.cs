using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModule
{
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string ProductName { get; set; } = default!;

        public ProductItemOrdered(int productId, string pictureUrl, string productName)
        {
            ProductId = productId;
            PictureUrl = pictureUrl;
            ProductName = productName;
        }
    }
}
