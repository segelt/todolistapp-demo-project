using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp.Domain
{
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; } = false;
    }
}
