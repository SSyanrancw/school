using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace School.Models
{
    public partial class ModelSchool : DbContext
    {
        public ModelSchool()
        {
        }

        public ModelSchool(DbContextOptions<ModelSchool> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseInstructor> CourseInstructors { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public virtual DbSet<OnlineCourse> OnlineCourses { get; set; }
        public virtual DbSet<OnsiteCourse> OnsiteCourses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<StudentGrade> StudentGrades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=School;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AI");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseID).ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DepartmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Department");
            });

            modelBuilder.Entity<CourseInstructor>(entity =>
            {
                entity.HasKey(e => new { e.CourseID, e.PersonID });

                entity.ToTable("CourseInstructor");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseInstructors)
                    .HasForeignKey(d => d.CourseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseInstructor_Course");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CourseInstructors)
                    .HasForeignKey(d => d.PersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseInstructor_Person");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentID).ValueGeneratedNever();

                entity.Property(e => e.Budget).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<OfficeAssignment>(entity =>
            {
                entity.HasKey(e => e.InstructorID);

                entity.ToTable("OfficeAssignment");

                entity.Property(e => e.InstructorID).ValueGeneratedNever();

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Instructor)
                    .WithOne(p => p.OfficeAssignment)
                    .HasForeignKey<OfficeAssignment>(d => d.InstructorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OfficeAssignment_Person");
            });

            modelBuilder.Entity<OnlineCourse>(entity =>
            {
                entity.HasKey(e => e.CourseID);

                entity.ToTable("OnlineCourse");

                entity.Property(e => e.CourseID).ValueGeneratedNever();

                entity.Property(e => e.URL)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.OnlineCourse)
                    .HasForeignKey<OnlineCourse>(d => d.CourseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnlineCourse_Course");
            });

            modelBuilder.Entity<OnsiteCourse>(entity =>
            {
                entity.HasKey(e => e.CourseID);

                entity.ToTable("OnsiteCourse");

                entity.Property(e => e.CourseID).ValueGeneratedNever();

                entity.Property(e => e.Days)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Time).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Course)
                    .WithOne(p => p.OnsiteCourse)
                    .HasForeignKey<OnsiteCourse>(d => d.CourseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnsiteCourse_Course");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StudentGrade>(entity =>
            {
                entity.HasKey(e => e.EnrollmentID);

                entity.ToTable("StudentGrade");

                entity.Property(e => e.Grade).HasColumnType("decimal(3, 2)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.CourseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGrade_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentGrades)
                    .HasForeignKey(d => d.StudentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentGrade_Student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
