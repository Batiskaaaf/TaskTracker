﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Model;

namespace TaskTracker.Data;
public class TaskTrackerDbContext : IdentityDbContext<ApplicationUser>
{
    public TaskTrackerDbContext( DbContextOptions<TaskTrackerDbContext> options) : base(options) {}

    public DbSet<Project> Projects { get; set; }
    public DbSet<Model.Task> Tasks { get; set; }
}
