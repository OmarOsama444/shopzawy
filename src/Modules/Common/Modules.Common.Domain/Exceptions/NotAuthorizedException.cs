using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Domain.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public string Code { get; private init; }
        public NotAuthorizedException(string code, string message) : base(message)
        {
            this.Code = code;     
        }
    }
}
