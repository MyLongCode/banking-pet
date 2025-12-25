using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class Session
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; } = default!;

        public string RefreshTokenHash { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

        public DateTimeOffset? RevokedAt { get; set; }

        public string? UserAgent { get; set; }
        public string? Ip { get; set; }
    }

}
