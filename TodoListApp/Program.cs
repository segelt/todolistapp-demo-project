using Microsoft.EntityFrameworkCore;
using TodoListApp.ApiModels;
using TodoListApp.Application.Abstractions.Services;
using TodoListApp.Infrastructure;
using TodoListApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHttpContextAccessor();

var connectionString = Environment.GetEnvironmentVariable("Conn_Str");
if (connectionString == null)
{
    connectionString = builder.Configuration.GetConnectionString("TodoAppDb");
}

builder.Services.AddDbContext(connectionString);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.RegisterDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Endpoints

app.Map("/", () =>
{
    return Results.Redirect("/swagger");
});

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

app.MapPost("/update-task", (ITodoTaskService taskService, EditTaskRequest editReq) =>
{
    if (!editReq.IsValid)
    {
        return Results.BadRequest("Invalid input.");
    }

    EditTodoTaskRequest editTaskRequest = new EditTodoTaskRequest
    {
        id = editReq.Id,
        Title = editReq.Title,
        DueDate = editReq.DueDate,
        IsCompleted = editReq.IsCompleted
    };

    bool success = taskService.EditTask(editTaskRequest);

    if (success)
    {
        return Results.Ok();
    }
    else
    {
        return Results.BadRequest();
    }

});

app.MapGet("/pending-tasks", (ITodoTaskService taskService) =>
{
    // TODO: Error handling middleware to log all exceptions thrown on this endpoint
    var todoTasks = taskService.GetPendingTasks();

    return Results.Ok(todoTasks.ToList());
});

app.MapGet("/overdue-tasks", (ITodoTaskService taskService) =>
{
    // TODO: Error handling middleware to log all exceptions thrown on this endpoint
    var todoTasks = taskService.GetOverdueTasks();

    return Results.Ok(todoTasks.ToList());
});

#endregion

app.Logger.LogInformation("App started...");
// Database handling..

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        StartupSetup.PrepareDatabase(context);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred preparing the DB. {exceptionMessage}", ex.Message);
    }
}

//app.UseHttpsRedirection();
app.Run();
