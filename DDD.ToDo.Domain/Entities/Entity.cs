using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.ToDo.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
    }
}
