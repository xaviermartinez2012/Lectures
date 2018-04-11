using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public virtual ICollection<CourseGrade> Transcript { get; set; } = new List<CourseGrade>();
		public virtual ICollection<CourseSection> EnrolledCourses { get; set; } = new List<CourseSection>();

		public RegistrationResults CanRegisterForCourseSection(CourseSection section) {
			if (section.EnrolledStudents.Where(s => s.Id == this.Id).Any())
				return RegistrationResults.AlreadyEnrolled;

			if (Transcript.Where(t => (int)t.GradeEarned <= (int)GradeTypes.C && t.CourseSection.CatalogCourse.Id == section.CatalogCourse.Id).Any())
				return RegistrationResults.AlreadyCompleted;

			foreach (var pre in section.CatalogCourse.Prerequisites) {
				if (!Transcript.Where(t => (int)t.GradeEarned <= (int)GradeTypes.C &&
					t.CourseSection.CatalogCourse.Id == pre.Id).Any())
					return RegistrationResults.PrerequisiteNotMet;
			}

			foreach (var en in EnrolledCourses) {
				if ((en.MeetingDays & section.MeetingDays) != DaysOfWeek.None) {
					if ((en.StartTime <= section.EndTime && section.StartTime <= en.EndTime) ||
						(section.StartTime <= en.EndTime && en.StartTime <= section.EndTime)){
						return RegistrationResults.TimeConflict;
					}
				}

			}

			return RegistrationResults.Success;
		}


	}
}
