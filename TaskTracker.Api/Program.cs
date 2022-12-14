using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;
using TaskTracker.Data.DbSeeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskTrackerDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("TaskTracker")));
builder.Services.AddScoped<IDbSeeder, DbSeeder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedDataBase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDataBase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbSeeder = scope.ServiceProvider.GetService<IDbSeeder>();
        dbSeeder.Seed();
    }
}