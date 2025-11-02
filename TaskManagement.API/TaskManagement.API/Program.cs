using TaskManagement.API.Endpoints;
using TaskManagement.API.Extentions;
using TaskManagement.Infrastructure.Services;
using TaskManagement.Infrastructure.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.feature.Task.Queries.GetAllTasks;
using TaskManagement.Infrastructure.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TaskContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetAllTasksHandler).Assembly));

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddServiceImplementation(typeof(TaskService).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure services for CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("TaskManagement.Client",
        builder =>
        {
            builder.WithOrigins("http://localhost", "http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("TaskManagement.Client");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map endpoints from folder
app.MapTaskManagement();
app.MapAuthentication();

app.Run();
