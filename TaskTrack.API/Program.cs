using Microsoft.EntityFrameworkCore;
using TaskTrack.API;
using TaskTrack.API.Middleware;
using TaskTrack.Repo.Data;
using TaskTrack.Repo.Interfaces;
using TaskTrack.Repo.Repositories;
using TaskTrack.Service.Interfaces;
using TaskTrack.Service.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = ConnectionStringHelper.Resolve(builder.Configuration);
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? ["http://localhost:3000"];

builder.Services.AddDbContext<TaskManagementDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy("Frontend", policy => policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ApiExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Frontend");
app.MapControllers();

app.Run();
