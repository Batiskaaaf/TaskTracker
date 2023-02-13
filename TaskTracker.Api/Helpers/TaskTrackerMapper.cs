using AutoMapper;
using TaskTracker.Model;
using TaskTracker.Model.DTO;

namespace TaskTracker.Api.Helpers
{
    public class TaskTrackerMapper : Profile
    {
        public TaskTrackerMapper()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Model.Task, TaskDTO>().ReverseMap();
        }
    }
}
