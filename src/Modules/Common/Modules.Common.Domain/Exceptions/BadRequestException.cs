using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Domain.Exceptions
{
    public class BadRequestException 
        : Exception
    {
        public BadRequestException( IDictionary<string, string[]> Errors , string code = "Validation.Error", string message = "Invalid input" )
            : base(message)
        {
            this.Errors = Errors;
            Code = "Invalid.Input";
        }
        public string Code { get; init; }
        public IDictionary<string, string[]> Errors { get; }
    }

}
