using AutoMapper;
using ToDoList.Entity;
using ToDoList.Model;

namespace ToDoList.Service.Helpers.Mappings
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile()
        {
            CreateMap<TaskEntity, TaskModel>();
            CreateMap<TaskStatusEntity, TaskStatusModel>();
        }
    }
}
