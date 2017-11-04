using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Entity
{
    public class TaskEntity : BaseEntity
    {
        public override Guid Id { get; set; }
        public string Description { get; set; }
        public Guid TaskStatusId { get; set; }
    }
}
