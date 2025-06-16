using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Domain.Exceptions
{

    public class NotFoundException : Exception
    {
        public NotFoundException(string code, string message) : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
