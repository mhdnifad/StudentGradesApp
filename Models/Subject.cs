using System.ComponentModel.DataAnnotations;

namespace StudentGrades.Models
{
    public class Subject
    {
        [Key]
        public int SubjectKey { get; set; }

        [Required]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; } = string.Empty;

        public ICollection<Student>? Students { get; set; }
    }
}
