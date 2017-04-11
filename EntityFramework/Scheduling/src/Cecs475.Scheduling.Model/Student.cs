using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
	public class Student {
		public int Id { get; set; }
		public virtual ICollection<CourseSection> CompletedCourses { get; set; } = new List<CourseSection>();

		public bool CanRegisterForCourseSection(CourseSection section) {
			throw new NotImplementedException();
		}
	}
}
