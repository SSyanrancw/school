using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace School.Models
{
    public partial class Person
    {
        public Person()
        {
            CourseInstructors = new HashSet<CourseInstructor>();
            StudentGrades = new HashSet<StudentGrade>();
        }

        [Display(Name = "ID")]
        public int PersonID { get; set; }

        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Display(Name = "Fecha de contratación")]
        public DateTime? HireDate { get; set; }

        [Display(Name = "Fecha de entrada")]
        public DateTime? EnrollmentDate { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        public virtual ICollection<StudentGrade> StudentGrades { get; set; }
    }
}
