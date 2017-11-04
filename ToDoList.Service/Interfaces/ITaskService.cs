using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Service.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> RetrieveAsync();
        Task<IEnumerable<TaskModel>> RetrieveByNameAsync(string name);
        Task<TaskModel> RetrieveByIdAsync(Guid id);
        Task<TaskModel> CreateAsync(TaskModel task);
        Task UpdateAsync(Guid id, TaskModel task);
        Task DeleteAsync(Guid id);

    }
}
