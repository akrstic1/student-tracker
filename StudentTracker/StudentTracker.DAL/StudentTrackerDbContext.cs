using System;
using Microsoft.EntityFrameworkCore;
using StudentTracker.Model;
using Microsoft.Extensions.Configuration;

namespace StudentTracker.DAL
{
    public class StudentTrackerDbContext : DbContext
    {
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassStudent> ClassStudents { get; set; }
        public DbSet<TermStudent> TermStudents { get; set; }

        public StudentTrackerDbContext(DbContextOptions<StudentTrackerDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var connectionString = configuration.GetConnectionString("LightStudentTrackerDbContext");

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //P
            var test1 = Guid.NewGuid();
            var test2 = Guid.NewGuid();
            //C
            var test3 = Guid.NewGuid();
            var test4 = Guid.NewGuid();
            //S
            var test5 = Guid.NewGuid();
            var test6 = Guid.NewGuid();
            //CS
            var test7 = Guid.NewGuid();
            var test8 = Guid.NewGuid();


            modelBuilder.Entity<Professor>().HasData(
                new Professor
                {
                    ProfessorId = test1,
                    FullName = "Mate Lulić",
                    Address = "Ilica 20",
                    Phone = "012345123"
                }
            );

            modelBuilder.Entity<Class>().HasData(
                new Class
                {
                    ClassId = test3,
                    Name = "ASP.NET MVC programming",
                    ProfessorId = test1,
                    Semester = 1
                },
                new Class
                {
                    ClassId = test4,
                    Name = "UI/UX design",
                    ProfessorId = test1,
                    Semester = 2
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = test5,
                    FullName = "Pero Perić",
                    Address = "Frankopanska 20",
                    Jmbag = "1234567890",
                    Phone = "023123123",
                },
                new Student
                {
                    StudentId = test6,
                    FullName = "Marko Markić",
                    Address = "Ozaljska 23",
                    Jmbag = "1234567890",
                    Phone = "0113123123",
                },
                new Student
                {
                    StudentId = Guid.NewGuid(),
                    FullName = "Darko Darkić",
                    Address = "Troljanska 42",
                    Jmbag = "1234567890",
                    Phone = "023123321"
                }
            );

            modelBuilder.Entity<ClassStudent>().HasData(
                new ClassStudent
                {
                    ClassStudentId = test7,
                    StudentId = test5,
                    ClassId = test3,
                    EnrollDate = new DateTime(),
                    Pass = 0
                },
                new ClassStudent
                {
                    ClassStudentId = test8,
                    StudentId = test6,
                    ClassId = test3,
                    EnrollDate = new DateTime(),
                    Pass = 0
                }
            );
        }
    }
}