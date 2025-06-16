using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Domain.Exceptions
{
    public class ConflictException: Exception 
    {
        public ConflictException(string code, string message) : base( message )
        {
            Code = code;
        }

        public string Code { get; }
    }
}
