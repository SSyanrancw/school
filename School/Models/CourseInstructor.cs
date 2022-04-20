using System;
using System.Collections.Generic;

#nullable disable

namespace School.Models
{
    public partial class CourseInstructor
    {
        public int CourseID { get; set; }
        public int PersonID { get; set; }

        public virtual Course Course { get; set; }
        public virtual Person Person { get; set; }
    }
}
