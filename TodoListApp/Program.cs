using Microsoft.AspNetCore.Mvc;
using TodoListApp.ApiModels;
using TodoListApp.Application.Abstractions.Repo;
using TodoListApp.Application.Abstractions.Services;
using TodoListApp.Application.Services;
using TodoListApp.Infrastructure;
using TodoListApp.Infrastructure.Data.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHttpContextAccessor();

string connectionString = builder.Configuration.GetConnectionString("TodoAppDb");
builder.Services.AddDbContext(connectionString);

builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
builder.Services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/create-task", (ITodoTaskService taskService, CreateTaskRequest req) =>
{
    CreateTodoTaskRequest createRequest = new CreateTodoTaskRequest
    {
        DueDate = req.DueDate,
        Title = req.Title
    };

    int? resultId = taskService.CreateTodoTask(createRequest);

    if (resultId.HasValue)
    {
        return Results.Ok(resultId.Value);
    }
    else
    {
        return Results.BadRequest();
    }
});

//app.UseHttpsRedirection();
app.Run();