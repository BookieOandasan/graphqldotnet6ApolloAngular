using System.ComponentModel.DataAnnotations;

namespace GraphQlApi.Notes
{
    public class Note
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Message { get; set; }
    }
}
