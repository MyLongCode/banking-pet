using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Entities
{
    public class EmailVerificationCode
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; } = default!;

        public string CodeHash { get; set; } = default!;
        public DateTimeOffset ExpiresAt { get; set; }

        public int Attempts { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UsedAt { get; set; }
    }

}
