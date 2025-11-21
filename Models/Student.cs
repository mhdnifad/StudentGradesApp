using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGrades.Models
{
    public class Student
    {
        [Key]
        public int StudentKey { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = string.Empty;

        [Display(Name = "Subject")]
        public int? SubjectKey { get; set; }

        [ForeignKey(nameof(SubjectKey))]
        public Subject? Subject { get; set; }

        [Range(0, 100)]
        public int Grade { get; set; }

        // Remarks computed property — NOT mapped to DB
        [NotMapped]
        [Display(Name = "Remarks")]
        public string Remarks => Grade >= 75 ? "PASS" : "FAIL";
    }
}
