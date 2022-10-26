namespace TodoListApp.Application.Abstractions
{
    /// <summary>
    /// A custom datetime provider is used to make <see cref="TodoListApp.Application.Abstractions.Services.ITodoTaskService"/> unit testable.
    /// <see cref="TodoListApp.Application.Abstractions.Services.ITodoTaskService.GetOverdueTasks"/> and <see cref="TodoListApp.Application.Abstractions.Services.ITodoTaskService.GetPendingTasks"/>
    /// use the current DateTime, which is normally not mockable. A provider is thus used instead in this application to provide mocking capabilities in
    /// unit tests.
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
