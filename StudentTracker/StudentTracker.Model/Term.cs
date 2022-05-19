using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTracker.Model
{
    public class Term
    {
        [Key]
        public Guid TermId { get; set; }
        public DateTime ApplyBy { get; set; }
        public DateTime ApplyOpen { get; set; }

        [ForeignKey(nameof(Class))]
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public ICollection<TermStudent> Students { get; set; }
    }
}