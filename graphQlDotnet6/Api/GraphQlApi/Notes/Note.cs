using System.ComponentModel.DataAnnotations;

namespace GraphQlApi.Notes
{
    public class Note
    {
        public Guid Id { get; set; }
        
        [Required]
        public string? Message { get; set; }

        public bool IsUrgent { get; set; }
        
        public string? CreateBy { get; internal set; }

        public DateTime CreateDate { get; internal set; }

        public string? LastModifiedBy { get; internal set; }

        public DateTime? LastModifiedDate { get; internal set; }


    }
}
