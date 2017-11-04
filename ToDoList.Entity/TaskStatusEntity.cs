using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Entity
{
    public class TaskStatusEntity : BaseEntity
    {
        public override Guid Id { get; set; }
        public string Descrition { get; set; }
    }
}
