using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Entity;
using ToDoList.Model;
using ToDoList.Repository.Interfaces;
using ToDoList.Service.Helpers.Errors;
using ToDoList.Service.Helpers.Exceptions;
using ToDoList.Service.Interfaces;

namespace ToDoList.Service
{
    public class TaskService : ITaskService
    {
        #region Properties

        private readonly ITaskRepository _taskRepository;

        #endregion

        #region Constructors

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        #endregion
        public async Task<IEnumerable<TaskModel>> RetrieveAsync()
        {
            var taskEntities = await _taskRepository.RetrieveAsync();
            var taskModels = Mapper.Map<IEnumerable<TaskModel>>(taskEntities);

            return taskModels;
        }

        public async Task<IEnumerable<TaskModel>> RetrieveByNameAsync(string name)
        {
            var taskEntities = await _taskRepository.RetrieveByNameAsync(name);
            var taskModels = Mapper.Map<IEnumerable<TaskModel>>(taskEntities);

            return taskModels;
        }

        public async Task<TaskModel> RetrieveByIdAsync(Guid id)
        {
            var taskEntity = await _taskRepository.RetrieveByIdAsync(id);
            if (taskEntity == null)
                throw new NotFoundException(ErrorHelper.CreateErrorForId("task", id));

            var taskModel = Mapper.Map<TaskModel>(taskEntity);

            return taskModel;
        }

        public async Task<TaskModel> CreateAsync(TaskModel task)
        {
            var taskEntity = Mapper.Map<TaskEntity>(task);

            taskEntity = await _taskRepository.CreateAsync(taskEntity);

            var taskModel = Mapper.Map<TaskModel>(taskEntity);

            return taskModel;
        }

        public async Task UpdateAsync(Guid id, TaskModel task)
        {
            var taskEntity = await _taskRepository.RetrieveByIdAsync(id);
            if (taskEntity == null)
                throw new NotFoundException(ErrorHelper.CreateErrorForId("task", id));

            Mapper.Map(task, taskEntity);
            taskEntity.Id = id;

            await _taskRepository.UpdateAsync(taskEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var taskEntity = await _taskRepository.RetrieveByIdAsync(id);
            if (taskEntity == null)
                throw new NotFoundException(ErrorHelper.CreateErrorForId("task", id));

            await _taskRepository.DeleteAsync(id);
        }
    }
}
