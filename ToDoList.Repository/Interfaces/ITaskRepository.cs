using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Entity;

namespace ToDoList.Repository.Interfaces
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> RetrieveByNameAsync(string name);
    }
}
