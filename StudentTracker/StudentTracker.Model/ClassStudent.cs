using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTracker.Model
{
    public class ClassStudent
    {
        [Key]
        public Guid ClassStudentId { get; set; }

        [Required]
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        [ForeignKey(nameof(Class))]
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        [Required]
        public DateTime EnrollDate { get; set; }

        [Required]
        public int Pass { get; set; }

        [Required]
        public int Grade { get; set; }
    }
}