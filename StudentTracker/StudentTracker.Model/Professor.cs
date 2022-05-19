using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentTracker.Model
{
    public class Professor
    {
        [Key]
        public Guid ProfessorId { get; set; }

        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$")]
        public string Phone { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}