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

app.UseHttpsRedirection();
app.Run();