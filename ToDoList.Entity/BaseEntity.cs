using System;

namespace ToDoList.Entity
{
    public class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public string Name { get; set; }
    }
}
