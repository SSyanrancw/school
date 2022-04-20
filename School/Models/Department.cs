using System;
using System.Collections.Generic;

#nullable disable

namespace School.Models
{
    public partial class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }

        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? Administrator { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
