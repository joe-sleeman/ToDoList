using AutoMapper;
using ToDoList.Entity;
using ToDoList.Model;

namespace ToDoList.Service.Helpers.Mappings
{
    public class ModelToEntityMappingProfile : Profile
    {
        public ModelToEntityMappingProfile()
        {
            CreateMap<TaskModel, TaskEntity>();
            CreateMap<TaskStatusModel, TaskStatusEntity>();
        }
    }
}
