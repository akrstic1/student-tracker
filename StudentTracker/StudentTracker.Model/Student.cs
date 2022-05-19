using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentTracker.Model
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Jmbag { get; set; }

        public virtual ICollection<TermStudent> Terms { get; set; }
        public virtual ICollection<ClassStudent> Classes { get; set; }
    }
}