namespace TodoListApp.Domain
{
    public class TodoTask
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; } = false;
    }
}
