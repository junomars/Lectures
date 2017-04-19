using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
    public enum GradeTypes {
        F = 5,
        D = 6,
        C = 7,
        B = 8,
        A = 9
    }

    public class CourseGrade {
        public int Id { get; set; }
        public Student Student { get; set; }
        public CourseSection CourseSection { get; set; }
        public GradeTypes Grade { get; set; }
    }
}
