using MassTransit.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Infrastructure
{
    public class JwtOptions
    {
        [Required]
        public string Issuer { get; init; } = string.Empty;
        [Required]
        public string Audience { get; init; } = string.Empty;
        [Required]
        public string SecretKey { get; init; } = string.Empty;
        [Required]
        public int LifeTimeInMinutes { get; init; } = 0;
    }
}
