using TodoListApp.Application.Abstractions;

namespace TodoListApp.Application.Implementations
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
