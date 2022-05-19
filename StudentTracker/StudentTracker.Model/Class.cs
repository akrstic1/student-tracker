using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTracker.Model
{
    public class Class
    {
        [Key]
        public Guid ClassId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Semester { get; set; }

        [Required]
        [ForeignKey(nameof(Professor))]
        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public virtual ICollection<Term> Terms { get; set; }
        public virtual ICollection<ClassStudent> Students { get; set; }
    }
}