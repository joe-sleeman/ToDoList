using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ToDoList.Repository.Helpers.Interfaces
{
    public interface IConnectionManager
    {
        SqlConnection CreateConnection();
    }
}
