using System;
using System.Collections.Generic;
using System.Linq;

namespace Cecs475.Scheduling.Model {
    public enum RegistrationResults {
        Success,
        PrerequisiteNotMet,
        TimeConflict,
        AlreadyEnrolled,
        AlreadyCompleted
    }

    public class Student {
        public int Id { get; set; }

        //		public virtual ICollection<CourseSection> CompletedCourses { get; set; } = new List<CourseSection>();
        public String FirstName { get; set; }

        public String LastName { get; set; }
        public List<CourseGrade> Transcript { get; set; }
        public List<CourseSection> EnrolledCourses { get; set; }

        public RegistrationResults CanRegisterForCourseSection(CourseSection section) {
            // student is not already enrolled in another section of section's catalog course
            // student has not passed section's catalog course in the past
            // student passed all the prerequisites for that section
            if (EnrolledCourses.Any(course => course.CatalogCourse == section.CatalogCourse))
                return RegistrationResults.AlreadyEnrolled;
            if (Transcript.Where(courseGrade => courseGrade.CourseSection.CatalogCourse == section.CatalogCourse)
                .Any(courseGrade => courseGrade.Grade > GradeTypes.C))
                return RegistrationResults.AlreadyCompleted;
            if (section.CatalogCourse.Prerequisites.Union(Transcript
                    .Where(courseGrade => courseGrade.Grade >= GradeTypes.C)
                    .Select(courseGrade => courseGrade.CourseSection.CatalogCourse))
                .Any())
                return RegistrationResults.PrerequisiteNotMet;
            if (EnrolledCourses.Where(course => (course.MeetingDays & section.MeetingDays) > 0)
                .Any(course => section.StartTime < course.StartTime || course.StartTime < section.StartTime))
                return RegistrationResults.TimeConflict;
            return RegistrationResults.Success;
        }
    }
}