using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class AddressDTO
    {
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string Fname { get; set; } = default!;
        public string Lname { get; set; } = default!;
    }
}
