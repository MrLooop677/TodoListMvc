using System.ComponentModel.DataAnnotations;

namespace TodoListMvc.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? File { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

    }
}
