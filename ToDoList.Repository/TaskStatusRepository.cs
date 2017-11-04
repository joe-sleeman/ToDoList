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
    public class TaskStatusRepository : ITaskStatusRepository
    {
        #region Properties

        private readonly IConnectionManager _connectionManager;
        #endregion

        #region Constructors

        public TaskStatusRepository(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<TaskStatusEntity>> RetrieveAsync()
        {
            IEnumerable<TaskStatusEntity> resultTaskStatusEntities = new List<TaskStatusEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command =
                    new SqlCommand("RetrieveTaskStatuses", connection) {CommandType = CommandType.StoredProcedure};
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                    resultTaskStatusEntities = await ToTaskStatusEntities(reader);
            }
            return resultTaskStatusEntities;
        }

        public async Task<TaskStatusEntity> RetrieveByIdAsync(Guid id)
        {
            TaskStatusEntity resultTaskStatusEntity = null;

            using (var connection = _connectionManager.CreateConnection())
            {
                var command =
                    new SqlCommand("RetrieveProductOptionsById", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                    resultTaskStatusEntity = ToTaskStatusEntity(reader);
            }
            return resultTaskStatusEntity;
        }

        public async Task<TaskStatusEntity> CreateAsync(TaskStatusEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command =
                    new SqlCommand("CreateTaskStatusEntity", connection) {CommandType = CommandType.StoredProcedure};

                if (entity.Id != Guid.Empty)
                    command.Parameters.AddWithValue("@Id", entity.Id);

                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Descrition);

                var id = await command.ExecuteScalarAsync();
                entity.Id = Guid.Parse(id.ToString());
            }
            return entity;
        }

        public async Task UpdateAsync(TaskStatusEntity entity)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("UpdateTaskStatus", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", entity.Id);
                command.Parameters.AddWithValue("@Name", entity.Name);
                command.Parameters.AddWithValue("@Description", entity.Descrition);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("DeleteTaskStatus", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<TaskStatusEntity>> RetrieveByTaskStatusIdAsync(Guid taskStatusId)
        {
            IEnumerable<TaskStatusEntity> result = new List<TaskStatusEntity>();

            using (var connection = _connectionManager.CreateConnection())
            {
                var command = new SqlCommand("RetrieveTaskStatusByTaskId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TaskStatusId", taskStatusId);
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                    result = await ToTaskStatusEntities(reader);
            }

            return result;
        }

        private TaskStatusEntity ToTaskStatusEntity(SqlDataReader reader)
        {
            return new TaskStatusEntity
            {
                Id = Guid.Parse(reader["Id"].ToString()),
                Name = reader["Name"].ToString(),
                Descrition = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString()
            };
        }

        private async Task<IEnumerable<TaskStatusEntity>> ToTaskStatusEntities(SqlDataReader reader)
        {
            var taskStatusEntities = new List<TaskStatusEntity>();

            while (await reader.ReadAsync())
            {
                taskStatusEntities.Add(ToTaskStatusEntity(reader));
            }

            return taskStatusEntities;
        }
        #endregion
    }
}
