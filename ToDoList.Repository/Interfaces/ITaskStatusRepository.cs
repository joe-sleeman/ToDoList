using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Entity;

namespace ToDoList.Repository.Interfaces
{
    public interface ITaskStatusRepository : IRepository<TaskStatusEntity>
    {
        Task<IEnumerable<TaskStatusEntity>> RetrieveByTaskStatusIdAsync(Guid taskStatusId);
    }
}
