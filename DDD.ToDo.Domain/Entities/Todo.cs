using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.ToDo.Domain.Entities
{
    public class Todo : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }

        private Todo(Guid id, string title, string description, bool isDone)
        {
            Id = id;
            Title = title;
            Description = description;
            IsDone = isDone;
        }

        public static Todo Create(string title, string description) 
            => new(Guid.NewGuid(), title, description, false);

        public bool ChangeStatus()
        {
            this.IsDone = !this.IsDone;
            return this.IsDone;
        }
    }
}
