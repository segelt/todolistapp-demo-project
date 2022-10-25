using System.Text.Json.Serialization;

namespace TodoListApp.ApiModels
{
    public class CreateTaskRequest
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("due date")]
        public DateTime? DueDate { get; set; }
    }
}
