using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public class AddressNotFoundException(string username):NotFoundException($"User {username} has no address ")
    {
    }
}
