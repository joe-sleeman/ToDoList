
using System;

namespace ToDoList.Service.Helpers.Errors
{
    public static class ErrorHelper
    {
        public static string CreateErrorForId(string type, Guid id)
        {
            return $"No {type} found with Id: {id}.";
        }
    }
}
