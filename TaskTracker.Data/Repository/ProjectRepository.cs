﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Data.Repository.IRepository;
using TaskTracker.Model;

namespace TaskTracker.Data.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {

        private readonly TaskTrackerDbContext db;

        public ProjectRepository(TaskTrackerDbContext db) : base(db)
        {
            this.db = db;
        }

        public Project GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }

        public ICollection<Model.Task> GetProjectTasks(int id)
        {   
            var project = GetFirstOrDefault(p => p.Id == id, "Tasks");
            return project.Tasks;
        }

        public void Update(Project obj)
        {
            db.Projects.Update(obj);
        }
    }
}
