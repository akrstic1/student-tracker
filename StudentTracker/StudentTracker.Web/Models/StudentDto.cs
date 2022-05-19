using System;

namespace StudentTracker.Web.Models
{
    public class StudentDto
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Jmbag { get; set; }
    }
}