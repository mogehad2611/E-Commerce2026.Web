using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class BasketNotFoundException(string Id):NotFoundException($"The Basket with id {Id} is Not Found")
    {
    }
}
