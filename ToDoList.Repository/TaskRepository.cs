using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ToDoList.Entity;
using ToDoList.Repository.Helpers.Interfaces;
using ToDoList.Repository.Interfaces;

namespace ToDoList.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IConnectionManager _connectionManager;

        public TaskRepository(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        public async Task<IEnumerable<TaskEntity>> RetrieveAsync()
        {
            IEnumerable<TaskEntity> result = new List<TaskEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveTasks", connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                    result = await ToTaskEntities(reader);
            }

            return result;
        }

        public async Task<TaskEntity> RetrieveByIdAsync(Guid id)
        {
            TaskEntity result = null;

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveTaskById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                    result = ToTaskEntity(reader);
            }

            return result;
        }

        public async Task<TaskEntity> CreateAsync(TaskEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("CreateTask", connection);
                command.CommandType = CommandType.StoredProcedure;
                if (entity.Id != Guid.Empty)
                    command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Description);
                command.Parameters.AddWithValue("@StatusId", entity.TaskStatusId);
                await connection.OpenAsync();

                var Id = await command.ExecuteScalarAsync();
                entity.Id = Guid.Parse(Id.ToString());
            }

            return entity;
        }

        public async Task UpdateAsync(TaskEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("UpdateTask");
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Name);
                command.Parameters.AddWithValue("@StatusId", entity.TaskStatusId);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("DeleteTask", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<TaskEntity>> RetrieveByNameAsync(string name)
        {
            IEnumerable<TaskEntity> result = new List<TaskEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveTasksByName", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", name);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                    result = await ToTaskEntities(reader);
            }

            return result;
        }

        private async Task<IEnumerable<TaskEntity>> ToTaskEntities(SqlDataReader reader)
        {
            var taskEntities = new List<TaskEntity>();

            while (await reader.ReadAsync())
            {
                taskEntities.Add(ToTaskEntity(reader));
            }

            return taskEntities;
        }

        private static TaskEntity ToTaskEntity(SqlDataReader reader)
        {
            return new TaskEntity
            {
                Id = Guid.Parse(reader["Id"].ToString()),
                Name = reader["Name"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                TaskStatusId = Guid.Parse(reader["TaskStatusId"].ToString())
            };
        }
    }
}
