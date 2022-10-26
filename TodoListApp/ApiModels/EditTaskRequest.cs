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

        /// <summary>
        /// Returns whether the user input is valid.
        /// User must provide at least a title or a due date or an isCompleted value to start the update operation.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return Title != null || DueDate != null || IsCompleted != null;
            }
        }
    }
}
