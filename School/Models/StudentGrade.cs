using System;
using System.Collections.Generic;

#nullable disable

namespace School.Models
{
    public partial class StudentGrade
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public decimal? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Person Student { get; set; }
    }
}
