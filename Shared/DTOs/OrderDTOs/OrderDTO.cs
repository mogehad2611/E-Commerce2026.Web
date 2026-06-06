using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public string BasketId { get; set; } = default!;
        public int DeliveryMethod { get; set; }
        public AddressDTO Address { get; set; } = default!;
    }
}
