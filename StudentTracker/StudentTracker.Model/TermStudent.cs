using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTracker.Model
{
    public class TermStudent
    {
        [Key]
        public Guid TermStudentId { get; set; }

        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey(nameof(Term))]
        public Guid TermId { get; set; }
        public Term Term { get; set; }
        public DateTime ApplyDate { get; set; }
    }
}