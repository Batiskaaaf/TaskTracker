using AutoMapper;
using TaskTracker.Model;
using TaskTracker.Model.DTO;

namespace TaskTracker.Api.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Model.Task, TaskDTO>().ReverseMap();
        }
    }
}
