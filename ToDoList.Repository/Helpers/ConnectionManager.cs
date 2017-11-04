using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ToDoList.Repository.Helpers.Interfaces;

namespace ToDoList.Repository.Helpers
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly string _connectionString;
        public ConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
