using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class UnAuthorizedException(string message="wrong user or password"):Exception(message)
    {
    }
}
