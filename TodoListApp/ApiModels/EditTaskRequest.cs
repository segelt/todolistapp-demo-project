using System.Text.Json.Serialization;

namespace TodoListApp.ApiModels
{
    public class EditTaskRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("dueDate")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("isCompleted")]
        public bool? IsCompleted { get; set; }

        public bool IsValid
        {
            get
            {
                return Title != null || DueDate != null || IsCompleted != null;
            }
        }
    }
}
