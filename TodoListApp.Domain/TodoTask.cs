namespace TodoListApp.Domain
{
    /// <summary>
    /// Domain model of the application. "Task" name was ambigous on <see cref="System.Threading.Tasks.Task"/>, so "TodoTask" is being used
    /// as the domain type name, and throughout the application.
    ///
    /// A task contains a title, may optionally contain a due date, and contains a "completed" status field. A task is not completed
    /// by default.
    /// </summary>
    public class TodoTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; } = false;
    }
}
