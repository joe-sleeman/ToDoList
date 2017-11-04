using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Service.Interfaces
{
    public interface ITaskStatusService
    {
        Task<IEnumerable<TaskStatusModel>> RetrieveByTaskIdAsync(Guid taskId);
        Task<TaskStatusModel> RetrieveByTaskIdAndIdAsync(Guid taskId, Guid id);
        Task<TaskStatusModel> CreateAsync(Guid taskId, TaskStatusModel taskStatus);
        Task UpdateAsync(Guid taskId, Guid id, TaskStatusModel taskStatus);
        Task DeleteAsync(Guid taskId, Guid id);
    }
}
